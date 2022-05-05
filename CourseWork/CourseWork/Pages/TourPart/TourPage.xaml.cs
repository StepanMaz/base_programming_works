using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

using static CourseWork.DBController;

namespace CourseWork.Pages.TourPart
{
    /// <summary>
    /// Interaction logic for TourPage.xaml
    /// </summary>
    public partial class TourPage : Page
    {
        public TourPage()
        {
            InitializeComponent();
			LoadTours();
			LoadTourMealType();
		}

		public void LoadTours()
		{
			DataTable table = GetTable("SELECT * FROM [dbo].[Tour]");

			TourTable.ItemsSource = table.DefaultView;

			TourTable.CanUserDeleteRows = false;
		}

		public void LoadTourMealType()
		{
			DataTable table = GetTable("SELECT * FROM [dbo].[TourMealType]");

			TourMealTypeTable.ItemsSource = table.DefaultView;

			TourMealTypeTable.CanUserDeleteRows = false;
		}
	}
}
