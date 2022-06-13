using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinephilia.Data;
using Cinephilia.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cinephilia.Controllers
{
    [Authorize]
    public class BrowseBiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrowseBiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BrowseBies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BrowseBies.Include(b => b.Entertainment).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BrowseBies/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BrowseBies == null)
            {
                return NotFound();
            }

            var browseBy = await _context.BrowseBies
                .Include(b => b.Entertainment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BrowseById == id);
            if (browseBy == null)
            {
                return NotFound();
            }

            return View(browseBy);
        }

        // GET: BrowseBies/Create
        public IActionResult Create()
        {
            ViewData["EntertainmentId"] = new SelectList(_context.Entertainments, "EntertainmentId", "EntertainmentId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: BrowseBies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrowseById,Genre,Language,Rating,UserId,EntertainmentId")] BrowseBy browseBy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(browseBy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntertainmentId"] = new SelectList(_context.Entertainments, "EntertainmentId", "EntertainmentId", browseBy.EntertainmentId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", browseBy.UserId);
            return View(browseBy);
        }

        // GET: BrowseBies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BrowseBies == null)
            {
                return NotFound();
            }

            var browseBy = await _context.BrowseBies.FindAsync(id);
            if (browseBy == null)
            {
                return NotFound();
            }
            ViewData["EntertainmentId"] = new SelectList(_context.Entertainments, "EntertainmentId", "EntertainmentId", browseBy.EntertainmentId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", browseBy.UserId);
            return View(browseBy);
        }

        // POST: BrowseBies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrowseById,Genre,Language,Rating,UserId,EntertainmentId")] BrowseBy browseBy)
        {
            if (id != browseBy.BrowseById)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(browseBy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrowseByExists(browseBy.BrowseById))
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
            ViewData["EntertainmentId"] = new SelectList(_context.Entertainments, "EntertainmentId", "EntertainmentId", browseBy.EntertainmentId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", browseBy.UserId);
            return View(browseBy);
        }

        // GET: BrowseBies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BrowseBies == null)
            {
                return NotFound();
            }

            var browseBy = await _context.BrowseBies
                .Include(b => b.Entertainment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BrowseById == id);
            if (browseBy == null)
            {
                return NotFound();
            }

            return View(browseBy);
        }

        // POST: BrowseBies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BrowseBies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BrowseBies'  is null.");
            }
            var browseBy = await _context.BrowseBies.FindAsync(id);
            if (browseBy != null)
            {
                _context.BrowseBies.Remove(browseBy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrowseByExists(int id)
        {
          return (_context.BrowseBies?.Any(e => e.BrowseById == id)).GetValueOrDefault();
        }
    }
}
