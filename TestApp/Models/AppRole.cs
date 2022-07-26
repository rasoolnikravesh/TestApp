﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
	public class AppRole : IdentityRole<Guid>
	{
		public AppRole()
		{
			AppRolePermissions = new List<AppRolePermissions>();
		}
		public IList<AppRolePermissions> AppRolePermissions { get; set; }
	}
}
