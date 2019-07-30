using GameStore.Application.Interfaces;
using GameStore.Domain.Models;
using GameStore.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Persistence
{
    public class GameStoreDbContext : DbContext, IGameStoreDbContext
    {
        public DbSet<Game> Games { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<GameCategory> GameCategories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public void Migrate()
        {
            Database.Migrate();
        }

        void IGameStoreDbContext.SeedData()
        {
            this.SeedData();
            SaveChanges();
        }

        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameStoreDbContext).Assembly);
        }
    }
}
