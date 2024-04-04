using Application.Behaviours;
using FluentAssertions;
namespace Application.FunctionalTests.Behaviours
{
	public class UnhandledExceptionBehaviourFunctionalTests
	{
		[Fact]
		public async Task Handle_NoExceptionThrown_ReturnsResponse()
		{
			// Arrange
			var behaviour = new UnhandledExceptionBehaviour<ExampleRequest, ExampleResponse>();
			var request = new ExampleRequest();
			var cancellationToken = new CancellationToken();

			Task<ExampleResponse> Next()
			{
				return Task.FromResult(new ExampleResponse());
			}

			// Act
			var response = await behaviour.Handle(request, Next, cancellationToken);

			// Assert
			response.Should().NotBeNull();
		}

		[Fact]
		public async Task Handle_ExceptionThrown_ReturnsException2()
		{
			// Arrange
			var behaviour = new UnhandledExceptionBehaviour<ExampleRequest, ExampleResponse>();
			var request = new ExampleRequest();
			var cancellationToken = new CancellationToken();
			var expectedExceptionMessage = "Test exception";

			Task<ExampleResponse> Next()
			{
				throw new Exception(expectedExceptionMessage);
			}

			// Act
			Func<Task<ExampleResponse>> act = async () => await behaviour.Handle(request, Next, cancellationToken);

			// Assert
			await act.Should().ThrowAsync<Exception>().WithMessage($"ExampleRequest:{expectedExceptionMessage}");
		}
	}

	public class ExampleRequest { }
	public class ExampleResponse { }
}
