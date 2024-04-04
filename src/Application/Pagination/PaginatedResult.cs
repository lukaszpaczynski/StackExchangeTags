namespace Application.Pagination
{
	public class PaginatedResult<TEntity>
	(IEnumerable<TEntity> data)
	where TEntity : class
	{
		public IEnumerable<TEntity> Data { get; } = data;
	}
}
