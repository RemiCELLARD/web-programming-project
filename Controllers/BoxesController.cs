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
    public class BoxesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly string _boxImgRoot;

        public BoxesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _boxImgRoot = _hostEnvironment.WebRootPath + "/uploads/images/Boxes";
        }

        // GET: Boxes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Box.Include(b => b.BoxTheme);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Boxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Box == null)
            {
                return NotFound();
            }

            var box = await _context.Box
                .Include(b => b.BoxTheme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (box == null)
            {
                return NotFound();
            }

            return View(box);
        }

        // GET: Boxes/Create
        public IActionResult Create()
        {
            ViewData["BoxThemeId"] = new SelectList(_context.Theme, "Id", "ThemeName");
            return View();
        }

        // POST: Boxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BoxName,BoxThemeId,BoxAgeCategory,BoxImgName,BoxDescription")] Box box)
        {
            if (ModelState.IsValid)
            {
                if (box.BoxImgFile != null)
                    box.BoxImgName = await _context.ImgManager.UploadImage(_boxImgRoot, box.BoxImgFile);
                _context.Add(box);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoxThemeId"] = new SelectList(_context.Theme, "Id", "ThemeName", box.BoxThemeId);
            return View(box);
        }

        // GET: Boxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Box == null)
            {
                return NotFound();
            }

            var box = await _context.Box.FindAsync(id);
            if (box == null)
            {
                return NotFound();
            }
            ViewData["BoxThemeId"] = new SelectList(_context.Theme, "Id", "ThemeName", box.BoxThemeId);
            return View(box);
        }

        // POST: Boxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BoxName,BoxThemeId,BoxAgeCategory,BoxImgName,BoxDescription")] Box box)
        {
            if (id != box.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (box.BoxImgFile != null && !(box.BoxImgFile.FileName.Equals(box.BoxImgName)))
                    {
                        box.BoxImgName = await _context.ImgManager.ReplaceImage(_boxImgRoot, box.BoxImgName, box.BoxImgFile);
                    }
                    if (box.BoxImgName != null)
                    {
                        if (box.BoxImgName.StartsWith("delete:"))
                        {
                            await _context.ImgManager.DeleteImage(_boxImgRoot, box.BoxImgName.Substring(7));
                            box.BoxImgName = null;
                        }
                    }
                    _context.Update(box);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoxExists(box.Id))
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
            ViewData["BoxThemeId"] = new SelectList(_context.Theme, "Id", "ThemeName", box.BoxThemeId);
            return View(box);
        }

        // GET: Boxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Box == null)
            {
                return NotFound();
            }

            var box = await _context.Box
                .Include(b => b.BoxTheme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (box == null)
            {
                return NotFound();
            }

            return View(box);
        }

        // POST: Boxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Box == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Box'  is null.");
            }
            var box = await _context.Box.FindAsync(id);
            if (box != null)
            {
                await _context.ImgManager.DeleteImage(_boxImgRoot, box.BoxImgName);
                _context.Box.Remove(box);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoxExists(int id)
        {
          return _context.Box.Any(e => e.Id == id);
        }
    }
}
