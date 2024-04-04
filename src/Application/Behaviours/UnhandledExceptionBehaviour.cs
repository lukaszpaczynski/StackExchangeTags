namespace Application.Behaviours
{
	public class UnhandledExceptionBehaviour<TRequest, TResponse>
	: IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			try
			{
				return await next();
			}
			catch (Exception ex)
			{
				throw new Exception($"{typeof(TRequest).Name}:" + ex.Message);
			}
		}
	}
}
