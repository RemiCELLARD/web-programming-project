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
using Web_Programming_Project.Data.Enum;
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
        [HttpGet]
        public async Task<IActionResult> Index(string brickSearch = "", int brickSize = 0, int brickColorId = -1, string prevBrickPriceSort = "up")
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
            ViewData["prevBrickPriceSort"] = prevBrickPriceSort;
            ViewData["brickSearch"] = brickSearch;
            ViewData["brickColorId"] = brickColorId;
            ViewData["brickSize"] = brickSize.ToString();
            ViewBag.BrickColors = await _context.BrickColor.ToListAsync();
            if (Request.Cookies.ContainsKey("notifTitle") && Request.Cookies.ContainsKey("notifMsg") && Request.Cookies.ContainsKey("notifIcon"))
            {
                ViewData["notifTitle"] = Request.Cookies["notifTitle"];
                ViewData["notifMsg"] = Request.Cookies["notifMsg"];
                ViewData["notifIcon"] = Request.Cookies["notifIcon"];
                Response.Cookies.Delete("notifTitle");
                Response.Cookies.Delete("notifMsg");
                Response.Cookies.Delete("notifIcon");
            }
            return View(await GetBrickAsync(brickSearch, brickSize, brickColorId, prevBrickPriceSort));
        }

        // GET: Boxes/Search
        public async Task<IActionResult> Search(string brickSearch = "", int brickSize = 0, int brickColorId = -1, string prevBrickPriceSort = "up")
        {
            return PartialView("_BricksTable", await GetBrickAsync(brickSearch, brickSize, brickColorId, prevBrickPriceSort));
        }

        // GET: Bricks/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
            ViewData["BrickColors"] = await _context.BrickColor.ToArrayAsync();
            ViewData["BrickColorId"] = -1;
            return View();
        }

        // POST: Bricks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrickName,BrickColorId,BrickSize,BrickPrice")] Brick brick)
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
            if (ModelState.IsValid)
            {
                _context.Add(brick);
                await _context.SaveChangesAsync();
                Response.Cookies.Append("notifTitle", "Creation of the brick");
                Response.Cookies.Append("notifMsg", "Creation of the '" + brick.BrickName + "' brick successfully completed.");
                Response.Cookies.Append("notifIcon", "check");
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrickColors"] = await _context.BrickColor.ToArrayAsync();
            ViewData["BrickColorId"] = brick.BrickColorId;
            return View(brick);
        }

        // GET: Bricks/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
            if (id == null || _context.Brick == null)
            {
                return NotFound();
            }

            var brick = await _context.Brick.FindAsync(id);
            if (brick == null)
            {
                return NotFound();
            }
            ViewData["BrickColors"] = await _context.BrickColor.ToArrayAsync();
            ViewData["BrickColorId"] = brick.BrickColorId;
            return View(brick);
        }

        // POST: Bricks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrickName,BrickColorId,BrickSize,BrickPrice")] Brick brick)
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
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
                Response.Cookies.Append("notifTitle", "Edition of the brick");
                Response.Cookies.Append("notifMsg", "Edition of the '" + brick.BrickName + "' brick successfully completed.");
                Response.Cookies.Append("notifIcon", "check");
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrickColors"] = await _context.BrickColor.ToArrayAsync();
            ViewData["BrickColorId"] = brick.BrickColorId;
            return View(brick);
        }

        // POST: Bricks/Delete/5
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteAjaxConfirmed(int id)
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
            return StatusCode(200);
        }

        /// <summary>
        /// Brick exist in context
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool BrickExists(int id)
        {
          return _context.Brick.Any(e => e.Id == id);
        }


        private async Task<List<Brick>> GetBrickAsync(string brickSearch = "", int brickSize = 0, int brickColorId = -1, string prevBrickPriceSort = "up")
        {
            IQueryable<Brick> result = _context.Brick;
            if (!string.IsNullOrWhiteSpace(brickSearch))
            {
                result = result.Where(brick => brick.BrickName.Contains(brickSearch));
            }
            if (brickSize != 0)
            {
                result = result.Where(brick => brick.BrickSize.Equals((BrickSizeEnum)brickSize));
            }
            if (brickColorId != -1)
            {
                result = result.Where(brick => brick.BrickColorId.Equals(brickColorId));
            }

            if (prevBrickPriceSort.Equals("up"))
            {
                result = result.OrderBy(brick => brick.BrickPrice);
            }
            else
            {
                result = result.OrderByDescending(brick => brick.BrickPrice);
            }
            result = result.Include(b => b.BrickColorObj);
            return await result.ToListAsync();
        }
    }
}
