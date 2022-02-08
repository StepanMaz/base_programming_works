using System;
using System.Linq;
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
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Window
    {
        public Page3()
        {
            InitializeComponent();
            Start();
            ChangeValues(false);
            Display();
        }

        private void BotTurn()
        {
            ChangeValues(turn);

            int b = values.Cast<int>().ToList().IndexOf(values.Cast<int>().Max());
            matrix[b / 5, b % 5] = turn;
            Display();
            Check();
            turn = !turn;
        }

        private int[,] values = new int[5, 5];

        public void ChangeValues(bool side)
        {
            values = new int[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(matrix[i, j] == null)
                    {
                        (int, int)[] mas = new (int, int)[8] { (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1) };
                        int counter = 0;
                        for (int v = 0; v < 8; v++)
                        {
                            int x = i, y = j;

                            bool valid = true;

                            int row = 0;
                            int nrow = 0;
                            for (int u = 0; u < 4; u++)
                            {
                                if (x < 0 || 4 < x || y < 0 || 4 < y)
                                {
                                    valid = false;
                                    break;
                                }
                                if (matrix[x, y] == !side)
                                {
                                    valid = false;
                                    nrow++;
                                }
                                if (matrix[x, y] == side)
                                {
                                    row++;
                                }
                                x += mas[counter].Item1;
                                y += mas[counter].Item2;
                            }

                            if(row == 3)
                            {
                                values[i, j] += 40;
                            }
                            if(row == 2)
                            {
                                values[i, j] += 6;
                            }
                            if(row == 1)
                            {
                                values[i, j] += 2;
                            }

                            if (nrow == 3)
                            {
                                values[i, j] += 15;
                            }
                            if (nrow == 2)
                            {
                                values[i, j] += 4;
                            }
                            if (nrow == 1)
                            {
                                values[i, j] += 1;
                            }

                            if (valid)
                            {
                                x = i;
                                y = j;
                                for (int u = 0; u < 4; u++)
                                {
                                    values[x, y]++;

                                    x += mas[counter].Item1;
                                    y += mas[counter].Item2;
                                }
                            }

                            counter++;
                        }
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        private  void Check()
        {
            if (!matrix.Cast<bool?>().Contains(null))
            {
                MessageBox.Show($"Draw");
                matrix = new bool?[5, 5];
                turn = true;
                Display();
                return;
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(matrix[i, j] == null)
                    {
                        continue;
                    }

                    (int, int)[] mas = new (int, int)[8] { (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1) };
                    int counter = 0;
                    for (int v = 0; v < 8; v++)
                    {
                        int x = i, y = j;

                        bool valid = true;

                        for (int u = 0; u < 4; u++)
                        {
                            if (x < 0 || 4 < x || y < 0 || 4 < y || matrix[i, j] != matrix[x, y])
                            {
                                valid = false;
                                break;
                            }
                            x += mas[counter].Item1;
                            y += mas[counter].Item2;
                        }

                        if (valid)
                        {
                            MessageBox.Show($"{((bool)matrix[i, j] ? "X" : "O")} Won");
                            matrix = new bool?[5, 5];
                            turn = true;
                            Display();
                            return;
                        }

                        counter++;
                    }
                }
            }
            
        }

        #region Display
        private bool?[,] matrix = new bool?[5, 5];
        private bool turn = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            if (matrix[(int.Parse(b.Name.Substring(1)) - 1) / 5, (int.Parse(b.Name.Substring(1)) - 1) % 5] != null)
            {
                return;
            }
            
            matrix[(int.Parse(b.Name.Substring(1)) - 1) / 5, (int.Parse(b.Name.Substring(1)) - 1) % 5] = turn;

            Display();
            Check();
            turn = !turn;
            if(turn)
                BotTurn();
        }

        private Button[,] buttons = new Button[5, 5];
        private void Start()
        {
            var a = field.Children.Cast<Button>().OrderBy(n => int.Parse(n.Name.Substring(1))).ToList();
            for (int i = 0, counter = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++, counter++)
                {
                    buttons[i, j] = a[counter];
                }
            }
        }
        private void Display()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    buttons[i, j].Content = matrix[i, j] != null ? ((bool)matrix[i, j] ? "X" : "O") : "";
                }
            }
        }
        #endregion
    }
}
