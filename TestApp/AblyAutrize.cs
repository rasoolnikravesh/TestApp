using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TestApp
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class AblyAutrize
	{
		private readonly RequestDelegate _next;

		public AblyAutrize(RequestDelegate next)
		{
			_next = next;
		}

		public Task Invoke(HttpContext httpContext)
		{
			var path = httpContext.Request.Path.Value;
			if (path == "/home/test1")
			{
				httpContext.Response.Redirect("/home/test2");

			}

			return _next(httpContext);

		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class AblyAutrizeExtensions
	{
		public static IApplicationBuilder UseAblyAutrize(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<AblyAutrize>();
		}
	}
}
