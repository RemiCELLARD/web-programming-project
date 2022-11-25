using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Web_Programming_Project.Data;
using Web_Programming_Project.Data.Enum;
using Web_Programming_Project.Models;

namespace Web_Programming_Project.Controllers
{
    public class ThemesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly string _themeImgRoot;

        public string ThemeImgRoot { 
            get { return _themeImgRoot; } 
        }

        public ThemesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _themeImgRoot = _hostEnvironment.WebRootPath + "/uploads/images/Themes";
        }

        // GET: Themes
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Theme.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string themeSearch, int themeAgeRange) 
        {
            ViewData["themeSearch"] = themeSearch;
            ViewData["themeAgeRange"] = themeAgeRange.ToString();
            IQueryable<Theme> result = _context.Theme;
            if (!string.IsNullOrWhiteSpace(themeSearch))
            {
                result = result.Where(theme => theme.ThemeName.Contains(themeSearch));
            }
            if(themeAgeRange != -1)
            {
                result = result.Where(theme => theme.ThemeAgeCategory.Equals((AgeCategoryEnum)themeAgeRange));
            }
            return View(await result.ToListAsync());
        }

        // GET: Themes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Theme == null)
            {
                return NotFound();
            }

            var theme = await _context.Theme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // GET: Themes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Themes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ThemeName,ThemeDescription,ThemeImgFile,ThemeAgeCategory")] Theme theme)
        {
            if (ModelState.IsValid)
            {
                theme.ThemeImgName = await _context.ImgManager.UploadImage(_themeImgRoot, theme.ThemeImgFile);
                _context.Add(theme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(theme);
        }

        // GET: Themes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Theme == null)
            {
                return NotFound();
            }

            var theme = await _context.Theme.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }
            return View(theme);
        }

        // POST: Themes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ThemeName,ThemeDescription,ThemeImgName,ThemeImgFile,ThemeAgeCategory")] Theme theme)
        {
            if (id != theme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (theme.ThemeImgFile != null && !(theme.ThemeImgFile.FileName.Equals(theme.ThemeImgName)))
                    { 
                        theme.ThemeImgName = await _context.ImgManager.ReplaceImage(_themeImgRoot, theme.ThemeImgName, theme.ThemeImgFile);
                    }
                    if (theme.ThemeImgName != null) 
                    {
                        if (theme.ThemeImgName.StartsWith("delete:"))
                        {
                            await _context.ImgManager.DeleteImage(_themeImgRoot, theme.ThemeImgName.Substring(7));
                            theme.ThemeImgName = null;
                        }
                    }
                    _context.Update(theme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeExists(theme.Id))
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
            return View(theme);
        }

        // GET: Themes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Theme == null)
            {
                return NotFound();
            }

            var theme = await _context.Theme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Theme == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Theme'  is null.");
            }
            var theme = await _context.Theme.FindAsync(id);
            if (theme != null)
            {
                await _context.ImgManager.DeleteImage(_themeImgRoot, theme.ThemeImgName);
                _context.Theme.Remove(theme);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeExists(int id)
        {
          return _context.Theme.Any(e => e.Id == id);
        }
    }
}
