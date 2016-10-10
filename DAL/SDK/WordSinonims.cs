using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class WordSinonims
    {
        private static IWordSinonimRepository _repository;

        static WordSinonims()
        {
            _repository = new Repository();
        }

        public void AddSinonim(int wordId, int wordSinonimId, DateTime datetime)
        {
            _repository.AddSinonim(wordId, wordSinonimId, datetime, string.Empty);
        }

        public void AddSinonimMissingWord(int wordId, DateTime datetime, string missingWordText)
        {
            _repository.AddSinonim(wordId, 0, datetime, missingWordText);
        }

        public List<WordSinonim> GetSinonimsByWordId(int wordId)
        {
            return _repository.GetSinonimsByWordId(wordId);

        }

    }
}
