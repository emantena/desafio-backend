using DeliveryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Repository.Mapping
{
	public class DeliverymanMap : IEntityTypeConfiguration<DeliveryMan>
	{
		public void Configure(EntityTypeBuilder<DeliveryMan> builder)
		{
			builder.HasKey(d => d.DeliveryManId);
			builder.Property(d => d.DeliveryManId).IsRequired();
			builder.Property(d => d.Name).IsRequired();
			builder.Property(d => d.BirthDate).IsRequired();
			builder.Property(d => d.CNH).IsRequired();
			builder.Property(d => d.CNPJ).IsRequired();
			builder.Property(d => d.CnhTypeId).IsRequired();
			builder.Property(d => d.UserId);
			builder.Property(d => d.CnhImage);

			builder.Ignore(v => v.IsValid);
			builder.Ignore(v => v.Notifications);
		}
	}
}
