using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using GameStore.Application.Commands.Game.CreateGame;
using GameStore.Application.Infrastructure;
using GameStore.Application.Infrastructure.Auth;
using GameStore.Application.Infrastructure.Mapping;
using GameStore.Application.Interfaces;
using GameStore.Application.Interfaces.Auth;
using GameStore.Application.Queries.Game.GetAllGames;
using GameStore.Domain.Common;
using GameStore.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace GameStore.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        private readonly IHostingEnvironment _hostingEnvironment;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IGameStoreDbContext, GameStoreDbContext>(
                options => options.UseMySql(
                    Configuration.GetConnectionString("GameStoreDatabase")));
            services.AddDbContext<IdentityDbContext<IdentityUser>, GameStoreIdentityDbContext>(
                o => o.UseMySql(
                    Configuration.GetConnectionString("GameStoreIdentityDatabase")));

            services.AddIdentity<IdentityUser, IdentityRole>(o =>
                {
                    o.Password.RequiredLength = 4;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<IdentityDbContext<IdentityUser>>()
                .AddDefaultTokenProviders();

            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetAllGamesQuery).GetTypeInfo().Assembly);
            services.AddScoped(SessionCart.GetCart);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ISecurityPasswordService, SecurityPasswordService>();

            services.AddRouting(options => options.LowercaseUrls = true);
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<CreateGameCommandValidator>());

            services.AddStackExchangeRedisCache(
                options =>
                {
                    options.Configuration = Configuration["Redis:Configuration"];
                    options.InstanceName = Configuration["Redis:InstanceName"];
                });
            services.AddSession();

            // Antiforgery tokens require data protection.
            services.AddDataProtection()
                .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(
                        Configuration["RedisKeys:Configuration"]),
                        Configuration["RedisKeys:InstanceName"]);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseHsts();
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    null,
                    "{controller}/{category}",
                    new { controller = "Game", action = "Index" }
                );
                routes.MapRoute(
                    null,
                    "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    "areas",
                    "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
            });
        }
    }
}