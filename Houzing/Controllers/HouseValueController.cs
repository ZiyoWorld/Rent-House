using Houzing.Data;
using Houzing.Data.Houses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Houzing.Controllers
{
    
    public class HouseValueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HouseValueController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin, Employer")]
        // GET: HouseItemsController
        public ActionResult Index()
        {
            return View(_context.HouseItems.ToList());
        }
        // GET: HouseItemsController/Details/5
        // detailni ishi bor hali
        [HttpGet]
        public ActionResult DetailsHouseValue(int id)
        {
            return View();
        }

        [Authorize]
        // GET: HouseItemsController/Create
        public ActionResult CreateHouseItems()
        {
            return View();
        }

        // POST: HouseItemsController/Create
        [HttpPost]
        public IActionResult CreateHouseItems(HouseItem houseItem)
        {
            _context.HouseItems.Add(houseItem);
            // сохраняем в бд все изменения
            _context.SaveChanges();
            return RedirectToAction("Apartment/CreateApartments");
        }
        [Authorize]
        // GET: HouseItemsController/Edit/5
        public IActionResult EditHouseValue()
        {
            return View("EditHouseValue");
        }
        [HttpPost]
        public IActionResult EditHouseValue(HouseItem houseItem)
        {
            _context.Entry(houseItem).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Apartment/CreateApartment");
        }
        // POST: HouseItemsController/Delete/5
        [Authorize(Roles = "Admin, Employer")]
        [HttpPost]
        public async Task<IActionResult> DeleteHouseValue(int? id)
        {
            if (id != null)
            {
                HouseItem? userOne = await _context.HouseItems.FirstOrDefaultAsync(p => p.Id == id);
                if (userOne != null)
                {
                    _context.HouseItems.Remove(userOne);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Properties/Index");
                }
            }
            return NotFound();
        }
    }
}
