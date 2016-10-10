using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDictionaryParse.Extensions
{
    public static class RawSQL
    {
        public static void executeQuery(string queryString)
        {
            string connectionString = @"data source=HOME\SQLEXPRESS; Database=KnowledgeSReplication; Integrated Security = SSPI;";
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(
                       connectionString))
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(
                    queryString, connection);
                connection.Open();
                System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}",
                            reader[0], reader[1]));
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
        }
    }
}
