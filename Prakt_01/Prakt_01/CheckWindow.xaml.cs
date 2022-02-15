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
using System.Diagnostics;

namespace Prakt_01
{
    /// <summary>
    /// Interaction logic for CheckWindow.xaml
    /// </summary>
    public partial class CheckWindow : Window
    {
        public CheckWindow()
        {
            InitializeComponent();
        }
        List<TimeSpan> spans = new List<TimeSpan>();
        bool started = false;
        int leng = 0;
        bool validate = true;
        Stopwatch sw;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!validate)//activation after clearing block
            {
                validate = true;
                return;
            }

            #region Correction check
            if (c.Text.Length != 0 && leng > c.Text.Length || c.Text != StudyWindow.word.Substring(0, c.Text.Length))
            {
                MessageBox.Show("Ввід некоректний");
                Restart();
                return;
            }
            leng = c.Text.Length;
            #endregion

            if (leng > 0)
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

                if (c.Text == StudyWindow.word)
                {
                    Calculate(spans);

                    Restart();
                }
            }
        }

        public void Restart()
        {
            c.Text = "";
            spans = new List<TimeSpan>();
            validate = false;
            started = false;
            leng = 0;
        }

        int trials = 0, pos = 0;
        public void Calculate(List<TimeSpan> spans)
        {
            trials++;
            double[] student_cof = { 6.314, 2.92, 2.353, 2.132, 2.015, 1.943, 1.895, 1.86, 1.833, 1.813, 1.8, 1.782, 1.761, 1.75, 1.75, 1.74, 1.734, 1.725, 1.72 };
            double M = Sum(spans.Count, (j) => spans[j].TotalSeconds) / spans.Count;
            double S = Sum(spans.Count, (j) => Math.Pow(spans[j].TotalSeconds - M, 2) / (spans.Count - 1));

            double[] Ms = System.IO.File.ReadAllLines(@"D:\Для_учебы\base_programming_works\Prakt_01\Prakt_01\save.txt").Select(t => double.Parse(t.Split(' ')[0])).ToArray();
            double[] Ss = System.IO.File.ReadAllLines(@"D:\Для_учебы\base_programming_works\Prakt_01\Prakt_01\save.txt").Select(t => double.Parse(t.Split(' ')[1])).ToArray();
            bool f = true;
            for (int i = 0; i < Ss.Length; i++)
            {
                double F = Math.Max(Ss[i], S) / Math.Min(Ss[i], S);

                if(F > 3.18)
                {
                    break;
                }
                double S_y = Sum(spans.Count, (j) => Math.Pow(spans[j].TotalSeconds - Ms[i], 2)) / (spans.Count - 1);
                double S_ = Math.Sqrt((Math.Pow(Ss[i], 2) + Math.Pow(S_y, 2)) * (spans.Count - 1) / (2 * spans.Count - 1));
                double t = (Ms[i] - M) / (S_ * Math.Sqrt(2 / spans.Count));
                if (t < student_cof[spans.Count - 2])
                {
                    f = false;
                }
            }
            if (!f)
            {
                pos++;
            }
            res.Content = (Math.Round((double)pos / (double)trials * 100)).ToString() + "%" + $" {pos}/{trials}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
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
    }
}
