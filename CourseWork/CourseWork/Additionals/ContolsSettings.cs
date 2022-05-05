using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Additionals
{
    public static class ContolsSettings
    {
        public static void ColumnsNaming(object sender, EventArgs e, params string[] names)
        {
            Structure(sender, names.Length, (clmn, i) => clmn.Header = names[i]);
        }

        public static void ColumnsVisibility(object sender, EventArgs e, params Visibility[] paramiters)
        {
            Structure(sender, paramiters.Length, (clmn, i) => clmn.Visibility = paramiters[i]);
        }

        private static void Structure(object dataGrid, int length, Action<DataGridColumn, int> action)
        {
            var columns = (dataGrid as DataGrid).Columns;
            if (length > columns.Count)
            {
                throw new Exception("Wrong input");
            }
            for (int i = 0; i < Math.Min(length, columns.Count); i++)
            {
                action.Invoke(columns[i], i);
            }
        }
    }
}
