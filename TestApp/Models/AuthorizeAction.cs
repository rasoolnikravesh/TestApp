using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
	public class AuthorizeAction : Base.Entity
	{

		[MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(50)]
		public string Title { get; set; }

		public AuthorizeController Controller { get; set; }

		public Guid ControllerId { get; set; }

		public ActionPermission Permission { get; set; }
	}
}
