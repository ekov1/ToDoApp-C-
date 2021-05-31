using ToDoApp.BLL.Services;
using ToDoApp.Core.Providers.Contracts;
using ToDoApp.DAL.Entities;

namespace ToDoApp.Controllers
{
    public class UserController
    {
        public readonly IWriter _writer;
        public readonly IReader _reader;
        private readonly UserService _userService;

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
    }
}
