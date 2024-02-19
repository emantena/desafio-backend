using DeliveryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Repository.Mapping
{
	public class VehicleRentMap : IEntityTypeConfiguration<VehicleRent>
	{
		public void Configure(EntityTypeBuilder<VehicleRent> builder)
		{
			builder.HasKey(vr => vr.VehicleRentId);
			builder.Property(vr => vr.VehicleId).IsRequired();
			builder.Property(vr => vr.PlanVersionId).IsRequired();
			builder.Property(vr => vr.DeliverymanId).IsRequired();

			builder.Property(vr => vr.StartRent).IsRequired();
			builder.Property(vr => vr.PrevisionEndRent).IsRequired();
			builder.Property(vr => vr.EndRent);

			builder.HasOne(vr => vr.Vehicle)
				.WithMany()
				.HasForeignKey(vr => vr.VehicleId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(vr => vr.PlanVersion)
				.WithMany()
				.HasForeignKey(vr => vr.PlanVersionId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(vr => vr.Deliveryman)
				.WithMany()
				.HasForeignKey(vr => vr.DeliverymanId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
