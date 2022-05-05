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
            var page = new MainPage();
            page.IsVisibleChanged += (s, e) => Return.Visibility = (!(bool)e.NewValue) ? Visibility.Visible : Visibility.Hidden;
            WindowFrame.Content = page;
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            WindowFrame.GoBack();
        }
    }
}
