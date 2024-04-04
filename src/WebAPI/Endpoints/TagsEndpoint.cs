using Application.Tags.Commands.CreateTags;
using Application.Tags.Queries.GetTags;

namespace WebAPI.Endpoints
{
	public class TagsEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/tags", async (ISender sender) =>
			{
				var result = await sender.Send(new GetTagsQuery());

				var response = result.Adapt<GetTagsResult>();

				return response is null ? Results.NotFound() : Results.Ok(response);
			});

			app.MapPost("/tags/{amount=1000}", async (int amount, ISender sender) =>
			{
				await sender.Send(new CreateTagsCommand(amount));

				return Results.NoContent();
			});
		}
	}
}
