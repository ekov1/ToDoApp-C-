using System.Collections.Generic;

namespace TODOApp.Entities
{
    public class Task : Entity
    {
        public Task()
        {
            AssignedTo = new List<int>();
        }

        public int ListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public List<int> AssignedTo { get; set; }
    }
}
