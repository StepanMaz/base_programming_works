using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages.UserPart
{
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
