using DeliveryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Repository.Mapping
{
	public class PlanVersionMap : IEntityTypeConfiguration<PlanVersion>
	{
		public void Configure(EntityTypeBuilder<PlanVersion> builder)
		{
			builder.HasKey(pv => pv.PlanVersionId);
			builder.Property(pv => pv.Price).IsRequired();

			builder.Property(pv => pv.MinDayRent).IsRequired();
			builder.Property(pv => pv.AdditionalCharge).IsRequired();
			builder.Property(pv => pv.DailyLateFee).IsRequired();

			builder.Property(pv => pv.PlanId).IsRequired();
			builder.HasOne(pv => pv.Plans)
				.WithMany()
				.HasForeignKey(pv => pv.PlanId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
