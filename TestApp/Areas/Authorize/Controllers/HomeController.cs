using Microsoft.AspNetCore.Mvc;

namespace TestApp.Areas.Authorize.Controllers
{
	[Area("Authorize")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
