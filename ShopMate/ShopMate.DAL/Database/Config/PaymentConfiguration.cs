using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMate.DAL.Database.Models;

public class PaymentConfiguration : IEntityTypeConfiguration<PaymentDetails>
{
    public void Configure(EntityTypeBuilder<PaymentDetails> builder)
    {
        builder.HasKey(p => p.Id);


        builder.Property(p => p.PaymentMethod)
              .IsRequired()
              .HasConversion<string>();

        builder.Property(p => p.TotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.ToTable("PaymentDetails");

    }
}


