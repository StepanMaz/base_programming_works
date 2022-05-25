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
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

using static CourseWork.DBController;

namespace CourseWork.Pages.ReportPart
{
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        private void Foo(object sender, RoutedEventArgs e)
        {
            var r = Additionals.DocumentOperations.ReplaceInDocument(AppDomain.CurrentDomain.BaseDirectory + @"\example.docx", new Dictionary<string, string>() { {"{Title}", "Титул" }, { "{Info1}", "f1" } , { "{Info2}", "f2" }, { "{Info3}", "f3" } });
            if (!r)
                MessageBox.Show("Hah");
        }
    }
}
