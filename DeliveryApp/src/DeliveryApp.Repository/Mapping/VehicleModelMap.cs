using DeliveryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Repository.Mapping
{
	public class VehicleModelMap : IEntityTypeConfiguration<VehicleModel>
	{
		public void Configure(EntityTypeBuilder<VehicleModel> builder)
		{
			builder.HasKey(vm => vm.VehicleModelId);
			builder.Property(vm => vm.Model).IsRequired();
			builder.Property(vm => vm.Active).IsRequired();

			builder.HasOne(vm => vm.VehicleBrand)
				.WithMany()
				.HasForeignKey(vm => vm.VehicleBrandId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
