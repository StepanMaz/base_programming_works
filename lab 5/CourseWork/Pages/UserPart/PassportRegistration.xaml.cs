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
