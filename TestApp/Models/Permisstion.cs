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
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Title { get; set; }

		public Guid AreaId { get; set; }
		public Area Area { get; set; }

		public Guid ControllerId { get; set; }
		public Controller Controller { get; set; }

		public Guid ActionId { get; set; }
		public Action Action { get; set; }

		public PermisstionLevel Level { get; set; }

	}

	public enum PermisstionLevel
	{
		Public = 0,
		Private = 1,
		Developer = 3,
	}
}
