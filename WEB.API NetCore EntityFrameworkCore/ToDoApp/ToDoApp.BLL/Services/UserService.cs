using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;

namespace ToDoApp.BLL.Services
{
    public class UserService
    {
        private readonly DatabaseContext _database;

        /// <summary>
        /// Initializes new instance of the UserService and creates a single default user 
        /// </summary>
        public UserService(DatabaseContext database)
        {
            _database = database;
        }

        public async Task<List<User>> GetAll()
        {
            return await _database.User.ToListAsync();
        }

        public List<User> GetAllUsers()
        {
            return _database.User.ToList();
        }

        public User GetUserByUsername(string username)
        {
            return _database.User.FirstOrDefault(u => u.Username == username);
        }
        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return _database.User.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
