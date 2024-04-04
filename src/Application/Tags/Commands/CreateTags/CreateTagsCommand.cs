using Application.Pagination;

namespace Application.Tags.Commands.CreateTags
{
	public record CreateTagsCommand(int TagAmount) : IRequest;
}
