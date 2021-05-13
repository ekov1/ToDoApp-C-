using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.BLL.Services;
using ToDoApp.Controllers;
using ToDoApp.Core.Providers;
using ToDoApp.Core.Providers.Contracts;
using ToDoApp.DAL.Data;

namespace ToDoApp.Core
{
    class Engine
    {
        private static readonly Engine instance = new Engine();

        private const string TerminationCommand = "Exit";
        private const string NullProvidersExceptionMessage = "cannot be null.";

        private readonly string _connectionStringCreate;
        private readonly string _connectionStringEdit;

        public readonly IReader _reader;
        public readonly IWriter _writer;

        private IConfigurationRoot _configuration;

        private readonly UserService _userService;
        private readonly ToDoListService _toDoListService;

        private readonly UserController _userController;
        private readonly ToDoListController _toDoListController;

        private readonly Menu _menu;

        private Engine()
        {
            // Read config file
            _configuration = ConfigInitializer.InitConfig();
            _connectionStringCreate = _configuration.GetConnectionString("Create");
            _connectionStringEdit = _configuration.GetConnectionString("Edit");

            InitializeDb(_connectionStringCreate,_connectionStringEdit);

            UserRepository userRepositiry = new UserRepository(_connectionStringEdit);
            ToDoListRepository toDoListRepository = new ToDoListRepository(_connectionStringEdit);

            _userService = new UserService(userRepositiry);
            _toDoListService = new ToDoListService(toDoListRepository);

            _reader = new ConsoleReader();
            _writer = new ConsoleWriter();

            _userController = new UserController(_userService, _writer, _reader);
            _toDoListController = new ToDoListController(_toDoListService,_userService, _writer, _reader);

            _menu = new Menu(_writer, _reader, _userController, _toDoListController);
        }

        public static Engine Instance
        {
            get
            {
                return instance;
            }
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                exit = _menu.MainMenu();
            }
        }

        private void InitializeDb(string connectionStringCreate, string connectionStringEdit)
        {
            if (!DatabaseInitilizer.CheckDatabaseExists(connectionStringCreate, "todo_db"))
            {
                // Create new database and tables 
                DatabaseInitilizer.InitilizeDatabase(connectionStringCreate);
                DatabaseInitilizer.InitilizeDatabaseTables(connectionStringEdit);
            }
        }
    }
}
