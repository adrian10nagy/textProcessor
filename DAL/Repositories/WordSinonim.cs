using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IWordSinonimRepository
    {
        void AddSinonim(int wordId, int wordSinonimId, DateTime datetime, string missingWordsText);
        List<WordSinonim> GetSinonimsByWordId(int wordId);
    }

    public partial class Repository : IWordSinonimRepository
    {
        public void AddSinonim(int wordId, int wordSinonimId, DateTime datetime, string missingWordsText)
        {
            _dbRead.Execute(
                "WordSinonimInsert",
            new[]
            {
                new SqlParameter("@wordId", wordId), 
                new SqlParameter("@wordSinonimId", wordSinonimId), 
                new SqlParameter("@created", datetime),
                new SqlParameter("@missingWordsText", missingWordsText)
            }, null);

        }
        

        public List<WordSinonim> GetSinonimsByWordId(int wordId)
        {
            var wordSinonims = new List<WordSinonim>();
            _dbRead.Execute(
                "WordSinonimGetAllById",
            new[]
            {
                new SqlParameter("@wordId", wordId), 
            },
                r => wordSinonims.Add(new WordSinonim()
                {
                    Id = Read<int>(r, "Id"),
                    WordMainId = Read<int>(r, "WordId"),
                    WordSinonimId = Read<int>(r, "WordSinonimId"),
                    Created = Read<DateTime>(r, "Created"),
                    Approved = Read<int>(r, "Approved"),
                }));

            return wordSinonims;
        }
    
    }
}
