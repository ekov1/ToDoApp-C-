using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Data
{
    public class UserRepository
    {
        private readonly ToDoContext _context;

        public UserRepository(ToDoContext context)
        {
            this._context = context;
        }

        public User GetUserByName(string username)
        {
            try
            {
                    User user = _context.User.Where(u => u.Username == username)
                        .Select(u => new User()
                        {
                            Id = u.Id,
                            Username = u.Username,
                            Password = u.Password,
                            RoleId = u.Role.Id,
                            Role = u.Role
                        })
                        .FirstOrDefault();

                    return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public User GetUserById(int userId)
        {
            try
            {
                    User user = _context.User.Where(u => u.Id == userId).FirstOrDefault();
                    return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
