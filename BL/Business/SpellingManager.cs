using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Business
{
    public static class SpellingManager
    {
        public static string OpenAndReadPDF(Object o)
        {
            PdfReader x = new PdfReader(o.ToString());

            return string.Empty;
        }
    }
}
