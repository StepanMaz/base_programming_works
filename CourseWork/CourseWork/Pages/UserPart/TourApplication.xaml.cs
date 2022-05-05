using System;
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
        public Dictionary<string, int> countries = new Dictionary<string, int>();

        public TourApplication()
        {
            InitializeComponent();

            StartDate.SelectedDate = DateTime.Today;
            EndDate.SelectedDate = DateTime.Today.AddMonths(1);

            DataTable table = GetTable("SELECT CountryID, NameUA FROM [dbo].[Country]");
            countries = table.AsEnumerable().
                        ToDictionary(t => (string)t.ItemArray[1], v => (int)v.ItemArray[0]);
        }

        private void AddCountryLimit(object sender, RoutedEventArgs e)
        {
            var btn = AddButton;
            CountryFilter.Children.Remove(btn);
            CountryFilter.Children.Add(new ComboBox() { ItemsSource = countries.Select(t => t.Key), Width = 120 });
            CountryFilter.Children.Add(btn);
        }
    }
}
