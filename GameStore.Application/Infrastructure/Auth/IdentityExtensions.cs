using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.Infrastructure.Auth
{
    public static class IdentityExtensions
    {
        public static bool IsAdminAvailable(this RoleManager<IdentityRole> roleManager)
        {
            return roleManager.Roles.Any(e => e.Name.Equals("Admin"));
        }
    }
}
