using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace UrlShortener.Helper
{
    public static class SqlHelper
    {
        public static void ExecuteQuery(string query)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<string> GetColumnValues(string query, string columnName)
        {
            var columnData = new List<string>();
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columnData.Add(reader[columnName].ToString());
                        }
                    }
                }
            }
            return columnData;
        }

        private static string GetConnectionString() => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}