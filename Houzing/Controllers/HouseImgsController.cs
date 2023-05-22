using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Houzing.Data;
using Houzing.Data.Houses;

namespace Houzing.Controllers
{
    public class HouseImgsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HouseImgsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HouseImgs
        public async Task<IActionResult> Index()
        {
              return _context.HouseImgs != null ? 
                          View(await _context.HouseImgs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.HouseImgs'  is null.");
        }

        // GET: HouseImgs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HouseImgs == null)
            {
                return NotFound();
            }

            var houseImg = await _context.HouseImgs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (houseImg == null)
            {
                return NotFound();
            }

            return View(houseImg);
        }

        // GET: HouseImgs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HouseImgs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageUrl")] HouseImg houseImg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(houseImg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(houseImg);
        }

        // GET: HouseImgs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HouseImgs == null)
            {
                return NotFound();
            }

            var houseImg = await _context.HouseImgs.FindAsync(id);
            if (houseImg == null)
            {
                return NotFound();
            }
            return View(houseImg);
        }

        // POST: HouseImgs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ImageUrl")] HouseImg houseImg)
        {
            if (id != houseImg.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(houseImg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseImgExists(houseImg.Id))
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
            return View(houseImg);
        }

        // GET: HouseImgs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HouseImgs == null)
            {
                return NotFound();
            }

            var houseImg = await _context.HouseImgs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (houseImg == null)
            {
                return NotFound();
            }

            return View(houseImg);
        }

        // POST: HouseImgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.HouseImgs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HouseImgs'  is null.");
            }
            var houseImg = await _context.HouseImgs.FindAsync(id);
            if (houseImg != null)
            {
                _context.HouseImgs.Remove(houseImg);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseImgExists(int? id)
        {
          return (_context.HouseImgs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
