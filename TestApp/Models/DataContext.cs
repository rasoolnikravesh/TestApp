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

		public DbSet<AuthorizeController> Controllers { get; set; }

		public DbSet<Permisstion> Permisstions { get; set; }

		public DbSet<AuthorizeAction> Actions { get; set; }

		public DbSet<AuthorizeArea> Areas { get; set; }
		

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<AuthorizeArea>()
				.HasMany(x => x.Controllers)
				.WithOne(x => x.Area)
				.HasForeignKey(x => x.AreaId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AuthorizeArea>()
				.HasIndex(x => x.Name).IsUnique();

			builder.Entity<AuthorizeController>()
				.HasMany(x => x.Actions)
				.WithOne(x => x.Controller)
				.HasForeignKey(x => x.ControlerId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);


			builder.Entity<AuthorizeController>()
				.HasIndex(x => x.Name).IsUnique();


			builder.Entity<AuthorizeAction>()
				.HasIndex(x => x.Name).IsUnique();

			builder.Entity<Permisstion>()
				.HasOne(x => x.Action)
				.WithOne(x => x.Permisstion)
				.HasForeignKey<Permisstion>(x => x.ActionId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);


			builder.Entity<Permisstion>().HasMany(x => x.AppRolePermisstions)
				.WithOne(x => x.Permisstion).HasForeignKey(x => x.PermisstionId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AppRole>()
				.HasMany(x => x.AppRolePermisstions)
				.WithOne(x => x.AppRole)
				.HasForeignKey(x => x.AppRoleId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AppRolePermisstions>()
				.HasKey(x => new { x.AppRoleId, x.PermisstionId }).IsClustered();
		}
	}
}
