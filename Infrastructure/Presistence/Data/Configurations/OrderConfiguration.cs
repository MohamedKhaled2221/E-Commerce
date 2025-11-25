using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    #region Part 2 Order Module Configuration
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, address => address.WithOwner());

            builder.HasMany(o => o.OrderItems)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.PaymentStatus)
                   .HasConversion(
                        status => status.ToString(),
                        value => Enum.Parse<OderPaymentStatus>(value)
                   );
            builder.HasOne(o => o.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(o => o.DeliveryMethodId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(o => o.Subtotal)
                   .HasColumnType("decimal(18,2)");
        }
    } 
    #endregion
}
