using System.Threading;
using System.Threading.Tasks;
using GameStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Interfaces
{
    public interface IGameStoreDbContext
    {
        DbSet<Game> Games { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<GameCategory> GameCategories { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<OrderLine> OrderLines { get; set; }

        void Migrate();

        void SeedData();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
