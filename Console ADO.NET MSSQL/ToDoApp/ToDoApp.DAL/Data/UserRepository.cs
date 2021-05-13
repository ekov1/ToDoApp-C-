using Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Data
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(string connectionString)
            : base(connectionString) { }

        public User GetUserByName(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection,
                        "SELECT [Id], [Username],[Password]" +
                        " FROM [todo_db].[dbo].[User]" +
                        " WHERE [Username] = @Username"))
                    {
                        AddParameter(command, "@Username", SqlDbType.NVarChar, username);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                User user = new User();
                                user.Id = reader.GetInt32(0);
                                user.Username = reader.GetString(1);
                                user.Password = reader.GetString(2);
                                return user;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public User GetUserById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection,
                        "SELECT [Id], [Username], [Password], [FirstName], [LastName], [UserRoleId]" +
                        " FROM [todo_db].[dbo].[User]" +
                        " WHERE [Id] = @Id"))
                    {
                        AddParameter(command, "@Id", SqlDbType.Int, id);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                User user = new User();
                                user.Id = reader.GetInt32(0);
                                user.Username = reader.GetString(1);
                                user.Password = reader.GetString(2);
                                user.FirstName = reader.GetString(3);
                                user.LastName = reader.GetString(4);

                                UserRole role;
                                int UserRoleId = reader.GetInt32(5);
                                if (Enum.TryParse<UserRole>(UserRoleId.ToString(), out role))
                                {
                                    user.Role = role;
                                }

                                return user;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public bool UserWithIdExists(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection,
                        "SELECT [Id] FROM [todo_db].[dbo].[User] WHERE [Id] = @Id"))
                    {
                        AddParameter(command, "@Id", SqlDbType.Int, id);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public List<User> GetAllUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection,
                        "SELECT [Id], [Username], [UserRoleId], [FirstName], [LastName], [Password] " +
                        "FROM [todo_db].[dbo].[User]"))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {

                            if (reader.HasRows)
                            {
                                List<User> users = new List<User>();

                                while (reader.Read())
                                {
                                    User user = new User();
                                    user.Id = reader.GetInt32(0);
                                    user.Username = reader.GetString(1);
                                    UserRole role;
                                    int UserRoleId = reader.GetInt32(2);
                                    if (Enum.TryParse<UserRole>(UserRoleId.ToString(), out role))
                                    {
                                        user.Role = role;
                                    }
                                    user.FirstName = reader.GetString(3);
                                    user.LastName = reader.GetString(4);
                                    user.Password = reader.GetString(5);

                                    users.Add(user);
                                }

                                return users;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public bool CreateUser(int idOfCreator, string username, string password, string firstName, string lastName, int userRoleId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();
                    int entityId;
                    object result;
                    using (SqlCommand command = CreateCommand(connection,
                        "INSERT INTO[todo_db].[dbo].[Entity]([DateCreated], [IdOfCreator], [LastModifiedAt], [LastModifiedBy])" +
                        " output INSERTED.ID" +
                        " VALUES (@DateCreated, @IdOfCreator, @LastModifiedAt, @LastModifiedBy)"))
                    {
                        AddParameter(command, "@DateCreated", SqlDbType.DateTime, DateTime.Now);
                        AddParameter(command, "@IdOfCreator", SqlDbType.Int, idOfCreator);
                        AddParameter(command, "@LastModifiedAt", SqlDbType.DateTime, DateTime.Now);
                        AddParameter(command, "@LastModifiedBy", SqlDbType.Int, idOfCreator);

                        result = command.ExecuteScalar();
                        entityId = (int)result;

                    }

                    using (SqlCommand command = CreateCommand(connection,
                                         "INSERT INTO[todo_db].[dbo].[User] ([EntityId], [Username], [Password], [FirstName], [LastName], [UserRoleId]) " +
                                         "output INSERTED.ID " +
                                         "VALUES (@EntityId, @Username, @Password, @FirstName, @LastName, @UserRoleId)"))
                    {
                        AddParameter(command, "@EntityId", SqlDbType.Int, entityId);
                        AddParameter(command, "@Username", SqlDbType.NVarChar, username);
                        AddParameter(command, "@Password", SqlDbType.NVarChar, password);
                        AddParameter(command, "@FirstName", SqlDbType.NVarChar, firstName);
                        AddParameter(command, "@LastName", SqlDbType.NVarChar, lastName);
                        AddParameter(command, "@UserRoleId", SqlDbType.Int, userRoleId);

                        result = command.ExecuteScalar();
                    }

                    return result != null;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool EditUser(int idOfEditor, User editUserData)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();
                    object result;
                    int entityId;

                    using (SqlCommand command = CreateCommand(connection,
                     "UPDATE [todo_db].[dbo].[User] " +
                     "SET [Username] = @Username, [Password] = @Password, [FirstName] = @FirstName, [LastName] = @LastName " +
                     "output INSERTED.EntityId" +
                     " WHERE [Id] = @Id"))
                    {
                        AddParameter(command, "@Username", SqlDbType.NVarChar, editUserData.Username);
                        AddParameter(command, "@Password", SqlDbType.NVarChar, editUserData.Password);
                        AddParameter(command, "@FirstName", SqlDbType.NVarChar, editUserData.FirstName);
                        AddParameter(command, "@LastName", SqlDbType.NVarChar, editUserData.LastName);
                        AddParameter(command, "@Id", SqlDbType.Int, editUserData.Id);

                        result = command.ExecuteScalar();
                        entityId = (int)result;
                    }

                    using (SqlCommand command = CreateCommand(connection,
                        "UPDATE [todo_db].[dbo].[Entity] " +
                        "SET [LastModifiedAt] = @LastModifiedAt, [LastModifiedBy] = @LastModifiedBy" +
                        " output INSERTED.ID" +
                        " WHERE [Id] = @EntityId"))
                    {
                        AddParameter(command, "@LastModifiedAt", SqlDbType.DateTime, DateTime.Now);
                        AddParameter(command, "@LastModifiedBy", SqlDbType.Int, idOfEditor);

                        AddParameter(command, "@EntityId", SqlDbType.Int, entityId);


                        result = command.ExecuteScalar();
                        entityId = (int)result;
                    }

                    return result != null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = CreateCommand(connection,
                       "Declare @EntityId int" +
                       " SET  @EntityId = (SELECT [EntityId] FROM [todo_db].[dbo].[User] WHERE [Id] = @Id)" +
                       " DELETE FROM [todo_db].[dbo].[User] WHERE [Id] = @Id" +
                       " DELETE FROM [todo_db].[dbo].[Entity] WHERE [Id] = @EntityId"))
                    {
                        AddParameter(command, "@Id", SqlDbType.Int, userId);

                        command.ExecuteScalar();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
