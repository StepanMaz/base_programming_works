using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using CourseWork.Pages.TourPart;

using static CourseWork.DBController;

namespace CourseWork.Pages.UserPart
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        int clientId;

        public Visibility visible { get; set; }

        public Orders(int id)
        {
            var settings = new Pages.TourPart.DatePickerTableSettings()
            {
                headers = new TourPart.setting<string>[] { "Тип", "Дата подачі", "Термін" },
                links = new TourPart.setting<TourPart.TableLink>[]
                {
                    new Pages.TourPart.TableLink(Pages.TourPart.TourConstituents.GetDict("OrderTypeId", "Type", "OrderType"), "OrderTypeId")
                },
                dates = new setting<(string, bool)>[] {(("ApplicationDate", true), 1) }
            };
            clientId = id;
            InitializeComponent();

            DataTable table = GetTable("SELECT o.OrderTypeId, o.ApplicationDate, ot.Term FROM [dbo].[Order] as o JOIN OrderType as ot ON o.OrderTypeID = ot.OrderTypeID");
            if(table.Rows.Count > 0)
            {
                OrderTable.ItemsSource = table.DefaultView;
                OrderTable.Loaded += (s, e) => settings.Apply(OrderTable);
            }
            else
            {
                visible = Visibility.Hidden;
            }
            OrderTable.CanUserAddRows = false;
        }
    }
}
