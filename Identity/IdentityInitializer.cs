using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BasicECommerce.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            if (!context.Roles.Any(i => i.Name == "Admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "Admin", Description = "Admin Role" };
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "User"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "User", Description = "Default Role" };
                manager.Create(role);
            }

            if (!context.Users.Any(i => i.UserName == "mrttk"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Emre",
                    Surname = "Tetik",
                    UserName = "mrttk",
                    Email = "mrttk@someone.com"
                };

                manager.Create(user, "password");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(i => i.UserName == "mrnobody"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Mr",
                    Surname = "Nobody",
                    UserName = "mrnobody",
                    Email = "mrnobody@mrnobody.com"
                };

                manager.Create(user, "password");
                manager.AddToRole(user.Id, "User");
            }

            base.Seed(context);
        }
    }
}