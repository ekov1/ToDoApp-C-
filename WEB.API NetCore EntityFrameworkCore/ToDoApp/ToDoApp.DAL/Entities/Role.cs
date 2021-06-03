using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class Role : Entity
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; } = new List<User>();
    }
}
