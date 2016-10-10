using DAL.Entities;
using DAL.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers.WordHelpers
{
    public static class Existence
    {
        private static List<string> _allWords;
        private static List<WordMain> _allMainWords;

        public static List<string> AllWords { get { return _allWords ?? getWords(); } }
        public static List<WordMain> AllMainWords { get { return _allMainWords ?? getMainWords(); } }

        static List<string> getWords()
        {
            _allWords = SDK.Kit.Instance.Words.GetAll().Select(field => field.Name).ToList();

            return _allWords;
        }

        static List<WordMain> getMainWords()
        {
            _allMainWords = SDK.Kit.Instance.Words.GetAllMainWords();

            return _allMainWords;
        }

        public static bool existsInDB(string word)
        {
            var exists = AllWords.Contains(word);

            return exists;
        }

        public static List<AnalizedWord> GetConvertedWordsByText(List<string> words)
        {
            var analizedWords = new List<AnalizedWord>();

            foreach (var word in words)
            {
                bool exists = existsInDB(word);
                analizedWords.Add(new AnalizedWord()
                {
                    Name = word,
                    Exists = exists
                });
            }

            return analizedWords;
        }

    }
}
