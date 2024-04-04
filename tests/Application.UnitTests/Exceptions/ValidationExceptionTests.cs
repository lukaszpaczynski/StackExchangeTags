using Application.Exceptions;
using FluentValidation.Results;

namespace Application.UnitTests.Exceptions
{
	public class ValidationExceptionTests
	{
		[Fact]
		public void Constructor_WithoutParameters_InitializesErrorsAsEmptyDictionary()
		{
			// Arrange & Act
			var exception = new ValidationException();

			// Assert
			Assert.NotNull(exception.Errors);
			Assert.Empty(exception.Errors);
		}

		[Fact]
		public void Constructor_WithFailures_InitializesErrorsWithFailureDetails()
		{
			// Arrange
			var failures = new List<ValidationFailure>
			{
				new ValidationFailure("Age", "must be over 18"),
				new ValidationFailure("Password", "must contain at least 8 characters"),
				new ValidationFailure("Password", "must contain a digit"),
				new ValidationFailure("Name", "must contain at least 2 characters")
			};

			// Act
			var exception = new ValidationException(failures);

			// Assert
			Assert.NotNull(exception.Errors);
			Assert.Equal(3, exception.Errors.Count);
			Assert.Equal(1, exception.Errors["Age"].Length);
			Assert.Equal(2, exception.Errors["Password"].Length);
			Assert.Equal(1, exception.Errors["Name"].Length);
			Assert.Contains("must be over 18", exception.Errors["Age"]);
			Assert.Contains("must contain at least 8 characters", exception.Errors["Password"]);
			Assert.Contains("must contain a digit", exception.Errors["Password"]);
			Assert.Contains("must contain at least 2 characters", exception.Errors["Name"]);
		}
	}
}
