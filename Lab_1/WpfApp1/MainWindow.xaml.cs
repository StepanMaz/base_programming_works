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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Page1 page1 = new Page1();
            page1.Show();
        }

        private void Page3_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Page3 page3 = new Page3();
            page3.Show();
        }

        private void Page4_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Page4 page4 = new Page4();
            page4.Show();
        }

        private void NextPage_Copy3_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Page5 page5 = new Page5();
            page5.Show();
        }
    }
}
