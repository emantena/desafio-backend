using DeliveryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Repository.Mapping
{
	public class PlansMap : IEntityTypeConfiguration<Plans>
	{
		public void Configure(EntityTypeBuilder<Plans> builder)
		{
			builder.HasKey(p => p.PlanId);
			builder.Property(p => p.Name).IsRequired();
			builder.Property(p => p.Description).IsRequired();
		}
	}
}
