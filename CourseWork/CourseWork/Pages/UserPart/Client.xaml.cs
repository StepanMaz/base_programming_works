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

namespace CourseWork.Pages.UserPart
{
    /// <summary>
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        int id;
        public Client(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void DocumntsClick(object sender, RoutedEventArgs e)
        {
            MainWindow.WindowFrame.Navigate(new Documents(id));
        }

        private void AddTourClick(object sender, RoutedEventArgs e)
        {
            MainWindow.WindowFrame.Navigate(new TourApplication(id));
        }
    }
}
