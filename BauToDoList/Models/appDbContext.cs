using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BauToDoList.Models
{
    public class appDbContext:IdentityDbContext<ApplicationUser>
    {

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Side> Sides { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<Category> Categories { get; set; }

        public appDbContext() : base("appConnection",throwIfV1Schema:false)     //connectionstringle aynı isme sahip bu parantez içi
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<appDbContext, BauToDoList.Migrations.Configuration>("appConnection"));
        }

        public static appDbContext Create()
        {

            return new appDbContext();
        }

        //public System.Data.Entity.DbSet<BauToDoList.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}