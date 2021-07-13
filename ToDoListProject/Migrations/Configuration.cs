namespace ToDoListProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    //importing the required namespaces
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ToDoListProject.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDoListProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        // The Seed method is called when ever we create a database, if when ever entity framework creates a new
        // database it will seed in with the respective user in.
        protected override void Seed(ToDoListProject.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Adding the newly created AddUsers inside the Seed method

            AddUsers(context);
        }

        // Adding a function to our configuraton class to add users
        void AddUsers(ToDoListProject.Models.ApplicationDbContext context)
        {
            // Adding new users
            var user = new ApplicationUser { UserName = "test@email.com" };
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            um.Create(user, "pwd123");
        }
    }
}
