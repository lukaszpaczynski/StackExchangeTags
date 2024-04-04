using Application.Pagination;

namespace Application.Tags.Queries.GetTags
{
	public record GetTagsQuery() : IRequest<GetTagsResult>;
	public record GetTagsResult(PaginatedResult<TagDto> Tags);
}
