using System;
using System.Collections.Generic;
using System.Linq;
using TODOApp.Data;
using TODOApp.Entities;
using TODOApp.Enums;

namespace TODOApp.Services
{
    /// <summary>
    /// Responsible for managing user related functionality 
    /// and tracking currently logged in user
    /// </summary>
    public class UserService
    {
        private const string StoreFileName = "Users.json";

        private readonly FileStorage _fileStorage;

        private readonly List<User> _applicationUsers = new List<User>();

        public UserService()
        {
            _fileStorage = new FileStorage();
            List<User> usersFromFile = _fileStorage.Read<List<User>>(StoreFileName);
            if (usersFromFile == null)
            {
                CreateFirstAdmin();
            }
            else
            {
                _applicationUsers = usersFromFile;
            }
        }

        /// <summary>
        /// Currently logged in user
        /// </summary>
        public User CurrentUser { get; private set; }

        /// <summary>
        /// Logs the user in the system by storing the data in the CurrentUser variable
        /// </summary>
        /// <param name="userName">The name of the user to be logged in</param>
        /// <param name="password">The password of the user to be logged in</param>
        public void LogIn(string userName, string password)
        {
            CurrentUser = _applicationUsers.FirstOrDefault(u => u.Username == userName && u.Password == password);
        }

        /// <summary>
        /// Logs the user out of the system by removing the value of the CurrentUser variable
        /// </summary>
        public void LogOut()
        {
            CurrentUser = null;
        }

        public void CreateFirstAdmin()
        {
            // Only Chuck Norris can start with capital letter
            User ChuckNorrisTheTexasAdministrator = new User()
            {
                Id = 1,
                Username = "admin",
                Password = "adminpassword",
                FirstName = "Chuck",
                LastName = "Norris",
                Role = UserRole.Admin,
                DateCreated = DateTime.Now,
                LastModifiedAt = DateTime.Now,
                IdOfCreator = 1, // Only Chuck Norris can create himself
                LastModifiedBy = 1
            };

            _applicationUsers.Add(ChuckNorrisTheTexasAdministrator);

            SaveToFile();
        }

        public bool CreateUser(string username, string password, string firstName, string lastName, UserRole role)
        {
            if (_applicationUsers.Any(u => u.Username == username))
            {
                return false;
            }

            int nextId = _applicationUsers.Last().Id + 1;

            User newUser = new User()
            {
                Id = nextId,
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Role = role,
                DateCreated = DateTime.Now,
                LastModifiedAt = DateTime.Now,
                LastModifiedBy = CurrentUser.Id,
                IdOfCreator = CurrentUser.Id
            };

            _applicationUsers.Add(newUser);

            SaveToFile();

            return true;
        }

        public bool UserWithIdExists(int id)
        {
            if (!_applicationUsers.Any(u => u.Id == id))
            {
                Console.WriteLine($"User with Id {id} does not exist.");
                return false;
            }
            return true;
        }

        public void DisplayUserData(int id)
        {
            User user = _applicationUsers.FirstOrDefault(u => u.Id == id);
            Console.WriteLine($"Id : {user.Id}");
            Console.WriteLine($"Username : {user.Username}");
            Console.WriteLine($"FirstName : {user.FirstName}");
            Console.WriteLine($"LastName : {user.LastName}");
            Console.WriteLine($"Password : {user.Password}");
        }

        public bool EditUser(string username, string password, string firstName, string lastName, int id)
        {
            if (_applicationUsers.Any(u => u.Username == username && u.Id != id))
            {
                Console.WriteLine($"User with username {username} already exist.");
                return false;
            }

            User user = _applicationUsers.FirstOrDefault(u => u.Id == id);
            user.Username = username;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Password = password;
            user.LastModifiedAt = DateTime.Now;
            user.LastModifiedBy = CurrentUser.Id;

            SaveToFile();

            return true;
        }

        public List<int> GetAllUserdsId()
        {
            return _applicationUsers.Select(u=>u.Id).ToList();
        }

        public void DeleteUser(int id)
        {
            _applicationUsers.RemoveAll(u => u.Id == id);

            SaveToFile();
        }

        private void SaveToFile()
        {
            _fileStorage.Write(StoreFileName, _applicationUsers);
        }
    }
}
