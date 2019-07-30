using GameStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Persistence.Configurations
{
    public class GameCategoryConfiguration : IEntityTypeConfiguration<GameCategory>
    {
        public void Configure(EntityTypeBuilder<GameCategory> builder)
        {
            builder.HasKey(e => new { e.GameId, e.CategoryId });

            builder
                .HasOne(e => e.Category)
                .WithMany(e => e.GameCategories)
                .HasForeignKey(e => e.CategoryId);

            builder
                .HasOne(e => e.Game)
                .WithMany(e => e.GameCategories)
                .HasForeignKey(e => e.GameId);
        }
    }
}
