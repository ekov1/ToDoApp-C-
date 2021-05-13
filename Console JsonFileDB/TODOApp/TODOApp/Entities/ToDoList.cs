using System.Collections.Generic;

namespace TODOApp.Entities
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
