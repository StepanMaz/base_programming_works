using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using static CourseWork.DBController;

namespace CourseWork.Pages.TourPart
{
    public partial class RouteInfoPage : Page
    {
        int id;
        public RouteInfoPage(int id)
        {
            this.id = id;
            InitializeComponent();

            LoadRouteAccomodation();
            LoadRouteTravelWays();
            LoadRouteCountries();
            LoadRouteMealType();
            LoadRouteService();
        }

        public void LoadRouteMealType()
        {
            DataTable table = GetTable($"SELECT * FROM [dbo].[RouteMealType] WHERE RouteId = {id}");

            MealTypeTable.ItemsSource = table.DefaultView;
        }

        public void LoadRouteAccomodation()
        {
            DataTable table = GetTable($"SELECT * FROM [dbo].[RouteAccomodation] WHERE RouteId = {id}");

            AccomodationTable.ItemsSource = table.DefaultView;
        }

        public void LoadRouteService()
        {
            DataTable table = GetTable($"SELECT * FROM [dbo].[RouteService] WHERE RouteId = {id}");

            ServiceTable.ItemsSource = table.DefaultView;
        }

        public void LoadRouteTravelWays()
        {
            DataTable table = GetTable($"SELECT * FROM [dbo].[RouteTravelWays] WHERE RouteId = {id}");

            TravelWaysTable.ItemsSource = table.DefaultView;
        }

        public void LoadRouteCountries()
        {
            DataTable table = GetTable($"SELECT * FROM [dbo].[RouteCountries] WHERE RouteId = {id}");

            CountyTable.ItemsSource = table.DefaultView;
        }
    }
}
