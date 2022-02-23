using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OP_Lab_2
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

        public void Page1_Open_Click(object sender, RoutedEventArgs e)
        {
            new Page_1().Show();
            Hide();
        }

        public void Page2_Open_Click(object sender, RoutedEventArgs e)
        {
            new Page_2().Show();
            Hide();
        }

        public void Page3_Open_Click(object sender, RoutedEventArgs e)
        {
            new Page_3().Show();
            Hide();
        }

        public void Page4_Open_Click(object sender, RoutedEventArgs e)
        {
            new Page_4().Show();
            Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

    public class Page_1 : Window
    {
        const string path = @"D:\Для_учебы\base_programming_works\Lab_2\OP_Lab_2\OP_Lab_2\file.txt";
        const int height = 400, width = 800;
        public Page_1()
        {
            Grid mainGrid = new Grid();
            Button add;
            bool delete_input_correct = false;
            Dictionary<TextBox, bool> inputs = new Dictionary<TextBox, bool>();

            #region Preinitialization
            this.Content = mainGrid;
            #endregion

            #region Window settings
            this.SetWindowSettings(new Size(w: width, h: height));
            #endregion

            #region Regex checkers
            TextChangedEventHandler rc1 = (s, e) => 
            {
                TextBox tb = (TextBox)s;
                Regex reg = new Regex($"^[0-9]{{{tb.Text.Length}}}$");
                if (!reg.IsMatch(tb.Text) && tb.Text.Length != 0 || tb.Text.Length == 9)
                {
                    MessageBox.Show("Неправильно введений символ.\nСлово має містити 8 цифр");
                    tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                }
                if(reg.IsMatch(tb.Text))
                    if (tb.Text.Length != 8)
                    {
                        inputs[tb] = false;
                        tb.Foreground = Brushes.Red;
                    }
                    else
                    {
                        inputs[tb] = true;
                        tb.Foreground = Brushes.Green;
                    }
            };
            TextChangedEventHandler rc2 = (s, e) => 
            {
                Regex reg = new Regex("[a-z,A-Z,а-я,А-Я]|і|'| ");
                TextBox tb = (TextBox)s;
                if(reg.Matches(tb.Text).Count != tb.Text.Length)
                {
                    MessageBox.Show("Неправильно введений символ.\nСлово має містити лише літери та пробіли");
                    tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                }
                if(tb.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != 3)
                {
                    inputs[tb] = false;
                    tb.Foreground = Brushes.Red;
                }
                else
                {
                    inputs[tb] = true;
                    tb.Foreground = Brushes.Green;
                }
            };
            TextChangedEventHandler rc3 = (s, e) => 
            {
                TextBox tb = (TextBox)s;
                Regex reg = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                Regex cont = new Regex(@"[0-9]|[a-zA-z]|\.|@");
                if (cont.Matches(tb.Text).Count != tb.Text.Length && tb.Text.Length != 0)
                {
                    MessageBox.Show("Неправильно введений символ.");
                    tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                }
                if (!reg.IsMatch(tb.Text))
                {
                    inputs[tb] = false;
                    tb.Foreground = Brushes.Red;
                }
                else
                {
                    inputs[tb] = true;
                    tb.Foreground = Brushes.Green;
                }
            };
            TextChangedEventHandler rc4 = (s, e) => 
            {
                TextBox tb = (TextBox)s;
                Regex reg = new Regex(@"^(\+[0-9]{2})?[0-9]{10}$");
                Regex cont = new Regex(@"[0-9]|\+");
                if (cont.Matches(tb.Text).Count != tb.Text.Length && tb.Text.Length != 0)
                {
                    MessageBox.Show("Неправильно введений символ.\nСлово має містити лише літери та пробіли");
                    tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                }
                if(!reg.IsMatch(tb.Text))
                {
                    inputs[tb] = false;
                    tb.Foreground = Brushes.Red;
                }
                else
                {
                    inputs[tb] = true;
                    tb.Foreground = Brushes.Green;
                }
            };
            TextChangedEventHandler rc5 = (s, e) => 
            {
                TextBox tb = (TextBox)s;
                Regex reg = new Regex(@"^[0-9]+$");
                Regex cont = new Regex(@"[0-9]");
                if (cont.Matches(tb.Text).Count != tb.Text.Length && tb.Text.Length != 0)
                {
                    MessageBox.Show("Неправильно введений символ.");
                    tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                }
                if (tb.Text.Length > 3)
                {
                    inputs[tb] = false;
                    tb.Foreground = Brushes.Red;
                }
                else
                {
                    inputs[tb] = true;
                    tb.Foreground = Brushes.Green;
                }
            };
            #endregion

            #region Input segment
            //Big center label
            mainGrid.AddLabelToGrid(size: new Size(w: 120, h: 60), margin: new Margin(b: 250), text: "Ввід", font_size: 20);
            //Labels
            mainGrid.AddLabelToGrid(size: new Size(w: 120, h: 60), margin: new Margin(b: 170, l: -600), text: "Номер залікової книжки");
            mainGrid.AddLabelToGrid(size: new Size(w: 120, h: 60), margin: new Margin(b: 170, l: -300), text: "ПІП"                   );
            mainGrid.AddLabelToGrid(size: new Size(w: 120, h: 60), margin: new Margin(b: 170, l:  0  ), text: "email"                 );
            mainGrid.AddLabelToGrid(size: new Size(w: 120, h: 60), margin: new Margin(b: 170, l:  300), text: "номер телефону"        );
            mainGrid.AddLabelToGrid(size: new Size(w: 120, h: 60), margin: new Margin(b: 170, l:  600), text: "вік"                   );
            //TextBoxes
            inputs.Add(mainGrid.AddTextBoxToGrid(size: new Size(w: 120, h: 40), margin: new Margin(b: 120, l: -600), func: rc1), false);
            inputs.Add(mainGrid.AddTextBoxToGrid(size: new Size(w: 120, h: 40), margin: new Margin(b: 120, l: -300), func: rc2), false);
            inputs.Add(mainGrid.AddTextBoxToGrid(size: new Size(w: 120, h: 40), margin: new Margin(b: 120, l:  0  ), func: rc3), false);
            inputs.Add(mainGrid.AddTextBoxToGrid(size: new Size(w: 120, h: 40), margin: new Margin(b: 120, l:  300), func: rc4), false);
            inputs.Add(mainGrid.AddTextBoxToGrid(size: new Size(w: 120, h: 40), margin: new Margin(b: 120, l:  600), func: rc5), false);
            //Submit button
            add = mainGrid.AddButtonToGrid(size: new Size(w: 120, h: 40), margin: new Margin(b: 20), text: "Додати");
            add.Click +=
                (sender, e) =>
                {
                    bool good = true;
                    foreach (var item in inputs)
                    {
                        if (!item.Value)
                        {
                            good = false;
                        }
                    }
                    if (!good)
                    {
                        MessageBox.Show("Щось не правильно введно");
                        return;
                    }


                    File.AppendAllText(path, string.Join(" ", inputs.Select(t => t.Key.Text)) + "\n");
                    foreach (var tb in inputs)
                    {
                        tb.Key.Text = "";
                    }
                    MessageBox.Show("Додано");
                };
            #endregion

            #region Delete segment
            //label
            mainGrid.AddLabelToGrid(size: new Size(w: 120, h: 60), margin: new Margin(b: -100, l: 0), text: "Видалення", font_size: 20);
            //Eplanetion label
            mainGrid.AddLabelToGrid(size: new Size(w: 130, h: 60), margin: new Margin(b: -175, l: -250), text: "Введіть номер\nзал. книжки", font_size: 17);
            //textbox
            TextBox delete_input = mainGrid.AddTextBoxToGrid(size: new Size(w: 120, h: 40), margin: new Margin(b: -175, l: 0), func: rc1);
            //button
            mainGrid.AddButtonToGrid(size: new Size(w: 120, h: 40), margin: new Margin(b: -175, l: 250), text: "Видалити").Click +=
                (sender, e) =>
                {
                    if (delete_input_correct)
                    {
                        File.WriteAllLines(path, File.ReadAllLines(path).Where(t => t != "" && t.Split(' ')[0] != delete_input.Text));
                        delete_input.Text = "";
                        MessageBox.Show("Видалено");
                    }
                    else
                    {
                        MessageBox.Show("Не правильно введена інформація\nдля видалення");
                        delete_input.Text = "";
                    }

                };
            #endregion

            #region Go to main window
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(b: 20, l: 20), text: "<=", font_size: 20, horizontalAlignment: HorizontalAlignment.Left, verticalAlignment: VerticalAlignment.Bottom).Click += (s, e) => 
            { 
                Hide();
                new MainWindow().Show();
            };
            #endregion
        }
    }

    public class Page_2 : Window
    {
        public Page_2()
        {
            Grid mainGrid = new Grid();

            #region Preinitialization
            this.Content = mainGrid;
            #endregion

            #region Window settings
            this.SetWindowSettings(new Size(w: 300, h: 450));
            #endregion

            #region Label
            mainGrid.AddLabelToGrid(size: new Size(w: 200, h: 60), margin: new Margin(b: 300), text: "Гра хрестики-нолики", font_size: 18);
            #endregion

            #region Buttons
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    buttons[i, j] = mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t: 80 + 50 * i, l: 20 + 50 * j), font_size: 20, horizontalAlignment: HorizontalAlignment.Left, verticalAlignment: VerticalAlignment.Top);
                    buttons[i, j].Name = $"_{i}_{j}";
                    buttons[i, j].Click += (s, e) =>
                    {
                        ButtonPress(int.Parse(((Button)s).Name.Split('_', StringSplitOptions.RemoveEmptyEntries)[0]),
                                    int.Parse(((Button)s).Name.Split('_', StringSplitOptions.RemoveEmptyEntries)[1]));
                    };
                }
            }
            #endregion

            #region Go to main window
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(b: 20, l: 20), text: "<=", font_size: 20, horizontalAlignment: HorizontalAlignment.Left, verticalAlignment: VerticalAlignment.Bottom).Click += (s, e) =>
            {
                Hide();
                new MainWindow().Show();
            };
            #endregion
        }

        Objs[,] field = new Objs[5, 5];
        Button[,] buttons = new Button[5, 5];
        int[,] values = new int[5, 5];

        public void ButtonPress(int i, int j)
        {
            field[i, j] = Objs.X;
            buttons[i, j].IsEnabled = false;
            Display();
            if (WinCheck())
            {
                return;
            }
            CountValues();
            int b = values.Cast<int>().ToList().IndexOf(values.Cast<int>().Max());
            field[b / 5, b % 5] = Objs.O;
            buttons[b / 5, b % 5].IsEnabled = false;
            Display();
            if (WinCheck())
            {
                return;
            }
        }

        public void CountValues()
        {
            values = new int[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (field[i, j] == Objs.Null)
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
                                if (field[x, y] == Objs.X)
                                {
                                    valid = false;
                                    nrow++;
                                }
                                if (field[x, y] == Objs.O)
                                {
                                    row++;
                                }
                                x += mas[counter].Item1;
                                y += mas[counter].Item2;
                            }

                            if (row == 3)
                            {
                                values[i, j] += 40;
                            }
                            if (row == 2)
                            {
                                values[i, j] += 6;
                            }
                            if (row == 1)
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

        private bool WinCheck()
        {
            if (!field.Cast<Objs>().Contains(Objs.Null))
            {
                MessageBox.Show($"Draw");
                new Page_2().Show();
                Close();
                return true;
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (field[i, j] == Objs.Null)
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
                            if (x < 0 || 4 < x || y < 0 || 4 < y || field[i, j] != field[x, y])
                            {
                                valid = false;
                                break;
                            }
                            x += mas[counter].Item1;
                            y += mas[counter].Item2;
                        }

                        if (valid)
                        {
                            MessageBox.Show($"{field[i,j]} Won");
                            new Page_2().Show();
                            Close();
                            return true;
                        }

                        counter++;
                    }
                }
            }
            return false;
        }

        public void Display()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    buttons[i, j].Content = field[i, j] == Objs.Null ? "" : field[i, j].ToString();
                }
            }
        }

        public enum Objs
        {
            Null,
            X,
            O,
        }
    }

    public class Page_3 : Window
    {
        Label inputField;
        public Page_3()
        {
            Grid mainGrid = new Grid();

            #region Preinitialization
            this.Content = mainGrid;
            #endregion

            #region Window settings
            this.SetWindowSettings(new Size(w: 400, h: 450));
            #endregion

            #region Label
            inputField = mainGrid.AddLabelToGrid(size: new Size(w: 280, h: 60), margin: new Margin(b: 300), text: "", font_size: 18);
            inputField.Background = Brushes.Gray;
            #endregion

            #region Buttons
            //numbers
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t: -100, l: -200), text: "7", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t: -100, l: -100), text: "8", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t: -100, l:  0  ), text: "9", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  0  , l: -200), text: "4", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  0  , l: -100), text: "5", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  0  , l:  0  ), text: "6", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  100, l: -200), text: "1", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  100, l: -100), text: "2", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  100, l:  0  ), text: "3", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  200, l: -100), text: "0", font_size: 20).Click += Add;
            //actions
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t: -100, l: 100), text: "/", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  0  , l: 100), text: "*", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  100, l: 100), text: "+", font_size: 20).Click += Add;
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  200, l: 100), text: "-", font_size: 20).Click += Add;
            //coma
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t:  200, l: -0), text: ",", font_size: 20).Click += Add;
            #endregion

            #region Func buttons
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t: -100, l: 200), text: "="    , font_size: 20).Click += (s, e) =>
            {
                string exp = inputField.Content.ToString();

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
                    exp = exp.Replace(mat, "+" + (double.Parse(mat.Split('*')[0]) * double.Parse(mat.Split('*')[1])).ToString());
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
                    inputField.Content = res.ToString();
                }
                else
                {
                    MessageBox.Show("Wrong input");
                }
            };
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t: 0, l: 200), text: "Clear", font_size: 20).Click += (s, e) =>
            {
                inputField.Content = "";
            };
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(t: 100, l: 200), text: "<-", font_size: 20).Click += (s, e) =>
            { 
                inputField.Content = inputField.Content.ToString().Substring(0, inputField.Content.ToString().Length - 1);
            };
            #endregion

            #region Go to main window
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(b: 20, l: 20), text: "<=", font_size: 20, horizontalAlignment: HorizontalAlignment.Left, verticalAlignment: VerticalAlignment.Bottom).Click += (s, e) =>
            {
                Hide();
                new MainWindow().Show();
            };
            #endregion
        }

        public void Add(object sender, RoutedEventArgs e)
        {
            inputField.Content += ((Button)sender).Content.ToString();
        }
    }

    public class Page_4 : Window
    {
        public Page_4()
        {
            Grid mainGrid = new Grid();

            #region Preinitialization
            this.Content = mainGrid;
            #endregion

            #region Window settings
            this.SetWindowSettings(new Size(w: 600, h: 400));
            #endregion

            #region Label
            mainGrid.AddLabelToGrid(size: new Size(w: 280, h: 100), margin: new Margin(), text: "Мазний Степан Валерійович\nКП-11\n2022", font_size: 18);
            #endregion

            #region Go to main window
            mainGrid.AddButtonToGrid(size: new Size(w: 40, h: 40), margin: new Margin(b: 20, l: 20), text: "<=", font_size: 20, horizontalAlignment: HorizontalAlignment.Left, verticalAlignment: VerticalAlignment.Bottom).Click += (s, e) =>
            {
                Hide();
                new MainWindow().Show();
            };
            #endregion
        }
    }

    #region Additionals
    public class Size
    {
        public int w, h;

        public Size(int w, int h)
        {
            this.w = w;
            this.h = h;
        }

        public void Apply(FrameworkElement element)
        {
            element.Height = h;
            element.Width = w;
        }
    }

    public class Margin
    {
        public int t, r, l, b;

        public Margin(int t = 0, int l = 0, int r = 0, int b = 0)
        {
            this.t = t;
            this.r = r;
            this.b = b;
            this.l = l;
        }

        public void Apply(FrameworkElement element)
        {
            element.Margin = new Thickness(l, t, r, b);
        }
    }
    #endregion

    public static class MyWpfExtentions
    {
        public static void SetWindowSettings(this Window window, 
            Size size, 
            WindowStartupLocation location = WindowStartupLocation.CenterScreen, 
            ResizeMode mode = ResizeMode.NoResize)
        {
            window.ResizeMode = ResizeMode.NoResize;
            window.Width = size.w;
            window.Height = size.h;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public static Label AddLabelToGrid(this Grid grid,
            Size size, Margin margin, string text,
            int font_size = 14,
            HorizontalAlignment textAlignment = HorizontalAlignment.Center,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center)
        {
            Label lb = new Label();
            grid.Children.Add(lb);

            lb.Content = text;

            size.Apply(lb);
            margin.Apply(lb);

            lb.FontSize = font_size;
            lb.HorizontalContentAlignment = textAlignment;

            lb.HorizontalAlignment = horizontalAlignment;
            lb.VerticalAlignment = verticalAlignment;

            return lb;
        }

        public static Button AddButtonToGrid(this Grid grid,
            Size size, Margin margin, string text = "",
            int font_size = 14,
            HorizontalAlignment textAlignment = HorizontalAlignment.Center,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center)
        {
            Button btn = new Button();
            grid.Children.Add(btn);

            btn.Content = text;

            size.Apply(btn);
            margin.Apply(btn);

            btn.FontSize = font_size;
            btn.HorizontalContentAlignment = textAlignment;

            btn.HorizontalAlignment = horizontalAlignment;
            btn.VerticalAlignment = verticalAlignment;

            return btn;
        }

        public static TextBox AddTextBoxToGrid(this Grid grid,
            Size size, Margin margin, TextChangedEventHandler func = null, int fontsize = 20,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center)
        {
            TextBox tb = new TextBox();
            grid.Children.Add(tb);

            tb.FontSize = fontsize;

            size.Apply(tb);
            margin.Apply(tb);

            if (func != null) 
               tb.TextChanged += func;

            tb.HorizontalAlignment = horizontalAlignment;
            tb.VerticalAlignment = verticalAlignment;

            return tb;
        }
    }
}
