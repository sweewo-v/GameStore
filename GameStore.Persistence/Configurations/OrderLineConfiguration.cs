using GameStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Persistence.Configurations
{
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(e => new { e.GameId, e.OrderId });

            builder
                .HasOne(e => e.Order)
                .WithMany(e => e.Lines)
                .HasForeignKey(e => e.OrderId);

            builder
                .HasOne(e => e.Game)
                .WithMany(e => e.Lines)
                .HasForeignKey(e => e.GameId);
        }
    }
}
