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
using Microsoft.IdentityModel.Tokens;

namespace Houzing.Controllers
{
    public class DealsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DealsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deals
        [Authorize(Roles = "Admin, Employer")]
        public async Task<IActionResult> Index(string searchBy, string search)
        {
            if (searchBy == "Id")
            {
                return View( await _context.Deal.Where(x => x.Id.ToString() == search || search == null)
                    .Include(d => d.Apartment).Include(d => d.Customer).Include(d => d.Employer)
                    .ToListAsync());
            }
            else if (searchBy == "DateDeal")
            {
                return View( await _context.Deal.Where(x => x.DateDeal.ToString() == search || search == null)
                    .Include(d => d.Apartment).Include(d => d.Customer).Include(d => d.Employer)
                    .ToListAsync());
            }
            else
            {
                return View(await _context.Deal.Include(d => d.Apartment).Include(d => d.Customer).Include(d => d.Employer).ToListAsync());
            }
        }
        public async Task<IActionResult> ManageDeals(string searchBy, string search)
        {
            var customerData = from a in _context.Customer
                          join h in _context.Deal on a.Id equals h.CustomerId
                          select new
                          {
                              h.Id,
                              h.DateDeal,
                              a.Email,
                              h.Summa,
                              h.PayType,
                              a.FirstName,
                              h.CustomerId,
                          };
            if (searchBy == "Id")
            {
                ViewBag.CustomerData = customerData.Where(x =>x.Id.ToString() == search || search == null);
            }else if(searchBy == "FirstName")
            {
                ViewBag.CustomerData = customerData.Where(x => x.FirstName == search || search == null);
            }
            else
            {
              ViewBag.CustomerData = customerData;
            }
            return View();
        }

        public async Task<IActionResult> Completly()
        {
            return View();
        }

        // GET: Deals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Deal == null)
            {
                return NotFound();
            }

            var deal = await _context.Deal
                .Include(d => d.Apartment)
                .Include(d => d.Customer)
                .Include(d => d.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }
        // GET: Deals/Create
        public IActionResult Create(int id)
        {
            
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id");
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Id");
            Customer s = _context.Customer.Find((int?)id);
            if (s != null)
            {
                ViewBag.Customer = s;
            }
            return View();
        }

        // POST: Deals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,FromDeal,ToDeal,DateDeal,Summa,PayType,ApartmentId,CustomerId,EmployerId")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Completly));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Id", deal.ApartmentId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", deal.CustomerId);
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Id", deal.EmployerId);
            return View(deal);
        }

        // GET: Deals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Deal == null)
            {
                return NotFound();
            }

            var deal = await _context.Deal.FindAsync(id);
            if (deal == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Id", deal.ApartmentId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", deal.CustomerId);
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Id", deal.EmployerId);
            return View(deal);
        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,FromDeal,ToDeal,DateDeal,Summa,PayType,ApartmentId,CustomerId,EmployerId")] Deal deal)
        {
            if (id != deal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealExists(deal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ManageDeals));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Id", deal.ApartmentId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", deal.CustomerId);
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Id", deal.EmployerId);
            return View(deal);
        }

        // GET: Deals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Deal == null)
            {
                return NotFound();
            }

            var deal = await _context.Deal
                .Include(d => d.Apartment)
                .Include(d => d.Customer)
                .Include(d => d.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Deal == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Deal'  is null.");
            }
            var deal = await _context.Deal.FindAsync(id);
            if (deal != null)
            {
                _context.Deal.Remove(deal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageDeals));
        }

        private bool DealExists(int id)
        {
          return (_context.Deal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
