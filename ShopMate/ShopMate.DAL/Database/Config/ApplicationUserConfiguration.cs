using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMate.DAL.Database.Models;

namespace ShopMate.DAL.Database.Config
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>

    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.Gender).HasConversion<string>();

            builder.HasOne(u => u.WishList)
                  .WithOne(w => w.User)
                  .HasForeignKey<WishList>(w => w.ApplicationUserId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .IsRequired();

            builder.HasOne(u => u.Cart)
                  .WithOne(c => c.ApplicationUser)
                  .HasForeignKey<Cart>(c => c.ApplicationUserId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .IsRequired();

            builder.HasMany(u => u.ShippingAddresses)
                  .WithOne(s => s.User)
                  .HasForeignKey(s => s.ApplicationUserId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .IsRequired();


            ////////////////////////////////////
            builder.HasMany(u => u.Orders)
                  .WithOne(o => o.User)
                  .HasForeignKey(o => o.ApplicationUserId)
                  .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.ProductReviews)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.ApplicationUserId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.Notifications)
                   .WithOne(n => n.User)
                   .HasForeignKey(n => n.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasMany(u => u.ContactMessages)
                   .WithOne(c => c.User)
                   .HasForeignKey(c => c.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }
}
