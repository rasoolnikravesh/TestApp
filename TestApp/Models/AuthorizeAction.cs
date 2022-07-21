using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
	public class AuthorizeAction
	{


		[Key]
		public Guid Id { get; set; }

		[MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(50)]
		public string Title { get; set; }

		public AuthorizeController Controller { get; set; }

		public Guid ControlerId { get; set; }

		public Permisstion Permisstion { get; set; }
	}
}
