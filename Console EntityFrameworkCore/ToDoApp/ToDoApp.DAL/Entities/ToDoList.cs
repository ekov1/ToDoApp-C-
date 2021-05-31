using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.DAL.Entities
{
    public class ToDoList : Entity
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        public virtual List<User> User { get; set; } = new List<User>();
    }
}
