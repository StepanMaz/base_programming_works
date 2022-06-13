using System;
using System.Collections.Generic;

using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace CourseWork.Additionals
{
    static class DocumentOperations
    {
        public static bool ReplaceInDocument(string path, Dictionary<string, string> replacements)
        {
            path = AppDomain.CurrentDomain.BaseDirectory + path;
            var wordAap = new Word.Application();
            var doc = wordAap.Documents.Add(path);

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
