using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    public partial class NewClientAdding : Page
    {
        string pip => Pip.Text;
        string telephone => Telephone.Text;
        string email => Email.Text;
        string comment => Comment.Text;

        public NewClientAdding()
        {
            InitializeComponent();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            if (!(pip.Split(' ') is string[] array && array.Length == 3))
            {
                MessageBox.Show("Неоректне ім'я");
                return;
            }

            if(!new Regex("[0-9]{10,12}").IsMatch(telephone))
            {
                MessageBox.Show("Неправильний номер телефону");
                return;
            }

            if (!new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$").IsMatch(email))
            {
                MessageBox.Show("Неправильний email");
                return;
            }

            bool res = Insert("Client", new Dictionary<string, object>()
                {
                    { "FirstName",        array[0] },
                    { "SecondName",       array[1] },
                    { "LastName",         array[2] },
                    { "TelephoneNumber",  telephone },
                    { "Email",            email },
                    { "RegistrationDate", DateTime.Now },
                    { "Comments",         comment }
                }
            );

            if (res)
            {
                MessageBox.Show("Додано");
                Pip.Text = Telephone.Text = Email.Text = Comment.Text = "";
            }
            else
            {
                MessageBox.Show("Щось не так");
            }
        }
    }
}
