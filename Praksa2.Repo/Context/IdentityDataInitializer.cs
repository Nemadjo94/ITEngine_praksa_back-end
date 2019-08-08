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

        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            await InitializeAsync(serviceProvider);

            var UserManager = serviceProvider.GetRequiredService<UserManager<Users>>();

            await SeedUsers(UserManager);

        }



        public static async Task SeedUsers(UserManager<Users> userManager)
        {
            if(userManager.FindByNameAsync("user1").Result == null)
            {
                Users user = new Users();
                user.UserName = "user1";
                user.Email = "user1@localhost";
                user.FirstName = "Nemanja";
                user.LastName = "Djordjevic";
                user.PhoneNumber = "+555 555 555";

                IdentityResult result = userManager.CreateAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByNameAsync("user2").Result == null)
            {
                Users user = new Users();
                user.UserName = "user2";
                user.Email = "user2@localhost";
                user.FirstName = "Mihajlo";
                user.LastName = "Djordjevic";
                user.PhoneNumber = "+555 555 666";

                IdentityResult result = userManager.CreateAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }

        //public static void SeedRoles(RoleManager<Roles> roleManager)
        //{
        //    //if (!roleManager.RoleExistsAsync("User").Result)
        //    //{
        //    //    Roles role = new Roles();
        //    //    role.Name = "User";
        //    //    role.Description = "Performs normal operations.";
        //    //    IdentityResult identityResult = roleManager.CreateAsync(role).Result;
        //    //}

        //    //if (!roleManager.RoleExistsAsync("Admin").Result)
        //    //{
        //    //    Roles role = new Roles();
        //    //    role.Name = "Admin";
        //    //    role.Description = "Performs all operations.";
        //    //    IdentityResult identityResult = roleManager.CreateAsync(role).Result;
        //    //}
        //}

    }
}
