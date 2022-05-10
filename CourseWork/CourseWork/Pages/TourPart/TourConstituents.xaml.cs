using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static CourseWork.DBController;

namespace CourseWork.Pages.TourPart
{
    public partial class TourConstituents : Page
    {
        #region Tables collections
        public readonly Dictionary<string, string[]> collections = new Dictionary<string, string[]>()
        {
            {"",        new [] {"Tour", "Route", "City", "Country", "MealType", "AdditionalType", "TravelWay"}},
            {"Tour",    new [] {"TourMealType"}},
            {"Route",   new [] {"RouteAccomodation", "RouteCountries", "RouteMealType", "RouteService", "RouteTravelWays"}},
            {"Country", new [] {"InaccessibleCountries"}},
        };
        private readonly Dictionary<string, TableSettings> settings = new Dictionary<string, TableSettings>()
        {
            {"MealType",                new TableSettings()
            {
                headers =      new setting<string>[]     {"Id", "Абревіатура", "Тип", "Опис"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)}
            }},
            {"TravelWay",               new TableSettings()
            {
                headers =      new setting<string>[]     {"Id", "Назва"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)}
            }},
            {"AdditionalType",          new TableSettings()
            {
                headers =      new setting<string>[]     {"Id", "Тип", "У використанні"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)}
            }},
            {"City",                    new ImageTableSettins()
            {
                headers =      new setting<string>[]     {"Id", "Країна", "Назва", "Зображення", "У використанні"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)},
                lengths = new setting<DataGridLength>[] {(100, 1)},
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("CountryId", "NameUA", ToTable("Country")), "CountryId"), 1)
                },
                images = new setting<(string, string)>[] {(("SELECT Image FROM City", "CityId"), 3) }
            }},
            {"Country",                 new ImageTableSettins()
            {
                headers =      new setting<string>[]     {"", "Назва", "Прапор", "У використанні"},
                buttons = new setting<(RoutedEventHandler handler, string name)>[]
                {
                    (new RoutedEventHandler((s, e) => MainWindow.WindowFrame.Navigate(new TourConstituents((int)((s as Button).DataContext as DataRowView).Row[0], "CountryId", "Country"))), "Детальніше")
                },
                images = new setting<(string, string)>[] {(("SELECT Image FROM Country", "CountryId"), 2) }
            }},
            {"Route",                   new ButtonedTableSettings()
            {
                headers =      new setting<string>[]     {"", "Назва", "Опис"},
                buttons = new setting<(RoutedEventHandler handler, string name)>[]
                {
                    (new RoutedEventHandler((s, e) => MainWindow.WindowFrame.Navigate(new TourConstituents((int)((s as Button).DataContext as DataRowView).Row[0], "RouteId", "Route"))), "Детальніше")
                },
            }},
            {"TourMealType",            new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"Id", "Тип", "Ціна"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)},
                links = new setting<TableLink>[] { (new TableLink(GetDict("MealTypeId", "Type", ToTable("MealType")), "MealTypeId"), 1) }
            }},
            {"Tour",                    new DatePickerTableSettings()
            {
                headers =      new setting<string>[]     {"", "Шлях", "Ціна","Початок", "Кінець"},
                buttons = new setting<(RoutedEventHandler, string)>[] 
                {
                (new RoutedEventHandler((s, e) => MainWindow.WindowFrame.Navigate(new TourConstituents((int)((s as Button).DataContext as DataRowView).Row[0], "TourId", "Tour"))), "Детальніше")
                },
                dates = new setting<string>[]{("Start", 3), ("[End]", 4) },
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("RouteId", "Name", ToTable("Route")), "RouteId"), 1)
                },
            }},
            {"InaccessibleCountries",   new DatePickerTableSettings()
            {
                headers =      new setting<string>[]     {"Від", "До", "Країна"},
                dates = new setting<string>[]{("DateFrom", 0), ("DateTo", 1) },
                readonlies = new setting<bool>[]{(true, 2)},
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("CountryId", "NameUA", ToTable("Country")), "CountryId"), 2)
                },
            }},
            {"RouteMealType",           new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"RouteId", "Тип", "Ціна"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)},
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("MealTypeId", "Type", ToTable("MealType")), "MealTypeId"), 1)
                }
            }},
            {"RouteAccomodation",       new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"", "", "Місто", "Назва", "Ночі", "Порядок"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0), (Visibility.Hidden, 1)},
                readonlies =   new setting<bool>[]       {(true, 0), (true, 1)},
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("CityId", "CityName", ToTable("City")), "CityId"), 2)
                }
            }},
            {"RouteService",            new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"", "", "Тип", "Назва", "Ціна", "Опис"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0), (Visibility.Hidden, 1)},
                readonlies =   new setting<bool>[]       {(true, 0), (true, 1)},
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("AddTypeId", "Type", ToTable("AdditionalType"), false), "AddTypeId"), 2)
                }
            }},
            {"RouteCountries",          new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"Шлях", "Країна"},
                readonlies = new setting<bool>[] {true},
                links = new setting<TableLink>[]
                {
                    new TableLink(GetDict("RouteId", "Name", ToTable("Route")), "RouteId"),
                    new TableLink(GetDict("CountryId", "NameUA", ToTable("Country")), "CountryId"),
                }
            }},
            {"RouteTravelWays",         new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"Шлях", "Тип", "Назва"},
                readonlies =   new setting<bool>[]       {(true, 0)},
                links = new setting<TableLink>[]
                {
                    new TableLink(GetDict("RouteId", "Name", ToTable("Route")), "RouteId"),
                    new TableLink(GetDict("TravelWayId", "Name", ToTable("TravelWay")), "TravelWayId")
                }
            }}
        };

        private static string ToTable(string el) => $"[dbo].[{el}]";
        #endregion

        private string query;

        #region Constructors
        public TourConstituents()
        {
            InitializeComponent();
            query = "SELECT * FROM {0}";
            LoadDefaultView("");
        }

        private TourConstituents(int element, string column, string collection)
        {
            InitializeComponent();
            query = $"SELECT * FROM {{0}} WHERE {column} = {element}";
            LoadDefaultView(collection);
        }
        #endregion

        #region Additionals
        public static Dictionary<int, string> GetDict(string keyColumn, string valueColumn, string tableName, bool inUse = true)
        {
            string command = $"SELECT {keyColumn}, {valueColumn} FROM {tableName} {(inUse ? "" : "Where inUse = 1")}";
            return GetTable(command).
                          AsEnumerable().
                          ToDictionary(t => (int)t[0], v => (string)v[1]);
        }
        #endregion

        private void LoadDefaultView(string collectionName)
        {
            var collection = collections[collectionName];
            CollectionComboBox.ItemsSource = collection;
            this.Loaded += (s, e) => CollectionComboBox.SelectedItem = collection.First();
        }

        private void LoadTable(string tableName)
        {
            MainTable.Columns.Clear();
            DataTable table = GetTable(String.Format(query, ToTable(tableName)));
            var setting = settings[tableName];
            setting.ReworkTable(table);
            MainTable.ItemsSource = table.DefaultView;
            setting.Apply(MainTable);
        }

        private void SelectedTableChanged(object sender, SelectionChangedEventArgs e)
        {
            var newTable = (sender as ComboBox).SelectedItem as string;
            LoadTable(newTable);
        }
    }
}
