using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class TagConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.HasKey(t => t.Id);
			builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
			builder.Property(t => t.Count).IsRequired();
		}
	}
}
