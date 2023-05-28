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
    public class DealsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DealsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Deal.Include(d => d.Apartment).Include(d => d.Customer).Include(d => d.Employer);
            return View(await applicationDbContext.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id");
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Id");
            return View();
        }

        // POST: Deals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Summ,FromDeal,ToDeal,DateDeal,Status,ApartmentId,CustomerId,EmployerId")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Summ,FromDeal,ToDeal,DateDeal,Status,ApartmentId,CustomerId,EmployerId")] Deal deal)
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool DealExists(int id)
        {
          return (_context.Deal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
