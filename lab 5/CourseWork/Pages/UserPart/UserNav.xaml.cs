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
    /// Interaction logic for UserNav.xaml
    /// </summary>
    public partial class UserNav : Page
    {
        public UserNav()
        {
            InitializeComponent();
        }

        private void UserClick(object sender, RoutedEventArgs e) => MainWindow.WindowFrame.Navigate(new UserPage());

        private void AddClientClick(object sender, RoutedEventArgs e) => MainWindow.WindowFrame.Navigate(new NewClientAdding());
    }
}
