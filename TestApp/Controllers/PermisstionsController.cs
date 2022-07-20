using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test = Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Controllers
{
	public class PermisstionsController : test.Controller
	{
		private readonly DataContext _context;

		public PermisstionsController(DataContext context)
		{
			_context = context;
		}

		// GET: Permisstions
		public async Task<IActionResult> Index()
		{
			var dataContext = _context.Permisstions.Include(p => p.Action).Include(p => p.Area).Include(p => p.Controller);
			return View(await dataContext.ToListAsync());
		}

		// GET: Permisstions/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var permisstion = await _context.Permisstions
				.Include(p => p.Action)
				.Include(p => p.Area)
				.Include(p => p.Controller)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (permisstion == null)
			{
				return NotFound();
			}

			return View(permisstion);
		}

		// GET: Permisstions/Create
		public IActionResult Create()
		{
			ViewData["ActionId"] = new SelectList(_context.Actions, "Id", "Id");
			ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id");
			ViewData["ControllerId"] = new SelectList(_context.Controllers, "Id", "Id");
			return View();
		}

		// POST: Permisstions/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Title,AreaId,ControllerId,ActionId,Level")] Permisstion permisstion)
		{
			if (ModelState.IsValid)
			{
				permisstion.Id = Guid.NewGuid();
				_context.Add(permisstion);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["ActionId"] = new SelectList(_context.Actions, "Id", "Id", permisstion.ActionId);
			ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", permisstion.AreaId);
			ViewData["ControllerId"] = new SelectList(_context.Controllers, "Id", "Id", permisstion.ControllerId);
			return View(permisstion);
		}

		// GET: Permisstions/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var permisstion = await _context.Permisstions.FindAsync(id);
			if (permisstion == null)
			{
				return NotFound();
			}
			ViewData["ActionId"] = new SelectList(_context.Actions, "Id", "Id", permisstion.ActionId);
			ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", permisstion.AreaId);
			ViewData["ControllerId"] = new SelectList(_context.Controllers, "Id", "Id", permisstion.ControllerId);
			return View(permisstion);
		}

		// POST: Permisstions/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Title,AreaId,ControllerId,ActionId,Level")] Permisstion permisstion)
		{
			if (id != permisstion.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(permisstion);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PermisstionExists(permisstion.Id))
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
			ViewData["ActionId"] = new SelectList(_context.Actions, "Id", "Id", permisstion.ActionId);
			ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", permisstion.AreaId);
			ViewData["ControllerId"] = new SelectList(_context.Controllers, "Id", "Id", permisstion.ControllerId);
			return View(permisstion);
		}

		// GET: Permisstions/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var permisstion = await _context.Permisstions
				.Include(p => p.Action)
				.Include(p => p.Area)
				.Include(p => p.Controller)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (permisstion == null)
			{
				return NotFound();
			}

			return View(permisstion);
		}

		// POST: Permisstions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var permisstion = await _context.Permisstions.FindAsync(id);
			_context.Permisstions.Remove(permisstion);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PermisstionExists(Guid id)
		{
			return _context.Permisstions.Any(e => e.Id == id);
		}
	}
}
