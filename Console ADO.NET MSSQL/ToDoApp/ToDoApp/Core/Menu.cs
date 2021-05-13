using Enums;
using System;
using ToDoApp.Controllers;
using ToDoApp.Core.Providers.Contracts;

namespace ToDoApp.Core
{
    public class Menu
    {
        public readonly IReader _reader;
        public readonly IWriter _writer;

        private readonly UserController _userController;
        private readonly ToDoListController _toDoListController;
        public Menu(IWriter writer, IReader reader, UserController userController, ToDoListController toDoListController)
        {
            _userController = userController;
            _toDoListController = toDoListController;
            _reader = reader;
            _writer = writer;
        }

        public void RenderMenu()
        {
            _writer.WriteLine("--------Main Menu--------");
            if (_userController.CurrentUser == null)
            {
                _writer.WriteLine(" 1. LogIn ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                _writer.WriteLine($"You are logged in as: {_userController.CurrentUser.Username}");
                Console.ForegroundColor = ConsoleColor.White;
                _writer.WriteLine(" 1. LogOut");

                if (_userController.CurrentUser.Role == UserRole.Admin)
                {
                    _writer.WriteLine("= Users Management =");
                    _writer.WriteLine(" 2. List Users");
                    _writer.WriteLine(" 3. Create User");
                    _writer.WriteLine(" 4. Edit User");
                    _writer.WriteLine(" 5. Delete User");
                }

                _writer.WriteLine("= ToDoLists Management =");
                _writer.WriteLine(" 6. List ToDoLists");
                _writer.WriteLine(" 7. Create ToDoList");
                _writer.WriteLine(" 8. Edit ToDoList");
                _writer.WriteLine(" 9. Delete ToDoList");
                _writer.WriteLine("10. Share ToDoList");

                _writer.WriteLine("= Tasks Management =");
                _writer.WriteLine("11. Create Task");
                _writer.WriteLine("12. List Tasks By List id");
                _writer.WriteLine("13. Edit Task");
                _writer.WriteLine("14. Assign task to User");
                _writer.WriteLine("15. Delete Task");
                _writer.WriteLine("16. Complete Task");

                _writer.WriteLine("");
                _writer.WriteLine("To exit type exit");
            }
            _writer.WriteLine("");
        }

        public bool MainMenu()
        {
            RenderMenu();

            string input = _reader.ReadLine();

            // Admin commands
            if (_userController.CurrentUser != null && _userController.CurrentUser.Role == UserRole.Admin)
            {
                switch (input)
                {
                    case "2":
                        _userController.ListUsers();
                        return false;
                    case "3":
                        _userController.CreateUser();
                        return false;
                    case "4":
                        _userController.EditUser();
                        return false;
                    case "5":
                        _userController.DeleteUser();
                        return false;
                }
            }

            switch (input)
            {
                case "1":
                    if (_userController.CurrentUser == null)
                    {
                        _userController.LogIn();
                    }
                    else
                    {
                        _userController.LogOut();
                    }
                    return false;
                case "6":
                    _toDoListController.ListToDoLists();
                    return false;
                case "7":
                    _toDoListController.CreateToDoList();
                    return false;
                case "8":
                    _toDoListController.EditToDoList();
                    return false;
                case "9":
                   _toDoListController.DeleteToDoList();
                    return false;
                case "10":
                    //  ShareToDoList();
                    return false;
                case "11":
                    // CreateTask();
                    return false;
                case "12":
                    //ListTasksByListId();
                    return false;
                case "13":
                    // EditTask();
                    return false;
                case "14":
                    // AssignTaskToUser();
                    return false;
                case "15":
                    // DeleteTask();
                    return false;
                case "16":
                    //CompleteTask();
                    return false;

                case "exit":
                    return true;
                default:
                    _writer.WriteLine("Unknown Command");
                    return false;
            }
        }
    }
}
