using Enums;
using System;
using System.Collections.Generic;
using ToDoApp.BLL.Services;
using ToDoApp.Core.Providers.Contracts;
using ToDoApp.DAL.Entities;

namespace ToDoApp.Controllers
{
    public class UserController
    {
        private readonly UserService _userService;
        public readonly IWriter _writer;
        public readonly IReader _reader;

        public UserController(UserService userService, IWriter writer, IReader reader)
        {
            _userService = userService;
            _writer = writer;
            _reader = reader;
        }

        /// <summary>
        /// Currently logged in user
        /// </summary>
        public User CurrentUser { get { return _userService.CurrentUser; } }

        public void LogIn()
        {
            _writer.WriteLine("Enter your username:");
            string username = _reader.ReadLine();
            _writer.WriteLine("Enter your password:");
            string password = _reader.ReadLine();
            _userService.Login(username, password);
            if (_userService.CurrentUser == null)
            {
                _writer.WriteLine("Login failed.");
            }
            else
            {
                _writer.WriteLine("Login successful.");
            }
        }

        public void LogOut()
        {
            _userService.LogOut();
        }

        public void ListUsers()
        {
            List<User> users = _userService.GetAllUsers();

            _writer.WriteLine("=======================================================");
            foreach (User user in users)
            {
                _writer.WriteLine($"Id : {user.Id}");
                _writer.WriteLine($"Role : {user.Role}");
                _writer.WriteLine($"Username : {user.Username}");
                _writer.WriteLine($"FirstName : {user.FirstName}");
                _writer.WriteLine($"LastName : {user.LastName}");
                _writer.WriteLine($"Password : {user.Password}");
                _writer.WriteLine("=======================================================");
            }
            _writer.WriteLine("");
        }

        public  void CreateUser()
        {
            _writer.WriteLine("Enter User Name:");
            string username = _reader.ReadLine();

            _writer.WriteLine("Enter User Password:");
            string password = _reader.ReadLine();

            _writer.WriteLine("Enter First Name:");
            string firstName = _reader.ReadLine();

            _writer.WriteLine("Enter Last Name:");
            string lastName = _reader.ReadLine();

            _writer.WriteLine("Enter User Role( Admin, RegularUser ):");
            string userRole = _reader.ReadLine();
            UserRole role;

            bool created = false;

            if (Enum.TryParse(userRole, out role))
            {
                created = _userService.CreateUser(username,password,firstName,lastName,role);
            }
            else
            {
                _writer.WriteLine("Ivalid Role!");
            }

            if (created)
            {
                _writer.WriteLine($"User with name '{username}' added");
            }
            else
            {
                _writer.WriteLine($"User with name '{username}' already exists");
            }
            _writer.WriteLine("");
        }

        public void EditUser()
        {
            _writer.WriteLine("Enter Id of User for Edit");
            string input = _reader.ReadLine();
            int id;
            if (int.TryParse(input, out id) && _userService.UserWithIdExists(id))
            {
                User editUserData = _userService.GetUserById(id);
                _writer.WriteLine($"Id : {editUserData.Id}");
                _writer.WriteLine($"Username : {editUserData.Username}");
                _writer.WriteLine($"FirstName : {editUserData.FirstName}");
                _writer.WriteLine($"LastName : {editUserData.LastName}");
                _writer.WriteLine($"Password : {editUserData.Password}");

                _writer.WriteLine("Enter User Name:");
                string username = _reader.ReadLine();

                _writer.WriteLine("Enter User Password:");
                string password = _reader.ReadLine();

                _writer.WriteLine("Enter First Name:");
                string firstName = _reader.ReadLine();

                _writer.WriteLine("Enter Last Name:");
                string lastName = _reader.ReadLine();

                bool edited = _userService.EditUser(username, password, firstName, lastName, id);
                if (edited)
                {
                    _writer.WriteLine($"User with id '{id}' edited");
                }
                else
                {
                    _writer.WriteLine("Edit FAIL");
                }
            }
            else
            {
                _writer.WriteLine("Invalid id");
            }
            _writer.WriteLine("");
        }

        public void DeleteUser()
        {
            _writer.WriteLine("Enter Id of User for Delete");
            string input = _reader.ReadLine();
            int id;
            if (int.TryParse(input, out id) && _userService.UserWithIdExists(id))
            {
                _userService.DeleteUser(id);
                _writer.WriteLine("User Deleted");
            }
            else
            {
                _writer.WriteLine("Invalid id");
            }
            _writer.WriteLine("");
        }
    }
}
