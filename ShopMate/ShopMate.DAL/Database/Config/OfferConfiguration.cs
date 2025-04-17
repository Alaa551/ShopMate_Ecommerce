using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMate.DAL.Database.Models;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(o => o.DiscountPercentage)
               .HasPrecision(5, 2);

        builder.HasMany(of => of.OfferNotifications)
            .WithOne(on => on.Offer)
                .HasForeignKey(on => on.OfferId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        builder.ToTable("Offers");

    }
}


