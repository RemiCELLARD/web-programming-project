using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
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
        public async Task<IActionResult> Index(string brickColorSearch = "", int brickColorRedSearch = -1, int brickColorGreenSearch = -1, int brickColorBlueSearch = -1, float brickColorAlphaSearch = -1.0f, string prevBrickColorSort = "up")
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
            ViewData["prevBrickColorSort"] = prevBrickColorSort;
            ViewData["brickColorSearch"] = brickColorSearch;
            ViewData["brickColorFilterRed"] = brickColorRedSearch;
            ViewData["brickColorFilterGreen"] = brickColorGreenSearch;
            ViewData["brickColorFilterBlue"] = brickColorBlueSearch;
            ViewData["brickColorFilterAlpha"] = brickColorAlphaSearch;
            if (Request.Cookies.ContainsKey("notifTitle") && Request.Cookies.ContainsKey("notifMsg") && Request.Cookies.ContainsKey("notifIcon"))
            {
                ViewData["notifTitle"] = Request.Cookies["notifTitle"];
                ViewData["notifMsg"] = Request.Cookies["notifMsg"];
                ViewData["notifIcon"] = Request.Cookies["notifIcon"];
                Response.Cookies.Delete("notifTitle");
                Response.Cookies.Delete("notifMsg");
                Response.Cookies.Delete("notifIcon");
            }
            return View(await GetBrickColorsAsync(brickColorSearch, brickColorRedSearch, brickColorGreenSearch, brickColorBlueSearch, brickColorAlphaSearch, prevBrickColorSort));
        }

        // GET: Themes/Search
        public async Task<IActionResult> Search(string brickColorSearch = "", int brickColorRedSearch = -1, int brickColorGreenSearch = -1, int brickColorBlueSearch = -1, float brickColorAlphaSearch = -1.0f, string prevBrickColorSort = "up")
        {
            return PartialView("_BrickColorsCard", await GetBrickColorsAsync(brickColorSearch, brickColorRedSearch, brickColorGreenSearch, brickColorBlueSearch, brickColorAlphaSearch, prevBrickColorSort));
        }

        // GET: BrickColors/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
            return View();
        }

        // POST: BrickColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrickColorName,BrickColorRed,BrickColorGreen,BrickColorBlue,BrickColorAlpha")] BrickColor brickColor)
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
            if (ModelState.IsValid)
            {
                _context.Add(brickColor);
                await _context.SaveChangesAsync();
                Response.Cookies.Append("notifTitle", "Creation of the colour");
                Response.Cookies.Append("notifMsg", "Creation of the '" + brickColor.BrickColorName + "' colour successfully completed.");
                Response.Cookies.Append("notifIcon", "check");
                return RedirectToAction(nameof(Index));
            }
            return View(brickColor);
        }

        // GET: BrickColors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrickColorName,BrickColorRed,BrickColorGreen,BrickColorBlue,BrickColorAlpha")] BrickColor brickColor)
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
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
                Response.Cookies.Append("notifTitle", "Edition of the colour");
                Response.Cookies.Append("notifMsg", "Edition of the '" + brickColor.BrickColorName + "' colour successfully completed.");
                Response.Cookies.Append("notifIcon", "check");
                return RedirectToAction(nameof(Index));
            }
            return View(brickColor);
        }


        // POST: BrickColors/Delete/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteAjaxConfirmed(int id)
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
            return StatusCode(200);
        }

        /// <summary>
        /// If color exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool BrickColorExists(int id)
        {
          return _context.BrickColor.Any(e => e.Id == id);
        }

        /// <summary>
        /// Search a color
        /// </summary>
        /// <param name="brickColorSearch"></param>
        /// <param name="brickColorRedSearch"></param>
        /// <param name="brickColorGreenSearch"></param>
        /// <param name="brickColorBlueSearch"></param>
        /// <param name="brickColorAlphaSearch"></param>
        /// <param name="prevBrickColorSort"></param>
        /// <returns></returns>
        public async Task<List<BrickColor>> GetBrickColorsAsync(string brickColorSearch = "", int brickColorRedSearch = -1, int brickColorGreenSearch = -1, int brickColorBlueSearch = -1, float brickColorAlphaSearch = -1.0f, string prevBrickColorSort = "up")
        {
            IQueryable<BrickColor> result = _context.BrickColor;
            if (!string.IsNullOrWhiteSpace(brickColorSearch))
            {
                result = result.Where(brickColor => brickColor.BrickColorName.Contains(brickColorSearch));
            }

            if (brickColorRedSearch >= 0 && brickColorGreenSearch >= 0 && brickColorBlueSearch >= 0 && brickColorAlphaSearch >= 0.0f) 
            {
                /* range for each colors */
                result = result.Where(brickColor => 
                    brickColor.BrickColorRed > (brickColorRedSearch - 50) && brickColor.BrickColorRed < (brickColorRedSearch + 50)
                    && brickColor.BrickColorGreen > (brickColorGreenSearch - 50) && brickColor.BrickColorGreen < (brickColorGreenSearch + 50)
                    && brickColor.BrickColorBlue > (brickColorBlueSearch - 50) && brickColor.BrickColorBlue < (brickColorBlueSearch + 50)
                    && brickColor.BrickColorAlpha > (brickColorAlphaSearch - 0.2f) && brickColor.BrickColorAlpha < (brickColorAlphaSearch + 0.2f)
                );
            }
            if (prevBrickColorSort.Equals("up"))
            {
                result = result.OrderBy(brickColor => brickColor.BrickColorName);
            }
            else
            {
                result = result.OrderByDescending(brickColor => brickColor.BrickColorName);
            }
            return await result.ToListAsync();
        }
    }
}
