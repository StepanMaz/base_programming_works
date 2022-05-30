using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using CourseWork.Pages.TourPart;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    public partial class ApplymentOnTourPage : Page
    {
        private const string UPDATE_BUTTON_TEXT = "{0:C2} ({1}%)";
        private readonly int ClientId, TourId;

        private int price;
        private int sale;
        private int MealTypeId;
        private List<int> services = new List<int>();

        private Action valuesUpdated;

        #region start
        Button lastbtn = null;
        public ApplymentOnTourPage(int client, int tour)
        {
            this.ClientId = client;
            this.TourId = tour;

            valuesUpdated += () => price = GetObject<int>($"SELECT dbo.GetPriceForTour({TourId}, {ClientId}, {MealTypeId}, {string.Join(",", services)})");
            valuesUpdated += () => sale = GetObject<int>($"SELECT dbo.GetClientSale({ClientId})");
            valuesUpdated += () => SubmitButton.Content = String.Format(UPDATE_BUTTON_TEXT, price, sale);

            InitializeComponent();
            LoadServices();
            LoadTourMealTypes();
        }

        public void LoadTourMealTypes()
        {
            DataTable table = GetTable($"SELECT * FROM dbo.GetMealTypesOfTour({TourId})");
            table.Columns.Add();

            var settings = new ButtonedTableSettings
            {
                headers = new setting<string>[] {"Тип", "Ціна", ""},
                links = new setting<TableLink>[] { (new TableLink(TourConstituents.GetDict("MealTypeId", "Type", "MealType"), "MealTypeId"), 1) },
                buttons = new setting<(RoutedEventHandler handler, string name)>[]
                {
                    (new RoutedEventHandler(AddMealType), "Вибрати")
                },
            };

            MealTypesTable.ItemsSource = table.DefaultView;
            MealTypesTable.CanUserAddRows = false;

            MealTypesTable.Loaded += (s, e) => settings.Apply(MealTypesTable);

            void AddMealType(object sender, RoutedEventArgs e)
            {
                var btn = sender as Button;
                btn.Content = "Вибрано";
                if (lastbtn is Button b)
                    b.Content = "Вибрати";
                MealTypeId = (int)(btn.DataContext as DataRowView).Row["MealTypeId"];
                lastbtn = btn;

                valuesUpdated.Invoke();
            }
        }
        public void LoadServices()
        {
            var settings = new ButtonedTableSettings()
            {
                height = 40,
                headers = new setting<string>[] { "", "Тип", "Назва", "Ціна", "Опис" },
                lengths = new setting<DataGridLength>[] {(100, 2), (300, 4)},
                buttons = new setting<(RoutedEventHandler handler, string name)>[]
                {
                    (new RoutedEventHandler(AddService), "Додати") 
                },
            };

            DataTable table = GetTable($"SELECT rs.RSId, at.Type, rs.Name, rs.Price, rs.Comment FROM [dbo].[RouteService] as rs JOIN Tour as t ON t.RouteId = rs.RouteId JOIN AdditionalType as at ON at.AddTypeId = rs.AddTypeId WHERE t.TourId = {TourId}");
            
            ServiceTable.ItemsSource = table.DefaultView;
            ServiceTable.IsReadOnly = true;

            ServiceTable.Loaded += (s, e) => settings.Apply(ServiceTable);

            void AddService(object sender, RoutedEventArgs e)
            {
                var btn = sender as Button;
                bool status = (string)btn.Content == "Додати";
                btn.Content = status ? "Видалити" : "Додати";
                if (status)
                {
                    services.Add((int)(btn.DataContext as DataRowView).Row[0]);
                }
                else
                {
                    services.Remove((int)(btn.DataContext as DataRowView).Row[0]);
                }
            }
        }
        #endregion

        #region Buttons click
        private void GetDetailInfo(object sender, RoutedEventArgs e)
        {

        }

        private void ApplyToTourClick(object sender, RoutedEventArgs e)
        {
            var clienttoue = new Dictionary<string, object>
            {
                { "ClientId",   ClientId},
                { "TourId",     TourId},
                { "ApplicationDate", DateTime.Today},
                { "FullPrice", GetObject<object>("")},
                { "MealTypeId", MealTypeId}
            };
            if(Insert("ClientTour", ))
            MainWindow.WindowFrame.NavigationService.RemoveBackEntry();
            MainWindow.WindowFrame.GoBack();
        }
        #endregion
    }
}
