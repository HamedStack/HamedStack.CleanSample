using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanSample.WebApi.Identity
{
    public class AdvIdentityDbContext : IdentityDbContext<AdvIdentityUser>
    {
        public AdvIdentityDbContext(DbContextOptions options) : base(options) { }

    }
}
