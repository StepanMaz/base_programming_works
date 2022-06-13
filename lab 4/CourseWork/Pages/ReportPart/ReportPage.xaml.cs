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
using System.Data;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

using static CourseWork.DBController;

namespace CourseWork.Pages.ReportPart
{
    public partial class ReportPage : Page
    {
        #region Queries
        const string AllToursQuery = "SELECT * FROM dbo.AllToursOfLastMonth",
                     PopularToursQuery = "SELECT * FROM dbo.GetToursByPopularity";
        #endregion

        public ReportPage()
        {
            InitializeComponent();
        }

        private void ToursClick(object sender, RoutedEventArgs e)
        {
            DataTable source = GetTable(AllToursQuery);

            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();
            var worksheet = excelApp.ActiveSheet as Excel.Worksheet;

            int row = 1;
            Append(row++, new string[] {"Назва", "Ціна", "Початок", "Кінець"});

            decimal sum = 0;
            foreach (var item in source.AsEnumerable())
            {
                var array = item.ItemArray;
                array[1] = string.Format("{0:C2}", array[1]);
                array[2] = ((DateTime)array[2]).ToString("dd.MM.yyyy");
                array[3] = ((DateTime)array[3]).ToString("dd.MM.yyyy");
                Append(row++, array);
                sum += (decimal)item["FullPrice"];
            }
            worksheet.Cells[row, 1] = "Total:";
            worksheet.Cells[row, 2] = sum;
            excelApp.Visible = true;

            void Append(int row, object[] values)
            {
                for (int i = 1; i <= values.Length; i++)
                {
                    worksheet.Cells[row, i] = values[i - 1];
                }
            }
        }

        private void PopularToursClick(object sender, RoutedEventArgs e)
        {
            DataTable source = GetTable(PopularToursQuery);

            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();
            var worksheet = excelApp.ActiveSheet as Excel.Worksheet;

            int row = 1;
            Append(row++, new string[] { "Назва", "Початок", "Кінець", "Кількість"});

            foreach (var item in source.AsEnumerable())
            {
                var array = item.ItemArray;
                array[1] = ((DateTime)array[1]).ToString("dd.MM.yyyy");
                array[2] = ((DateTime)array[2]).ToString("dd.MM.yyyy");
                Append(row++, array);
            }
            excelApp.Visible = true;

            void Append(int row, object[] values)
            {
                for (int i = 1; i <= values.Length; i++)
                {
                    worksheet.Cells[row, i] = values[i - 1];
                }
            }
        }
    }
}
