using Azure.Core;
using Houzing.Data;
using Houzing.Data.Houses;
using Houzing.Models;
using Houzing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using TechTalk.SpecFlow.CommonModels;

namespace Houzing.Controllers
{
    
    public class HouseValueController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        public HouseValueController(ApplicationDbContext context, IWebHostEnvironment environment, IFileService fileService)
        {
            _context = context;
            _environment = environment;
            _fileService = fileService;
        }
        [Authorize(Roles = "Admin, Employer")]
        // GET: HouseItemsController
        public ActionResult Index()
        {
            return View(_context.HouseItems.ToList());
        }
        // GET: HouseItemsController/Details/5
        [Authorize(Roles = "User")]
        // GET: HouseItemsController/Create
        [HttpGet]
        public ActionResult CreateHouseItems()
        {
            Owner res = _context.Owners.ToList().LastOrDefault();
            ViewBag.Rest = res.Id;
            return View();
        }

        // POST: HouseItemsController/Create
        [HttpPost]
        public IActionResult CreateHouseItems(HouseItem houseItem, IFormFile formFile)
        {
            if (!ModelState.IsValid)
            {
                return View(houseItem);
            }
            if (houseItem.ImageFile != null)
            {
                var fileReult = _fileService.SaveImage(houseItem.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(houseItem);
                }
                var imageName = fileReult.Item2;
                houseItem.ImagePath = imageName;
            }
            _context.HouseItems.Add(houseItem);
            _context.SaveChanges();
            //    // сохраняем в бд все изменения
            return RedirectToAction("CreateApartments", "Apartment");
        }
        

        [Authorize(Roles = "User")]



        // GET: HouseItemsController/Edit/5
        public IActionResult EditHouseValue(int? Id)
        {
            if (Id != null)
            {
                Owner s = _context.Owners.Find((int)Id);
                if (s != null)
                {
                    ViewBag.Houses = s;
                }
                else return RedirectToAction("EditHouseValue");
            }
            else return RedirectToAction("EditHouseValue");
            return View();

        }
        [HttpPost]
        public IActionResult EditHouseValue(HouseItem houseItem)
        {
            _context.Entry(houseItem).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("EditApartments", "Apartment");
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
