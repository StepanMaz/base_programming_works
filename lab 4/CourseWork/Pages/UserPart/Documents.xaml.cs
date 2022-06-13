﻿using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using CourseWork.Pages.TourPart;
using System.Windows.Media.Imaging;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    public partial class Documents : Page
    {
        int clientId;
        public Documents(int clientId)
        {
            this.clientId = clientId;
            InitializeComponent();

            var passportsettings = new DatePickerTableSettings
            {
                headers = new setting<string>[] { "", "Паспорт", "Дата видачі", "Дата зпливу", "Данні", "Країна" },
                visibilities = new setting<Visibility>[] { Visibility.Hidden },
                dates = new setting<(string, bool)>[] { (("IssueDate", true), 2), (("ExperationDate", true), 3) },
                links = new setting<TableLink>[]
                {
                    (new TableLink(TourConstituents.GetDict("PassportId", "Data", "Passport"), "PassportId"), 1),
                    (new TableLink(TourConstituents.GetDict("CountryId", "NameUa", "Country"), "CountryId"), 5)
                }
            };
            VisaTable.AutoGeneratedColumns += (s, e) => passportsettings.Apply(VisaTable);

            var visasettings = new DatePickerTableSettings
            {
                headers = new setting<string>[] { "", "", "Данні", "Дата видачі", "Дата зпливу" },
                visibilities = new setting<Visibility>[] { Visibility.Hidden, Visibility.Hidden },
                dates = new setting<(string, bool)>[] { (("IssueDate", true), 3), (("ExperationDate", true), 4) }
            };
            PassportsTable.AutoGeneratedColumns += (s, e) => visasettings.Apply(PassportsTable);

            LoadPassports();
            LoadVisas();
        }

        private void LoadPassports()
        {
            PassportsTable.IsReadOnly = true;

            DataTable table = GetTable($"SELECT * FROM [dbo].[Passport] WHERE ClientId = {clientId}");

            if(table.Rows.Count > 0)
            {
                PassportsTable.ItemsSource = table.DefaultView;
            }
            else
            {
                PassportsTable.Visibility = Visibility.Hidden;
                NoPassports.Visibility = Visibility.Visible;
            }
        }

        private void LoadVisas()
        {
            VisaTable.IsReadOnly = true;

            DataTable table = GetTable($"SELECT * FROM [dbo].[Visa] WHERE PassportId IN (SELECT PassportId FROM [dbo].[Passport] WHERE ClientId = {clientId})");

            if(table.Rows.Count > 0)
            {
                VisaTable.ItemsSource = table.DefaultView;

            }
            else
            {
                VisaTable.Visibility = Visibility.Hidden;
                NoVisas.Visibility = Visibility.Visible;
            }
        }

        private void AddPassportClick(object sender, RoutedEventArgs e)
        {
            new Pages.UserPart.PassportRegistration(clientId).ShowDialog();
            PassportsTable.Columns.Clear();
            LoadPassports();
        }

        private void AddVisaClick(object sender, RoutedEventArgs e)
        {
            new Pages.UserPart.VisaRegistration(clientId).ShowDialog();
            VisaTable.Columns.Clear();
            LoadVisas();
        }
    }
}
