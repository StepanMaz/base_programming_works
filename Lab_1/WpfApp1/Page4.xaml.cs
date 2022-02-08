using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Window
    {
        public Page4()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        private void Button_Press(object sender, RoutedEventArgs e)
        {
            expression.Content = expression.Content.ToString() + ((Button)sender).Content;
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            string exp = expression.Content.ToString();

            #region clearing
            while (exp.Contains("++") || exp.Contains("--") || exp.Contains("+-") || exp.Contains("-+"))
            {
                exp = exp.Replace("++", "+");
                exp = exp.Replace("--", "+");
                exp = exp.Replace("+-", "-");
                exp = exp.Replace("-+", "-");
            }
            exp = exp.Trim('+');
            #endregion

            Regex mult = new Regex(@"\-?(\d+|(\d+\,\d+))\*(\-?|\+?)(\d+|(\d+\,\d+))");
            while (mult.IsMatch(exp))
            {
                string mat = mult.Match(exp).ToString();
                exp = exp.Replace(mat, "+"+(double.Parse(mat.Split('*')[0]) * double.Parse(mat.Split('*')[1])).ToString());
                exp = exp.Replace("+-", "-");
                exp = exp.Trim('+');
            }

            Regex div = new Regex(@"\-?(\d+|(\d+\,\d+))\/(\-?|\+?)(\d+|(\d+\,\d+))");
            while (div.IsMatch(exp))
            {
                string mat = div.Match(exp).ToString();
                exp = exp.Replace(mat, "+" + (double.Parse(mat.Split('/')[0]) / double.Parse(mat.Split('/')[1])).ToString());
                exp = exp.Replace("+-", "-");
                exp = exp.Trim('+');
            }

            Regex plus = new Regex(@"\-?(\d+|(\d+\,\d+))\+(\d+|(\d+\,\d+))");
            while (plus.IsMatch(exp))
            {
                string mat = plus.Match(exp).ToString();
                exp = exp.Replace(mat, "+" + (double.Parse(mat.Split('+')[0]) + double.Parse(mat.Split('+')[1])).ToString());
                exp = exp.Replace("+-", "-");
                exp = exp.Trim('+');
            }

            Regex minus = new Regex(@"\-?(\d+|(\d+\,\d+))\-(\d+|(\d+\,\d+))");
            while (minus.IsMatch(exp))
            {
                string mat = minus.Match(exp).ToString();
                exp = exp.Replace(mat, "+" + (double.Parse(mat.Split('-')[0]) - double.Parse(mat.Split('-')[1])).ToString());
                exp = exp.Replace("+-", "-");
                exp = exp.Trim('+');
            }

            double res;
            if (double.TryParse(exp, out res))
            {
                expression.Content = res.ToString();
            }
            else
            {
                MessageBox.Show("Wrong input");
            }
        }

        private void Backspace(object sender, RoutedEventArgs e)
        {
            expression.Content = expression.Content.ToString().Substring(0, expression.Content.ToString().Length - 1);
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            expression.Content = "";
        }
    }
}
