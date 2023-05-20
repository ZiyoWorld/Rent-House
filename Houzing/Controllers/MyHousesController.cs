using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Houzing.Controllers
{
    public class MyHousesController : Controller
    {
        // GET: MyHousesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MyHousesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MyHousesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyHousesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: MyHousesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyHousesController/Edit/5
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

        // GET: MyHousesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyHousesController/Delete/5
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
