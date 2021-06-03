using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.BLL.Services;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;
using ToDoApp.Models.DTO.Response;
using ToDoApp.WEB.Auth;

namespace ToDoApp.WEB.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController() : base()
        {
            _userService = new UserService(new DatabaseContext());
        }

        [HttpGet]
        public async Task<List<UserResponseDTO>> GetAll()
        {
            User currentuser = _userService.GetCurrentUser(Request);
            if (currentuser != null)
            {
                List<UserResponseDTO> users = new List<UserResponseDTO>();

                foreach (var user in await _userService.GetAll())
                {
                    users.Add(new UserResponseDTO()
                    {
                        Id = user.Id,
                        CreatedAt = user.CreatedOn,
                        Username = user.Username,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role.Name
                    });
                }

                return users;
            }
            return null;
        }
    }
}
