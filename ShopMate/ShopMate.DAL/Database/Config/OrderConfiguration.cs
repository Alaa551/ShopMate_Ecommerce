using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMate.DAL.Database.Models;

namespace ShopMate.DAL.Database.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(o => o.OrderDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(o => o.OrderStatus)
                .HasConversion<string>();

            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            builder.HasMany(o => o.OrderItems)
                .WithOne(item => item.Order)
                .HasForeignKey(item => item.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();


            builder.HasMany(o => o.OrderNotifications)
                .WithOne(n => n.Order)
                .HasForeignKey(n => n.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(o => o.PaymentDetails)
                .WithOne(p => p.Order)
                .HasForeignKey<PaymentDetails>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.ShippingAddress)
                .WithMany(s => s.Orders)
                .HasForeignKey(s => s.ShippingAddressId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Orders");

        }
    }


}
