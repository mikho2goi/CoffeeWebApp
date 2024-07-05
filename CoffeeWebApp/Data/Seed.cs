
using CoffeeWebApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace CoffeeWebApp.Data
{
    public class Seed 
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();
                string adminUserEmail = "thienle@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new UserModel()
                    {
                        UserName = "thienle",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "0939582942");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new UserModel()
                    {
                        UserName = "user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAppUser, "0939582942");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
