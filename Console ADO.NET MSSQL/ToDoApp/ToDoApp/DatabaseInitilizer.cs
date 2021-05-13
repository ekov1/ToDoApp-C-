using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

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
                    // connection = new SqlConnection("server=(local)\\SQLEXPRESS;Trusted_Connection=yes");

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

        public static void InitilizeDatabase(string connectionstring)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string script = File.ReadAllText("Scripts\\createDB.sql");
                DbCommand command = connection.CreateCommand();
                command.CommandText = script;
                command.ExecuteNonQuery();
            }

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string script = File.ReadAllText("Scripts\\CreateDataBaseTables.sql");
                DbCommand command = connection.CreateCommand();
                command.CommandText = script;
                command.ExecuteNonQuery();
            }
        }

        public static void InitilizeDatabaseTables(string connectionstring)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string script = File.ReadAllText("Scripts\\InitializeTables.sql");
                DbCommand command = connection.CreateCommand();
                command.CommandText = script;
                command.ExecuteNonQuery();
            }
        }
    }
}
