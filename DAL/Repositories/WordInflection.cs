using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IWordInflectionRepository
    {
        List<WordInflection> GetAllInflectedWordsByName(string searchText);
        List<WordInflection> GetAllInflectedWorsdByLexemId(int id);
        List<WordInflection> GetAllInflectedWordsByLastChars(string lastChars);
        List<string> GetAllInflectedWords();
    }

    public partial class Repository : IWordInflectionRepository
    {
        public List<WordInflection> GetAllInflectedWordsByName(string searchText)
        {
            List<WordInflection> words = new List<WordInflection>();

            _dbRead.Execute(
                "WordsInflextionGetByName",
            new[]{
                new SqlParameter("@searchText",searchText)
            },
                r => words.Add(new WordInflection()
                {
                    Id = Read<int>(r, "id"),
                    FormNoAcc = Read<string>(r, "FormNoAcc"),
                    TypeId = Read<int>(r, "TypeId"),
                    Description = Read<string>(r, "Description"),
                    ModelType = Read<string>(r, "ModelType"),
                    LexemId = Read<int>(r, "LexemId")
                }));

            return words;
        }

        public List<WordInflection> GetAllInflectedWorsdByLexemId(int id)
        {
            List<WordInflection> words = new List<WordInflection>();

            _dbRead.Execute(
                "WordsInflextionGetByLexemId",
            new[]{
                new SqlParameter("@lexemId", id)
            },
                r => words.Add(new WordInflection()
                {
                    Id = Read<int>(r, "id"),
                    FormNoAcc = Read<string>(r, "FormNoAcc"),
                    TypeId = Read<int>(r, "TypeId"),
                    Description = Read<string>(r, "Description"),
                    ModelType = Read<string>(r, "ModelType"),
                    LexemId = id

                }));

            return words;
        }

        public List<WordInflection> GetAllInflectedWordsByLastChars(string lastChars)
        {
            List<WordInflection> words = new List<WordInflection>();

            _dbRead.Execute(
                "WordsInflextionGetByLastChars",
            new[]{
                new SqlParameter("@lastChars", lastChars)
            },
                r => words.Add(new WordInflection()
                {
                    FormNoAcc = Read<string>(r, "FormNoAcc"),
                }));

            return words;
        }

        public List<string> GetAllInflectedWords()
        {
            List<string> words = new List<string>();

            _dbRead.Execute(
                "WordsInflextionGetAll",
            null,
                r => words.Add( 
                    Read<string>(r, "FormNoAcc")
                ));

            return words;
        }
    }
}
