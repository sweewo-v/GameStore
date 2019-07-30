using System;
using GameStore.Application.Infrastructure.Auth;
using GameStore.Application.Interfaces;
using GameStore.Application.Interfaces.Auth;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider
                    .GetService<IGameStoreDbContext>();
                var identityContext = scope.ServiceProvider
                    .GetService<IdentityDbContext<IdentityUser>>();
                var securityPasswordService = scope.ServiceProvider
                    .GetService<ISecurityPasswordService>();
                var roleManager = scope.ServiceProvider
                    .GetService<RoleManager<IdentityRole>>();

                context.Migrate();
                identityContext.Database.Migrate();

                if (!roleManager.IsAdminAvailable())
                {
                    Console.WriteLine($"Security password: {securityPasswordService.SecurityPassword}");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
