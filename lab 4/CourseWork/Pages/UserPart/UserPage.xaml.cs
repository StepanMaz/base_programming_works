using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                dates = new TourPart.setting<(string, bool)>[] { (("RegistrationDate", true), 6) },
                buttons = new TourPart.setting<(RoutedEventHandler, string)>[]
                {
                    (new RoutedEventHandler(InfoButtonClick), "Інформація")
                },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => Delete("Client", "ClientId", ((s as DataGrid).SelectedItem as DataRowView).Row)}
                },
                update = (s, e) => Update("Client", (e.Row.Item as DataRowView).Row)
            };

            DataTable table = GetTable("SELECT * FROM [dbo].[Client]");

            UserTable.ItemsSource = table.DefaultView;
            UserTable.Loaded += (s, e) => settings.Apply(UserTable);

            UserTable.CanUserDeleteRows = true;
            UserTable.CanUserAddRows = false;

            void Delete(string table, string column, DataRow value)
            {
                TryDeleteById(table, $"{column} = {value[column]}");
            }
            void Update(string table, DataRow row)
            {
                DBController.Update(table, new Dictionary<string, object>()
                {
                    { "FirstName",        row["FirstName"] },
                    { "SecondName",       row["SecondName"] },
                    { "LastName",         row["LastName"] },
                    { "TelephoneNumber",  row["TelephoneNumber"] },
                    { "Email",            row["Email"] },
                    { "RegistrationDate", row["RegistrationDate"] },
                    { "Comments",         row["Comments"] }
                }, $"ClientId = {row["ClientId"]}");
            }
        }
    }
}
