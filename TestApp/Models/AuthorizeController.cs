using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
	public class AuthorizeController : Base.Entity
	{
		public AuthorizeController()
		{
			this.Actions = new List<AuthorizeAction>();
		}

		[MaxLength(50)]
		public string Name { get; set; }
		[MaxLength(50)]
		public string Title { get; set; }

		public AuthorizeArea Area { get; set; }

		public Guid? AreaId { get; set; }

		public IList<AuthorizeAction> Actions { get; set; }

		public ControllerPermission Permission { get; set; }
	}
}
