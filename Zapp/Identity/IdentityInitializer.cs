using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Zapp.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            //Roller

            if (!context.Roles.Any(i=> i.Name=="admin"))// Admin rolünde bir rol yok ise onu oluşturcaz.
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole(){Name = "admin",Description = "yönetici rolü"};

                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "user"))// user rolünde bir rol yok ise onu oluşturcaz.
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "user", Description = "kullanıcı rolü" };

                manager.Create(role);
            }

            //Userlar


            if (!context.Users.Any(i => i.UserName == "veli.shn"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser() { Name = "Veli", Surname = "Sahin", Email = "abcd@xyz.com", UserName = "veli.shn" };

                manager.Create(user, "123456");
                manager.AddToRole(user.Id, "admin");


            }


            //if (!context.Users.Any(i => i.UserName == "mht.ugur"))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);

            //    var user = new ApplicationUser() { Name = "Mehmet", Surname = "Uğur", Email = "abcd@xyz.com", UserName = "mht.ugur" };

            //    manager.Create(user, "123456"); // password
            //    manager.AddToRole(user.Id, "admin");
            //    manager.AddToRole(user.Id, "user");



            //}

            //if (!context.Users.Any(i => i.UserName == "veli.shn"))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);

            //    var user = new ApplicationUser() { Name = "Veli", Surname = "Sahin", Email = "abcd@xyz.com", UserName = "veli.shn" };

            //    manager.Create(user, "123456");
            //    manager.AddToRole(user.Id, "user");


            //}

            base.Seed(context);
        }
    }
}