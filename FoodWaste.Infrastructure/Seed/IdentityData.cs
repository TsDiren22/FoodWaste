using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodWaste.Infrastructure.Seed
{
    public class IdentityData
    {
        private const string employeeUser = "Employee";
        private const string emailPhysio = "abc@abc.com";
        private const string employeePassword = "abcd";
        
        private const string studentUser = "Student";
        private const string emailDiren = "ghi@ghi.com";
        private const string studentPassword = "abcd";


        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByNameAsync(employeeUser);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = employeeUser,
                    Email = emailPhysio
                };
                await userManager.CreateAsync(user, employeePassword);
                await userManager.AddClaimAsync(user, new Claim("Employee", "true"));
            }
            

            IdentityUser user2 = await userManager.FindByNameAsync(studentUser);
            if (user2 == null)
            {
                user2 = new IdentityUser
                {
                    UserName = studentUser,
                    Email = emailDiren
                };
                await userManager.CreateAsync(user2, studentPassword);
                await userManager.AddClaimAsync(user2, new Claim("Student", "true"));
            }
        }
    }
}
