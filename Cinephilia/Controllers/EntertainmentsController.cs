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
    public class EntertainmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntertainmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Entertainments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Entertainments.Include(e => e.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Entertainments/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entertainments == null)
            {
                return NotFound();
            }

            var entertainment = await _context.Entertainments
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EntertainmentId == id);
            if (entertainment == null)
            {
                return NotFound();
            }

            return View(entertainment);
        }

        // GET: Entertainments/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Entertainments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntertainmentId,Category,UserId")] Entertainment entertainment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entertainment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", entertainment.UserId);
            return View(entertainment);
        }

        // GET: Entertainments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entertainments == null)
            {
                return NotFound();
            }

            var entertainment = await _context.Entertainments.FindAsync(id);
            if (entertainment == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", entertainment.UserId);
            return View(entertainment);
        }

        // POST: Entertainments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntertainmentId,Category,UserId")] Entertainment entertainment)
        {
            if (id != entertainment.EntertainmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entertainment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntertainmentExists(entertainment.EntertainmentId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", entertainment.UserId);
            return View(entertainment);
        }

        // GET: Entertainments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entertainments == null)
            {
                return NotFound();
            }

            var entertainment = await _context.Entertainments
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EntertainmentId == id);
            if (entertainment == null)
            {
                return NotFound();
            }

            return View(entertainment);
        }

        // POST: Entertainments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entertainments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Entertainments'  is null.");
            }
            var entertainment = await _context.Entertainments.FindAsync(id);
            if (entertainment != null)
            {
                _context.Entertainments.Remove(entertainment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntertainmentExists(int id)
        {
          return (_context.Entertainments?.Any(e => e.EntertainmentId == id)).GetValueOrDefault();
        }
    }
}
