using System.Data;
using System.Windows;
using System.Windows.Controls;
using CourseWork.Pages.TourPart;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    public partial class Client : Page
    {
        int id;
        public Client(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void DocumntsClick(object sender, RoutedEventArgs e)
        {
            MainWindow.WindowFrame.Navigate(new Documents(id));
        }

        private void AddTourClick(object sender, RoutedEventArgs e)
        {
            MainWindow.WindowFrame.Navigate(new TourApplication(id));
        }

        private void GetServices(object sender, RoutedEventArgs e)
        {
            Back.Visibility = Visibility.Visible;
            ClientTable.IsReadOnly = true;
            var settings = new Pages.TourPart.LinkedTableSettings()
            {
                headers = new setting<string>[] { "Тип", "Назва", "Ціна"},
                links = new setting<TableLink>[]
                {
                    new TableLink(Pages.TourPart.TourConstituents.GetDict("AddTypeId", "Type", "AdditionalType"), "AddTypeId")
                }
            };
            var id = ((sender as Button).DataContext as DataRowView).Row["CTId"];
            DataTable table = GetTable($"SELECT rs.AddTypeId, rs.Name, rs.Price FROM ClientTourAdditions as cta JOIN RouteService as rs ON cta.RSId = rs.RSId WHERE cta.CTId = {id}");
            ClientTable.Columns.Clear();
            ClientTable.ItemsSource = table.DefaultView;
            settings.Apply(ClientTable);
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            (sender as Button).Visibility = Visibility.Hidden;
            ToursClick(sender, e);
        }

        private void ToursClick(object sender, RoutedEventArgs e)
        {
            var settings = new Pages.TourPart.DatePickerTableSettings()
            {
                headers = new setting<string>[] {"", "Назва", "Ціна", "Початок", "Кінець" },
                buttons = new TourPart.setting<(RoutedEventHandler, string)>[]
                {
                   (new RoutedEventHandler(GetServices), "Детальніше")
                },
                dates = new TourPart.setting<(string, bool)>[] { (("Start", true), 3), (("[End]", true), 4) },
            };


            DataTable table = GetTable($"SELECT CTId, r.Name, ct.FullPrice, t.Start, t.[End] FROM ClientTour as ct JOIN Tour as t ON t.TourId = ct.TourId JOIN Route as r ON r.RouteID = t.RouteID WHERE ct.ClientId = {id}");
            ClientTable.Columns.Clear();
            ClientTable.ItemsSource = table.DefaultView;
            ClientTable.Visibility = Visibility.Visible;
            ClientTable.CanUserAddRows = false;
            ClientTable.IsReadOnly = true;
            settings.Apply(ClientTable);
        }
    }
}
