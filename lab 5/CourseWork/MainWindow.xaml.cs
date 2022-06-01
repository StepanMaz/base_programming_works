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
using System.Windows.Shapes;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
