using Microsoft.Extensions.Configuration;
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

        private IConfigurationRoot _configuration;

        private readonly string _connectionString;

        private readonly ToDoContext _context;

        private readonly UserRepository _userRepository;

        private readonly UserService _userService;

        private readonly UserController _userController;

        public readonly IReader _reader;
        public readonly IWriter _writer;

        private readonly Menu _menu;

        private Engine()
        {
            // Read config file
            _configuration = ConfigInitializer.InitConfig();
            _connectionString = _configuration.GetConnectionString("Default");

            _context = new ToDoContext();
            _context.Database.EnsureCreated();

            _userRepository = new UserRepository(_context);
            //ToDoListRepository toDoListRepository = new ToDoListRepository(_connectionStringEdit);

            _userService = new UserService(_userRepository);
            //_toDoListService = new ToDoListService(toDoListRepository);

            _reader = new ConsoleReader();
            _writer = new ConsoleWriter();

            _userController = new UserController(_userService, _writer, _reader);
            //_toDoListController = new ToDoListController(_toDoListService,_userService, _writer, _reader);

            _menu = new Menu(_writer, _reader, _userController);
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
    }
}
