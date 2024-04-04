using Domain.Abstractions;
using Domain.Entities;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Infrastructure.HttpServices
{
	public class StackExchangeApiClient : IStackExchangeApiClient
	{
		private readonly string _baseUrl = "https://api.stackexchange.com/2.3/";
		private readonly HttpClient _httpClient;

		public StackExchangeApiClient()
		{
			_httpClient = new HttpClient(new HttpClientHandler
			{
				AutomaticDecompression = DecompressionMethods.GZip
			});
		}

		public async Task<List<Tag>> GetTags(int expectedTagCount)
		{
			List<Tag> tags = new List<Tag>();

			for (int pageIndex = 1; tags.Count < expectedTagCount; pageIndex++)
			{
				var response = await _httpClient.GetAsync(_baseUrl + $"tags?page={pageIndex}&pagesize=100&order=desc&sort=popular&site=stackoverflow");

				if (response.IsSuccessStatusCode)
				{
					var parsedResponse = JObject.Parse(await response.Content.ReadAsStringAsync()).ToObject<StackExchangeResponse>();
					tags.AddRange(parsedResponse.Items);
				}
			}

			return tags.Take(expectedTagCount).ToList();
		}
	}
}
