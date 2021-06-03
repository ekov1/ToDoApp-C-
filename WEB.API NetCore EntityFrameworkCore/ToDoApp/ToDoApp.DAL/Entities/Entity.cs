using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public abstract class Entity
    {
        [Required]
        [Key]
        public int Id { get; set; } 

        [Required]
        public DateTime CreatedOn { get; set; } 

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        [Required]
        public int UpdatedBy { get; set; }
    }
}
