using GameStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(128);

            builder.Property(e => e.City).IsRequired().HasMaxLength(128);

            builder.Property(e => e.Country).IsRequired().HasMaxLength(128);

            builder.Property(e => e.Line1).IsRequired().HasMaxLength(128);

            builder.Property(e => e.Line2).IsRequired(false).HasMaxLength(128);

            builder.Property(e => e.Line3).IsRequired(false).HasMaxLength(128);

            builder.Property(e => e.State).IsRequired(false).HasMaxLength(128);

            builder.Property(e => e.Zip).IsRequired(false).HasMaxLength(10);

            builder.Property(e => e.GiftWrap).IsRequired();

            builder.Property(e => e.AddedDateTime).IsRequired();
        }
    }
}
