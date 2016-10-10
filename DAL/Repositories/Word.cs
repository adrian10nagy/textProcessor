
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IWordRepository
    {
        List<Word> GetAllWords();
        List<WordMain> GetAllMainWords();
        List<WordMain> GetAllWordsByName(string searchText);
        List<WordMain> GetAllWordsByNameRelative(string searchText);
        WordMain GetWordById(int id);
        int Add(Word word);
        int AddNoun(Noun word);
        int AddVerb(Verb word);
        int AddAdverb(AdVerb word);
        int AddAdjective(Adjective word);
        WordMain GetRandomMainWord(double frequency, int wordLength);
        WordMain GetWordOfTheDay(DateTime date);
    }

    public partial class Repository : IWordRepository
    {
        public List<Word> GetAllWords()
        {
            List<Word> words = new List<Word>();
            _dbRead.Execute(
                "WordsGetAll",
            null,
                r => words.Add(new Word()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name"),

                }));

            return words;
        }

        public List<WordMain> GetAllMainWords()
        {
            List<WordMain> words = new List<WordMain>();

            _dbRead.Execute(
                "WordsMainGetAll",
            null,
                r => words.Add(new WordMain()
                {
                    id = Read<int>(r, "id"),
                    FormNoAcc = Read<string>(r, "FormNoAcc"),
                    Frequency = Read<double>(r, "Frequency"),
                    ModelNr = Read<string>(r, "ModelNr"),
                    ModelType = Read<string>(r, "ModelType"),
                    Pronunciation = Read<string>(r, "Pronunciation"),
                    lexemId = Read<int>(r, "lexemId")

                }));

            return words;
        }

        public int Add(Word word)
        {
            var idWord = 0;

            _dbRead.Execute(
                "WordInsert",
            new[] { 
                new SqlParameter("@name", word.Name), 
                new SqlParameter("@id_Origin", (int)word.Origin), 
                new SqlParameter("@stateRef", word.StateRef),
                new SqlParameter("@created", word.Created) 
            },
                r => idWord = Read<int>(r, "id")
            );

            return idWord;
        }

        public int AddWord(Word word)
        {
            var idWord = 0;

            _dbRead.Execute(
                "aaaa",
            new[] { 
                new SqlParameter("@aaaa", word.Name), 
            },
                r => idWord = Read<int>(r, "id")
            );

            return idWord;
        }

        public int AddNoun(Noun wordNoun)
        {
            var idWord = 0;

            _dbRead.Execute(
                "NounInsert",
            new[] { 
                new SqlParameter("@Id_Word", wordNoun.WordId), 
                new SqlParameter("@Id_Proper", (int)wordNoun.Proper), 
                new SqlParameter("@Id_Multiplicity", (int)wordNoun.Multiplicity), 
                new SqlParameter("@Id_Case", (int)wordNoun.Case), 
                new SqlParameter("@Id_Gender", (int)wordNoun.Gender), 
            },
                r => idWord = Read<int>(r, "id")
            );

            return idWord;
        }

        public int AddVerb(Verb word)
        {
            var idWord = 0;

            _dbRead.Execute(
                "aaaa",
            new[] { 
                new SqlParameter("@aaaa", word.Word.Id), 
            },
                r => idWord = Read<int>(r, "id")
            );

            return idWord;
        }

        public int AddAdverb(AdVerb word)
        {
            var idWord = 0;

            _dbRead.Execute(
                "aaaa",
            new[] { 
                new SqlParameter("@aaaa", word.Word.Id), 
            },
                r => idWord = Read<int>(r, "id")
            );

            return idWord;
        }

        public int AddAdjective(Adjective word)
        {
            var idWord = 0;

            _dbRead.Execute(
                "aaaa",
            new[] { 
                new SqlParameter("@aaaa", word.Word.Id), 
            },
                r => idWord = Read<int>(r, "id")
            );

            return idWord;
        }

        public List<WordMain> GetAllWordsByName(string searchText)
        {
            List<WordMain> words = new List<WordMain>();

            _dbRead.Execute(
                "WordsMainGetByName",
            new[]{
                new SqlParameter("@searchText",searchText)
            },
                r => words.Add(new WordMain()
                {
                    id = Read<int>(r, "id"),
                    FormNoAcc = Read<string>(r, "FormNoAcc"),
                    Frequency = Read<double>(r, "Frequency"),
                    ModelNr = Read<string>(r, "ModelNr"),
                    ModelType = Read<string>(r, "ModelType"),
                    Pronunciation = Read<string>(r, "Pronunciation"),
                    lexemId = Read<int>(r, "lexemId")

                }));

            return words;
        }


        public List<WordMain> GetAllWordsByNameRelative(string searchText)
        {
            List<WordMain> words = new List<WordMain>();

            _dbRead.Execute(
                "WordsMainGetByNameRelative",
            new[]{
                new SqlParameter("@searchText",searchText)
            },
                r => words.Add(new WordMain()
                {
                    id = Read<int>(r, "id"),
                    FormNoAcc = Read<string>(r, "FormNoAcc"),
                    Frequency = Read<double>(r, "Frequency"),
                    ModelNr = Read<string>(r, "ModelNr"),
                    ModelType = Read<string>(r, "ModelType"),
                    Pronunciation = Read<string>(r, "Pronunciation"),
                    lexemId = Read<int>(r, "lexemId")

                }));

            return words;
        }


        public WordMain GetWordById(int id)
        {
            WordMain word = null;

            _dbRead.Execute(
                "WordsMainGetById",
            new[]{
                new SqlParameter("@lexemId",id)
            },
                r => word = new WordMain()
                {
                    id = Read<int>(r, "id"),
                    FormNoAcc = Read<string>(r, "FormNoAcc"),
                    Frequency = Read<double>(r, "Frequency"),
                    ModelNr = Read<string>(r, "ModelNr"),
                    ModelType = Read<string>(r, "ModelType"),
                    Pronunciation = Read<string>(r, "Pronunciation"),
                    lexemId = Read<int>(r, "lexemId"),
                    description = Read<string>(r, "ModelDescription")
                });

            return word;
        }



        public WordMain GetRandomMainWord(double frequency, int wordLength)
        {
            WordMain word = null;

            _dbRead.Execute(
                "WordMainGetRandomLexem",
            new[]
            {
                new SqlParameter("@frequency", (float)frequency),
                new SqlParameter("@wordLength", wordLength)
                
            },
                r => word = new WordMain()
                {
                    id = Read<int>(r, "id"),
                    FormNoAcc = Read<string>(r, "FormNoAcc"),
                    Frequency = Read<double>(r, "Frequency"),
                    ModelNr = Read<string>(r, "ModelNr"),
                    ModelType = Read<string>(r, "ModelType"),
                    Pronunciation = Read<string>(r, "Pronunciation"),
                    lexemId = Read<int>(r, "lexemId")

                });

            return word;
        }

        public WordMain GetWordOfTheDay(DateTime date)
        {
            WordMain word = null;

            _dbRead.Execute(
                "WordOfTheDayGet",
            new[]
            {
                new SqlParameter("@created",date)
            },
                r => word = new WordMain()
                {
                    FormNoAcc = Read<string>(r, "FormNoAcc"),
                    lexemId = Read<int>(r, "lexemId")
                });

            return word;
        }
    }
}
