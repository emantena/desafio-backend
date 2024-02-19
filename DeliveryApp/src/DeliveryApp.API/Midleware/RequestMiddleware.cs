using DeliveryApp.Domain.ValueObjects;

namespace DeliveryApp.API.Midleware
{
	public class RequestMiddleware
	{
		private readonly RequestDelegate _next;

		public RequestMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			_ = int.TryParse(httpContext.User.Claims
								.FirstOrDefault(c => c.Type == "userId")
								?.Value, out var userId);

			_ = int.TryParse(httpContext.User.Claims
								.FirstOrDefault(c => c.Type == "userId")
								?.Value, out var partnerId);

			Context.UserId = userId;
			Context.CorrelationId = Guid.NewGuid();

			await _next(httpContext); // calling next middleware

		}
	}
}
