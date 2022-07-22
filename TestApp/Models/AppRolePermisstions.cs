using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
	public class AppRolePermissions
	{
		public Guid PermissionId { get; set; }
		public Permission Permission { get; set; }

		public Guid AppRoleId { get; set; }
		public AppRole AppRole { get; set; }
	}
}
