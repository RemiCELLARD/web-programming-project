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
    public class BricksInBoxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BricksInBoxController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BricksInBox
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BrickInBox.Include(b => b.BrickInBoxBox).Include(b => b.BrickInBoxBrick);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BricksInBox/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BrickInBox == null)
            {
                return NotFound();
            }

            var brickInBox = await _context.BrickInBox
                .Include(b => b.BrickInBoxBox)
                .Include(b => b.BrickInBoxBrick)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brickInBox == null)
            {
                return NotFound();
            }

            return View(brickInBox);
        }

        // GET: BricksInBox/Create
        public IActionResult Create()
        {
            ViewData["BrickInBoxBoxId"] = new SelectList(_context.Set<Box>(), "Id", "Id");
            ViewData["BrickInBoxBrickId"] = new SelectList(_context.Brick, "Id", "Id");
            return View();
        }

        // POST: BricksInBox/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrickInBoxBoxId,BrickInBoxBrickId,BrickInBoxQuantity")] BrickInBox brickInBox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brickInBox);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrickInBoxBoxId"] = new SelectList(_context.Set<Box>(), "Id", "Id", brickInBox.BrickInBoxBoxId);
            ViewData["BrickInBoxBrickId"] = new SelectList(_context.Brick, "Id", "Id", brickInBox.BrickInBoxBrickId);
            return View(brickInBox);
        }

        // GET: BricksInBox/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BrickInBox == null)
            {
                return NotFound();
            }

            var brickInBox = await _context.BrickInBox.FindAsync(id);
            if (brickInBox == null)
            {
                return NotFound();
            }
            ViewData["BrickInBoxBoxId"] = new SelectList(_context.Set<Box>(), "Id", "Id", brickInBox.BrickInBoxBoxId);
            ViewData["BrickInBoxBrickId"] = new SelectList(_context.Brick, "Id", "Id", brickInBox.BrickInBoxBrickId);
            return View(brickInBox);
        }

        // POST: BricksInBox/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrickInBoxBoxId,BrickInBoxBrickId,BrickInBoxQuantity")] BrickInBox brickInBox)
        {
            if (id != brickInBox.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brickInBox);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrickInBoxExists(brickInBox.Id))
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
            ViewData["BrickInBoxBoxId"] = new SelectList(_context.Set<Box>(), "Id", "Id", brickInBox.BrickInBoxBoxId);
            ViewData["BrickInBoxBrickId"] = new SelectList(_context.Brick, "Id", "Id", brickInBox.BrickInBoxBrickId);
            return View(brickInBox);
        }

        // GET: BricksInBox/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BrickInBox == null)
            {
                return NotFound();
            }

            var brickInBox = await _context.BrickInBox
                .Include(b => b.BrickInBoxBox)
                .Include(b => b.BrickInBoxBrick)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brickInBox == null)
            {
                return NotFound();
            }

            return View(brickInBox);
        }

        // POST: BricksInBox/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BrickInBox == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BrickInBox'  is null.");
            }
            var brickInBox = await _context.BrickInBox.FindAsync(id);
            if (brickInBox != null)
            {
                _context.BrickInBox.Remove(brickInBox);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrickInBoxExists(int id)
        {
          return _context.BrickInBox.Any(e => e.Id == id);
        }
    }
}
