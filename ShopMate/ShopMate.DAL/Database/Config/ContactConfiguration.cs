using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMate.DAL.Database.Models;

public class ContactConfiguration : IEntityTypeConfiguration<ContactMessage>
{
    public void Configure(EntityTypeBuilder<ContactMessage> builder)
    {

        builder.HasKey(c => c.Id);

        builder.Property(m => m.Subject)
          .IsRequired()
          .HasMaxLength(150);

        builder.Property(m => m.Message)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(m => m.Status)
            .HasConversion<string>();

        builder.ToTable("ContactMessages");

    }
}





