using Application.Interfaces;
using Application.Tags.Commands.CreateTags;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Tags.Commands.CreateTags
{
	public class CreateTagsCommandHandlerTests
	{
		[Fact]
		public async Task Handle_ValidCommand_AddsTagsToContextAndSavesChanges()
		{
			// Arrange
			var mockApiClient = new Mock<IStackExchangeApiClient>();
			var mockContext = new Mock<IApplicationDbContext>();

			var tags = new List<Tag>
			{
				new Tag { Name = "Tag1", Count = 10 },
				new Tag { Name = "Tag2", Count = 20 }
			};

			var mockDbSet = new Mock<DbSet<Tag>>();
			mockDbSet.Setup(x => x.Add(It.IsAny<Tag>())).Verifiable();
			mockContext.Setup(x => x.Tags).Returns(mockDbSet.Object);

			mockApiClient.Setup(x => x.GetTags(It.IsAny<int>())).ReturnsAsync(tags);

			var handler = new CreateTagsCommandHandler(mockApiClient.Object, mockContext.Object);
			var command = new CreateTagsCommand(TagAmount: 2);

			// Act
			await handler.Handle(command, CancellationToken.None);

			// Assert
			mockDbSet.Verify(x => x.Add(It.IsAny<Tag>()), Times.Exactly(2));
			mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
		}
	}
}
