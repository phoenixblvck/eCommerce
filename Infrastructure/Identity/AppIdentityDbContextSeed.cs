using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager){
            if(!userManager.Users.Any()){
                var user = new AppUser
                {
                    DisplayName = "Andries",
                    Email = "andries@test.com",
                    UserName = "andries@test.com",
                    Address = new Address {
                        FirstName = "Andries",
                        LastName = "Bingani",
                        Street = "10 The street",
                        City = "Vosloorus",
                        State =  "NY",
                        ZipCode = "1475"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}