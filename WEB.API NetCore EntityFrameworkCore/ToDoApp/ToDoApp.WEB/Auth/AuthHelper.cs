using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;
using ToDoApp.BLL.Services;
using ToDoApp.DAL.Entities;

namespace ToDoApp.WEB.Auth
{
    public static class AuthHelper
    {
        public static User GetCurrentUser(this UserService userService, HttpRequest request)
        {
            StringValues UsernameSV;
            StringValues PasswordSV;

            request.Headers.TryGetValue("Username", out UsernameSV);
            request.Headers.TryGetValue("Password", out PasswordSV);

            if (UsernameSV.Count != 0 && PasswordSV.Count != 0)
            {
                string username = UsernameSV.First();
                string password = PasswordSV.First();
                return userService.GetUserByUsernameAndPassword(username,password);
            }

            return null;
        }
    }
}
