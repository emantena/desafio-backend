using DeliveryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Repository.Mapping
{
	public class OrderMap : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(o => o.OrderId);

			builder.Property(o => o.OrderId).IsRequired();
			builder.Property(o => o.UserId).IsRequired();
			builder.Property(o => o.OrderStatusId).IsRequired();
			builder.Property(o => o.CreateAt).IsRequired();
			builder.Property(o => o.RacePrice).IsRequired();
			builder.Property(o => o.DeliveryManId).IsRequired();


			builder.HasOne(o => o.User)
							.WithMany()
							.HasForeignKey(o => o.UserId)
							.OnDelete(DeleteBehavior.NoAction);

			builder.Ignore(v => v.IsValid);
			builder.Ignore(v => v.Notifications);
		}
	}
}
