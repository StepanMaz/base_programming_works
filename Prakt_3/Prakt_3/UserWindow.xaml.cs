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
using System.Text.RegularExpressions;

namespace Prakt_3
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window, IWindow
    {
        bool login, pass1, pass2;
        public UserWindow()
        {
            InitializeComponent();

            NewUserLogin.TextChanged += (s, e) => { login = ((TextBox)s).Text.Length != 0; AddNewUserButton(); };
            NewUserPassword.TextChanged += (s, e) => { pass1 = ((TextBox)s).Text.Length != 0; AddNewUserButton(); };
            NewUserRepeatedPassword.TextChanged += (s, e) => { pass2 = ((TextBox)s).Text.Length != 0; AddNewUserButton(); };
        }

        public string value { get; set; }

        public void Open()
        {
            this.Show();
            (string name, string secondName) = DBController.GetUserInfo(value);
            UserName.Text = name;
            UserSecondName.Text = secondName;
        }

        public void AddNewUserButton()
        {
            if(login && pass2 && pass1)
            {
                AddNewUser.IsEnabled = true;
            }
            else
            {
                AddNewUser.IsEnabled = false;
            }
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            var letter = new Regex("[a-z]");
            var bigLetter = new Regex("[A-Z]");
            var number = new Regex("[0-9]");
            var symbols = new Regex("[!@#$%^&*]");
            string password = NewPassword.Text;
            if(password != RepeatNewPassword.Text)
            {
                MessageBox.Show("Паролі різні");
                RepeatNewPassword.Text = "";
                NewPassword.Text = "";
                return;
            }
            if (password.Length >= 8 && bigLetter.IsMatch(password) && symbols.IsMatch(password))
            {
                DBController.ChangePassword(value, password);
                MessageBox.Show("Пароль змінено");
                RepeatNewPassword.Text = "";
                NewPassword.Text = "";
            }
            else
            {
                MessageBox.Show("Паролі не відповідають стандарту\nМають містити велику літеру та спец сивол");
                RepeatNewPassword.Text = "";
                NewPassword.Text = "";
            }
        }

        private void ReturnButton(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void ChangeInfo(object sender, RoutedEventArgs e)
        {
            DBController.ChangeInfo(value, UserName.Text, UserSecondName.Text);
            MessageBox.Show("Змінено");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var letter = new Regex("[a-z]");
            var bigLetter = new Regex("[A-Z]");
            var number = new Regex("[0-9]");
            var symbols = new Regex("[!@#$%^&*]");
            string password = NewUserPassword.Text;
            if (password != NewUserRepeatedPassword.Text)
            {
                MessageBox.Show("Паролі різні");
                NewUserPassword.Text = "";
                NewUserRepeatedPassword.Text = "";
                return;
            }
            if (password.Length >= 8 && bigLetter.IsMatch(password) && symbols.IsMatch(password))
            {
                if(DBController.AddUser(NewUserLogin.Text, password, NewUserName.Text, NewUserSecondName.Text))
                {
                    MessageBox.Show("Додано");
                    NewUserPassword.Text = "";
                    NewUserRepeatedPassword.Text = "";
                    NewUserName.Text = "";
                    NewUserLogin.Text = "";
                    NewUserSecondName.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Паролі не відповідають стандарту\nМають містити велику літеру та спец сивол");
                NewUserPassword.Text = "";
                NewUserRepeatedPassword.Text = "";
            }
        }
    }
}
