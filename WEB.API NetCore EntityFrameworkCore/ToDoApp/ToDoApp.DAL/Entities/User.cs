using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class User : Entity
    {
        [Required]
        [MaxLength(20)]
        public string Username { get; set; } 

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; } 

        public virtual Role Role { get; set; }

        public int RoleId { get; set; }

        public virtual ICollection<ToDoList> ToDoList { get; set; } = new List<ToDoList>();

        public virtual ICollection<Task> Task { get; set; } = new List<Task>();
    }
}
