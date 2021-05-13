using Enums;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;

namespace ToDoApp.BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Currently logged in user
        /// </summary>
        public User CurrentUser { get; private set; }

        /// <summary>
        /// Initializes new instance of the UserService and creates a single default user 
        /// </summary>
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Logs the user in the system by storing the data in the CurrentUser variable
        /// </summary>
        /// <param name="username">The name of the user to be logged in</param>
        public void Login(string username, string password)
        {
            User user = _userRepository.GetUserByName(username);
            if (user != null && user.Password == password)
            {
                CurrentUser = _userRepository.GetUserById(user.Id);
            }
        }

        /// <summary>
        /// Logs the user out of the system by removing the value of the CurrentUser variable
        /// </summary>
        public void LogOut()
        {
            CurrentUser = null;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public bool CreateUser(string username, string password, string firstName, string lastName, UserRole userRole)
        {
            int userRoleId = (int)userRole;
            return _userRepository.CreateUser(CurrentUser.Id, username, password, firstName, lastName, userRoleId);
        }

        public bool UserWithIdExists(int userId)
        {
            return _userRepository.UserWithIdExists(userId);
        }

        public bool EditUser(string username, string password, string firstName, string lastName, int id)
        {
            User editUserData = new User()
            {
                Id = id,
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            };

            return _userRepository.EditUser(CurrentUser.Id, editUserData);
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

        public void ShareToDoList(int listId, int userId)
        {

        }
    }
}
