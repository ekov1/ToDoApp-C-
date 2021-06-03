using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class Task : Entity
    {
        public virtual ToDoList ToDoList { get; set; }

        [Required]
        public int ToDoListId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public bool IsComplete { get; set; }

        public virtual ICollection<User> User { get; set; } = new List<User>();
    }
}
