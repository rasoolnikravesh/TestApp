using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
	public sealed class DataContext : IdentityDbContext<AppUser, AppRole, Guid>
	{

		public DataContext()
		{

		}
		public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
		{
			Database.EnsureCreated();
		}

		public DbSet<AuthorizeController> Controllers { get; set; }

		public DbSet<Permission> Permissions { get; set; }

		public DbSet<ActionPermission> ActionPermissions { get; set; }

		public DbSet<ControllerPermission> ControllerPermissions { get; set; }

		public DbSet<AppRolePermissions> AppRolePermissions { get; set; }

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
				.HasForeignKey(x => x.ControllerId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);


			builder.Entity<AuthorizeController>()
				.HasIndex(x => new { x.Name, x.AreaId }).IsUnique();

			builder.Entity<AuthorizeAction>()
				.HasIndex(x => new { x.Name, x.ControllerId }).IsUnique();

			builder.Entity<ActionPermission>()
				.HasOne(x => x.Action)
				.WithOne(x => x.Permission)
				.HasForeignKey<ActionPermission>(x => x.ActionId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ActionPermission>()
				.HasBaseType<Permission>();

			builder.Entity<ControllerPermission>()
				.HasOne(x => x.Controller)
				.WithOne(x => x.Permission)
				.HasForeignKey<ControllerPermission>(x => x.ControllerId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ControllerPermission>()
				.HasBaseType<Permission>();

			builder.Entity<Permission>()
				.HasMany(x => x.AppRolePermissions)
				.WithOne(x => x.Permission)
				.HasForeignKey(x => x.PermissionId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			

			builder.Entity<AppRole>()
				.HasMany(x => x.AppRolePermissions)
				.WithOne(x => x.AppRole)
				.HasForeignKey(x => x.AppRoleId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AppRolePermissions>()
				.HasKey(x => new { x.AppRoleId, x.PermissionId }).IsClustered();
		}
	}
}
