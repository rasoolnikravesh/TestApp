using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
	public class ControllerPermission : Permission
	{
		public ControllerPermission()
		{
			this.AppRolePermissions = new List<AppRolePermissions>();
		}

		public Guid ControllerId { get; set; }

		public AuthorizeController Controller { get; set; }
	}
}
