using Houzing.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Houzing.Controllers
{
    public class AparmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AparmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AparmentController
        public ActionResult Index()
        {
            return View(_context.Apartments.ToList());
        }

        // GET: AparmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AparmentController/Create
        public ActionResult CreateApartments()
        {
            return View();
        }

        // POST: AparmentController/Create
        [HttpPost]
        public ActionResult CreateApartments(IFormCollection collection)
        {
            return View();
        }
        // GET: AparmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AparmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AparmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AparmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
