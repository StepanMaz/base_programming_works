using System;
using System.Collections.Generic;
using System.Windows;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    public partial class PassportRegistration : Window
    {
        public string data { get; set; }
        public DateTime start { get; set; } = DateTime.Now;
        public DateTime end { get; set; } = DateTime.Now.AddYears(4);

        private readonly int client;

        public PassportRegistration(int client)
        {
            InitializeComponent();
            DataContext = this;

            this.client = client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(data == "")
            {
                MessageBox.Show("Введіть дані");
                return;
            }

            if (start > end)
            {
                MessageBox.Show("Некоректні дати");
                return;
            }

            var passport = new Dictionary<string, object>
            {
                {"ClientId",       client},
                {"Data",           data  },
                {"IssueDate",      start },
                {"ExperationDate", end   }
            };

            if (!Insert("Passport", passport))
                MessageBox.Show("Помилка при додаванні паспорта");
            Close();
        }
    }
}
