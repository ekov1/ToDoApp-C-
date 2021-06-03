using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Extensions;

namespace ToDoApp.DAL.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly string _connectionString = "Data Source=.\\;Trusted_Connection=True;Initial Catalog=ToDoDb;User Id=sa;Password=Pass@word";

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Task> Task { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(this._connectionString)
                 .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().Property(r => r.CreatedOn).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Role>().Property(r => r.UpdatedOn).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<User>().Property(u => u.CreatedOn).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<User>().Property(u => u.UpdatedOn).HasDefaultValueSql("getdate()");
            
            modelBuilder.Seed();
        }
    }
}
