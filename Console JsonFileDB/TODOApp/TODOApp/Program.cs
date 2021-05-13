using System;
using System.Collections.Generic;
using System.Linq;
using TODOApp.Enums;
using TODOApp.Services;

namespace TODOApp
{
    class Program
    {
        private static UserService _userService = new UserService();
        private static ToDoListService _toDoListService = new ToDoListService();
        private static TaskService _taskService = new TaskService();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                exit = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            RenderMenu();

            string input = Console.ReadLine();

            // Admin commands
            if (_userService.CurrentUser != null && _userService.CurrentUser.Role == UserRole.Admin)
            {
                switch (input)
                {
                    case "2":
                        ListUsers();
                        return false;
                    case "3":
                        CreateUser();
                        return false;
                    case "4":
                        EditUser();
                        return false;
                    case "5":
                        DeleteUser();
                        return false;
                }
            }

            switch (input)
            {
                case "1":
                    if (_userService.CurrentUser == null)
                    {
                        LogIn();
                    }
                    else
                    {
                        LogOut();
                    }
                    return false;
                case "6":
                    ListToDoLists();
                    return false;
                case "7":
                    CreateToDoList();
                    return false;
                case "8":
                    EditToDoList();
                    return false;
                case "9":
                    DeleteToDoList();
                    return false;
                case "10":
                    ShareToDoList();
                    return false;
                case "11":
                    CreateTask();
                    return false;
                case "12":
                    ListTasksByListId();
                    return false;
                case "13":
                    EditTask();
                    return false;
                case "14":
                    AssignTaskToUser();
                    return false;
                case "15":
                    DeleteTask();
                    return false;
                case "16":
                    CompleteTask();
                    return false;

                case "exit":
                    return true;
                default:
                    Console.WriteLine("Unknown Command");
                    return false;
            }
        }

        private static void RenderMenu()
        {
            Console.WriteLine("--------Main Menu--------");
            if (_userService.CurrentUser == null)
            {
                Console.WriteLine(" 1. LogIn ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You are logged in as: {_userService.CurrentUser.Username}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" 1. LogOut");

                if (_userService.CurrentUser.Role == UserRole.Admin)
                {
                    Console.WriteLine("= Users Management =");
                    Console.WriteLine(" 2. List Users");
                    Console.WriteLine(" 3. Create User");
                    Console.WriteLine(" 4. Edit User");
                    Console.WriteLine(" 5. Delete User");
                }

                Console.WriteLine("= ToDoLists Management =");
                Console.WriteLine(" 6. List ToDoLists");
                Console.WriteLine(" 7. Create ToDoList");
                Console.WriteLine(" 8. Edit ToDoList");
                Console.WriteLine(" 9. Delete ToDoList");
                Console.WriteLine("10. Share ToDoList");

                Console.WriteLine("= Tasks Management =");
                Console.WriteLine("11. Create Task");
                Console.WriteLine("12. List Tasks By List id");
                Console.WriteLine("13. Edit Task");
                Console.WriteLine("14. Assign task to User");
                Console.WriteLine("15. Delete Task");
                Console.WriteLine("16. Complete Task");

                Console.WriteLine();
                Console.WriteLine("To exit type exit");
            }
            Console.WriteLine();
        }

        #region UserCommands

        private static void LogIn()
        {
            Console.WriteLine("Enter your user name:");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            _userService.LogIn(userName, password);
            if (_userService.CurrentUser == null)
            {
                Console.WriteLine("Login failed.");
            }
            else
            {
                Console.WriteLine("Login successful.");
            }
            Console.WriteLine();
        }

        private static void LogOut()
        {
            _userService.LogOut();
        }

        private static void CreateUser()
        {
            Console.WriteLine("Enter User Name:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter User Password:");
            string password = Console.ReadLine();

            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter User Role( Admin, RegularUser ):");
            string userRole = Console.ReadLine();
            UserRole role;

            bool created = false;

            if (Enum.TryParse(userRole, out role))
            {
                created = _userService.CreateUser(username, password, firstName, lastName, role);
            }
            else
            {
                Console.WriteLine("Ivalid Role!");
            }

            if (created)
            {
                Console.WriteLine($"User with name '{username}' added");
            }
            else
            {
                Console.WriteLine($"User with name '{username}' already exists");
            }
            Console.WriteLine();
        }

        private static void EditUser()
        {
            Console.WriteLine("Enter Id of User for Edit");
            string input = Console.ReadLine();
            int id;
            if (int.TryParse(input, out id) && _userService.UserWithIdExists(id))
            {
                _userService.DisplayUserData(id);
                Console.WriteLine("Enter User Name:");
                string username = Console.ReadLine();

                Console.WriteLine("Enter User Password:");
                string password = Console.ReadLine();

                Console.WriteLine("Enter First Name:");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter Last Name:");
                string lastName = Console.ReadLine();

                bool edited = _userService.EditUser(username, password, firstName, lastName, id);
                if (edited)
                {
                    Console.WriteLine($"User with id '{id}' edited");
                }
                else
                {
                    Console.WriteLine("Edit FAIL");
                }
            }
            else
            {
                Console.WriteLine("Invalid id");
            }
            Console.WriteLine();
        }

        private static void ListUsers()
        {
            List<int> usersId = _userService.GetAllUserdsId();

            Console.WriteLine("=======================================================");
            foreach (int id in usersId)
            {
                _userService.DisplayUserData(id);
                Console.WriteLine("=======================================================");
            }
            Console.WriteLine();
        }

        private static void DeleteUser()
        {
            Console.WriteLine("Enter Id of User for Delete");
            string input = Console.ReadLine();
            int id;
            if (int.TryParse(input, out id) && _userService.UserWithIdExists(id))
            {
                _userService.DeleteUser(id);
            }
            else
            {
                Console.WriteLine("Invalid id");
            }
            Console.WriteLine();
        }

        #endregion

        #region ToDoListCommands

        private static void ListToDoLists()
        {
            List<int> toDoListIds = _toDoListService.GetAllToDoListIdsByUser(_userService.CurrentUser.Id);
            if (toDoListIds.Any())
            {
                Console.WriteLine("=======================================================");
                foreach (int id in toDoListIds)
                {
                    _toDoListService.DisplayListData(id);
                    Console.WriteLine("=======================================================");
                }
            }
            else
            {
                Console.WriteLine("=======================================================");
                Console.WriteLine("No ToDoList found");
                Console.WriteLine("=======================================================");
            }
            Console.WriteLine();
        }

        private static void CreateToDoList()
        {
            Console.WriteLine("Enter List Title:");
            string title = Console.ReadLine();

            bool created = _toDoListService.CreateToDolist(title, _userService.CurrentUser.Id);
            if (created)
            {
                Console.WriteLine($"ToDoList with title '{title}' added");
            }
            else
            {
                Console.WriteLine($"ToDoList with title '{title}' already exists");
            }
            Console.WriteLine();
        }

        private static void EditToDoList()
        {
            Console.WriteLine("Enter Id of ToDoList for Edit");
            string input = Console.ReadLine();
            int listId;
            if (int.TryParse(input, out listId) && _toDoListService.ToDoListWithIdExists(listId)
                && _toDoListService.CanEditList(listId, _userService.CurrentUser.Id))
            {
                _toDoListService.DisplayListData(listId);
                Console.WriteLine("Enter List Title:");
                string title = Console.ReadLine();
                bool edited = _toDoListService.EditToDoList(listId, title, _userService.CurrentUser.Id);
                if (edited)
                {
                    Console.WriteLine($"ToDoList with id {listId} edited");
                }
                else
                {
                    Console.WriteLine("Edit FAIL");
                }
            }
            else
            {
                Console.WriteLine("Invalid id");
            }
            Console.WriteLine();
        }

        private static void DeleteToDoList()
        {
            Console.WriteLine("Enter Id of ToDoList for Delete");
            string input = Console.ReadLine();
            int listId;
            if (int.TryParse(input, out listId) && _toDoListService.ToDoListWithIdExists(listId))
            {
                _toDoListService.DeleteToDoList(listId, _userService.CurrentUser.Id);
                Console.WriteLine("Deleted");
            }
            else
            {
                Console.WriteLine("Invalid id");
            }
            Console.WriteLine();
        }

        private static void ShareToDoList()
        {
            Console.WriteLine("Enter Id of ToDoList to Share:");
            string inputListId = Console.ReadLine();
            int listId;

            Console.WriteLine("Enter Id of User you want to Share with:");
            string inputUserId = Console.ReadLine();
            int userId;
            if (int.TryParse(inputListId, out listId) && int.TryParse(inputUserId, out userId)
                && _toDoListService.ToDoListWithIdExists(listId) && _userService.UserWithIdExists(userId))
            {
                _toDoListService.ShareToDoList(listId, userId);
                Console.WriteLine($"List with id {listId} Shared with user with id {userId}");
            }
            else
            {
                Console.WriteLine("Share FAILED");
            }
            Console.WriteLine();
        }

        #endregion

        #region TaskCommands

        private static void CreateTask()
        {
            Console.WriteLine("Enter List Id:");
            string inputListId = Console.ReadLine();
            int listId;

            if (int.TryParse(inputListId, out listId) && _toDoListService.ToDoListWithIdExists(listId)
                && _toDoListService.CanEditList(listId, _userService.CurrentUser.Id))
            {
                Console.WriteLine("Enter Task Title:");
                string title = Console.ReadLine();

                Console.WriteLine("Enter Task Description:");
                string description = Console.ReadLine();

                bool created = _taskService.CreateTask(title, description, listId, _userService.CurrentUser.Id);
                if (created)
                {
                    Console.WriteLine($"Task with title | {title} | and description | {description} | created in list with id {listId}.");
                }
                else
                {
                    Console.WriteLine($"Task with title {title} already exists in list with id {listId}");
                }
            }
            else
            {
                Console.WriteLine("Invalid list Id");
            }
            Console.WriteLine();
        }

        private static void ListTasksByListId()
        {
            Console.WriteLine("Enter List Id:");
            string inputListId = Console.ReadLine();
            int listId;

            if (int.TryParse(inputListId, out listId) && _toDoListService.ToDoListWithIdExists(listId)
                && _toDoListService.CanViewList(listId, _userService.CurrentUser.Id))
            {
                List<int> taskIds = _taskService.GetTaskIdsByListId(listId);

                Console.WriteLine("=======================================================");
                _toDoListService.DisplayListData(listId);
                Console.WriteLine("=======================================================");

                foreach (int id in taskIds)
                {
                    _taskService.DisplayTaskData(id);
                    Console.WriteLine("=======================================================");
                }
            }
            else
            {
                Console.WriteLine("Invalid list Id");
            }
            Console.WriteLine();
        }

        private static void EditTask()
        {
            Console.WriteLine("Enter List Id:");
            string inputListId = Console.ReadLine();
            int listId;

            if (int.TryParse(inputListId, out listId) && _toDoListService.ToDoListWithIdExists(listId)
                && _toDoListService.CanEditList(listId, _userService.CurrentUser.Id))
            {
                Console.WriteLine("Enter Task Id:");
                string inputTaskId = Console.ReadLine();
                int taskId;

                if (int.TryParse(inputTaskId, out taskId))
                {
                    _taskService.DisplayTaskData(taskId);

                    Console.WriteLine("Enter Task Title:");
                    string title = Console.ReadLine();

                    Console.WriteLine("Enter Task Description:");
                    string description = Console.ReadLine();

                    Console.WriteLine("Enter Task IsComplete status (true/false)");
                    string inputIsComplete = Console.ReadLine();
                    bool isComplete;

                    if (bool.TryParse(inputIsComplete, out isComplete))
                    {
                        bool edited = _taskService.EditTask(title, description, isComplete, taskId, _userService.CurrentUser.Id);
                        if (edited)
                        {
                            Console.WriteLine($"Task with id {taskId} edited");
                        }
                        else
                        {
                            Console.WriteLine("Edit FAIL");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid isComplete status");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid task Id");
                }
            }
            else
            {
                Console.WriteLine("Invalid list Id");
            }
            Console.WriteLine();
        }

        private static void DeleteTask()
        {
            Console.WriteLine("Enter List Id:");
            string inputListId = Console.ReadLine();
            int listId;

            if (int.TryParse(inputListId, out listId) && _toDoListService.ToDoListWithIdExists(listId)
                && _toDoListService.CanEditList(listId, _userService.CurrentUser.Id))
            {
                Console.WriteLine("Enter Task Id:");
                string inputTaskId = Console.ReadLine();
                int taskId;

                if (int.TryParse(inputTaskId, out taskId) && _taskService.TaskWithIdExists(taskId))
                {
                    _taskService.DeleteTask(taskId);
                    Console.WriteLine($"Task with id {taskId} deleted");
                }
                else
                {
                    Console.WriteLine("Invalid task Id");
                }
            }
            else
            {
                Console.WriteLine("Invalid list Id");
            }
            Console.WriteLine();
        }

        private static void AssignTaskToUser()
        {
            Console.WriteLine("Enter List Id:");
            string inputListId = Console.ReadLine();
            int listId;

            if (int.TryParse(inputListId, out listId) && _toDoListService.ToDoListWithIdExists(listId)
              && _toDoListService.CanEditList(listId, _userService.CurrentUser.Id))
            {
                Console.WriteLine("Enter User Id to asign to");
                string inputUserId = Console.ReadLine();
                int userId;
                if (int.TryParse(inputUserId, out userId) && _userService.UserWithIdExists(userId)
                    && _toDoListService.IsListSharedWithUser(listId, userId))
                {
                    Console.WriteLine("Enter Task Id:");
                    string inputTaskId = Console.ReadLine();
                    int taskId;

                    if (int.TryParse(inputTaskId, out taskId) && _taskService.TaskWithIdExists(taskId))
                    {
                        _taskService.AssignTask(taskId, userId, _userService.CurrentUser.Id);
                        Console.WriteLine($"Task with id {taskId} assigned to user with id {userId}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid task Id");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid id");
                }
            }
            else
            {
                Console.WriteLine("Invalid list Id");
            }
            Console.WriteLine();
        }

        private static void CompleteTask()
        {
            Console.WriteLine("Enter List Id:");
            string inputListId = Console.ReadLine();
            int listId;

            if (int.TryParse(inputListId, out listId) && _toDoListService.ToDoListWithIdExists(listId)
              && _toDoListService.CanEditList(listId, _userService.CurrentUser.Id))
            {
                Console.WriteLine("Enter Task Id:");
                string inputTaskId = Console.ReadLine();
                int taskId;

                if (int.TryParse(inputTaskId, out taskId) && _taskService.TaskWithIdExists(taskId))
                {
                    _taskService.CompleteTask(taskId, _userService.CurrentUser.Id);
                    Console.WriteLine($"Task with id {taskId} completed");
                }
                else
                {
                    Console.WriteLine("Invalid task Id");
                }
            }
            else
            {
                Console.WriteLine("Invalid list Id");
            }
            Console.WriteLine();
        }

        #endregion
    }
}
