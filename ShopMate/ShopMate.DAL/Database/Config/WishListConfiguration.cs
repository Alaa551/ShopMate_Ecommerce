using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMate.DAL.Database.Models;

namespace ShopMate.DAL.Database.Config
{
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.HasKey(c => c.Id);


            builder.HasMany(p => p.WishListItems)
                   .WithOne(c => c.WishList)
                   .HasForeignKey(c => c.WishListId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();


            builder.ToTable("WishLists");

        }
    }


}



