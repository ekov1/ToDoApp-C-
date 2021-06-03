using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.BLL.Services;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;
using ToDoApp.WEB.Auth;

namespace ToDoApp.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly UserService _userService;

        public LogInController() : base()
        {
            _userService = new UserService(new DatabaseContext());
        }

        

    }
}
