using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodWaste.Infrastructure.Data
{

    public class AccountContext : IdentityDbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }
    }
}
