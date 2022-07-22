using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			string con = Configuration.GetConnectionString("AppCon");
			services.AddDbContext<DataContext>(op =>
			{
				op.UseSqlServer(con);
			});
			services.AddIdentity<AppUser, AppRole>(x =>
				{
					x.Password.RequireUppercase = false;
					x.Password.RequireLowercase = false;
					x.Password.RequireDigit = false;
					x.Password.RequiredLength = 6;
					x.Password.RequireNonAlphanumeric = false;
					x.SignIn.RequireConfirmedAccount = false;
					x.SignIn.RequireConfirmedEmail = false;
					
				}).AddEntityFrameworkStores<DataContext>()
			 .AddDefaultTokenProviders();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthorization();
			app.UseAblyAuthorize();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapAreaControllerRoute(
					name: "Identity",
					areaName: "Identity",
					pattern: "auth/{controller=User}/{action=Login}/{id?}"
				);

				endpoints.MapAreaControllerRoute(
					name: "Authorize",
					areaName: "Authorize",
					pattern: "Authorize/{controller=Home}/{action=Index}/{id?}"
				);

				endpoints.MapControllerRoute(
					name: "areaRoute",
					pattern: "{area:exists}/{controller}/{action}"
				);

				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}"
				);
			});
			SeedUser(userManager, roleManager).Wait();

		}

		private static async Task SeedUser(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			AppUser user = await userManager.FindByEmailAsync("root@localhost");
			if (user == null)
			{
				AppUser appuser = new AppUser()
				{
					UserName = "Root",
					Email = "root@localhost",
					EmailConfirmed = true,
				};
				await userManager.CreateAsync(appuser, "123456");
				await roleManager.CreateAsync(new AppRole()
				{
					Name = "Developer",
				});

				bool result = await userManager.IsInRoleAsync(appuser, "Developer");

				if (!result)
				{
					await userManager.AddToRoleAsync(appuser, "Developer");
				}
			}
		}
	}
}
