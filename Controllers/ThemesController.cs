using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Index(string themeSearch = "", int themeAgeRange = 0, string prevThemeSort = "up") 
        {
            ViewData["prevThemeSort"] = prevThemeSort;
            ViewData["themeSearch"] = themeSearch;
            ViewData["themeAgeRange"] = themeAgeRange.ToString();
            if (Request.Cookies.ContainsKey("notifTitle") && Request.Cookies.ContainsKey("notifMsg") && Request.Cookies.ContainsKey("notifIcon"))
            {
                ViewData["notifTitle"] = Request.Cookies["notifTitle"];
                ViewData["notifMsg"] = Request.Cookies["notifMsg"];
                ViewData["notifIcon"] = Request.Cookies["notifIcon"];
                Response.Cookies.Delete("notifTitle");
                Response.Cookies.Delete("notifMsg");
                Response.Cookies.Delete("notifIcon");
            }
            return View(await GetThemeAsync(themeSearch, themeAgeRange, prevThemeSort));
        }

        // GET: Themes/Search
        public async Task<IActionResult> Search(string themeSearch = "", int themeAgeRange = 0, string prevThemeSort = "up")
        {
            return PartialView("_ThemesCard", await GetThemeAsync(themeSearch, themeAgeRange, prevThemeSort));
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Themes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ThemeName,ThemeDescription,ThemeImgFile,ThemeAgeCategory")] Theme theme)
        {
            if (ModelState.IsValid)
            {
                theme.ThemeImgName = await _context.ImgManager.UploadImage(_themeImgRoot, theme.ThemeImgFile);
                _context.Add(theme);
                await _context.SaveChangesAsync();
                Response.Cookies.Append("notifTitle", "Creation of the theme");
                Response.Cookies.Append("notifMsg", "Creation of the '"+theme.ThemeName+"' theme successfully completed.");
                Response.Cookies.Append("notifIcon", "check");
                return RedirectToAction(nameof(Index));
            }
            return View(theme);
        }

        // GET: Themes/Edit/5
        [Authorize]
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
        [Authorize]
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
                Response.Cookies.Append("notifTitle", "Edition of the theme");
                Response.Cookies.Append("notifMsg", "Edition of the '" + theme.ThemeName + "' theme successfully completed.");
                Response.Cookies.Append("notifIcon", "check");
                return RedirectToAction(nameof(Index));
            }
            return View(theme);
        }

        // POST: Themes/Delete/5
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteAjaxConfirmed(int id)
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
            return StatusCode(200);
        }

        /// <summary>
        /// Theme exist ?
        /// </summary>
        /// <param name="id">Id of the Theme</param>
        /// <returns>Existence of the Theme in DB</returns>
        private bool ThemeExists(int id)
        {
          return _context.Theme.Any(e => e.Id == id);
        }

        /// <summary>
        /// Get all Theme based on search filters
        /// </summary>
        /// <param name="themeSearch">Part of the name of the theme</param>
        /// <param name="themeAgeRange">Age range</param>
        /// <param name="prevThemeSort">Sorting (A-Z or Z-A)</param>
        /// <returns></returns>
        private async Task<List<Theme>> GetThemeAsync(string themeSearch = "", int themeAgeRange = 0, string prevThemeSort = "down")
        {
            IQueryable<Theme> result = _context.Theme;
            if (!string.IsNullOrWhiteSpace(themeSearch))
            {
                result = result.Where(theme => theme.ThemeName.Contains(themeSearch));
            }
            if (themeAgeRange != 0)
            {
                result = result.Where(theme => theme.ThemeAgeCategory.Equals((AgeCategoryEnum)themeAgeRange));
            }

            if (prevThemeSort.Equals("up"))
            {
                result = result.OrderBy(theme => theme.ThemeName);
            }
            else
            {
                result = result.OrderByDescending(theme => theme.ThemeName);
            }
            return await result.ToListAsync();
        }
    }
}
