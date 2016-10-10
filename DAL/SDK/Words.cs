using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class Words
    {
        private static IWordRepository _repository;

        static Words()
        {
            _repository = new Repository();
        }

        //public Words(IWordRepository repository)
        //{
        //    _repository = repository;
        //}

        public List<Word> GetAll()
        {
            return _repository.GetAllWords();
        }

        public int AddWord(Word word)
        {
            return _repository.Add(word);
        }

        public int AddNoun(Noun wordNoun)
        {
            return _repository.AddNoun(wordNoun);
        }

        public int AddVerb(Word word)
        {
            return _repository.Add(word);
        }

        public int AddAdverb(Word word)
        {
            return _repository.Add(word);
        }

        public int AddAdjective(Word word)
        {
            return _repository.Add(word);
        }

        public List<WordMain> GetAllMainWords()
        {
            return _repository.GetAllMainWords();
        }

        public List<WordMain> GetAllWordsByName(string searchText)
        {
            if (searchText == string.Empty)
                return new List<WordMain>();

            var wordsMain = _repository.GetAllWordsByName(searchText);

            return wordsMain;
        }

        public List<WordMain> GetAllWordsByNameRelative(string searchText)
        {
            if (searchText == string.Empty)
                return new List<WordMain>();

            var wordsMain = _repository.GetAllWordsByNameRelative(searchText);

            return wordsMain;
        }

        public WordMain GetWordById(int id)
        {
            return _repository.GetWordById(id);
        }

        public WordMain GetRandomMainWord(double frequency, int wordLength)
        {
            return _repository.GetRandomMainWord(frequency, wordLength);
        }

        public WordMain GetWordOfTheDay(DateTime date)
        {
            return _repository.GetWordOfTheDay(date);
        }
    }
}
