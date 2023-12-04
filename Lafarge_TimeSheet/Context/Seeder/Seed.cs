using Lafarge_TimeSheet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lafarge_TimeSheet.Context.Seeder
{
    public class Seed
    {
        public static async Task SeedUsersAndRoleAsync(IApplicationBuilder appBuilder)
        {
            try
            {
                using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
                {
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!await roleManager.RoleExistsAsync("Admin"))
                        await roleManager.CreateAsync(new IdentityRole("Admin"));

                    if (!await roleManager.RoleExistsAsync("User"))
                        await roleManager.CreateAsync(new IdentityRole("User"));

                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    var adminUser = await userManager.FindByEmailAsync("adminaccount@lafarge.com");
                    if (adminUser == null)
                    {
                        var newAdmin = new ApplicationUser()
                        {
                            FirstName = "Admin",
                            LastName = "Account",
                            UserName = "admin",
                            Email = "adminaccount@lafarge.com",
                            EmailConfirmed = true

                        };
                        await userManager.CreateAsync(newAdmin, "Admin@Lafarge247");
                        await userManager.AddToRoleAsync(newAdmin, "Admin");
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                // Inspect innerException for more details
            }
           
        }
    }
}
