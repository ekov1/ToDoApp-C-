using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;

namespace ToDoApp.BLL.Services
{
    public  class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Currently logged in user
        /// </summary>
        public User CurrentUser { get; private set; }

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

        public User GetUserByName(string username)
        {
            return _userRepository.GetUserByName(username);
        }
    }
}
