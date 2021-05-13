using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Data
{
    public class ToDoListRepository : BaseRepository
    {
        public ToDoListRepository(string connectionString)
          : base(connectionString) { }

        public bool CreateToDoList(int idOfCreator, string title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();
                    int entityId;
                    object result;
                    using (SqlCommand command = CreateCommand(connection,
                        "INSERT INTO[todo_db].[dbo].[Entity] ([DateCreated], [IdOfCreator], [LastModifiedAt], [LastModifiedBy])" +
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
                                         "INSERT INTO[todo_db].[dbo].[ToDoList] ([EntityId], [Title]) " +
                                         "output INSERTED.ID " +
                                         "VALUES (@EntityId, @Title)"))
                    {
                        AddParameter(command, "@EntityId", SqlDbType.Int, entityId);
                        AddParameter(command, "@Title", SqlDbType.NVarChar, title);

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

        public List<ToDoList> GetAllToDoListIdsByUser(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection,
                        "SELECT [todo_db].[dbo].[ToDoList].[Id], [Title]" +
                        " FROM [todo_db].[dbo].[ToDoList]" +
                        " INNER JOIN [todo_db].[dbo].[Entity] ON [todo_db].[dbo].[ToDoList].[EntityId] = [todo_db].[dbo].[Entity].[Id]" +
                         " WHERE [todo_db].[dbo].[Entity].[IdOfCreator] = @Id"))
                    {
                        AddParameter(command, "@Id", SqlDbType.Int, userId);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            List<ToDoList> toDoLists = new List<ToDoList>();
                            while (reader.Read())
                            {
                                ToDoList toDoList = new ToDoList();
                                toDoList.Id = reader.GetInt32(0);
                                toDoList.Title = reader.GetString(1);

                                toDoLists.Add(toDoList);
                            }
                            return toDoLists;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);

            }
            return new List<ToDoList>();
        }

        public bool ToDoListWithIdExists(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();
                    object result;
                    using (SqlCommand command = CreateCommand(connection,
                        "SELECT [Id]" +
                        " FROM [todo_db].[dbo].[ToDoList]" +
                        " WHERE [Id] = @Id"))
                    {
                        AddParameter(command, "@Id", SqlDbType.Int, id);

                        result = command.ExecuteScalar();
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

        public bool CanEditList(int listId, int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();
                    object result;
                    using (SqlCommand command = CreateCommand(connection,
                        "SELECT [IdOfCreator]" +
                        " FROM [todo_db].[dbo].[ToDoList]" +
                        " INNER JOIN [todo_db].[dbo].[Entity] ON [todo_db].[dbo].[ToDoList].[EntityId] = [todo_db].[dbo].[Entity].[Id]" +
                         " WHERE [todo_db].[dbo].[Entity].[IdOfCreator] = @UserId " +
                         " AND [todo_db].[dbo].[ToDoList].[Id] = @ListId"))
                    {
                        AddParameter(command, "@UserId", SqlDbType.Int, userId);
                        AddParameter(command, "@ListId", SqlDbType.Int, listId);

                        result = command.ExecuteScalar();
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

        public ToDoList GetToDoListById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = CreateCommand(connection,
                         "SELECT [todo_db].[dbo].[ToDoList].[Id], [Title], [IdOfCreator], [DateCreated], [LastModifiedBy], [LastModifiedAt]" +
                        " FROM [todo_db].[dbo].[ToDoList]" +
                        " INNER JOIN [todo_db].[dbo].[Entity] ON [todo_db].[dbo].[ToDoList].[EntityId] =[todo_db].[dbo].[Entity].[Id]" +
                         " WHERE [todo_db].[dbo].[ToDoList].[Id] = @ListId"
                        ))
                    {
                        AddParameter(command, "@ListId", SqlDbType.Int, id);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                ToDoList tdl = new ToDoList();
                                tdl.Id = reader.GetInt32(0);
                                tdl.Title = reader.GetString(1);
                                tdl.IdOfCreator = reader.GetInt32(2);
                                tdl.DateCreated = reader.GetDateTime(3);
                                tdl.LastModifiedBy = reader.GetInt32(4);
                                tdl.LastModifiedAt = reader.GetDateTime(5);
                                return tdl;
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

        public bool EditToDoList(int listId, string title, int modifiedById)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();
                    object result;
                    int entityId;

                    using (SqlCommand command = CreateCommand(connection,
                     "UPDATE  [todo_db].[dbo].[ToDoList]" +
                     " SET [Title] = @Title" +
                     " output INSERTED.EntityId" +
                     " WHERE [Id] = @Id"))
                    {
                        AddParameter(command, "@Title", SqlDbType.NVarChar, title);
                        AddParameter(command, "@Id", SqlDbType.Int, listId);

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
                        AddParameter(command, "@LastModifiedBy", SqlDbType.Int, modifiedById);
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

        public bool DeleteToDoList(int listid, int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = CreateCommand(connection,
                       "Declare @EntityId int" +
                       " SET  @EntityId = (SELECT [EntityId] FROM [todo_db].[dbo].[ToDoList] WHERE [Id] = @Id)" +
                       " DELETE FROM [todo_db].[dbo].[ToDoList] WHERE [Id] = @Id" +
                       " DELETE FROM [todo_db].[dbo].[Entity] WHERE [Id] = @EntityId"))
                    {
                        AddParameter(command, "@Id", SqlDbType.Int, listid);

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

        public void ShareToDoList(int listId, int userId)
        {

        }
    }


}
