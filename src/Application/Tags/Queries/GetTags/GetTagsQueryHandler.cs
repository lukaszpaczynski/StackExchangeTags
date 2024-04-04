using Application.Interfaces;
using Application.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Application.Tags.Queries.GetTags
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, GetTagsResult>
	{
		private readonly IApplicationDbContext _context;

        public GetTagsQueryHandler(IApplicationDbContext context)
        {
			_context = context;
        }

        public async Task<GetTagsResult> Handle(GetTagsQuery query, CancellationToken cancellationToken)
        {
			var tags = await _context.Tags.ToListAsync();

			int totalPopulation = tags.Sum(t => t.Count);
			var populatedTags = tags.Select(t =>
			{
				return new TagDto
				{
					Name = t.Name,
					PopulationPercentage = Math.Ceiling((double)t.Count / totalPopulation * 100 * 100) / 100
				};
			}).OrderBy(tag => tag.Name);

			return new GetTagsResult(new PaginatedResult<TagDto>(populatedTags));
		}
    }
}
