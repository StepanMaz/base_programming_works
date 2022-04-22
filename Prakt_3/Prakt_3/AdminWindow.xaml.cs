using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace Prakt_3
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window, IWindow
    {
        DataTable table;
        int row = 0;
        public AdminWindow()
        {
            InitializeComponent();
            table = DBController.GetAllUsers();

            Table.AutoGenerateColumns = true;
            Table.IsReadOnly = true;
            ShowTable();

        }

        public void ShowTable()
        {
            table = DBController.GetAllUsers();
            if (table.Rows.Count <= 1)
            {
                Info.Content = "";
                Prev.IsEnabled = false;
                Next.IsEnabled = false;
                Status.IsEnabled = false;
            }
            else
            {
                Prev.IsEnabled = true;
                Next.IsEnabled = true;
                Status.IsEnabled = true;
                table.Rows[0].Delete();
                table.AcceptChanges();
                Table.ItemsSource = table.DefaultView;

                DisplayRow();
            }
        }

        public string value { get; set; }

        public void Open()
        {
            this.Show();
        }

        public void DisplayRow()
        {
            Info.Content = $"Login: {table.Rows[row].ItemArray[1]}\t\tАктивний: {table.Rows[row].ItemArray[4]}\n" +
                           $"Ім'я: {table.Rows[row].ItemArray[2]}\t\tПрізвище: {table.Rows[row].ItemArray[3]}";
            Status.Content = !(bool)table.Rows[row].ItemArray[4] ? "Активувати" : "Деактивувати";
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            row++;
            if (row == table.Rows.Count)
                row = 0;
            DisplayRow();
        }

        private void PrevClick(object sender, RoutedEventArgs e)
        {
            row--;
            if (row == -1)
                row = table.Rows.Count - 1;
            DisplayRow();
        }

        private void Status_Click(object sender, RoutedEventArgs e)
        {
            DBController.ChangeStatus((int)table.Rows[row].ItemArray[0], !(bool)table.Rows[row].ItemArray[4]);
            ShowTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Name.Text != "")
            {
                try
                {
                    if(DBController.AddUser(Name.Text, "", "", ""))
                    {
                        Name.Text = "";
                        ShowTable();
                    }
                }
                catch
                {
                    MessageBox.Show("Користувач з таким ім'ям вже є");
                }
            }
            else
            {
                MessageBox.Show("Введіть ім'я");
            }
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
