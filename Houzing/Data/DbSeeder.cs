using Microsoft.AspNetCore.Identity;
using Houzing.Constants;
namespace Houzing.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminsAsync(IServiceProvider service)
        {
            // Seed Roles
            UserManager<ApplicationUser>? userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Employer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            // creating admin
            var admin = new ApplicationUser
            {
                UserName = "Mirziyod",
                Email = "mirziyod@gmail.com",
                FirstName = "Mirziyod",
                LastName = "Sunatillayev",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var adminInDb = await userManager.FindByEmailAsync(admin.Email);
            if (adminInDb == null)
            {
                await userManager.CreateAsync(admin, "Mirziyod123*");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }

            /// creating Employer
            var emp = new ApplicationUser
            {
                UserName = "Employer",
                Email = "emp@gmail.com",
                FirstName = "Employer",
                LastName = "Employerivech",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var empInDb = await userManager.FindByEmailAsync(emp.Email);
            if (empInDb == null)
            {
                await userManager.CreateAsync(emp, "Emp123*");
                await userManager.AddToRoleAsync(emp, Roles.Employer.ToString());
            }

            // creating User
            var userbek = new ApplicationUser
            {
                UserName = "User",
                Email = "user@gmail.com",
                FirstName = "User",
                LastName = "Userbekov",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var userbekInDb = await userManager.FindByEmailAsync(userbek.Email);
            if (userbekInDb == null)
            {
                await userManager.CreateAsync(userbek, "Userbek123*");
                await userManager.AddToRoleAsync(userbek, Roles.User.ToString());
            }
        }
    }
}
