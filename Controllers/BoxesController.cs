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
        [HttpGet]
        public async Task<IActionResult> Index(string boxSearch = "", int boxAgeRange = 0, int boxThemeId = -1, string prevBoxSort = "up")
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
            ViewData["prevBoxSort"] = prevBoxSort;
            ViewData["boxSearch"] = boxSearch;
            ViewData["boxThemeId"] = boxThemeId;
            ViewData["boxAgeRange"] = boxAgeRange.ToString();
            ViewBag.Themes = await _context.Theme.ToListAsync();
            if (Request.Cookies.ContainsKey("notifTitle") && Request.Cookies.ContainsKey("notifMsg") && Request.Cookies.ContainsKey("notifIcon"))
            {
                ViewData["notifTitle"] = Request.Cookies["notifTitle"];
                ViewData["notifMsg"] = Request.Cookies["notifMsg"];
                ViewData["notifIcon"] = Request.Cookies["notifIcon"];
                Response.Cookies.Delete("notifTitle");
                Response.Cookies.Delete("notifMsg");
                Response.Cookies.Delete("notifIcon");
            }
            return View(await GetBoxAsync(boxSearch, boxAgeRange, boxThemeId, prevBoxSort));
        }

        // GET: Boxes/Search
        public async Task<IActionResult> Search(string boxSearch = "", int boxAgeRange = 0, int boxThemeId = -1, string prevBoxSort = "up")
        {
            return PartialView("_BoxesCard", await GetBoxAsync(boxSearch, boxAgeRange, boxThemeId, prevBoxSort));
        }

        // GET: Boxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
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
            box.BoxBricksInBox = await _context.BrickInBox.Include(b => b.BrickInBoxBrick).Include(b => b.BrickInBoxBrick.BrickColorObj).Where(b => b.BrickInBoxBoxId == id).ToListAsync();
            float totalPrice = 0f;
            /* Get price for all bricks */
            foreach (BrickInBox pack in box.BoxBricksInBox)
            {
                totalPrice += pack.BrickInBoxBrick.BrickPrice * pack.BrickInBoxQuantity;
            }
            ViewData["totalPrice"] = totalPrice.ToString("0.00");
            return View(box);
        }

        // GET: Boxes/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
            ViewData["BoxThemeId"] = new SelectList(_context.Theme, "Id", "ThemeName");
            ViewBag.Bricks = await _context.Brick.Include(b => b.BrickColorObj).ToListAsync();
            return View();
        }

        // POST: Boxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BoxName,BoxThemeId,BoxAgeCategory,BoxImgName,BoxImgFile,BoxDescription")] Box box, string[] brickIds)
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
            if (ModelState.IsValid)
            {
                box.BoxImgName = await _context.ImgManager.UploadImage(_boxImgRoot, box.BoxImgFile);
                _context.Add(box);
                await _context.SaveChangesAsync();

                Box dbBox = await _context.Box.OrderByDescending(box => box.Id).FirstOrDefaultAsync();
                UpdateBricksInBox(brickIds, dbBox);
                await _context.SaveChangesAsync();
                Response.Cookies.Append("notifTitle", "Creation of the box");
                Response.Cookies.Append("notifMsg", "Creation of the '" + box.BoxName + "' box successfully completed.");
                Response.Cookies.Append("notifIcon", "check");
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoxThemeId"] = new SelectList(_context.Theme, "Id", "ThemeName", box.BoxThemeId);
            return View(box);
        }

        // GET: Boxes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
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
            ViewBag.Bricks = await _context.Brick.Include(b => b.BrickColorObj).ToListAsync();
            
            box.BoxBricksInBox = await _context.BrickInBox.Include(b => b.BrickInBoxBrick).Include(b => b.BrickInBoxBrick.BrickColorObj).Where(b => b.BrickInBoxBoxId == id).ToListAsync();
            float totalPrice = 0f;
            /* Get price for all bricks */
            foreach (BrickInBox pack in box.BoxBricksInBox)
            {
                totalPrice += pack.BrickInBoxBrick.BrickPrice * pack.BrickInBoxQuantity;
            }
            ViewData["totalPrice"] = totalPrice;

            return View(box);
        }

        // POST: Boxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BoxName,BoxThemeId,BoxAgeCategory,BoxImgName,BoxImgFile,BoxDescription")] Box box, string[] brickIds)
        {
            ViewData["AbsoluteUri"] = Request.GetDisplayUrl();
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
                    box.BoxBricksInBox = await _context.BrickInBox.Include(b => b.BrickInBoxBrick).Include(b => b.BrickInBoxBrick.BrickColorObj).Where(b => b.BrickInBoxBoxId == id).ToListAsync();
                    UpdateBricksInBox(brickIds, box);
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
                Response.Cookies.Append("notifTitle", "Edition of the box");
                Response.Cookies.Append("notifMsg", "Edition of the '" + box.BoxName + "' box successfully completed.");
                Response.Cookies.Append("notifIcon", "check");
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoxThemeId"] = new SelectList(_context.Theme, "Id", "ThemeName", box.BoxThemeId);
            box.BoxBricksInBox = await _context.BrickInBox.Include(b => b.BrickInBoxBrick).Include(b => b.BrickInBoxBrick.BrickColorObj).Where(b => b.BrickInBoxBoxId == id).ToListAsync();
            return View(box);
        }

        // POST: Boxes/Delete/5
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteAjaxConfirmed(int id)
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
                Response.Cookies.Append("notifTitle", "Deletion of the box");
                Response.Cookies.Append("notifMsg", "The box has been successfully removed.");
                Response.Cookies.Append("notifIcon", "check");
            }
            
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

        /// <summary>
        /// If the box exist in database
        /// </summary>
        /// <param name="id">Id of the box</param>
        /// <returns></returns>
        private bool BoxExists(int id)
        {
          return _context.Box.Any(e => e.Id == id);
        }

        /// <summary>
        /// Get list of boxes based on filters
        /// </summary>
        /// <param name="boxSearch">Name of the boxes</param>
        /// <param name="boxAgeRange">Age range for boxes</param>
        /// <param name="boxThemeId">ThemeId of the boxes</param>
        /// <param name="prevBoxSort">Sort order</param>
        /// <returns>List of boxes</returns>
        private async Task<List<Box>> GetBoxAsync(string boxSearch = "", int boxAgeRange = 0, int boxThemeId = -1, string prevBoxSort = "up")
        {
            IQueryable<Box> result = _context.Box;
            if (!string.IsNullOrWhiteSpace(boxSearch))
            {
                result = result.Where(box => box.BoxName.Contains(boxSearch));
            }
            if (boxAgeRange != 0)
            {
                result = result.Where(box => box.BoxAgeCategory.Equals((AgeCategoryEnum)boxAgeRange));
            }
            if (boxThemeId != -1)
            {
                result = result.Where(box => box.BoxThemeId.Equals(boxThemeId));
            }

            if (prevBoxSort.Equals("up"))
            {
                result = result.OrderBy(box => box.BoxName);
            }
            else
            {
                result = result.OrderByDescending(box => box.BoxName);
            }
            result = result.Include(b => b.BoxTheme);
            return await result.ToListAsync();
        }

        /// <summary>
        /// Create/Delete or Update the table junction
        /// </summary>
        /// <param name="selectBricks">Brick selected in form</param>
        /// <param name="boxToUpdate">Box object</param>
        private void UpdateBricksInBox(string[] selectBricks, Box boxToUpdate)
        {
            if (selectBricks == null)
            {
                boxToUpdate.BoxBricksInBox = new List<BrickInBox>();
                return;
            }

            var selectBricksHS = new HashSet<string>(selectBricks);
            var boxBricks = new HashSet<int>(boxToUpdate.BoxBricksInBox.Select(c => c.BrickInBoxBrickId));
            foreach (Brick brick in _context.Brick)
            {
                if (selectBricksHS.Contains(brick.Id.ToString()))
                {
                    string inputKey = "brickAmount" + brick.Id.ToString();
                    int brickAmount;
                    if (Request.Form.ContainsKey(inputKey))
                    {
                        if (int.TryParse(Request.Form[inputKey], out brickAmount))
                        {
                            if (!boxBricks.Contains(brick.Id))
                            {
                                BrickInBox pack = new BrickInBox { BrickInBoxBoxId = boxToUpdate.Id, BrickInBoxBrickId = brick.Id, BrickInBoxQuantity = brickAmount };
                                boxToUpdate.BoxBricksInBox.Add(pack);
                                _context.Add(pack);
                            }
                            else
                            {
                                /* Edit value */
                                BrickInBox pack = boxToUpdate.BoxBricksInBox.FirstOrDefault(i => i.BrickInBoxBrickId == brick.Id);
                                pack.BrickInBoxQuantity = brickAmount;
                                _context.Update(pack);
                            }
                        }
                    }
                }
                else
                {
                    if (boxBricks.Contains(brick.Id))
                    {
                        BrickInBox brickToRemove = boxToUpdate.BoxBricksInBox.FirstOrDefault(i => i.BrickInBoxBrickId == brick.Id);
                        _context.Remove(brickToRemove);
                    }
                }
            }
        }
    }
}
