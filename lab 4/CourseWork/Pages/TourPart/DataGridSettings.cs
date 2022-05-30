using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static CourseWork.DBController;

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
        public double? height;
        public setting<string>[] headers;
        public setting<DataGridLength>[] lengths;
        public setting<Visibility>[] visibilities;
        public setting<bool>[] readonlies;

        public Dictionary<Key, Action<object, KeyEventArgs>> keyPresses;
        public EventHandler<DataGridRowEditEndingEventArgs> insert, update;

        protected DataGrid grid;
        protected ObservableCollection<DataGridColumn> columns;

        protected EventHandler sourceChanged;
        public void CallEvent(){sourceChanged.Invoke(null, null); sourceChanged = null; }

        public virtual void Apply(DataGrid dataGrid)
        {
            grid ??= dataGrid;

            columns ??= dataGrid.Columns;

            grid.MinRowHeight = 25;
            if (height != null)
                grid.RowHeight = (double)height;

            Iterate(headers, (i, v) => columns[i].Header = v);
            Iterate(lengths, (i, v) => columns[i].Width = v);
            Iterate(visibilities, (i, v) => columns[i].Visibility = v);
            Iterate(readonlies, (i, v) => columns[i].IsReadOnly = v);

            InitEvents();
        }

        private void InitEvents()
        {
            if(keyPresses != null)
            {
                if(!keyPresses.ContainsKey(Key.Enter))
                    keyPresses.Add(Key.Enter, (s, e) => grid.CommitEdit());
                if (!keyPresses.ContainsKey(Key.Escape))
                    keyPresses.Add(Key.Escape, (s, e) => { grid.CancelEdit(); grid.UnselectAllCells(); grid.UnselectAll(); });
            }

            KeyEventHandler del =
                (s, e) =>
                {
                    if (keyPresses.ContainsKey(e.Key))
                        keyPresses[e.Key].Invoke(s, e);
                };

            grid.PreviewKeyDown += del;
            grid.RowEditEnding += EditEnded;

            sourceChanged +=
                (s, e) =>
                {
                    grid.PreviewKeyDown -= del;
                    grid.RowEditEnding -= EditEnded;
                };
        }

        private void EditEnded(object sender, DataGridRowEditEndingEventArgs e)
        {
            (sender as DataGrid).RowEditEnding -= EditEnded;
            grid.CommitEdit();
            grid.UnselectAll();
            if (e.Row.IsNewItem)
                insert?.Invoke(sender, e);
            else
                update?.Invoke(sender, e);
            (sender as DataGrid).RowEditEnding += EditEnded;
        }

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
        public setting<(string binding, bool @readonly)>[] dates;

        public override void Apply(DataGrid dataGrid)
        {
            grid ??= dataGrid;
            columns ??= dataGrid.Columns;
            Iterate(dates, (i, v) => columns[i] = GetDatePickerColumn(v.binding, v.@readonly));

            base.Apply(dataGrid);
        }

        public DataGridTemplateColumn GetDatePickerColumn(string binding, bool @readonly)
        {
            Binding templateColumnBinding = new Binding(binding);
            templateColumnBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            templateColumnBinding.Mode = BindingMode.TwoWay;
            FrameworkElementFactory datePickerFactoryElem = new FrameworkElementFactory(typeof(DatePicker));
            datePickerFactoryElem.SetValue(DatePicker.SelectedDateProperty, templateColumnBinding);
            datePickerFactoryElem.SetValue(DatePicker.DisplayDateProperty, templateColumnBinding);
            datePickerFactoryElem.SetValue(DatePicker.IsEnabledProperty, !@readonly);
            DataTemplate cellTemplate = new DataTemplate();
            cellTemplate.VisualTree = datePickerFactoryElem;
            DataGridDateColumn templateColumn = new DataGridDateColumn();
            templateColumn.CellTemplate = cellTemplate;

            return templateColumn;
        }
    }

    public class DataGridDateColumn : DataGridTemplateColumn
    {
        protected override object PrepareCellForEdit(FrameworkElement editingElement,
                                                     RoutedEventArgs editingEventArgs)
        {
            editingElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            return base.PrepareCellForEdit(editingElement, editingEventArgs);
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
        public setting<string>[] images;

        public override void Apply(DataGrid dataGrid)
        {
            grid ??= dataGrid;
            columns ??= dataGrid.Columns;

            Iterate(images, (i, v) => columns[i] = GetImageColumn(v));

            base.Apply(dataGrid);
        }

        public DataGridTemplateColumn GetImageColumn(string columnName)
        {
            FrameworkElementFactory image = new FrameworkElementFactory(typeof(Image));
            DataTemplate cellTemplate = new DataTemplate();
            cellTemplate.VisualTree = image;
            Binding binding = new Binding(columnName) { Converter = new ImageConvertor()};
            image.SetValue(Image.SourceProperty, binding);
            DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
            templateColumn.Header = columnName;
            templateColumn.CellTemplate = cellTemplate;
            templateColumn.Width = 100;

            var style = new Style(typeof(DataGridCell));
            style.Setters.Add(new EventSetter(DataGridCell.PreviewMouseDoubleClickEvent, new MouseButtonEventHandler((s, e) => Beginedit(s, e, columnName))));

            templateColumn.CellStyle = style;

            return templateColumn;
        }

        public void Beginedit(object sender, MouseButtonEventArgs e, string columnName)
        {
            var gridrow = DataGridRow.GetRowContainingElement(e.Source as DataGridCell);
            var row = gridrow.Item as DataRowView;
            if(row != null)
            {
                var datarow = row.Row;
                new ImageInput(
                        (status, image) =>
                        {
                            if (status == ImageInput.Status.Succeed)
                            {
                                datarow[columnName] = image;
                            }
                        }).ShowDialog();
            }
        }

        public class ImageConvertor : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value.GetType() == typeof(System.DBNull))
                    return null;
                return CourseWork.DataConverter.GetImageFromByteArray((byte[])value);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
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
                Converter = new LinkConverter() { source = values },
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
