using GameStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Persistence.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(128);

            builder.Property(e => e.Description).IsRequired(false).HasMaxLength(4096);

            builder.Property(e => e.Price).IsRequired();

            builder.Property(e => e.AddedDateTime).IsRequired();
        }
    }
}
