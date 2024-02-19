using DeliveryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Repository.Mapping
{
	public class VehicleMap : IEntityTypeConfiguration<Vehicle>
	{
		public void Configure(EntityTypeBuilder<Vehicle> builder)
		{
			builder.HasKey(v => v.VehicleId);
			builder.HasKey(v => v.VehicleId);
			builder.Property(v => v.Plate).IsRequired();
			builder.Property(v => v.YearManufacture).IsRequired();
			builder.Property(v => v.CreateAt).IsRequired();
			builder.Property(v => v.IsRent).IsRequired();

			builder.HasOne(v => v.VehicleModel)
				.WithMany()
				.HasForeignKey(v => v.VehicleModelId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Ignore(v => v.IsValid);
			builder.Ignore(v => v.Notifications);
		}
	}
}
