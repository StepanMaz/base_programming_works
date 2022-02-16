using System;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;

namespace Prakt_01
{
    /// <summary>
    /// Interaction logic for StudyWindow.xaml
    /// </summary>
    public partial class StudyWindow : Window
    {
        int trials = 3;
        public const string word = "длагнитор";

        List<TimeSpan> spans = new List<TimeSpan>();
        bool started = false;
        int leng = 0;
        public StudyWindow()
        {
            InitializeComponent();
            VerifField.Text = word;
        }

        bool validate = true;
        Stopwatch sw;
        private void InputField_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CountProtection.IsEnabled = false;

            if (!validate)//activation after clearing block
            {
                validate = true;
                return;
            }

            #region Correction check
            if (leng > InputField.Text.Length || InputField.Text != word.Substring(0, InputField.Text.Length))
            {
                MessageBox.Show("Ввід некоректний");
                Restart();
                return;
            }
            leng = InputField.Text.Length;
            #endregion

            if(leng > 0)
            {
                if (!started)
                {
                    sw = new Stopwatch();
                    sw.Start();
                    started = true;
                }
                else
                {
                    sw.Stop();
                    spans.Add(sw.Elapsed);
                    sw = new Stopwatch();
                    sw.Start();
                }

                if (InputField.Text == word)
                {
                    Calculate(spans);

                    Restart();
                }
            }
        }

        public void Restart()
        {
            validate = false;
            InputField.Text = "";
            spans = new List<TimeSpan>();
            started = false;
            leng = InputField.Text.Length;
        }

        public void Calculate(List<TimeSpan> spans)
        {
            double[] student_cof = { 6.314, 2.92, 2.353, 2.132, 2.015, 1.943, 1.895, 1.86, 1.833, 1.813, 1.8, 1.782, 1.761, 1.75, 1.75, 1.74, 1.734, 1.725, 1.72 };
            bool valid = true;
            for (int i = 0; i < spans.Count; i++)
            {
                var y = spans.ToList();
                y.RemoveAt(i);

                double M = Sum(y.Count, (j) => y[j].TotalSeconds) / y.Count;

                double Square_S = Sum(y.Count, (j) => Math.Pow(y[j].TotalSeconds - M, 2) / (y.Count - 1));
               
                double t = (spans[i].TotalSeconds - M)/Math.Sqrt(Square_S);
                
                if (t > student_cof[y.Count - 1])
                {
                    valid = false;
                    break;
                }
            }
            if (valid)
            {
                double M = Sum(spans.Count, (j) => spans[j].TotalSeconds) / spans.Count;
                double S = Sum(spans.Count, (j) => Math.Pow(spans[j].TotalSeconds - M, 2) / (spans.Count - 1));
                System.IO.File.AppendAllText(@"D:\Для_учебы\base_programming_works\Prakt_01\Prakt_01\save.txt", $"{M} {S}\n");
            }
            trials--;
            if(trials == 0)
            {
                InputField.IsEnabled = false;
                MessageBox.Show("Навчання закінчилось");
            }
            else
            {
                MessageBox.Show($"{trials} вводу залишилось");
            }
        }

        public double Sum(int times, Func<int, double> f)
        {
            double res = 0;

            for (int i = 0; i < times; i++)
            {
                res += f(i);
            }

            return res;
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        private void CountProtection_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            trials = CountProtection.SelectedIndex + 3;
            CountProtection.IsEditable = false;
        }
    }
}
