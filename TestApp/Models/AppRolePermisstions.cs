using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
	public class AppRolePermisstions
	{
		public Guid PermisstionId { get; set; }
		public Permisstion Permisstion { get; set; }

		public Guid AppRoleId { get; set; }
		public AppRole AppRole { get; set; }
	}
}
