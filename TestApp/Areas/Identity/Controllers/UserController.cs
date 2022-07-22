using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace TestApp.Areas.Identity.Controllers
{
	[Area("Identity")]
	public class UserController : Controller
	{
		public SignInManager<AppUser> SignInManager { get; }
		public UserManager<AppUser> UserManager { get; }

		public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			SignInManager = signInManager;
			UserManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> Login()
		{
			return await Task.Run(View);

		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			AppUser user = await UserManager.FindByEmailAsync(model.Email);
			if (user == null) return View(model);
			SignInResult result = await SignInManager.PasswordSignInAsync(user, model.Password, true, true);
			if (result.Succeeded)
			{
				return Redirect("/");
			}

			return View(model);

		}


	}
}
