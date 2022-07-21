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
    public class AuthorizeAreasController : Controller
    {
        private readonly DataContext _context;

        public AuthorizeAreasController(DataContext context)
        {
            _context = context;
        }

        // GET: AuthorizeAreas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Areas.ToListAsync());
        }

        // GET: AuthorizeAreas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizeArea = await _context.Areas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorizeArea == null)
            {
                return NotFound();
            }

            return View(authorizeArea);
        }

        // GET: AuthorizeAreas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorizeAreas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Title")] AuthorizeArea authorizeArea)
        {
            if (ModelState.IsValid)
            {
                authorizeArea.Id = Guid.NewGuid();
                _context.Add(authorizeArea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorizeArea);
        }

        // GET: AuthorizeAreas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizeArea = await _context.Areas.FindAsync(id);
            if (authorizeArea == null)
            {
                return NotFound();
            }
            return View(authorizeArea);
        }

        // POST: AuthorizeAreas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Title")] AuthorizeArea authorizeArea)
        {
            if (id != authorizeArea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorizeArea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorizeAreaExists(authorizeArea.Id))
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
            return View(authorizeArea);
        }

        // GET: AuthorizeAreas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizeArea = await _context.Areas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorizeArea == null)
            {
                return NotFound();
            }

            return View(authorizeArea);
        }

        // POST: AuthorizeAreas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var authorizeArea = await _context.Areas.FindAsync(id);
            _context.Areas.Remove(authorizeArea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorizeAreaExists(Guid id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }
    }
}
