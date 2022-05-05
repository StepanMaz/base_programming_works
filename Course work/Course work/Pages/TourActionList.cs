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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Course_work
{
    public partial class MainWindow : System.Windows.Window
    {
        partial void TourActionListInit()
        {
            EnumButton.Click += (s, e) => GoForward(TourEnumerationPage);
            CountriesButton.Click += (s, e) => GoForward(CountriesPage);
            CityButton.Click += (s, e) => GoForward(CitiesPage);

            TourActionListPageReturn.Click += (s, e) => Return();
        }
    }
}
