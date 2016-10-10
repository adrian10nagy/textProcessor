using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class WordInflections
    {
        private static IWordInflectionRepository _repository;

        static WordInflections()
        {
            _repository = new Repository();
        }

        public List<WordInflection> GetAllInflectedWorsdByLexemId(int id)
        {
            return _repository.GetAllInflectedWorsdByLexemId(id);
        }

        public List<WordInflection> GetAllInflectedWordsByName(string searchText)
        {
            if (searchText == string.Empty)
                return new List<WordInflection>();

            var wordsMain = _repository.GetAllInflectedWordsByName(searchText);

            return wordsMain;
        }

        public List<WordInflection> GetAllInflectedWordsByLastChars(string lastChars)
        {
            return _repository.GetAllInflectedWordsByLastChars(lastChars);
        }

        public List<string> GetAllInflectedWords()
        {
            return _repository.GetAllInflectedWords();
        }
    }
}
