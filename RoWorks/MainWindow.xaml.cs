using DAL.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using DAL.Helpers.Extensions;
using DAL.Helpers.WordHelpers;

namespace RoWorks
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

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            var analyzedWords = new List<DAL.Entities.AnalizedWord>();
            var highlighted = new Dictionary<int, int>();
            txtSecondary.Text = string.Empty;

            TextRange textRange = new TextRange(mainSearchInput.Document.ContentStart, mainSearchInput.Document.ContentEnd);
            var txtRichText = textRange.Text;
            var listSentences = txtRichText.Split(new Char[] { '.', '!', '?', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var sentence in listSentences)
            {
                var listWords = sentence.Split(new Char[] { ' ', ',', '"' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                analyzedWords.AddRange(Existence.GetConvertedWordsByText(listWords));
            }

            var unidentifiedWords = analyzedWords.Where(word => word.Exists == false).ToList();

            foreach (var word in unidentifiedWords)
            {
                var indexes = txtRichText.AllIndexesOf(word.Name);
                foreach (var index in indexes)
                {
                    if (!highlighted.Contains(index))
                    {
                        highlighted.Add(index.Key, index.Value);
                    }
                }
            }

            var txtRichSelection = mainSearchInput.Selection;
            txtRichSelection.ClearAllProperties();

            txtSecondary.Text = string.Join("\n", highlighted);
           
        }

        

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextRange textRange = new TextRange(mainSearchInput.Document.ContentStart, mainSearchInput.Document.ContentEnd);
            //var theText = textRange.Text;

            //foreach (var highlight in highlighted)
            //{

            //    var start = mainSearchInput.Document.ContentStart;
            //    var startPos = start.GetPositionAtOffset(5);
            //    var endPos = start.GetPositionAtOffset(8);
            //    txtRichSelection.Select(startPos, endPos);
            //    txtRichSelection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));

            //}
        }


        //public static string HighlightKeyWords(this string text, string keywords, string cssClass, bool fullMatch)
        //{
        //    if (text == String.Empty || keywords == String.Empty || cssClass == String.Empty)
        //        return text;
        //    var words = keywords.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    if (!fullMatch)
        //        return words.Select(word => word.Trim()).Aggregate(text,
        //                     (current, pattern) =>
        //                     Regex.Replace(current,
        //                                     pattern,
        //                                       string.Format("<span style=\"background-color:{0}\">{1}</span>",
        //                                       cssClass,
        //                                       "$0"),
        //                                       RegexOptions.IgnoreCase));
        //    return words.Select(word => "\\b" + word.Trim() + "\\b")
        //                .Aggregate(text, (current, pattern) =>
        //                          Regex.Replace(current,
        //                          pattern,
        //                            string.Format("<span style=\"background-color:{0}\">{1}</span>",
        //                            cssClass,
        //                            "$0"),
        //                            RegexOptions.IgnoreCase));

        //}
    }

}
