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
        public async Task<IActionResult> CreateHouseItems(HouseItemModel houseItem)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName1 = ProcessUploadedFile1(houseItem);
                string uniqueFileName2 = ProcessUploadedFile2(houseItem);
                string uniqueFileName3 = ProcessUploadedFile3(houseItem);
                HouseItem newHouseItem = new()
                {
                    Name = houseItem.Name,
                    Description = houseItem.Description,
                    Room = houseItem.Room,
                    Garage = houseItem.Garage,
                    Bath = houseItem.Bath,
                    YearBuilt = houseItem.YearBuilt,
                    Area = houseItem.Area,
                    Parking = houseItem.Parking,
                    Garden = houseItem.Garden,
                    Balcony = houseItem.Balcony,
                    SalePrice = houseItem.SalePrice,
                    Location = houseItem.Location,
                    OwnerId = houseItem.OwnerId,
                    Category = houseItem.Category,
                    ImagePath1 = uniqueFileName1,
                    ImagePath2 = uniqueFileName2,
                    ImagePath3 = uniqueFileName3,
                };  
                _context.HouseItems.Add(newHouseItem);
                await _context.SaveChangesAsync();

            }
            else return NotFound();

            
            //    // сохраняем в бд все изменения
            return RedirectToAction("Create", "Apartments");
        }

        private string ProcessUploadedFile1(HouseItemModel houseItem)
        {
            string uniqueFileName = string.Empty;
            if (houseItem.ImageFile1 != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "photos\\cover");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + houseItem.ImageFile1.FileName;
                string imageFilePath = Path.Combine(uploadFolder, uniqueFileName);
                houseItem.ImageFile1.CopyTo(new FileStream(imageFilePath, FileMode.Create));
            }
            return uniqueFileName;
        }
        private string ProcessUploadedFile2(HouseItemModel houseItem)
        {
            string uniqueFileName = string.Empty;
            if (houseItem.ImageFile2 != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "photos\\cover");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + houseItem.ImageFile2.FileName;
                string imageFilePath = Path.Combine(uploadFolder, uniqueFileName);
                houseItem.ImageFile2.CopyTo(new FileStream(imageFilePath, FileMode.Create));
            }
            return uniqueFileName;
        }
        private string ProcessUploadedFile3(HouseItemModel houseItem)
        {
            string uniqueFileName = string.Empty;
            if (houseItem.ImageFile3 != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "photos\\cover");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + houseItem.ImageFile3.FileName;
                string imageFilePath = Path.Combine(uploadFolder, uniqueFileName);
                houseItem.ImageFile3.CopyTo(new FileStream(imageFilePath, FileMode.Create));
            }
            return uniqueFileName;
        }


        [Authorize(Roles = "User")]
        // GET: HouseItemsController/Edit/5
        [HttpGet]
        public IActionResult EditHouseItem(int? Id)
        {
            if (Id != null)
            {
                HouseItem s = _context.HouseItems.Find((int)Id);
                if (s != null)
                {
                    ViewBag.Houses = s;
                }
                else return RedirectToAction("EditHouseItem");
            }
            return View();
        }
        [HttpPost]
        public IActionResult EditHouseItem(HouseItem houseItem)
        {
            _context.Entry(houseItem).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index", "Apartment");
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
                    return RedirectToAction("Index", "Owner");
                }
            }
            return NotFound();
        }
    }
}
