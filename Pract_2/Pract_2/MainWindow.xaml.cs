using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pract_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dT;
        const int Radius = 20;
        int PointCount = 5;
        Polygon myPolygon = new Polygon()
        {
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };
        List<Ellipse> EllipseArray = new List<Ellipse>();
        PointCollection pC = new PointCollection();
        Algorithm algorithm = new EvolutionAlgorithm();

        public MainWindow()
        {
            dT = new DispatcherTimer();
            InitializeComponent();

            InitPoints();
            dT.Tick += OneStep;
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }

        public void InitPoints()
        {
            //circle

            //int counterx = 0;
            //int countery = 0;
            //InitPoints(() =>
            //{
            //    double x = MyCanvas.Width / 2 + Math.Cos(2 * Math.PI * counterx / PointCount) * 170;
            //    counterx++;
            //    return x;
            //},
            //() =>
            //{
            //    double y = MyCanvas.Height / 2 + Math.Sin(2 * Math.PI * countery / PointCount) * 170;
            //    countery++;
            //    return y;
            //});

            //random
            Random rnd = new Random();
            InitPoints(() => rnd.Next(Radius, (int)MyCanvas.Width - Radius),
                       () => rnd.Next(Radius, (int)MyCanvas.Height - Radius));
        }

        private void InitPoints(Func<double> x, Func<double> y)
        {
            Random rnd = new Random();

            pC.Clear();
            EllipseArray.Clear();

            //Point collection
            for (int i = 0; i < PointCount; i++)
            {
                pC.Add(new Point()
                {
                    X = x.Invoke(),
                    Y = y.Invoke(),
                });
            }

            //EllipseArray
            for (int i = 0; i < PointCount; i++)
            {
                Ellipse el = new Ellipse();

                el.Height = el.Width = Radius;
                el.Fill = Brushes.LightBlue;

                el.StrokeThickness = 2;
                el.Stroke = Brushes.Black;

                EllipseArray.Add(el);
            }
            algorithm.Init(pC);
        }

        private void OneStep(object sender, EventArgs e)
        {
            MyCanvas.Children.Clear();

            Draw(algorithm.GetWay());
        }

        private void Draw(int[] BestWayIndex)
        {
            for (int i = 0; i < EllipseArray.Count; i++)
            {
                if (i == BestWayIndex[0])
                    EllipseArray[i].Fill = Brushes.Red;
                else
                    EllipseArray[i].Fill = Brushes.LightBlue;
            }

            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
            myPolygon.Points.Clear();

            for (int i = 0; i < BestWayIndex.Length; i++)
            {
                myPolygon.Points.Add(pC[BestWayIndex[i]]);
            }
            MyCanvas.Children.Add(myPolygon);
        }

        #region Button
        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            if (dT.IsEnabled)
            {
                dT.Stop();
                Alg.IsEnabled = true;
                NumElemCB.IsEnabled = true;
            }
            else
            {
                Alg.IsEnabled = false;
                NumElemCB.IsEnabled = false;
                dT.Start();
            }
        }
        #endregion

        #region Selection changed
        private void VelCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dT.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt16(((ListBoxItem)((ComboBox)e.Source).SelectedItem).Content));
        }

        private void NumElemCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;
            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
        }


        private void Alg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;
            switch (item.Content)
            {
                case "Evolution":
                    algorithm = new EvolutionAlgorithm();
                    break;
                case "Greedy":
                    algorithm = new GreedyAlgorithm();
                    break;
            }
            algorithm.Init(pC);
        }
        #endregion

        #region Algoritms
        public abstract class Algorithm
        {
            public abstract void Init(PointCollection pC);
            public abstract int[] GetWay();
        }

        class GreedyAlgorithm : Algorithm
        {
            int startIndex = 0;
            int bestWayStart = 0;
            double[,] distances;

            PointCollection points;

            public override void Init(PointCollection pC)
            {
                points = pC;

                distances = new double[pC.Count, pC.Count];

                for (int i = 0; i < pC.Count; i++)
                {
                    for (int j = 0; j < pC.Count; j++)
                    {
                        distances[i, j] = Math.Sqrt(Math.Pow(pC[i].X - pC[j].X, 2) + Math.Pow(pC[i].Y - pC[j].Y, 2));
                    }
                }
            }

            public override int[] GetWay()
            {
                int[] way;
                if (startIndex < points.Count)
                {
                    way = BuildWay(startIndex);
                    if (GetLength(way) < GetLength(BuildWay(bestWayStart)))
                    {
                        bestWayStart = startIndex;
                    }
                    startIndex++;
                }
                else
                {
                    way = BuildWay(bestWayStart);
                }

                return way;
            }

            private double GetLength(int[] indexes)
            {
                double sum = 0;
                for (int i = 0; i < indexes.Length - 1; i++)
                {
                    sum += distances[indexes[i], indexes[i + 1]];
                }
                sum += distances[indexes.Last(), indexes.First()];
                return sum;
            }


            private int[] BuildWay(int start)
            {
                List<int> indexes = new List<int>();
                for (int i = 0; i < points.Count; i++)
                {
                    indexes.Add((start + i) % points.Count);
                }
                indexes.Remove(start);

                int[] way = new int[points.Count + 1];
                way[0] = start;

                for (int i = 1; i < points.Count; i++)
                {
                    int lessIndex = indexes[0];

                    for (int j = 0; j < indexes.Count; j++)
                    {
                        if (distances[way[i - 1], indexes[j]] < distances[way[i - 1], lessIndex])
                        {
                            lessIndex = indexes[j];
                        }
                    }

                    indexes.Remove(lessIndex);
                    way[i] = (int)lessIndex;
                }

                way[way.Length - 1] = start;
                return way;
            }
        }

        class EvolutionAlgorithm : Algorithm
        {
            Random rnd = new Random();
            const int populations = 10;
            List<int[]> ways;
            PointCollection points;
            public override void Init(PointCollection pC)
            {
                ways = new List<int[]>();
                points = pC;
                for (int i = 0; i < 2 * populations; i++)
                {
                    ways.Add(new int[points.Count]);
                }

                for (int i = 0; i < populations; i++)
                {
                    FillLine(i);
                }
            }

            public override int[] GetWay()
            {
                for (int i = 0; i < populations; i++)
                {
                    int i1 = rnd.Next(populations);
                    int i2 = rnd.Next(populations);
                    int crossPoint = rnd.Next(2, points.Count - 1);

                    var t1 = ways[i1][..crossPoint].Union(ways[i2][crossPoint..]);
                    var t2 = ways[i2][..crossPoint].Union(ways[i1][crossPoint..]);

                    if (rnd.Next(0, 2) == 0)
                    {
                        ways[i + populations] = t1.Union(t2).ToArray();
                    }
                    else
                    {
                        ways[i + populations] = t2.Union(t1).ToArray();
                    }

                    if (rnd.NextDouble() < 0.8)//Mutation
                    {
                        i1 = rnd.Next(points.Count);
                        i2 = rnd.Next(points.Count);
                        if (i1 < i2)
                        {
                            var arr = ways[i + populations];
                            arr = arr[..i1].Concat(arr[i1..i2].Reverse()).Concat(arr[i2..]).ToArray();
                            ways[i + populations] = arr;
                        }
                        else
                        {
                            var arr = ways[i + populations];
                            arr = arr[..i2].Concat(arr[i2..i1].Reverse()).Concat(arr[i1..]).ToArray();
                            ways[i + populations] = arr;
                        }
                    }
                }
                ways = ways.OrderBy(t => GetLength(t)).ToList();
                return ways[0];
            }

            private double GetLength(int[] indexes)
            {
                double sum = 0;
                for (int i = 0; i < indexes.Length - 1; i++)
                {
                    sum += Math.Sqrt(Math.Pow(points[indexes[i]].X - points[indexes[i + 1]].X, 2) + Math.Pow(points[indexes[i]].Y - points[indexes[i + 1]].Y, 2));
                }
                sum += Math.Sqrt(Math.Pow(points[indexes.Last()].X - points[indexes.First()].X, 2) + Math.Pow(points[indexes.Last()].Y - points[indexes.First()].Y, 2));
                return sum;
            }

            private void FillLine(int line)
            {
                List<int> example = new List<int>();
                for (int i = 0; i < points.Count; i++)
                {
                    example.Add(i);
                }
                for (int i = 0; i < points.Count; i++)
                {
                    int ind = rnd.Next(0, example.Count);
                    ways[line][i] = example[ind];
                    example.RemoveAt(ind);
                }
            }
        }
    }
    #endregion
}
