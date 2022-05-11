using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void InfoButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.WindowFrame.Navigate(new Client((int)(((FrameworkElement)sender).DataContext as DataRowView).Row["ClientID"]));
        }

        public void LoadUsers()
        {
            var settings = new Pages.TourPart.DatePickerTableSettings()
            {
                height = 25,
                headers = new TourPart.setting<string>[] { "Id", "Ім'я", "Прізвище", "По батькові", "Телефон", "Email", "Дата регістрації", "Примітка" },
                dates = new TourPart.setting<(string, bool)>[] { (("RegestrationDate", true), 6) },
                buttons = new TourPart.setting<(RoutedEventHandler, string)>[] 
                { 
                    (new RoutedEventHandler(InfoButtonClick), "Інформація")
                }
            };

            DataTable table = GetTable("SELECT * FROM [dbo].[Client]");

            UserTable.ItemsSource = table.DefaultView;
            UserTable.Loaded += (s, e) => settings.Apply(UserTable);

            UserTable.IsReadOnly = true;
            UserTable.CanUserDeleteRows = false;
        }
    }
}
