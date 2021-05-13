using System;

namespace TODOApp.Entities
{
    public abstract class Entity
    {
        // TODO: Make Guid 
        public int Id { get; set; } // TODO: private set
        public DateTime DateCreated { get; set; } // TODO: private set
        public DateTime LastModifiedAt { get; set; }
        public int LastModifiedBy { get; set; }
        public int IdOfCreator { get; set; }

    }
}
