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
    public partial class MainWindow : Window
    {
        partial void TourActionListInit();
        partial void TourEnumerationInit();
        partial void CountriesInit();
        partial void CitiesInit();

        public MainWindow()
        {
            InitializeComponent();

            foreach (var item in this.Container.Children.OfType<Grid>())
            {
                item.Visibility = Visibility.Hidden;
            }
            
            current = MainPage;
            current.Visibility = Visibility.Visible;

            TourActionListInit();
            TourEnumerationInit();
            CountriesInit();
            CitiesInit();
        }

        #region History
        public Grid current;
        public List<Grid> history = new List<Grid>();

        public void GoForward(Grid next)
        {
            history.Add(current);
            current.Visibility = Visibility.Hidden;
            next.Visibility = Visibility.Visible;
            current = next;
        }

        public void Return()
        {
            current.Visibility = Visibility.Hidden;
            current = history.Last();
            history.Remove(current);

            current.Visibility = Visibility.Visible;
        }
        #endregion

        #region Buttons
        private void CliensClick(object sender, RoutedEventArgs e) => GoForward(null);
        private void ToursClick(object sender, RoutedEventArgs e) => GoForward(TourActionListPage);
        private void AdditionalsClick(object sender, RoutedEventArgs e) => GoForward(null);
        private void Exit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
        #endregion
    }
}
