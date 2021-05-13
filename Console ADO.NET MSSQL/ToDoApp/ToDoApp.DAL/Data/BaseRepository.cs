using System.Data;
using System.Data.SqlClient;

namespace ToDoApp.DAL.Data
{
    public class BaseRepository
    {
        protected readonly string _sqlConnectionString;

        public BaseRepository(string connectionString)
        {
            _sqlConnectionString = connectionString;
        }

        protected SqlCommand CreateCommand(SqlConnection connection, string sql)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            return command;
        }

        protected void AddParameter(SqlCommand command, string parametername, SqlDbType parameterType, object parameterValue)
        {
            command.Parameters.Add(parametername, parameterType).Value = parameterValue;
        }
    }
}
