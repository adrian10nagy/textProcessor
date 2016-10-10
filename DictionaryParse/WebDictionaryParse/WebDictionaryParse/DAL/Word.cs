//using DAL.Settings;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebDictionaryParse.Entities;

//namespace WebDictionaryParse.DAL
//{
//    public static class Word
//    {
//        private static  SqlDatabase<SqlConnection, SqlCommand, SqlParameter> _dbRead = new SqlDatabase<SqlConnection, SqlCommand, SqlParameter>();


//        public static T Read<T>(IDataRecord record, string key)
//        {
//            return Read(record, key, default(T));
//        }

//        public static T Read<T>(IDataRecord record, string key, T defaultValue)
//        {
//            return record[key] != DBNull.Value ? (T)record[key] : defaultValue;
//        }

//        public static List<WordMain> GetAllMainWords()
//        {
//            List<WordMain> words = new List<WordMain>();

//            _dbRead.Execute(
//                "WordsMainGetAll",
//            null,
//                r => words.Add(new WordMain()
//                {
//                    id = Read<int>(r, "id"),
//                    FormNoAcc = Read<string>(r, "FormNoAcc"),
//                    Frequency = Read<double>(r, "Frequency"),
//                    ModelNr = Read<string>(r, "ModelNr"),
//                    ModelType = Read<string>(r, "ModelType"),
//                    Pronunciation = Read<string>(r, "Pronunciation"),
//                    lexemId = Read<int>(r, "lexemId")

//                }));

//            return words;
//        }

//    }
//}
