using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.DAL.Entities
{
    public abstract class Entity
    {
        // TODO: Make Guid 
        [Required]
        [Key]
        public int Id { get; set; } // TODO: private set

        [Required]
        public DateTime CreatedOn { get; set; } // TODO: private set

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        [Required]
        public int UpdatedBy { get; set; }
    }
}
