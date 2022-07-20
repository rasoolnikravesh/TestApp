using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
	public class DataContext : IdentityDbContext<AppUser, AppRole, Guid>
	{

		public DataContext()
		{

		}
		public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
		{
			Database.EnsureCreated();
		}

		public DbSet<Controller> Controllers { get; set; }

		public DbSet<Permisstion> Permisstions { get; set; }

		public DbSet<Action> Actions { get; set; }

		public DbSet<Area> Areas { get; set; }
	}
}
