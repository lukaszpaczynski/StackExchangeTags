using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
	public interface IApplicationDbContext
	{
		DbSet<Tag> Tags { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
