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

namespace CourseWork.Pages.TourPart
{
    /// <summary>
    /// Interaction logic for TourPartNav.xaml
    /// </summary>
    public partial class TourPartNav : Page
    {
        public TourPartNav()
        {
            InitializeComponent();
        }
        private void TourClick(object sender, RoutedEventArgs e)
           => MainWindow.WindowFrame.Navigate(new TourPage());
        private void RouteClick(object sender, RoutedEventArgs e)
            => MainWindow.WindowFrame.Navigate(new RoutePage());
        private void CitiesClick(object sender, RoutedEventArgs e)
            => MainWindow.WindowFrame.Navigate(new CityPage());
        private void CountriesClick(object sender, RoutedEventArgs e)
            => MainWindow.WindowFrame.Navigate(new CountryPage());
        private void EnumerationClick(object sender, RoutedEventArgs e)
            => MainWindow.WindowFrame.Navigate(new TourEnumeration());

    }
}
