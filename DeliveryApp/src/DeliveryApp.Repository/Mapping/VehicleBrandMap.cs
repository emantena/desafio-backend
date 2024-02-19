using DeliveryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Repository.Mapping
{
	public class VehicleBrandMap : IEntityTypeConfiguration<VehicleBrand>
	{
		public void Configure(EntityTypeBuilder<VehicleBrand> builder)
		{
			builder.HasKey(vb => vb.VehicleBrandId);
			builder.Property(vb => vb.Brand).IsRequired();
			builder.Property(vb => vb.Active).IsRequired();
		}
	}
}
