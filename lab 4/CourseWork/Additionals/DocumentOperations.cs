using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace CourseWork.Additionals
{
    static class DocumentOperations
    {
        public static bool CreateExcelDocument<T>(DataTable table, string[] column_names, T startValue = default, Func<T, T> rowNaming = null)
        {
            try
            {
                var excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.Workbooks.Add();
                var worksheet = excelApp.ActiveSheet as Excel.Worksheet;

                int start = rowNaming == null ? 1 : 2;
                for (int i = 0; i < column_names.Length; i++)
                {
                    worksheet.Cells[1, start + i] = column_names[i];
                }
                int row = 2;
                if (table != null)
                    foreach (var item in table.AsEnumerable())
                    {
                        if (rowNaming != null)
                        {
                            worksheet.Cells[row, 1] = startValue.ToString();
                            startValue = rowNaming.Invoke(startValue);
                        }
                             
                        for (int i = 0; i < item.ItemArray.Length; i++)
                        {
                            worksheet.Cells[row, start + i] = item.ItemArray[i];
                        }
                        row++;
                    }
                excelApp.Visible = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ReplaceInDocument(string path, Dictionary<string, string> replacements)
        {
            var wordAap = new Word.Application();
            var doc = wordAap.Documents.Open(path);

            var range = doc.Range();

            foreach (var item in replacements)
            {
                range.Find.Execute(FindText: item.Key, Replace: Word.WdReplace.wdReplaceAll, ReplaceWith: item.Value);
            }
            wordAap.Visible = true;

            return true;
        }
    }
}
