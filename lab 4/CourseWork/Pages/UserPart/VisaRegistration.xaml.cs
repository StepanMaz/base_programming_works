using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Data;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    public partial class VisaRegistration : Window
    {
        public string data { get; set; }
        public DateTime start { get; set; } = DateTime.Now;
        public DateTime end { get; set; } = DateTime.Now.AddYears(4);

        private readonly int client;

        public VisaRegistration(int client)
        {
            InitializeComponent();
            DataContext = this;

            this.client = client;

            LoadPassports();
            LoadCountries();
        }

        private Dictionary<string, int> passports;
        private int passportId => passports[Passports.Text];
        private Dictionary<string, int> countries;
        private int countryId => countries[Countries.Text];
        public void LoadPassports()
        {
            passports = GetPassports();
            if (passports.Count() == 0)
            {
                MessageBox.Show("В вас немає паспортів");
                new PassportRegistration(client).ShowDialog();
                passports = GetPassports();
            }
            Passports.ItemsSource = passports.Keys;
            Passports.SelectedItem = passports.First().Key;

            Dictionary<string, int> GetPassports() => GetTable($"SELECT Data, PassportId FROM Passport WHERE ClientId = {client}").
                AsEnumerable().ToDictionary(k => (string)k.ItemArray[0], v => (int)v.ItemArray[1]);
        }

        public void LoadCountries()
        {
            countries = GetCountries();
            Countries.ItemsSource = countries.Keys;
            Countries.SelectedItem = countries.First().Key;

            Dictionary<string, int> GetCountries() => GetTable($"SELECT NameUA, CountryId FROM Country").
                AsEnumerable().ToDictionary(k => (string)k.ItemArray[0], v => (int)v.ItemArray[1]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (data == "")
            {
                MessageBox.Show("Введіть дані");
                return;
            }

            if (start > end)
            {
                MessageBox.Show("Некоректні дати");
                return;
            }

            var visa = new Dictionary<string, object>
            {
                {"PassportId",     passportId},
                {"IssueDate",      start     },
                {"ExperationDate", end       },
                {"Data",           data      },
                {"CountryId",      countryId }
            };

            if (!Insert("Visa", visa))
                MessageBox.Show("Помилка при додаванні візи");
            Close();
        }
    }
}
