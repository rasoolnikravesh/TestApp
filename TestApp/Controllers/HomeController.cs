using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Controllers
{
	public class HomeController : Microsoft.AspNetCore.Mvc.Controller
	{
		private readonly ILogger<HomeController> _logger;

		public DataContext DataContext { get; }

		public HomeController(ILogger<HomeController> logger, DataContext dataContext)
		{
			_logger = logger;
			DataContext = dataContext;
		}

		public IActionResult Index()
		{
			var perm = DataContext.Permisstions.ToList();
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult Test1()
		{
			return View();
		}

		public IActionResult Test2()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
