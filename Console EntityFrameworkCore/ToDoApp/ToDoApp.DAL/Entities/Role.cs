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
        //[Index(IsUnique = true)]
        public string Name { get; set; } // TODO: private set

        public virtual List<User> User { get; set; } = new List<User>();
    }
}
