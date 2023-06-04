using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Houzing.Data;
using Houzing.Data.Houses;
using Microsoft.AspNetCore.Authorization;
using Houzing.Models;

namespace Houzing.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ApartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchBy, string search)
        {
          var allData = from a in _context.Apartments join h in _context.HouseItems on a.HouseItemId equals h.Id                      
                          select new {
                              a.Id,
                              h.Room,
                              a.HouseItemId,
                              h.ImagePath1,
                              a.Adress,
                              h.Bath,
                              h.Garage,
                              h.Area,
                              a.MaxPrice,
                              a.MinPrice,
                              a.City,
                              a.Region,
                              a.Country,
                              h.Name,
                               };
            if (searchBy == "Adress")
            {
                ViewBag.AllData = allData.Where(x => x.Adress == search || search == null);
            }
            else if (searchBy == "Room")
            {
                ViewBag.AllData = allData.Where(x => x.Room == search || search == null);
            }
            else if (searchBy == "MinPrice")
            {
                ViewBag.AllData = allData.Where(x => x.MinPrice == search || search == null);
            }
            else if (searchBy == "Region")
            {
                ViewBag.AllData = allData.Where(x => x.Region == search || search == null);
            }
            else
            {
                ViewBag.AllData = allData;
            }
            

            return View();
        }


        [Authorize(Roles = "Admin, Employer")]
        public async Task<IActionResult> ManageApartment(string searchBy, string search)
        {
            if (searchBy == "Id")
            {
                return View(await _context.Apartments.Where(x => x.Id.ToString() == search || search == null).ToListAsync());
            }
            else if (searchBy == "Adress")
            {
                return View( await _context.Apartments.Where(x => x.Adress == search || search == null).ToListAsync());
            }
            else
            {
                return View( await _context.Apartments.Include(a => a.HouseItem).Include(a => a.Owner).ToListAsync());
            }
        }
        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           if (id == null || _context.Apartments == null)
           {
                return NotFound();
           }

            var apartment = await _context.Apartments
                .Include(a => a.HouseItem)
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.HouseItemVm = _context.HouseItems.ToList().LastOrDefault().Id;
            ViewBag.OwnerVm = _context.Owners.ToList().LastOrDefault().Id;

            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adress,Country,Region,City,NumberHouse,Floor,TypeHouse,Repair,MaxPrice,MinPrice,Status,HouseItemId,OwnerId")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HouseItemId"] = new SelectList(_context.HouseItems, "Id", "Id", apartment.HouseItemId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", apartment.OwnerId);
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Apartments == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            ViewData["HouseItemId"] = new SelectList(_context.HouseItems, "Id", "Id", apartment.HouseItemId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", apartment.OwnerId);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Adress,Country,Region,City,NumberHouse,Floor,TypeHouse,Repair,MaxPrice,MinPrice,Status,HouseItemId,OwnerId")] Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.Id))
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
            ViewData["HouseItemId"] = new SelectList(_context.HouseItems, "Id", "Id", apartment.HouseItemId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", apartment.OwnerId);
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Apartments == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.HouseItem)
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Apartments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Apartments'  is null.");
            }
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment != null)
            {
                _context.Apartments.Remove(apartment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(int? id)
        {
          return (_context.Apartments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
