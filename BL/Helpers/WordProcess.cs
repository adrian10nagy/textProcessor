using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public static class WordProcess
    {
        public static async Task<List<string>> GetUnrecognizedWordsFromText(string textString)
        {
            List<string> wordList = new List<string>();
            List<string> allWordsInText = new List<string>();

            textString = RemoveTabNewLineNewRowFromString(textString);
            textString = RemoveSpecialCharacters(textString).Result;
            textString = ConvertUnrecognizedChars(textString);
            allWordsInText = textString.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            allWordsInText = allWordsInText.ConvertAll(d => d.ToLower()).ConvertAll(c => c.Trim()).Distinct().ToList();
            allWordsInText = RemoveUnnecesaryHyphenFromWords(allWordsInText);
            var wordsAlreadyChecked = BL.Business.KitBL.Instance.WordBL.AlreadyCheckedWords;
            //allWordsInTExt = allWordsInTExt.Except(wordsAlreadyChecked).ToList();
            if (allWordsInText.Count > 500)
            {
                for (int i = 0; i < 4; i++)
                {
                    var toSkip = i * allWordsInText.Count / 4;
                    var toTake = allWordsInText.Count / 4;
                    if (i == 3)
                    {
                        toTake = toTake * 2;
                    }
                    var items = allWordsInText.Skip(toSkip).Take(toTake).ToList();
                    List<string> wordsUnrecognized = await GetUnrecognizedWords(items);
                    wordList.AddRange(wordsUnrecognized);
                }
            }
            else
            {
                List<string> wordsUnrecognized = await GetUnrecognizedWords(allWordsInText);
                wordList.AddRange(wordsUnrecognized);
            }

            return wordList;
        }

        private static string RemoveTabNewLineNewRowFromString(string textString)
        {
            textString = textString.Replace("\n", String.Empty);
            textString = textString.Replace("\r", String.Empty);
            textString = textString.Replace("\t", String.Empty);

            return textString;
        }

        public static async Task<string> RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsLetter(c) || c == '-' || c == ' ')
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        public static async Task<List<string>> GetUnrecognizedWords(List<string> words)
        {
            List<string> wordsDB = new List<string>();
            List<string> listUnknownWords = new List<string>();

            //await Task.Run(() =>
            //{
                words = words.Distinct().ToList();
                words = words.OrderByDescending(o => o).ToList();
                wordsDB = BL.Business.KitBL.Instance.WordBL.AllWords;
                listUnknownWords = words.Except(wordsDB).Distinct().ToList();
            //});
            //BL.Business.KitBL.Instance.WordBL.AddAlreadyCheckedWords(wordsDB);

            return listUnknownWords;
        }

        private static string ConvertUnrecognizedChars(string text)
        {
            var stringWithRemoveUnrecognizedChars = text.ToLower();
            stringWithRemoveUnrecognizedChars = stringWithRemoveUnrecognizedChars.Replace("ş", "ș");
            stringWithRemoveUnrecognizedChars = stringWithRemoveUnrecognizedChars.Replace('ţ', 'ț');

            return stringWithRemoveUnrecognizedChars;
        }

        private static List<String> RemoveUnnecesaryHyphenFromWords(List<string> allWordsInText)
        {
            allWordsInText.Remove("-");

            for (int i = 0; i < allWordsInText.Count(); i++)
            {
                if (allWordsInText[i][0].Equals('-'))
                {
                    var x1 = allWordsInText[i];
                    var x = x1.Substring(1, allWordsInText[i].Length - 1);
                    allWordsInText[i] = x;
                }

                if (allWordsInText[i][allWordsInText[i].Length - 1].Equals('-'))
                {
                    allWordsInText[i] = allWordsInText[i].Substring(0, allWordsInText[i].Length - 2);
                }
            }

            return allWordsInText;
        }

        #region toReuse

        private static string TextStringProcessed;

        public static string GetStringDifferences(string oldString, string newString)
        {
            List<string> diff;

            if (string.IsNullOrEmpty(oldString) || string.IsNullOrEmpty(newString))
            {
                var res = string.Empty;

                if (string.IsNullOrEmpty(oldString) && string.IsNullOrEmpty(newString))
                {
                    res = string.Empty;
                }
                else if (string.IsNullOrEmpty(newString))
                {
                    res = oldString;
                }
                else
                {
                    res = newString;
                }

                return res;
            }
            else
            {
                List<string> setOld = oldString.Split(new Char[] { '.', '!', '?', '\n', '\r', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
                List<string> setNew = newString.Split(new Char[] { '.', '!', '?', '\n', '\r', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();

                diff = setNew.Except(setOld).ToList();
            }

            return diff.ToString();
        }

        public static async Task<List<string>> UnrecognizedWords(List<string> words)
        {
            List<string> wordsDB = BL.Business.KitBL.Instance.WordBL.AllWords;
            List<string> wordsAlreadyChecked = BL.Business.KitBL.Instance.WordBL.AlreadyCheckedWords;
            List<string> unrecognizedwords = new List<string>();
            List<string> recognizedwords = new List<string>();
            List<string> listUnknownWords = words.Except(wordsAlreadyChecked).ToList();

            foreach (var item in listUnknownWords)
            {
                if (!wordsDB.Contains(item.ToLower()))
                {
                    unrecognizedwords.Add(item);
                }
                else
                {
                    recognizedwords.Add(item);
                }
            }
            BL.Business.KitBL.Instance.WordBL.AlreadyCheckedWords = recognizedwords;

            return unrecognizedwords;
        }

        private static string RemoveNumbers(string textString)
        {
            textString = new String(textString.Where(c => !char.IsDigit(c)).ToArray());

            return textString;
        }

        private static string KeepOnlyLettersAndSpaces(string textString)
        {
            textString = new String(textString.Where(c => char.IsLetter(c) || c == '-' || c == ' ').ToArray());

            return textString;
        }

        public static async Task<List<string>> GetWords(string textString)
        {
            var analyzedWords = new List<string>();

            List<string> listSentences = textString.Split(new Char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).ToList();


            for (int i = 0; i < listSentences.Count; i++)
            {
                string stringWithoudSpecalChars = await RemoveSpecialCharacters(listSentences[i]);
                List<string> listAllWords = stringWithoudSpecalChars.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                listAllWords = listAllWords.ConvertAll(d => d.ToLower());
                List<string> wordsUnrecognized = await UnrecognizedWords(listAllWords);

                analyzedWords.AddRange(wordsUnrecognized);
            }

            return analyzedWords;
        }

        //var verifiedWords = allWordsInText.Except(wordList).ToList();
        //BL.Business.KitBL.Instance.WordBL.AlreadyCheckedWords = verifiedWords;
        //var test = string.Join(" ",
        //       allWordsInText.Select(w => wordsAlreadyChecked.Contains(w) ? "" : w));

        //string textStringAdded = GetStringDifferences(TextStringProcessed, textString);
        //TextStringProcessed = textString;
        //textStringAdded = RemoveTabNewLineNewRowFromString(textStringAdded);
        //textStringAdded = KeepOnlyLettersAndSpaces(textStringAdded);
        //List<string> listSentences = textStringAdded.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();


        #endregion
    }
}
