using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.DAL.Entities
{
    public class User : Entity
    {
        [Required]
        [MaxLength(20)]
        //[Index(IsUnique = true)]
        public string Username { get; set; } // TODO: private set

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; } // TODO: private set

        [MaxLength(20)]
        public string LastName { get; set; } // TODO: private set

        
        public virtual Role Role { get; set; }

        public int RoleId { get; set; }

        public virtual List<ToDoList> ToDoList { get; set; } = new List<ToDoList>();

        public virtual List<Task> Task { get; set; } = new List<Task>();
    }
}
