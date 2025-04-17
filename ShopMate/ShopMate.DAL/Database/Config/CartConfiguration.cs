using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMate.DAL.Database.Models;

namespace ShopMate.DAL.Database.Config
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);


            builder.HasMany(p => p.CartItems)
                   .WithOne(c => c.Cart)
                   .HasForeignKey(c => c.CartId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();


            builder.ToTable("Carts");

        }
    }
}
