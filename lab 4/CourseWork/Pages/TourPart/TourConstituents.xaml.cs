using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                readonlies =   new setting<bool>[]       {(true, 0)},
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "MealType", "MealTypeId")}
                },
                insert = new EventHandler<DataGridRowEditEndingEventArgs>((s, e) => InsertValues("MealType", (e.Row.Item as DataRowView).Row, new (string, object)[]{GetMaxId("MealType","MealTypeId") }, "Abbreviation", "Type", "Description")),
                update = new EventHandler<DataGridRowEditEndingEventArgs>((s, e) => UpdateValues("MealType", (e.Row.Item as DataRowView).Row, $"MealTypeId = {(e.Row.Item as DataRowView).Row.ItemArray[0]}", "Abbreviation", "Type", "Description"))
            }},
            {"TravelWay",               new TableSettings()
            {
                headers =      new setting<string>[]     {"Id", "Назва"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)},
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "TravelWay", "TravelWayId")}
                },
                insert = (s, e) => InsertValues("TravelWay", (e.Row.Item as DataRowView).Row, default, "Name"),
                update = (s, e) => UpdateValues("TravelWay", (e.Row.Item as DataRowView).Row, $"TravelWayId = {(e.Row.Item as DataRowView).Row.ItemArray[0]}", "Name")
            }},
            {"AdditionalType",          new TableSettings()
            {
                headers =      new setting<string>[]     {"Id", "Тип"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)},
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "AdditionalType", "AddTypeId")}
                },
                insert = (s, e) => InsertValues("AdditionalType", (e.Row.Item as DataRowView).Row, new (string, object)[]{GetMaxId("AdditionalType","AddTypeId") }, "Type"),
                update = (s, e) => UpdateValues("AdditionalType", (e.Row.Item as DataRowView).Row, $"AddTypeId = {(e.Row.Item as DataRowView).Row.ItemArray[0]}", "Type")
            }},
            {"City",                    new ImageTableSettins()
            {
                headers =      new setting<string>[]     {"Id", "Країна", "Назва", "Зображення"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)},
                lengths = new setting<DataGridLength>[] {(100, 1)},
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("CountryId", "NameUA", ToTable("Country")), "CountryId"), 1)
                },
                images = new setting<string>[] {("Image", 3) },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "City", "CityId")}
                },
                insert = (s, e) => InsertValues("City", (e.Row.Item as DataRowView).Row, new (string, object)[]{ GetMaxId("City","CityId") }, "CountryID", "CityName", "Image"),
                update = (s, e) => UpdateValues("City", (e.Row.Item as DataRowView).Row, $"CityId = {(e.Row.Item as DataRowView).Row.ItemArray[0]}", "CountryID", "CityName", "Image")
            }},
            {"Country",                 new ImageTableSettins()
            {
                headers =      new setting<string>[]     {"", "Назва", "Прапор"},
                buttons = new setting<(RoutedEventHandler handler, string name)>[]
                {
                    (new RoutedEventHandler((s, e) =>
                    {
                        if((s as Button).DataContext is DataRowView row)
                            MainWindow.WindowFrame.Navigate(new TourConstituents((int)row.Row[0], "CountryId", "Country"));
                    }
                    ), "Детальніше")
                },
                images = new setting<string>[] {("Image", 2) },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "Country", "CountryId")}
                },
                insert = (s, e) => InsertValues("Country", (e.Row.Item as DataRowView).Row, new (string, object)[]{GetMaxId("Country","CountryId") }, "NameUA", "Image"),
                update = (s, e) => UpdateValues("Country", (e.Row.Item as DataRowView).Row, $"CountryId = {(e.Row.Item as DataRowView).Row.ItemArray[0]}", "NameUA", "Image")
            }},
            {"Route",                   new ButtonedTableSettings()
            {
                headers =      new setting<string>[]     {"", "Назва", "Опис"},
                buttons = new setting<(RoutedEventHandler handler, string name)>[]
                {
                    (new RoutedEventHandler((s, e) =>
                    {
                        if((s as Button).DataContext is DataRowView row)
                        MainWindow.WindowFrame.Navigate(new TourConstituents((int)row.Row[0], "RouteId", "Route"));
                    }), "Детальніше")
                },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "Route", "RouteId")}
                },
                insert = (s, e) => InsertValues("Route", (e.Row.Item as DataRowView).Row, new (string, object)[]{GetMaxId("Route","RouteId")}, "Name", "Description"),
                update =(s, e) => UpdateValues("Route", (e.Row.Item as DataRowView).Row, $"RouteId = {(e.Row.Item as DataRowView).Row.ItemArray[0]}", "Name", "Description")
            }},
            {"TourMealType",            new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"Id", "Тип", "Ціна"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)},
                links = new setting<TableLink>[] { (new TableLink(GetDict("MealTypeId", "Type", ToTable("MealType")), "MealTypeId"), 1) },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "TourMealType", "TourId", "MealTypeId")}
                },
                insert = (s, e) => InsertValues("TourMealType", (e.Row.Item as DataRowView).Row, new (string, object)[]{ ("TourId", elem) }, "MealTypeId", "Price"),
                update =(s, e) => UpdateValues("TourMealType", (e.Row.Item as DataRowView).Row, $"TourId = {(e.Row.Item as DataRowView).Row.ItemArray[0]} AND MealTypeId = {(e.Row.Item as DataRowView).Row.ItemArray[1]}", "Price")
            }},
            {"Tour",                    new DatePickerTableSettings()
            {
                headers =      new setting<string>[]     {"", "Шлях", "Ціна","Початок", "Кінець"},
                buttons = new setting<(RoutedEventHandler, string)>[]
                {
                    (new RoutedEventHandler((s, e) =>
                    {
                        if((s as Button).DataContext is DataRowView row)
                        MainWindow.WindowFrame.Navigate(new TourConstituents((int)row.Row[0], "TourId", "Tour"));
                    }), "Детальніше")
                },
                dates = new setting<(string, bool)>[]{(("Start", false), 3), (("[End]", false), 4) },
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("RouteId", "Name", ToTable("Route")), "RouteId"), 1)
                },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "Tour", "TourId")}
                },
                insert = (s, e) => InsertValues("Tour", (e.Row.Item as DataRowView).Row, default, "RouteId", "BasePrice", "Start", "[End]"),
                update =(s, e) => UpdateValues("Tour", (e.Row.Item as DataRowView).Row, $"TourId = {(e.Row.Item as DataRowView).Row.ItemArray[0]}", "RouteId", "BasePrice", "Start", "[End]")
            }},
            {"InaccessibleCountries",   new DatePickerTableSettings()
            {
                headers =      new setting<string>[]     {"Від", "До", "Країна", ""},
                dates = new setting<(string, bool)>[]{(("DateFrom", false), 0), (("DateTo", false), 1) },
                readonlies = new setting<bool>[]{(true, 2)},
                visibilities = new setting<Visibility>[]{(Visibility.Hidden, 3)},
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("CountryId", "NameUA", ToTable("Country")), "CountryId"), 2)
                },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "InaccessibleCountries", "Id")}
                },
                insert = (s, e) => InsertValues("InaccessibleCountries", (e.Row.Item as DataRowView).Row, default, "CountryId", "DateFrom", "DateTo"),
                update =(s, e) => UpdateValues("InaccessibleCountries", (e.Row.Item as DataRowView).Row, $"Id = {(e.Row.Item as DataRowView).Row.ItemArray[3]}", "CountryId", "DateFrom", "DateTo")
            }},
            {"RouteMealType",           new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"RouteId", "Тип", "Ціна"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0)},
                readonlies =   new setting<bool>[]       {(true, 0)},
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("MealTypeId", "Type", ToTable("MealType")), "MealTypeId"), 1)
                },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "RouteMealType", "RouteID", "MealTypeId")}
                },
                insert = (s, e) => InsertValues("RouteMealType", (e.Row.Item as DataRowView).Row, new (string, object)[]{ ("RouteId", elem) }, "MealTypeId", "Price"),
                update =(s, e) => UpdateValues("RouteMealType", (e.Row.Item as DataRowView).Row, $"RouteID = {(e.Row.Item as DataRowView).Row.ItemArray[0]} AND MealTypeId = {(e.Row.Item as DataRowView).Row.ItemArray[1]}", "RouteID", "MealTypeId", "Price")
            }},
            {"RouteAccomodation",       new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"", "", "Місто", "Назва", "Ночі", "Порядок"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0), (Visibility.Hidden, 1)},
                readonlies =   new setting<bool>[]       {(true, 0), (true, 1)},
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("CityId", "CityName", ToTable("City")), "CityId"), 2)
                },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "RouteAccomodation", "RouteAcmdId")}
                },
                insert = (s, e) => InsertValues("RouteAccomodation", (e.Row.Item as DataRowView).Row, new (string, object)[]{GetMaxId("RouteAccomodation", "RouteAcmdId"), ("RouteId", elem) }, "CityID", "Name", "Duration", "[Order]"),
                update =(s, e) => UpdateValues("RouteAccomodation", (e.Row.Item as DataRowView).Row, $"RouteAcmdId = {(e.Row.Item as DataRowView).Row.ItemArray[0]}", "RouteId", "CityId", "Name", "Duration", "[Order]")
            }},
            {"RouteService",            new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"", "", "Тип", "Назва", "Ціна", "Опис"},
                visibilities = new setting<Visibility>[] {(Visibility.Hidden, 0), (Visibility.Hidden, 1)},
                readonlies =   new setting<bool>[]       {(true, 0), (true, 1)},
                links = new setting<TableLink>[]
                {
                    (new TableLink(GetDict("AddTypeId", "Type", ToTable("AdditionalType")), "AddTypeId"), 2)
                },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "RouteService", "RSId")}
                },
                insert = (s, e) => InsertValues("RouteService", (e.Row.Item as DataRowView).Row,  new (string, object)[]{ GetMaxId("RouteService", "RSId"), ("RouteId", elem) }, "AddtypeId", "Name", "Price", "Comment"),
                update =(s, e) => UpdateValues("RouteService", (e.Row.Item as DataRowView).Row, $"RSId = {(e.Row.Item as DataRowView).Row.ItemArray[0]}", "RsId", "RouteId", "AddtypeId", "Name", "Price", "Comment")
            }},
            {"RouteCountries",          new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"Шлях", "Країна"},
                readonlies = new setting<bool>[] {true},
                visibilities = new setting<Visibility>[]{(Visibility.Hidden, 2)},
                links = new setting<TableLink>[]
                {
                    new TableLink(GetDict("RouteId", "Name", ToTable("Route")), "RouteId"),
                    new TableLink(GetDict("CountryId", "NameUA", ToTable("Country")), "CountryId"),
                },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "RouteCountries", "Id")}
                },
                insert = (s, e) => InsertValues("RouteCountries", (e.Row.Item as DataRowView).Row, new (string, object)[]{("RouteId", elem)}, "CountryId"),
                update =(s, e) => UpdateValues("RouteCountries", (e.Row.Item as DataRowView).Row, $"Id = {(e.Row.Item as DataRowView).Row.ItemArray[2]}", "CountryId")
            }},
            {"RouteTravelWays",         new LinkedTableSettings()
            {
                headers =      new setting<string>[]     {"Шлях", "Тип", "Назва"},
                readonlies =   new setting<bool>[]       {(true, 0)},
                links = new setting<TableLink>[]
                {
                    new TableLink(GetDict("RouteId", "Name", ToTable("Route")), "RouteId"),
                    new TableLink(GetDict("TravelWayId", "Name", ToTable("TravelWay")), "TravelWayId")
                },
                keyPresses = new Dictionary<Key, Action<object, KeyEventArgs>>
                {
                    {Key.Delete, (s, e) => TryDelete(s, "RouteTravelWays", "RouteId", "TravelWayId")}
                },
                insert = (s, e) => InsertValues("RouteTravelWays", (e.Row.Item as DataRowView).Row,  new (string, object)[]{ ("RouteId", elem) }, "TravelWayId", "Name"),
                update =(s, e) => UpdateValues("RouteCountries", (e.Row.Item as DataRowView).Row, $"RouteId = {(e.Row.Item as DataRowView).Row.ItemArray[0]} AND TravelWayId = {(e.Row.Item as DataRowView).Row.ItemArray[1]}", "RouteId", "TravelWayId", "Name")
            }}
        };

        private static void TryDelete(object grid, string tableName, params string[] columnName)
        {
            DataGrid _grid = grid as DataGrid;

            if (_grid.SelectedItem is DataRowView row)
            {
                if(!TryDeleteById(tableName, string.Join(" AND ", columnName.Select(t => t + " = " + row[t]))))
                {
                    MessageBox.Show("Рядок не може бути видалено, бо він використовується");
                    return;
                }
                row.Delete();
            }
        }

        private static (string, object) GetMaxId(string table, string column)=> (column, GetObject<int>($"SELECT MAX({column}) FROM {table}") + 1);

        private static void InsertValues(string table, DataRow row, (string, object)[] Idfield, params string[] values)
        {
            var dic = values.ToDictionary(t => t, v => row[v.Trim('[', ']')]);
            if (Idfield != null)
                foreach (var item in Idfield)
                {
                    dic.Add(item.Item1, item.Item2);
                }

            if (!Insert(table, dic))
            {
                MessageBox.Show("Некоректні данні");
                _this.Reload();
            }
        }
        private static void UpdateValues(string table, DataRow row, string conditon, params string[] values)
        {
            if(!Update(table, values.ToDictionary(t => t, v => row[v.Trim('[', ']')]), conditon))
            {
                MessageBox.Show("Некоректні данні");
                _this.Reload();
            }
        }

        private static string ToTable(string el) => $"[dbo].[{el}]";
        #endregion

        private string query;

        #region Constructors
        static TourConstituents _this;
        public TourConstituents()
        {
            _this = this;
            InitializeComponent();
            query = "SELECT * FROM {0}";
            LoadDefaultView("");
        }

        static int elem = 0;
        private TourConstituents(int element, string column, string collection)
        {
            elem = element;
            _this = this;
            InitializeComponent();
            query = $"SELECT * FROM {{0}} WHERE {column} = {element}";
            LoadDefaultView(collection);
        }
        #endregion

        #region Additionals
        public static Dictionary<int, string> GetDict(string keyColumn, string valueColumn, string tableName)
        {
            string command = $"SELECT {keyColumn}, {valueColumn} FROM {tableName}";
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

        Action sourceChanged;
        private void LoadTable(string tableName)
        {
            if (sourceChanged != null)
                sourceChanged.Invoke();
            MainTable.Columns.Clear();
            DataTable table = GetTable(String.Format(query, ToTable(tableName)));
            var setting = settings[tableName];
            MainTable.ItemsSource = table.DefaultView;
            setting.Apply(MainTable);

            sourceChanged = setting.CallEvent;
        }

        private void SelectedTableChanged(object sender, SelectionChangedEventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            var newTable = CollectionComboBox.SelectedItem as string;
            LoadTable(newTable);
        }
    }
}
