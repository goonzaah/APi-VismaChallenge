using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data.EF.EntityTypeConfigurations
{
    public class RentPriceEntityTypeConfiguration : IEntityTypeConfiguration<RentPrice>
    {
        public void Configure(EntityTypeBuilder<RentPrice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Time)
                .IsRequired(true);

            builder.Property(x => x.Value)
                .IsRequired(true);

            builder.HasOne(x=>x.Product)
                .WithMany()
                .HasForeignKey(x=>x.ProductId)
                .IsRequired();
        }
    }
}
