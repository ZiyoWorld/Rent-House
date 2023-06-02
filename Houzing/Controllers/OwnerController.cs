using Houzing.Constants;
using Houzing.Data;
using Houzing.Data.Houses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Houzing.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OwnerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Employer")]
        public IActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Id")
            {
                return View(_context.Owners.Where(x => x.Id.ToString() == search || search == null).ToList());
            } 
            else if (searchBy == "Name") {
                return View(_context.Owners.Where(x => x.FirstName == search || search == null).ToList());
            }
            else
            {
                return View(_context.Owners.ToList());
            }
        }
        // Delete user by Id
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Owner? user = await _context.Owners.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                {
                    _context.Owners.Remove(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Owners == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }
        // Create page is all memebers

        [Authorize(Roles="User")]
        public IActionResult CreateOwner()
        {
        //    if (id == null) return RedirectToAction("Index");
        //    ViewBag.OwnerId = id;
            return View();
        }
        [HttpPost]
        public IActionResult CreateOwner(Owner owner)
        {
            if(owner != null)
            {
                _context.Owners.Add(owner);
                // сохраняем в бд все изменения
                _context.SaveChanges();
                return RedirectToAction("CreateHouseItems", "HouseValue");
            }
            else
            {
                return RedirectToAction("CreateOwner");
            }
            
        }
        // EditPage page is all memebers
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult EditOwner(int? Id)
        {
            if (Id != null)
            {
                Owner s = _context.Owners.Find((int)Id);
                if (s != null)
                {
                    ViewBag.Owners = s;
                }
                else return RedirectToAction("EditOwner");
            }
            else return RedirectToAction("EditOwner");
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Owner owner)
        {
            _context.Entry(owner).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index", "Apartment");
        }
    }
}
