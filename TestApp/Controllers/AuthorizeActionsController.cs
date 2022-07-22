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
	public class AuthorizeActionsController : Controller
	{
		private readonly DataContext _context;

		public AuthorizeActionsController(DataContext context)
		{
			_context = context;
		}

		// GET: AuthorizeActions
		public async Task<IActionResult> Index()
		{
			var dataContext = _context.Actions.Include(a => a.Controller);
			return View(await dataContext.ToListAsync());
		}

		// GET: AuthorizeActions/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			AuthorizeAction authorizeAction = await _context.Actions
				.Include(a => a.Controller)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (authorizeAction == null)
			{
				return NotFound();
			}

			return View(authorizeAction);
		}

		// GET: AuthorizeActions/Create
		public async Task<IActionResult> Create()
		{
			var listItem = await _context.Controllers.Include(x => x.Area)
				.Select(authorizeController => new SelectListItem
				{
					Value = authorizeController.Id.ToString(),
					Text = $"{authorizeController.Area.Title} - {authorizeController.Title}",
				}).ToListAsync();
			ViewData["ControllerId"] = listItem;
			return View();
		}

		// POST: AuthorizeActions/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Title,ControllerId")] AuthorizeAction authorizeAction)
		{
			if (ModelState.IsValid)
			{

				// ReSharper disable once SpecifyStringComparison
				bool hasAny = _context.Controllers
					.Where(x => x.Name.ToLower() == authorizeAction.Name.ToLower())
					.Any(x => x.AreaId == authorizeAction.ControllerId);
				if (hasAny)
				{
					ModelState.AddModelError("Name", "تکراری");
					return View(authorizeAction);
				}
				authorizeAction.Id = Guid.NewGuid();
				_context.Add(authorizeAction);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			var listItem = await _context.Controllers.Include(x => x.Area)
				.Select(authorizeController => new SelectListItem
				{
					Value = authorizeController.Id.ToString(),
					Text = $"{authorizeController.Area.Title} - {authorizeController.Title}",
					Selected = authorizeAction.ControllerId == authorizeController.Id,
				})
				.ToListAsync();
			ViewData["ControllerId"] = listItem;
			return View(authorizeAction);
		}

		// GET: AuthorizeActions/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var authorizeAction = await _context.Actions.FindAsync(id);
			if (authorizeAction == null)
			{
				return NotFound();
			}

			var listItem = await _context.Controllers.Include(x => x.Area)
				.Select(authorizeController => new SelectListItem
				{
					Value = authorizeController.Id.ToString(),
					Text = $"{authorizeController.Area.Title} - {authorizeController.Title}",
					Selected = authorizeAction.ControllerId == authorizeController.Id,
				})
				.ToListAsync();
			ViewData["ControllerId"] = listItem; return View(authorizeAction);
		}

		// POST: AuthorizeActions/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Title,ControllerId")] AuthorizeAction authorizeAction)
		{
			if (id != authorizeAction.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					bool hasAny = _context.Controllers
						.Where(x => x.Name.ToLower() == authorizeAction.Name.ToLower())
						.Any(x => x.AreaId == authorizeAction.ControllerId);
					if (hasAny)
					{
						ModelState.AddModelError("Name", "تکراری");
						return View(authorizeAction);
					}

					_context.Update(authorizeAction);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AuthorizeActionExists(authorizeAction.Id))
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

			var listItem = await _context.Controllers.Include(x => x.Area)
				.Select(authorizeController => new SelectListItem
				{
					Value = authorizeController.Id.ToString(),
					Text = $"{authorizeController.Area.Title} - {authorizeController.Title}",
					Selected = authorizeAction.ControllerId == authorizeController.Id,
				})
				.ToListAsync();
			ViewData["ControllerId"] = listItem;
			return View(authorizeAction);
		}

		// GET: AuthorizeActions/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var authorizeAction = await _context.Actions
				.Include(a => a.Controller)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (authorizeAction == null)
			{
				return NotFound();
			}

			return View(authorizeAction);
		}

		// POST: AuthorizeActions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var authorizeAction = await _context.Actions.FindAsync(id);
			_context.Actions.Remove(authorizeAction);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool AuthorizeActionExists(Guid id)
		{
			return _context.Actions.Any(e => e.Id == id);
		}
	}
}
