using DeliveryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Repository.Mapping
{
	public class CnhTypeMap : IEntityTypeConfiguration<CnhType>
	{
		public void Configure(EntityTypeBuilder<CnhType> builder)
		{
			builder.HasKey(ct => ct.CnhTypeId);
			builder.Property(ct => ct.Type).IsRequired();
			builder.Property(ct => ct.Description).IsRequired();
		}
	}
}
