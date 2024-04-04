using Domain.Entities;

namespace Domain.UnitTests.Entities
{
	public class TagTests
	{
		[Fact]
		public void Tag_Creation_Succeeds()
		{
			// Arrange
			int id = 1;
			string name = "TestTag";
			int count = 10;

			// Act
			var tag = new Tag
			{
				Id = id,
				Name = name,
				Count = count
			};

			// Assert
			Assert.NotNull(tag);
			Assert.Equal(id, tag.Id);
			Assert.Equal(name, tag.Name);
			Assert.Equal(count, tag.Count);
		}
	}
}
