using Microsoft.AspNetCore.Identity;
using MovieRecommendation.Application.Enums.Account;
using MovieRecommendation.Domain.Entities;

namespace MovieRecommendation.Persistence.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new ApplicationRole()
            {
                Name = Roles.SuperAdmin.ToString(),
                CreatedDate = DateTime.Now
            });
            await roleManager.CreateAsync(new ApplicationRole()
            {
                Name = Roles.Admin.ToString(),
                CreatedDate = DateTime.Now
            });
            await roleManager.CreateAsync(new ApplicationRole()
            {
                Name = Roles.Moderator.ToString(),
                CreatedDate = DateTime.Now
            });
            await roleManager.CreateAsync(new ApplicationRole()
            {
                Name = Roles.Basic.ToString(),
                CreatedDate = DateTime.Now
            });
        }
    }
}
