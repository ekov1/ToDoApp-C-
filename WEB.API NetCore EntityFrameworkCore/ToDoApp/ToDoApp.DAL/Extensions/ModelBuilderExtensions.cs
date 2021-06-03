using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                  new Role() { Id = 1, Name = "Admin", CreatedBy = 1, UpdatedBy = 1 },
                  new Role() { Id = 2, Name = "RegularUser", CreatedBy = 1, UpdatedBy = 1 }
                );

            modelBuilder.Entity<User>().HasData(
                 new User()
                 {
                     Id = 1,
                     Username = "admin",
                     Password = "adminpassword",
                     CreatedBy = 1,
                     UpdatedBy = 1,
                     RoleId = 1
                 },
                  new User()
                  {
                      Id = 2,
                      Username = "a",
                      Password = "a",
                      CreatedBy = 1,
                      UpdatedBy = 1,
                      RoleId = 1
                  }
                );
        }
    }
}
