using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

using static CourseWork.DBController;
using System.Globalization;

namespace CourseWork.Pages.TourPart
{
    #region Main
    public class setting<type>
    {
        public type value;
        public int? pos;

        public setting(type value, int? pos = null) => (this.value, this.pos) = (value, pos);

        public static implicit operator setting<type>(type value) => new setting<type>(value);
        public static implicit operator setting<type>((type, int) value) => new setting<type>(value.Item1, value.Item2);
    }
    #endregion

    #region table settings
    public class TableSettings
    { 
        public setting<string>[]         headers;
        public setting<DataGridLength>[] lengths;
        public setting<Visibility>[]     visibilities;
        public setting<bool>[]           readonlies;

        protected DataGrid grid;
        protected ObservableCollection<DataGridColumn> columns;

        public virtual void Apply(DataGrid dataGrid)
        {
            grid ??= dataGrid;
            columns ??= dataGrid.Columns;

            Iterate(headers, (i, v) => columns[i].Header = v);
            Iterate(lengths, (i, v) => columns[i].Width = v);
            Iterate(visibilities, (i, v) => columns[i].Visibility = v);
            Iterate(readonlies, (i, v) => columns[i].IsReadOnly = v);
        }

        public virtual void ReworkTable(DataTable table) { }

        protected void Iterate<type>(setting<type>[] array, Action<int, type> action)
        {
            foreach (var item in Reorganize(array))
                action.Invoke((int)item.pos, item.value);
        }
        protected setting<type>[] Reorganize<type>(setting<type>[] array)
        {
            if (array == null) return new setting<type>[0];

            var signed = array.Where(t => t.pos != null).ToList();
            var unsigned = array.Where(t => t.pos == null).ToList();
            int i = 0;
            foreach (var item in unsigned)
            {
                item.pos = i++;
            }
            return unsigned.Where(t => !signed.Any(g => g.pos == t.pos)).Concat(signed).OrderBy(t => t.pos).ToArray();
        }
    }

    public class LinkedTableSettings : TableSettings
    {
        public setting<TableLink>[] links;
        public override void Apply(DataGrid dataGrid)
        {
            grid ??= dataGrid;
            columns ??= dataGrid.Columns;
            Iterate<TableLink>(links, (i, v) => v.GetColumn(grid, i));

            base.Apply(dataGrid);
        }
    }

    public class DatePickerTableSettings : ButtonedTableSettings
    {
        public setting<string>[] dates;

        public override void Apply(DataGrid dataGrid)
        {
            grid ??= dataGrid;
            columns ??= dataGrid.Columns;
            Iterate(dates, (i, v) => columns[i] = GetDatePickerColumn(v));

            base.Apply(dataGrid);
        }

        public DataGridTemplateColumn GetDatePickerColumn(string binding)
        {
            Binding templateColumnBinding = new Binding(binding);
            templateColumnBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            templateColumnBinding.Mode = BindingMode.TwoWay;
            FrameworkElementFactory datePickerFactoryElem = new FrameworkElementFactory(typeof(DatePicker));
            datePickerFactoryElem.SetValue(DatePicker.SelectedDateProperty, templateColumnBinding);
            datePickerFactoryElem.SetValue(DatePicker.DisplayDateProperty, templateColumnBinding);
            DataTemplate cellTemplate = new DataTemplate();
            cellTemplate.VisualTree = datePickerFactoryElem;
            DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
            templateColumn.CellTemplate = cellTemplate;
            return templateColumn;
        }
    }

    public class ButtonedTableSettings : LinkedTableSettings
    {
        public setting<(RoutedEventHandler handler, string name)>[] buttons;

        public override void Apply(DataGrid dataGrid)
        {
            grid ??= dataGrid;
            columns ??= dataGrid.Columns;
            Iterate(buttons, (i, v) => columns[i] = GetButtonColumn(v.handler, v.name));
            
            base.Apply(dataGrid);
        }

        public DataGridTemplateColumn GetButtonColumn(RoutedEventHandler handler, string name)
        {
            DataGridTemplateColumn buttonColumn = new DataGridTemplateColumn();
            DataTemplate buttonTemplate = new DataTemplate();
            FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonTemplate.VisualTree = buttonFactory;
            buttonFactory.AddHandler(Button.ClickEvent, handler);
            buttonFactory.SetValue(Button.ContentProperty, name);
            buttonColumn.CellTemplate = buttonTemplate;
            return buttonColumn;
        }
    }

    public class ImageTableSettins : ButtonedTableSettings
    {
        public setting<(string source, string column)>[] images;

        public override void Apply(DataGrid dataGrid)
        {
            grid ??= dataGrid;
            columns ??= dataGrid.Columns;

            Iterate(images, (i, v) => columns[i] = GetImageColumn(i.ToString(), v));

            base.Apply(dataGrid);
        }

        public override void ReworkTable(DataTable table)
        {
            foreach (var item in Reorganize(images))
            {
                table.Columns.RemoveAt((int)item.pos);
                var column = new DataColumn($"{item.pos}", typeof(BitmapFrame));
                table.Columns.Add(column);
                column.SetOrdinal((int)item.pos);
            }
        }

        public DataGridTemplateColumn GetImageColumn(string columnNumber, setting<(string source, string column)> src)
        {
            FrameworkElementFactory image = new FrameworkElementFactory(typeof(Image));
            DataTemplate cellTemplate = new DataTemplate();
            cellTemplate.VisualTree = image;
            Binding binding = new Binding(columnNumber);
            image.SetValue(Image.SourceProperty, binding);
            DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
            templateColumn.Header = columnNumber;
            templateColumn.CellTemplate = cellTemplate;
            templateColumn.Width = 100;

            EventHandler<DataGridRowEventArgs> handler = (s, e) =>
                {
                    DataRowView row = e?.Row?.Item as DataRowView;

                    if (row != null)
                    {
                        try
                        {
                            row.Row[columnNumber] = DataConverter.GetImageFromByteArray(GetObject<byte[]>(src.value.source + $" WHERE {src.value.column} = " + row.Row[src.value.column]));

                        }
                        catch { }
                    }
                };
            EventHandler<DataGridRowEventArgs> unloadhandler = (s, e) =>
             {
                 DataRowView row = e?.Row?.Item as DataRowView;

                 if (row != null)
                 {
                     try
                     {
                         row.Row[columnNumber] = null;
                     }
                     catch { }
                 }
             };
            grid.LoadingRow += handler;
            grid.UnloadingRow += unloadhandler;
            grid.SourceUpdated += (s, e) =>
            { 
                grid.LoadingRow -= handler;
                grid.UnloadingRow -= unloadhandler;
            };

            return templateColumn;
        }
    }
    #endregion

    #region additionals
    public class TableLink
    {
        Dictionary<int, string> values;
        string binding;

        public TableLink(Dictionary<int, string> values, string binding)
        {
            this.values = values;
            this.binding = binding;
        }

        public void GetColumn(DataGrid grid, int clmn)
        {
            DataGridComboBoxColumn column = new DataGridComboBoxColumn();
            column.Header = binding;
            column.ItemsSource = values.Values;
            column.SelectedItemBinding = new Binding(binding)
            {
                Converter = new LinkConverter() { source = values},
                Mode = BindingMode.TwoWay
            };
            grid.Columns[clmn] = column;
        }

        public class LinkConverter : IValueConverter
        {
            public Dictionary<int, string> source;

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (!(value is string _value))
                    return null;

                return source.FirstOrDefault(x => x.Value == _value).Key;
            }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (!(value is int key))
                    return null;

                return !source.TryGetValue(key, out var result) ? null : result;
            }
        }
    }
    #endregion
}
