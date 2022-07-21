using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
