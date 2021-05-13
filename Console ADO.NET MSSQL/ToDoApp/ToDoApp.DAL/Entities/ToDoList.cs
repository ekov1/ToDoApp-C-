using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class ToDoList : Entity
    {
        public ToDoList()
        {
            SharedWith = new List<int>();
        }

        public string Title { get; set; }

        public List<int> SharedWith { get; set; }
    }
}
