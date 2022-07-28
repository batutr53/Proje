using Microsoft.EntityFrameworkCore;
using Proje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proje.DataAccess.Contexts
{
    public class ProjeContext:DbContext
    {
        public ProjeContext(DbContextOptions<ProjeContext> options): base(options)
        {

        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

          //  modelBuilder.Entity<UserRole>().HasKey(x => new { x.RoleId, x.UserId });
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
      //  public DbSet<UserRole> UserRoles { get; set; }

    }
}
