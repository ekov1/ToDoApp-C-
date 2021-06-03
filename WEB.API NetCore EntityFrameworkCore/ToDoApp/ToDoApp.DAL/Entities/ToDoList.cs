using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class ToDoList : Entity
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        public virtual ICollection<User> User { get; set; } = new List<User>();
    }
}
