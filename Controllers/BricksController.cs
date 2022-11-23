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
    public class BricksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BricksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bricks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Brick.Include(b => b.BrickColorObj);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bricks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Brick == null)
            {
                return NotFound();
            }

            var brick = await _context.Brick
                .Include(b => b.BrickColorObj)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brick == null)
            {
                return NotFound();
            }

            return View(brick);
        }

        // GET: Bricks/Create
        public IActionResult Create()
        {
            ViewData["BrickColorId"] = new SelectList(_context.BrickColor, "Id", "BrickColorName");
            return View();
        }

        // POST: Bricks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrickName,BrickColorId,BrickSize,BrickPrice")] Brick brick)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brick);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrickColorId"] = new SelectList(_context.BrickColor, "Id", "BrickColorName", brick.BrickColorId);
            return View(brick);
        }

        // GET: Bricks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brick == null)
            {
                return NotFound();
            }

            var brick = await _context.Brick.FindAsync(id);
            if (brick == null)
            {
                return NotFound();
            }
            ViewData["BrickColorId"] = new SelectList(_context.BrickColor, "Id", "BrickColorName", brick.BrickColorId);
            return View(brick);
        }

        // POST: Bricks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrickName,BrickColorId,BrickSize,BrickPrice")] Brick brick)
        {
            if (id != brick.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brick);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrickExists(brick.Id))
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
            ViewData["BrickColorId"] = new SelectList(_context.BrickColor, "Id", "BrickColorName", brick.BrickColorId);
            return View(brick);
        }

        // GET: Bricks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Brick == null)
            {
                return NotFound();
            }

            var brick = await _context.Brick
                .Include(b => b.BrickColorObj)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brick == null)
            {
                return NotFound();
            }

            return View(brick);
        }

        // POST: Bricks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brick == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Brick'  is null.");
            }
            var brick = await _context.Brick.FindAsync(id);
            if (brick != null)
            {
                _context.Brick.Remove(brick);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrickExists(int id)
        {
          return _context.Brick.Any(e => e.Id == id);
        }
    }
}
