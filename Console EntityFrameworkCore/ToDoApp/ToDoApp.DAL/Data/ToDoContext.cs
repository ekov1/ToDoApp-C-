using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Extensions;

namespace ToDoApp.DAL.Data
{
    public class ToDoContext : DbContext
    {
        private readonly string _connectionString = "Data Source=.\\;Trusted_Connection=True;Initial Catalog=ToDoDb;User Id=sa;Password=Pass@word";

        public DbSet<User> User { get; set; }
        public DbSet<ToDoList> ToDoList { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(this._connectionString)
            .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
