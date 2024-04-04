using Application.Interfaces;
using Domain.Entities;
using System.Reflection;

namespace Infrastructure.Database
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		public DbSet<Tag> Tags => Set<Tag>();

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
