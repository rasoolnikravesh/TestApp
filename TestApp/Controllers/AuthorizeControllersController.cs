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
	public class AuthorizeControllersController : Controller
	{
		private readonly DataContext _context;

		public AuthorizeControllersController(DataContext context)
		{
			_context = context;
		}

		// GET: AuthorizeControllers
		public async Task<IActionResult> Index()
		{
			var dataContext = _context.Controllers.Include(a => a.Area);
			return View(await dataContext.ToListAsync());
		}

		// GET: AuthorizeControllers/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			AuthorizeController authorizeController = await _context.Controllers
				.Include(a => a.Area)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (authorizeController == null)
			{
				return NotFound();
			}

			return View(authorizeController);
		}

		// GET: AuthorizeControllers/Create
		public IActionResult Create()
		{
			ViewData["AreaId"] = new SelectList(_context.Areas, nameof(AuthorizeController.Id),
				nameof(AuthorizeController.Title));
			return View();
		}

		// POST: AuthorizeControllers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Title,AreaId")] AuthorizeController authorizeController)
		{
			if (ModelState.IsValid)
			{
				authorizeController.Id = Guid.NewGuid();

				// ReSharper disable once SpecifyStringComparison
				bool hasAny = _context.Controllers
					.Where(x => x.Name.ToLower() == authorizeController.Name.ToLower())
					.Any(x => x.AreaId == authorizeController.AreaId);
				if (hasAny) 
				{
					ModelState.AddModelError(authorizeController.Name,"نام تکراری است");
					return View(authorizeController);
					
				}
				_context.Add(authorizeController);
				await _context.SaveChangesAsync();
			}
			ViewData["AreaId"] = new SelectList(_context.Areas, nameof(AuthorizeController.Id),
				nameof(AuthorizeController.Title), authorizeController.AreaId);
			return View(authorizeController);
		}

		// GET: AuthorizeControllers/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var authorizeController = await _context.Controllers.FindAsync(id);
			if (authorizeController == null)
			{
				return NotFound();
			}
			ViewData["AreaId"] = new SelectList(_context.Areas, nameof(AuthorizeController.Id), nameof(AuthorizeController.Title), authorizeController.AreaId);
			return View(authorizeController);
		}

		// POST: AuthorizeControllers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Title,AreaId")] AuthorizeController authorizeController)
		{
			if (id != authorizeController.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{

					// ReSharper disable once SpecifyStringComparison
					bool hasAny = _context.Controllers
						.Where(x => x.Name.ToLower() == authorizeController.Name.ToLower())
						.Any(x => x.AreaId == authorizeController.AreaId);
					if (hasAny) return RedirectToAction(nameof(Index));
					_context.Update(authorizeController);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AuthorizeControllerExists(authorizeController.Id))
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
			ViewData["AreaId"] = new SelectList(_context.Areas, nameof(AuthorizeController.Id),
				nameof(AuthorizeController.Title), authorizeController.AreaId);
			return View(authorizeController);
		}

		// GET: AuthorizeControllers/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			AuthorizeController authorizeController = await _context.Controllers
				.Include(a => a.Area)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (authorizeController == null)
			{
				return NotFound();
			}

			return View(authorizeController);
		}

		// POST: AuthorizeControllers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var authorizeController = await _context.Controllers.FindAsync(id);
			_context.Controllers.Remove(authorizeController);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool AuthorizeControllerExists(Guid id)
		{
			return _context.Controllers.Any(e => e.Id == id);
		}
	}
}
