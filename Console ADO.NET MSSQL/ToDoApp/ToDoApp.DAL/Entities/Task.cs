using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class Task : Entity
    {
        public int ListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}
