using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace BL.Helpers
{
    public static class FileProcess
    {
        public static bool IsFileSupported(string contentType)
        {
            var isSupported = false;

            switch (contentType)
            {
                case ("text/plain"):
                    isSupported = true;
                    break;

                case ("application/vnd.openxmlformats-officedocument.wordprocessingml.document"):
                    isSupported = true;
                    break;

                case ("image/png"):
                    isSupported = true;
                    break;


                case ("audio/mp3"):
                    isSupported = true;
                    break;

                case ("audio/wav"):
                    isSupported = true;
                    break;
            }

            return isSupported;
        }

        public static List<String> GetWordsFromFile(HttpPostedFileWrapper postedFile)
        {
            var textFromFile = string.Empty;

            if (IsFileSupported(postedFile.ContentType))
            {
                if (postedFile.ContentType == "text/plain")
                {
                    textFromFile = GetWordsForTxtFromStream(postedFile.InputStream);
                }
                else if (postedFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                {
                    textFromFile = GetWordsForDocxFromStream(postedFile.InputStream);
                }
                else if (postedFile.ContentType == "pdf")
                {

                }
                else
                {
                    return null;
                }
            }

             var words = WordProcess.GetUnrecognizedWordsFromText(textFromFile).Result;

            return words;
        }

        public static string GetWordsForTxtFromStream(Stream stream)
        {
            StreamReader streamReader = new StreamReader(stream);
            string streamReadToEnd = streamReader.ReadToEnd();
            string streamText = HttpUtility.UrlDecode(streamReadToEnd);

            return streamText;
        }

        public static string GetWordsForDocxFromStream(Stream stream)
        {
            var fileText = string.Empty;
            using (WordprocessingDocument document = WordprocessingDocument.Open(stream, false))
            {
                fileText = document.MainDocumentPart.Document.InnerText;
                var dfileText = document.MainDocumentPart.Document.Body;
                //var dfile2Text = document.MainDocumentPart.Document.b;
                var qa = dfileText.InnerText;
                var qa2 = dfileText.Descendants();
                var qa3 = dfileText.All(o => o.InnerText != "");
                var qa4 = dfileText.Elements();
                var qa5 = dfileText.NextSibling();
                StringBuilder sb = new StringBuilder();
                foreach (var item in qa2)
                {
                    var x = item.InnerText;
                    sb.Append(" " + x + " ");
                }
                var xxxxxxdsaf = sb.ToString();

                StringBuilder sb5 = new StringBuilder();
                var body3 = document.MainDocumentPart.Document.Body;
                body3.ClearAllAttributes();
                foreach (var text in body3.Descendants<Paragraph>())
                {
                    text.ClearAllAttributes();
                    sb5.Append(text.InnerText + " ");
                }
                fileText = sb5.ToString();
            }

            return fileText;
        }

        public static void SaveFile(string fileName, string textToWrite)
        {
            var filePath =  HttpContext.Current.Server.MapPath("~/App_Data/" + fileName) ;
            System.IO.File.WriteAllText(filePath, textToWrite);
        }
    }
}
