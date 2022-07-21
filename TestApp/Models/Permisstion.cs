using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Models
{
	public class Permisstion
	{
		public Permisstion()
		{
			this.AppRolePermisstions = new List<AppRolePermisstions>();
		}
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Title { get; set; }
		public PermisstionLevel Level { get; set; }


		public Guid ActionId { get; set; }
		public AuthorizeAction Action { get; set; }

		public IList<AppRolePermisstions> AppRolePermisstions { get; set; }
	}

	public enum PermisstionLevel
	{
		Public = 0,
		Private = 1,
		Developer = 3,
	}
}
