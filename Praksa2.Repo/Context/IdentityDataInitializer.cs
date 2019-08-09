using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Praksa2.Repo;
using Praksa2.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praksa2.API
{
    public class IdentityDataInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<Roles>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult identityResult;
            foreach(var roleName in roleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    identityResult = await RoleManager.CreateAsync(new Roles(roleName));
                }
            }
        }

        public static async Task SeedSomeUsers(IServiceProvider serviceProvider)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<Users>>();

            if (UserManager.FindByNameAsync("user1").Result == null)
            {
                Users user = new Users();
                user.UserName = "user1";
                user.Email = "user1@localhost";
                user.FirstName = "Nemanja";
                user.LastName = "Djordjevic";
                user.PhoneNumber = "+555 555 555";

                IdentityResult result = UserManager.CreateAsync(user, "Nemanja994@").Result;

                if (result.Succeeded)
                {
                     UserManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (UserManager.FindByNameAsync("user2").Result == null)
            {
                Users user = new Users();
                user.UserName = "user2";
                user.Email = "user2@localhost";
                user.FirstName = "Mihajlo";
                user.LastName = "Djordjevic";
                user.PhoneNumber = "+555 555 666";

                IdentityResult result = UserManager.CreateAsync(user, "Mihajlo000@").Result;

                if (result.Succeeded)
                {
                    UserManager.AddToRoleAsync(user, "User").Wait();
                }
            }

        }

        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            await InitializeAsync(serviceProvider);
            await SeedSomeUsers(serviceProvider);

            var UserManager = serviceProvider.GetRequiredService<UserManager<Users>>();


        }


    }
}
