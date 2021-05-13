using TODOApp.Enums;

namespace TODOApp.Entities
{
    public class User : Entity
    {
        public string Username { get; set; } // TODO: private set
        public string Password { get; set; }
        public string FirstName { get; set; } // TODO: private set
        public string LastName { get; set; } // TODO: private set
        public UserRole Role { get; set; }
    }
}
