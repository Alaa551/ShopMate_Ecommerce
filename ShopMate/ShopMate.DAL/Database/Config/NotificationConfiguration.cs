using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMate.DAL.Database.Models;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {

        builder.HasDiscriminator<string>("NotificationType")
            .HasValue<Notification>("Base")
            .HasValue<OrderNotification>("Order")
            .HasValue<OfferNotification>("Offer");



        builder.ToTable("Notifications");

    }
}





