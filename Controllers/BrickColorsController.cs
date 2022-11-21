using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Programming_Project.Data;
using Web_Programming_Project.Models;

namespace Web_Programming_Project.Controllers
{
    public class BrickColorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrickColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BrickColors
        public async Task<IActionResult> Index()
        {
              return View(await _context.BrickColor.ToListAsync());
        }

        // GET: BrickColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BrickColor == null)
            {
                return NotFound();
            }

            var brickColor = await _context.BrickColor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brickColor == null)
            {
                return NotFound();
            }

            return View(brickColor);
        }

        // GET: BrickColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BrickColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrickColorName,BrickColorRed,BrickColorGreen,BrickColorBlue,BrickColorAlpha")] BrickColor brickColor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brickColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brickColor);
        }

        // GET: BrickColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BrickColor == null)
            {
                return NotFound();
            }

            var brickColor = await _context.BrickColor.FindAsync(id);
            if (brickColor == null)
            {
                return NotFound();
            }
            return View(brickColor);
        }

        // POST: BrickColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrickColorName,BrickColorRed,BrickColorGreen,BrickColorBlue,BrickColorAlpha")] BrickColor brickColor)
        {
            if (id != brickColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brickColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrickColorExists(brickColor.Id))
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
            return View(brickColor);
        }

        // GET: BrickColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BrickColor == null)
            {
                return NotFound();
            }

            var brickColor = await _context.BrickColor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brickColor == null)
            {
                return NotFound();
            }

            return View(brickColor);
        }

        // POST: BrickColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BrickColor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BrickColor'  is null.");
            }
            var brickColor = await _context.BrickColor.FindAsync(id);
            if (brickColor != null)
            {
                _context.BrickColor.Remove(brickColor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrickColorExists(int id)
        {
          return _context.BrickColor.Any(e => e.Id == id);
        }
    }
}
