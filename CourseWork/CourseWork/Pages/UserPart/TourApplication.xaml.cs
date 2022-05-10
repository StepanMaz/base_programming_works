﻿using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    public partial class TourApplication : Page
    {
        readonly Dictionary<string, int> countries = new Dictionary<string, int>();
        readonly Dictionary<string, int> ways = new Dictionary<string, int>();
        List<string> countryFilter = new List<string>();
        List<string> wayFilter = new List<string>();
        bool reverse = false;

        #region Bindings 
        public List<string> CountryItemsCollection => countries.Keys.Where(t => !countryFilter.Contains(t)).ToList();
        public List<string> TravelWaysItemsCollection => ways.Keys.Where(t => !wayFilter.Contains(t)).ToList();
        public Binding CountryBinding = new Binding("CountryItemsCollection") {ElementName = nameof(TourApplication)};
        public Binding TravelWaysBinding = new Binding("TravelWaysItemsCollection") { ElementName = nameof(TourApplication) };
        #endregion

        int id;
        public TourApplication(int id)
        {
            this.id = id;
            InitializeComponent();

            ButtomPrice.PreviewTextInput += (s, e) => e.Handled = !new System.Text.RegularExpressions.Regex("[0-9]+").IsMatch(e.Text);
            TopPrice.PreviewTextInput += (s, e) => e.Handled = !new System.Text.RegularExpressions.Regex("[0-9]+").IsMatch(e.Text);

            ButtomPrice.TextChanged += (s, e) => UpdatePossibleTours();
            TopPrice.TextChanged += (s, e) => UpdatePossibleTours();
            StartDate.SelectedDateChanged += (s, e) => UpdatePossibleTours();
            EndDate.SelectedDateChanged += (s, e) => UpdatePossibleTours();

            StartDate.SelectedDate = new DateTime(2019, 01, 01);
            EndDate.SelectedDate = new DateTime(2029, 01, 01);

            countries = GetTable("SELECT CountryID, NameUA FROM [dbo].[Country]").AsEnumerable().
                        ToDictionary(t => (string)t.ItemArray[1], v => (int)v.ItemArray[0]);
            ways = GetTable("SELECT TravelWayId, Name FROM [dbo].[TravelWay]").AsEnumerable().
                   ToDictionary(t => (string)t.ItemArray[1], v => (int)v.ItemArray[0]);


            ToursTable.AutoGeneratedColumns += (s, e) => Additionals.ContolsSettings.ColumnsNaming(s, e, "Назва", "Ціна", "Початок", "Кінець", "Опис");
            ToursTable.AutoGeneratingColumn +=
                (s, e) =>
                {
                    if((string)e.Column.Header == "Id")
                    {
                        DataGridTemplateColumn buttonColumn = new DataGridTemplateColumn();
                        DataTemplate buttonTemplate = new DataTemplate();
                        FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));
                        buttonTemplate.VisualTree = buttonFactory;
                        buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(ApplyButtonClick));
                        buttonFactory.SetValue(ContentProperty, "Підписатись");
                        buttonColumn.CellTemplate = buttonTemplate;
                        e.Column = buttonColumn;
                    }
                    if((string)e.Column.Header == "Start" || (string)e.Column.Header == "End")
                        e.Column = Additionals.ContolsSettings.AddFormat(e.Column, "d MMMM yyyy");
                };
            void ApplyButtonClick(object sender, RoutedEventArgs e)
            {
                MainWindow.WindowFrame.Navigate(new ApplymentOnTourPage(id, (int)(((FrameworkElement)sender).DataContext as DataRowView).Row["Id"]));
            }
        }


        private void UpdatePossibleTours()
        {
            string travelWays = string.Join(",", wayFilter.Select(t => ways[t]));
            string start = ToDate(StartDate, "0001-01-01");
            string end = ToDate(EndDate, "9999-12-31");
            DataTable table = GetTable(
                "SELECT * FROM" +
                " (" +
                " SELECT t.TourId as Id, r.Name, t.BasePrice, t.Start, t.[End], r.Description" +
                $" FROM dbo.GetToursByCountries('{string.Join(",", countryFilter.Select(t => countries[t]))}') as t" +
                $" JOIN Route as r ON t.RouteId = r.RouteId" +
                $" WHERE t.Start BETWEEN '{start}' AND '{end}'" +
                $" AND t.[End] BETWEEN '{start}' AND '{end}'" +
                $" AND t.RouteID IN (SELECT RouteId FROM dbo.GetToursByTravelWays{(reverse ? "Reverse" : "")}('{travelWays}'))" +
                $") as d" +
                $" WHERE d.BasePrice BETWEEN {ToPrice(ButtomPrice, "0")} AND {ToPrice(TopPrice, "100000")}");
            ToursTable.ItemsSource = table.DefaultView;

            string ToPrice(TextBox textBox, string @default)
            {
                var text = textBox.Text;
                if (text != "")
                {
                    return text;
                }
                return @default;
            }
            string ToDate(DatePicker datePicker, string @default)
            {
                var date = datePicker.SelectedDate;
                if(date != null)
                {
                    return ((DateTime)date).ToString("yyyy-MM-dd");
                }
                return @default;
            }
        }

        #region filters
        private void AddCountryLimit(object sender, RoutedEventArgs e) => AddToFilter(CountryFilter, countryFilter, CountryBinding, (150, 20, 125, 20));
        private void AddWay(object sender, RoutedEventArgs e) => AddToFilter(TravelWaysFilter, wayFilter, TravelWaysBinding, (105, 20, 80, 20));
        private void AddToFilter(StackPanel panel, List<string> filter, Binding sourceBinding, (double pnl, double cmb, double btn, double hgt) sizes)
        {
            var btn = panel.Children[^1];
            panel.Children.Remove(btn);

            var horStackPanel = new StackPanel() { Width = sizes.pnl, Height = sizes.hgt, Orientation = Orientation.Horizontal};
            var delButton     = new Button()     { Width = sizes.btn, Height = sizes.hgt, Content = "X" };
            var comboBox      = new ComboBox()   { Width = sizes.pnl, Height = sizes.hgt };

            comboBox.SetBinding(ComboBox.ItemsSourceProperty, sourceBinding);

            delButton.Click += (s, e) => filter.Remove(comboBox.SelectedItem.ToString());
            delButton.Click += (s, e) => panel.Children.Remove(horStackPanel);
            delButton.Click += (s, e) => UpdatePossibleTours();

            comboBox.SelectionChanged +=
                (s, e) =>
                {
                    var added = (e.AddedItems as object[]).FirstOrDefault() as string;
                    var removed = (e.RemovedItems as object[]).FirstOrDefault() as string;
                    filter.Add(added);
                    filter.Remove(removed);
                };
            comboBox.SelectionChanged += (s, e) => UpdatePossibleTours();

            horStackPanel.Children.Add(delButton);
            horStackPanel.Children.Add(comboBox);

            panel.Children.Add(horStackPanel);
            panel.Children.Add(btn);
        }
        #endregion
        private void ReverseClick(object sender, RoutedEventArgs e)
        {
            reverse = !reverse;
            UpdatePossibleTours();
            (sender as Button).Content = reverse ? "Вкл" : "Викл";
        }
    }
}
