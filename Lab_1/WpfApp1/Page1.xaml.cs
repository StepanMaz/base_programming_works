using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Window
    {
        private string path = string.Join("\\", AppDomain.CurrentDomain.BaseDirectory.Split('\\').TakeWhile(t => t != "bin")) + @"\Страница_1.txt";

        Dictionary<TextBox, Regex> pat;
        public Page1()
        {
            InitializeComponent();
            Update();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var a = page1.Children.Cast<UIElement>().
                Where(t => t.GetType() == typeof(TextBox)).Cast<TextBox>().
                OrderBy(x => x.Name);
            File.AppendAllText(path, string.Join(" ", a.Select(e => e.Text)) + "\n");
            MessageBox.Show("OK");
            foreach (var item in a)
            {
                item.Text = "";
            }
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string d = del_student.Text;
            File.WriteAllLines(path, File.ReadAllLines(path).Where(t => t != "" && t.Split(' ')[0] != d));
            del_student.Text = "";
            MessageBox.Show("Deleted");
            Update();
        }

        private void Update()
        {
            lb.Content = File.ReadAllText(path);
        }

        
        private void _1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
