using Infrastructure.HttpServices;

namespace Infrastructure.UnitTests.HttpServices
{
	public class StackExchangeApiClientTests
	{
		[Fact]
		public async Task GetTags_ReturnsExpectedNumberOfTags()
		{
			// Arrange
			var stackExchangeApiClient = new StackExchangeApiClient();

			// Act
			var tags = await stackExchangeApiClient.GetTags(10);

			// Assert
			Assert.Equal(10, tags.Count);
		}

		[Fact]
		public async Task GetTags_ReturnsEmptyList_WhenRequestedTagCountIsZero()
		{
			// Arrange
			var stackExchangeApiClient = new StackExchangeApiClient();

			// Act
			var tags = await stackExchangeApiClient.GetTags(0);

			// Assert
			Assert.Empty(tags);
		}
	}
}
