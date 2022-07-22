using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class AblyAuthorize
	{
		private readonly RequestDelegate _next;

		public AblyAuthorize(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext, DataContext dbContext)
		{
			//string path = httpContext.Request.Path.Value;
			//RouteValueDictionary routeDictionary = httpContext.Request.RouteValues;
			//var actionPermissions = await dbContext.ActionPermissions.ToListAsync();
			//var controllerPermissions = await dbContext.ControllerPermissions.ToListAsync();

			//if (routeDictionary.ContainsKey("area"))
			//{
			//	object area = routeDictionary["area"];
			//	object controller = routeDictionary["controller"];
			//	object action = routeDictionary["action"];
			//	foreach (ControllerPermission controllerPermission in controllerPermissions)
			//	{
			//		if (string.Equals(controllerPermission.Name,
			//			    controller.ToString()!, StringComparison.CurrentCultureIgnoreCase))
			//		{

			//		}
			//	}

			//}
			//else
			//{
			//	object controller = routeDictionary["controller"];
			//	object action = routeDictionary["action"];


			//}
			//if (path == "/home/test1")
			//{
			//	httpContext.Response.Redirect("/home/test2");

			//}

			await _next(httpContext);

		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class AblyAuthorizeExtensions
	{
		public static IApplicationBuilder UseAblyAuthorize(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<AblyAuthorize>();
		}
	}
}
