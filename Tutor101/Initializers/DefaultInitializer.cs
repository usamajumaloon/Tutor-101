using Microsoft.AspNetCore.Identity;
using Tutor101.Data.Entities;

namespace Tutor101.Initializers
{
    public static class DefaultInitializer
    {
        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var defaultUser = new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Name = "Admin"
                };

                var result = userManager.CreateAsync(defaultUser, "Admin@123").Result;
            }
        }
    }
}
