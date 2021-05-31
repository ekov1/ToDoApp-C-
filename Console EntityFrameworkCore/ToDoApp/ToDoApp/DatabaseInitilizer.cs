using System;
using System.Data.SqlClient;

namespace ToDoApp
{
    class DatabaseInitilizer
    {
        public static bool CheckDatabaseExists(string connectionstring, string databaseName)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string sqlCreateDBQuery;

                try
                {
                    sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name  = '{0}'", databaseName);

                    using (connection)
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, connection))
                        {
                            connection.Open();

                            object resultObj = sqlCmd.ExecuteScalar();

                            int databaseID = 0;

                            if (resultObj != null)
                            {
                                int.TryParse(resultObj.ToString(), out databaseID);
                            }

                            connection.Close();

                            result = (databaseID > 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    result = false;
                }
            }

            return result;
        }
    }
}
