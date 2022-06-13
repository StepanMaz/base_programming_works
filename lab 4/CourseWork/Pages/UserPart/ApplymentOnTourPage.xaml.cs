using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using CourseWork.Pages.TourPart;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    public partial class ApplymentOnTourPage : Page
    {
        private const string UPDATE_BUTTON_TEXT = "{0:C2} ({1}%)";
        private readonly int ClientId, TourId;

        private int price;
        private decimal sale;
        private int MealTypeId;
        private List<int> services = new List<int>();

        private Dictionary<string, int> passports;
        private int passportId => passports[Passport.Text];

        private Action valuesUpdated;

        #region start
        Button lastbtn = null;
        public ApplymentOnTourPage(int client, int tour)
        {
            InitializeComponent();

            this.ClientId = client;
            this.TourId = tour;

            valuesUpdated += () => price = GetObject<int>($"SELECT dbo.GetPriceForTour({TourId}, {ClientId}, {MealTypeId}, '{string.Join(",", services)}')");
            valuesUpdated += () => sale = GetObject<decimal>($"SELECT dbo.GetClientSale({ClientId})");
            valuesUpdated += () => SubmitButton.Content = String.Format(UPDATE_BUTTON_TEXT, (price * (100 - sale) / 100), sale);

            valuesUpdated.Invoke();

            LoadPassports();
            LoadServices();
            LoadTourMealTypes();
        }

        public void LoadPassports()
        {
            passports = GetPassports();
            if (passports.Count() == 0)
            {
                MessageBox.Show("В вас немає паспортів");
                new PassportRegistration(ClientId).ShowDialog();
                passports = GetPassports();
            }
            Passport.ItemsSource = passports.Keys;
            Passport.SelectedItem = passports.First().Key;

            Dictionary<string, int> GetPassports() => GetTable($"SELECT Data, PassportId FROM Passport WHERE ClientId = {ClientId}").
                AsEnumerable().ToDictionary(k => (string)k.ItemArray[0], v => (int)v.ItemArray[1]);
        }

        public void LoadTourMealTypes()
        {
            DataTable table = GetTable($"SELECT * FROM dbo.GetMealTypesOfTour({TourId})");
            table.Columns.Add();

            var settings = new ButtonedTableSettings
            {
                headers = new setting<string>[] {"Тип", "Ціна", ""},
                links = new setting<TableLink>[] { (new TableLink(TourConstituents.GetDict("MealTypeId", "Type", "MealType"), "MealTypeId"), 1) },
                buttons = new setting<(RoutedEventHandler handler, string name)>[]
                {
                    (new RoutedEventHandler(AddMealType), "Вибрати")
                },
            };

            MealTypesTable.ItemsSource = table.DefaultView;
            MealTypesTable.CanUserAddRows = false;

            MealTypesTable.Loaded += (s, e) => settings.Apply(MealTypesTable);

            void AddMealType(object sender, RoutedEventArgs e)
            {
                var btn = sender as Button;
                if(lastbtn == btn)
                {
                    lastbtn = null;
                    btn.Content = "Вибрати";
                    MealTypeId = 0;
                }
                else
                {
                    btn.Content = "Вибрано";
                    if (lastbtn is Button b)
                        b.Content = "Вибрати";
                    MealTypeId = (int)(btn.DataContext as DataRowView).Row["MealTypeId"];
                    lastbtn = btn;
                }

                valuesUpdated.Invoke();
            }
        }
        public void LoadServices()
        {
            var settings = new ButtonedTableSettings()
            {
                height = 40,
                headers = new setting<string>[] { "", "Тип", "Назва", "Ціна", "Опис" },
                lengths = new setting<DataGridLength>[] {(100, 2), (300, 4)},
                buttons = new setting<(RoutedEventHandler handler, string name)>[]
                {
                    (new RoutedEventHandler(AddService), "Додати") 
                },
            };

            DataTable table = GetTable($"SELECT rs.RSId, at.Type, rs.Name, rs.Price, rs.Comment FROM [dbo].[RouteService] as rs JOIN Tour as t ON t.RouteId = rs.RouteId JOIN AdditionalType as at ON at.AddTypeId = rs.AddTypeId WHERE t.TourId = {TourId}");
            
            ServiceTable.ItemsSource = table.DefaultView;
            ServiceTable.IsReadOnly = true;

            ServiceTable.Loaded += (s, e) => settings.Apply(ServiceTable);

            void AddService(object sender, RoutedEventArgs e)
            {
                var btn = sender as Button;
                bool status = (string)btn.Content == "Додати";
                btn.Content = status ? "Видалити" : "Додати";
                if (status)
                {
                    services.Add((int)(btn.DataContext as DataRowView).Row[0]);
                }
                else
                {
                    services.Remove((int)(btn.DataContext as DataRowView).Row[0]);
                }
                valuesUpdated.Invoke();
            }
        }
        #endregion

        #region Buttons click
        private void GetDetailInfo(object sender, RoutedEventArgs e)
        {
            string accomodationformat = "Назва: \"{1}\" Місто: {0} Дні: {2}",
                   servicesformat     = "{0}: \"{1}\"",
                   travelwaysformat   = "{0}: \"{1}\"",
                   countryformat      = "{0}",
                   dateformat         = "dd.MM.yyyy";

            #region Get data
            if (!(GetReader($"SELECT * FROM Tour WHERE TourId = {TourId}") is SqlDataReader row))
            {
                MessageBox.Show("Невдалося загрузити данні");
                return;
            }

            row.Read();
            if (!(row.GetDateTime(row.GetOrdinal("Start"  )) is DateTime start      &&
                  row.GetDateTime(row.GetOrdinal("End"  ))   is DateTime end        &&
                  row.GetInt32(   row.GetOrdinal("RouteId")) is int      routeId      ))
            {
                MessageBox.Show("Невдалося обробити данні");
                return;
            }
            row.Close();

            if (!(GetObject<string>($"SELECT Name FROM Route WHERE RouteId = {routeId}") is string Title))
            {
                MessageBox.Show("Невдалося загрузити данні");
                return;
            }

            if (!(GetTable($"SELECT CityName, Name, Duration FROM RouteAccomodation as ra JOIN City as c ON c.CityId = ra.CityId WHERE ra.RouteId = {routeId}") is DataTable accomodation))
            {
                MessageBox.Show("Невдалося загрузити данні");
                return;
            }

            if (!(GetTable($"SELECT at.Type, rs.Name, rs.Price FROM RouteService as rs JOIN AdditionalType as at ON at.AddTypeId = rs.AddTypeId WHERE rs.RouteId = {routeId}") is DataTable services))
            {
                MessageBox.Show("Невдалося загрузити данні");
                return;
            }

            if (!(GetTable($"SELECT tw.Name, rtw.Name FROM RouteTravelWays as rtw JOIN TravelWay as tw ON tw.TravelWayId = rtw.TravelWayId WHERE rtw.RouteId = {routeId}") is DataTable travelWays))
            {
                MessageBox.Show("Невдалося загрузити данні");
                return;
            }

            if (!(GetTable($"SELECT c.NameUA FROM RouteCountries as rc JOIN Country as c ON rc.CountryId = c.CountryId WHERE rc.RouteId = {routeId}") is DataTable countries))
            {
                MessageBox.Show("Невдалося загрузити данні");
                return;
            }
            #endregion

            var _accomodation = accomodation.AsEnumerable().Select(t => string.Format(accomodationformat, t.ItemArray));
            var _services     =     services.AsEnumerable().Select(t => string.Format(servicesformat,     t.ItemArray));
            var _travelways   =   travelWays.AsEnumerable().Select(t => string.Format(travelwaysformat,   t.ItemArray));
            var _countries    =    countries.AsEnumerable().Select(t => string.Format(countryformat,      t.ItemArray));

            var replacements = new Dictionary<string, string>
            {
                {"{Title}",         Title                            },
                {"{DateStart}",     start.ToString(dateformat)       },
                {"{DateEnd}",       end.ToString(dateformat)         },
                {"{Accomodation}",  string.Join(";\t", _accomodation)},
                {"{TravelWays}",    string.Join(";\t", _travelways)  },
                {"{Services}",      string.Join(";\t", _services)    },
                {"{Countries}",     string.Join(";\t", _countries)   }
            };

            CourseWork.Additionals.DocumentOperations.ReplaceInDocument(@"\example.docx", replacements);
        }

        private void ApplyToTourClick(object sender, RoutedEventArgs e)
        {
            if(MealTypeId == 0)
            {
                MessageBox.Show("Виберіть тип харчування");
                return;
            }

            var clienttoue = new Dictionary<string, object>
            {
                { "ClientId",           ClientId                    },
                { "TourId",             TourId                      },
                { "ApplicationDate",    DateTime.Today              },
                { "FullPrice",          price * (100 - sale) / 100  },
                { "MealTypeId",         MealTypeId                  }
            };
            List<string> message = new List<string>();

            if (Insert("ClientTour", clienttoue))
                message.Add("Тур зареєстровано");
            else
                message.Add("Тур не додано");

            int id = GetObject<int>("SELECT MAX(CTId) FROM ClientTour");
            foreach (var item in services)
            {
                var service = new Dictionary<string, object>
                {
                    {"CTId", id},
                    {"RSId", item}
                };
                if(!Insert("ClientTourAdditions", service))
                    message.Add("Додавання невдалося");
            }

            var countries = GetTable($"SELECT * FROM GetClientNeededVisas({ClientId}, {TourId})").AsEnumerable().Select(t => (int)t.ItemArray.First());

            foreach (var item in countries)
            {
                var visas = new Dictionary<string, object>
                {
                    {"PassportId", passportId},
                    {"IssueDate", DateTime.Now},
                    {"ExperationDate", DateTime.Now.AddYears(4)},
                    {"Data", new Random().Next(10000, 1000000).ToString().PadLeft(7, '0')},
                    {"CountryId", item}
                };
                if (!Insert("Visa", visas))
                    message.Add("Додавання візи невдалося");
            }

            MessageBox.Show(string.Join("\n", message));
            MainWindow.WindowFrame.NavigationService.RemoveBackEntry();
            MainWindow.WindowFrame.GoBack();
        }
        #endregion
    }
}
