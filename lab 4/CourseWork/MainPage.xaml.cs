using System.Windows;
using System.Windows.Controls;

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
        private void ToursClick(object sender, RoutedEventArgs e) => MainWindow.WindowFrame.Navigate(new Pages.TourPart.TourConstituents());
        private void AdditionalsClick(object sender, RoutedEventArgs e) => MainWindow.WindowFrame.Navigate(new Pages.ReportPart.ReportPage());
        private void Exit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
        #endregion
    }
}
