using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Models
{
	public class ActionPermission : Permission
	{
		public ActionPermission()
		{
			this.AppRolePermissions = new List<AppRolePermissions>();
		}

		public Guid ActionId { get; set; }

		public AuthorizeAction Action { get; set; }

	}


}
