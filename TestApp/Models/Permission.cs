using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
	public class Permission
	{

		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Title { get; set; }

		public PermissionLevel Level { get; set; }


		public IList<AppRolePermissions> AppRolePermissions { get; set; }

	}

}
