using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{

	public class AuthorizeArea
	{

		public AuthorizeArea()
		{
			this.Controllers = new List<AuthorizeController>();
		}
		[Key]
		public Guid Id { get; set; }

		[MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(50)]
		public string Title { get; set; }

		public IList<AuthorizeController> Controllers { get; set; }
	}
}
