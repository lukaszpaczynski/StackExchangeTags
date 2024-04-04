using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IStackExchangeApiClient
    {
		Task<List<Tag>> GetTags(int expectedTagCount);
	}
}
