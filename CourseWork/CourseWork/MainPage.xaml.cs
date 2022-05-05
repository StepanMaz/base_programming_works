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

namespace CourseWork
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        #region Buttons
        private void CliensClick(object sender, RoutedEventArgs e) => MainWindow.WindowFrame.Navigate(new Pages.UserPart.UserNav());
        private void ToursClick(object sender, RoutedEventArgs e) => MainWindow.WindowFrame.Navigate(new Pages.TourPart.TourPartNav());
        private void AdditionalsClick(object sender, RoutedEventArgs e) => MainWindow.WindowFrame.Navigate(this);
        private void Exit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
        #endregion
    }
}
