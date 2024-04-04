using Application.Interfaces;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Tags.Commands.CreateTags
{
	public class CreateTagsCommandValidator : AbstractValidator<CreateTagsCommand>
	{
		public CreateTagsCommandValidator()
		{
			RuleFor(x => x.TagAmount).GreaterThan(0)
				.WithMessage("Tag amount must be greater than 0");
		}
	}

	public class CreateTagsCommandHandler : IRequestHandler<CreateTagsCommand>
	{
		private readonly IStackExchangeApiClient _stackExchangeApiClient;
		private readonly IApplicationDbContext _context;

		public CreateTagsCommandHandler(IStackExchangeApiClient stackExchangeApiClient, IApplicationDbContext context)
		{
			_stackExchangeApiClient = stackExchangeApiClient;
			_context = context;
		}

		public async Task Handle(CreateTagsCommand command, CancellationToken cancellationToken)
		{
			List<Tag> tags = await _stackExchangeApiClient.GetTags(command.TagAmount);

			foreach (var tag in tags)
			{
				var newTag = new Tag
				{
					Name = tag.Name,
					Count = tag.Count
				};

				_context.Tags.Add(newTag);
			}

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
