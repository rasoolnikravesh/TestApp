using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Controllers
{
	public class ActionPermissionsController : Controller
	{
		private readonly DataContext _context;

		public ActionPermissionsController(DataContext context)
		{
			_context = context;
		}

		// GET: ActionPermissions
		public async Task<IActionResult> Index()
		{
			var dataContext = _context.ActionPermissions.Include(a => a.Action);
			return View(await dataContext.ToListAsync());
		}

		// GET: ActionPermissions/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			ActionPermission actionPermission = await _context.ActionPermissions
				.Include(a => a.Action)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (actionPermission == null)
			{
				return NotFound();
			}

			return View(actionPermission);
		}

		// GET: ActionPermissions/Create
		public IActionResult Create()
		{
			ViewData["ActionId"] = new SelectList(_context.Actions, "Id", nameof(ActionPermission.Title));
			ViewData["Levels"] = new List<SelectListItem>
			{
				new SelectListItem
				{
					Value = 0.ToString(),
					Text = "Public",
				},
				new SelectListItem
				{
					Value = 1.ToString(),
					Text = "Private",
				},
				new SelectListItem
				{
					Value = 3.ToString(),
					Text = "Developer",
				}
			};
			return View();
		}

		// POST: ActionPermissions/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ActionId,Id,Name,Title,Level")] ActionPermission actionPermission)
		{
			if (ModelState.IsValid)
			{
				actionPermission.Id = Guid.NewGuid();
				_context.Add(actionPermission);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewData["ActionId"] = new SelectList(_context.Actions, "Id", nameof(ActionPermission.Title),
				actionPermission.ActionId);
			ViewData["Levels"] = new List<SelectListItem>
			{
				new SelectListItem
				{
					Value = 0.ToString(),
					Text = "Public",
				},
				new SelectListItem
				{
					Value = 1.ToString(),
					Text = "Private",
				},
				new SelectListItem
				{
					Value = 3.ToString(),
					Text = "Developer",
				}
			};
			return View(actionPermission);
		}

		// GET: ActionPermissions/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var actionPermission = await _context.ActionPermissions.FindAsync(id);
			if (actionPermission == null)
			{
				return NotFound();
			}
			ViewData["ActionId"] = new SelectList(_context.Actions, "Id", nameof(ActionPermission.Title), actionPermission.ActionId);
			ViewData["Levels"] = new List<SelectListItem>
			{
				new SelectListItem
				{
					Value = 0.ToString(),
					Text = "Public",
				},
				new SelectListItem
				{
					Value = 1.ToString(),
					Text = "Private",
				},
				new SelectListItem
				{
					Value = 3.ToString(),
					Text = "Developer",
				}
			};
			return View(actionPermission);
		}

		// POST: ActionPermissions/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("ActionId,Id,Name,Title,Level")] ActionPermission actionPermission)
		{
			if (id != actionPermission.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(actionPermission);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ActionPermissionExists(actionPermission.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}

			ViewData["ActionId"] = new SelectList(_context.Actions, "Id", nameof(ActionPermission.Title),
				actionPermission.ActionId);
			ViewData["Levels"] = new List<SelectListItem>
			{
				new SelectListItem
				{
					Value = 0.ToString(),
					Text = "Public",
				},
				new SelectListItem
				{
					Value = 1.ToString(),
					Text = "Private",
				},
				new SelectListItem
				{
					Value = 3.ToString(),
					Text = "Developer",
				}
			};
			return View(actionPermission);
		}

		// GET: ActionPermissions/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var actionPermission = await _context.ActionPermissions
				.Include(a => a.Action)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (actionPermission == null)
			{
				return NotFound();
			}

			return View(actionPermission);
		}

		// POST: ActionPermissions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var actionPermission = await _context.ActionPermissions.FindAsync(id);
			_context.ActionPermissions.Remove(actionPermission);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ActionPermissionExists(Guid id)
		{
			return _context.ActionPermissions.Any(e => e.Id == id);
		}
	}
}
