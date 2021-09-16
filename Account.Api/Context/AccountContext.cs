using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Account.Api.Context
{
    public class AccountContext : IdentityDbContext<IdentityUser>
    {
        public AccountContext(DbContextOptions options) : base(options)
        {
        }
    }
}
