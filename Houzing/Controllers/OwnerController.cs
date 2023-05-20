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
        public IActionResult Index()
        {
            return View(_context.Owners.ToList());
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
        // Create page are only changed by User
        [Authorize(Roles = "User")]
        public IActionResult CreateOwner()
        {
            //if (id == null) return RedirectToAction("Index");
            //ViewBag.OwnerId = id;
            return View();
        }
        [HttpPost]
        public IActionResult CreateOwner(Owner owner)
        {
            _context.Owners.Add(owner);
            // сохраняем в бд все изменения
            _context.SaveChanges();
            return RedirectToAction("HouseItem/CreateHouseItems");
        }
        // Edit page are only used User
        [Authorize(Roles="User")]
        [HttpGet]
        public IActionResult EditOwner()
        {
            return View("EditOwner");
        }
        [HttpPost]
        public IActionResult Edit(Owner owner)
        {
            _context.Entry(owner).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("HouseItem/CreateHouseItems");
        }

    }
}
