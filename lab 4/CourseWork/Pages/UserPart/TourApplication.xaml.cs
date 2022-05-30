using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using CourseWork.Pages.TourPart;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    public partial class TourApplication : Page
    {
        const int BURNING_TOUR_DAYS = 5;

        public bool ReverseIsEnabled { get; set; } = false;

        readonly Dictionary<string, int> countries = new Dictionary<string, int>();
        readonly Dictionary<string, int> ways = new Dictionary<string, int>();
        public List<string> countryFilter = new List<string>();
        public List<string> wayFilter = new List<string>();
        bool reverse = false;

        #region Bindings 
        public List<string> CountryItemsCollection { get => countries.Keys.Where(t => !countryFilter.Contains(t)).ToList(); }
        public List<string> TravelWaysItemsCollection { get => ways.Keys.Where(t => !wayFilter.Contains(t)).ToList(); }
        #endregion

        #region table setting
        DatePickerTableSettings settings;
        #endregion

        int id;
        public TourApplication(int id)
        {
            settings = new DatePickerTableSettings()
            {
                headers = new TourPart.setting<string>[] { "", "Назва", "Ціна", "Опис", "Початок", "Кінець" },
                readonlies = new setting<bool>[] {true, true, true, true, true, true},
                lengths = new setting<DataGridLength>[] {(80, 3)},
                buttons = new TourPart.setting<(RoutedEventHandler, string)>[]
                {
                   (new RoutedEventHandler(ApplyButtonClick), "Підписатись")
                },
                dates = new TourPart.setting<(string, bool)>[] { (("Start", true), 4), (("[End]", true), 5) },
            };
            void ApplyButtonClick(object sender, RoutedEventArgs e)
            {
                MainWindow.WindowFrame.Navigate(new ApplymentOnTourPage(id, (int)(((FrameworkElement)sender).DataContext as DataRowView).Row["TourId"]));
            }

            this.id = id;
            InitializeComponent();

            ButtomPrice.PreviewTextInput += (s, e) => e.Handled = !new System.Text.RegularExpressions.Regex("[0-9]+").IsMatch(e.Text);
            TopPrice.PreviewTextInput += (s, e) => e.Handled = !new System.Text.RegularExpressions.Regex("[0-9]+").IsMatch(e.Text);

            ButtomPrice.TextChanged += (s, e) => UpdatePossibleTours();
            TopPrice.TextChanged += (s, e) => UpdatePossibleTours();
            StartDate.SelectedDateChanged += (s, e) => UpdatePossibleTours();
            EndDate.SelectedDateChanged += (s, e) => UpdatePossibleTours();

            countries = GetTable("SELECT CountryID, NameUA FROM [dbo].[Country]").AsEnumerable().
                        ToDictionary(t => (string)t.ItemArray[1], v => (int)v.ItemArray[0]);
            ways = GetTable("SELECT TravelWayId, Name FROM [dbo].[TravelWay]").AsEnumerable().
                   ToDictionary(t => (string)t.ItemArray[1], v => (int)v.ItemArray[0]);

            ToursTable.LoadingRow +=
                (s, e) =>
                {
                    var date = (DateTime)(e.Row.Item as DataRowView).Row["Start"];
                    if (DateTime.Now < date && date < DateTime.Now.AddDays(BURNING_TOUR_DAYS))
                        e.Row.Background = Brushes.Red;
                };

            ToursTable.Loaded += (s, e) => UpdatePossibleTours();

            TravelWaysFilter.LayoutUpdated += (s, e) => ReverseIsEnabled = TravelWaysFilter.Children.Count != 0;
        }


        private void UpdatePossibleTours()
        {
            string start = ToDate(StartDate, "0001-01-01");
            string end = ToDate(EndDate, "9999-12-31");

            string countriesList = string.Join(",", countryFilter.Select(t => countries[t]));
            string travelWaysList = string.Join(",", wayFilter.Select(t => ways[t]));
            DataTable table = GetTable(
                $"SELECT * FROM dbo.GetPossibleTours('{countriesList}', '{travelWaysList}', '{start}', '{end}', {ToPrice(ButtomPrice, "0")}, {ToPrice(TopPrice, "0")}, {(reverse ? 1 : 0)})");
            ToursTable.Columns.Clear();
            ToursTable.ItemsSource = table.DefaultView;
            settings.Apply(ToursTable);

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
        private void AddCountryLimit(object sender, RoutedEventArgs e) => AddToFilter(CountryFilter, countryFilter, CountryItemsCollection, (140, 100, 20, 25));
        private void AddWay(object sender, RoutedEventArgs e) => AddToFilter(TravelWaysFilter, wayFilter, TravelWaysItemsCollection, (105, 80, 20, 25));
        private void AddToFilter(StackPanel panel, List<string> filter, IEnumerable<string> source, (double pnl, double cmb, double btn, double hgt) sizes)
        {
            var btn = panel.Children[^1];
            panel.Children.Remove(btn);

            var horStackPanel = new StackPanel() { Width = sizes.pnl, Height = sizes.hgt, Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Top};
            var delButton     = new Button()     { Content = "X", Width = sizes.btn};
            var comboBox      = new ComboBox()   { Width = sizes.pnl};

            comboBox.ItemsSource = source;

            delButton.Click += (s, e) => filter.Remove(comboBox.SelectedItem?.ToString());
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
