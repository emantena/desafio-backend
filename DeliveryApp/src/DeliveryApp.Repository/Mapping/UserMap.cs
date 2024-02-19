using DeliveryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Repository.Mapping
{
	public class UserMap : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.UserId);

			builder.Property(u => u.UserId);
			builder.Property(u => u.RoleId).IsRequired();
			builder.Property(u => u.Active).IsRequired();
			builder.Property(u => u.Name).IsRequired();
			builder.Property(u => u.Email).IsRequired();
			builder.Property(u => u.Password).IsRequired();
			builder.Property(u => u.CreateAt).IsRequired();

			builder.Ignore(v => v.IsValid);
			builder.Ignore(v => v.Notifications);
		}
	}
}
