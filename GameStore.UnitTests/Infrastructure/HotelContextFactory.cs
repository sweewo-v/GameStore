using Microsoft.EntityFrameworkCore;
using System;
using GameStore.Persistence;
using GameStore.Persistence.Extensions;

namespace GameStore.UnitTests.Infrastructure
{
    public class GameStoreContextFactory
    {
        public static GameStoreDbContext Create()
        {
            var options = new DbContextOptionsBuilder<GameStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new GameStoreDbContext(options);
            context.Database.EnsureCreated();

            context.SeedData();
            context.SaveChanges();

            return context;
        }

        public static void Destroy(GameStoreDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
