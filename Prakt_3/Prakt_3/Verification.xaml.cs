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

namespace Prakt_3
{
    public interface IWindow
    {
        string value { get; set; }
        public void Open();
    }

    public partial class Verification : Window
    {
        string login;
        string password;
        int tries = 3;

        IWindow toOpen;
        Window source;

        public Verification(Window source, IWindow toOpen)
        {
            InitializeComponent();
            this.toOpen = toOpen;
            this.source = source;
        }

        public Verification(Window source, IWindow toOpen, string login)
        {
            InitializeComponent();
            this.toOpen = toOpen;
            this.source = source;
            this.login = login;
            Login.Text = login;
            Login.IsEnabled = false;
        }

        private void EnterClick(object sender, RoutedEventArgs e)
        {
            if(DBController.VerifyUser(login, password))
            {
                source.Close();
                this.Close();
                toOpen.value = login;
                toOpen.Open();
            }
            else
            {
                MessageBox.Show("Неправильний пароль або логін");
                tries--;
                TriesAmount.Content = "Кількість спроб: " + tries;
                Password.Password = "";
                if (Login.IsEnabled)
                    Login.Text = "";
                if (tries == 0)
                {
                    Application.Current.Shutdown();
                }
            }
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            login = ((TextBox)sender).Text;
            ButtonCheck();
        }

        private void Password_TextChanged(object sender, RoutedEventArgs e)
        {
            password = ((PasswordBox)sender).Password;
            ButtonCheck();
        }

        private void ButtonCheck()
        {
            if (!string.IsNullOrEmpty(login))
                EnterButton.IsEnabled = true;
            else
                EnterButton.IsEnabled = false;
        }
    }
}
