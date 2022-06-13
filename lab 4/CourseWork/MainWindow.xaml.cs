using System.Windows;
using System.Windows.Controls;

namespace CourseWork
{
    public partial class MainWindow : Window
    {
        public static Frame WindowFrame;
        public MainWindow()
        {
            InitializeComponent();
            WindowFrame ??= Main;
            WindowFrame.Navigated += 
                (s, e) =>
                {
                    if (!WindowFrame.CanGoBack)
                        Return.Visibility = Visibility.Hidden;
                    else
                        Return.Visibility = Visibility.Visible;
                };
            WindowFrame.Content = new MainPage();
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            WindowFrame.GoBack();
        }
    }
}
