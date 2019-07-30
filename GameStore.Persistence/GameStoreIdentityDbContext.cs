using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Persistence
{
    public class GameStoreIdentityDbContext: IdentityDbContext<IdentityUser>
    {
        public GameStoreIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
