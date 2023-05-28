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
                    Price= houseItem.Price,
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
                //    // сохраняем в бд все изменения
                return RedirectToAction("Create", "Apartments");
            }
             return View();
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
        public async Task<IActionResult> EditHouseItem(int? id)
        {
            HouseItem model = await _context.HouseItems.FindAsync(id);

            if(model != null)
            {
                HouseEditModel viewModel = new()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Room = model.Room,

                    Garage = model.Garage,
                    Bath = model.Bath,
                    YearBuilt = model.YearBuilt,

                    Area = model.Area,
                    Price = model.Price,
                    Parking = model.Parking,

                    Garden = model.Garden,
                    Balcony = model.Balcony,
                    SalePrice = model.SalePrice,

                    Location = model.Location,
                    OwnerId = model.OwnerId,
                    Category = model.Category,

                    ImagePathEdit1 = model.ImagePath1,
                    ImagePathEdit2 = model.ImagePath2,
                    ImagePathEdit3 = model.ImagePath3,
                };
                ViewBag.Houses = viewModel;
                return View(viewModel);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHouseItem(HouseEditModel houseItem)
        {
            HouseItem existMenu = await _context.HouseItems.FindAsync(houseItem.Id);

            if (ModelState.IsValid)
            {
                existMenu.Name = houseItem.Name;
                existMenu.Description = houseItem.Description;
                existMenu.Room = houseItem.Room;
                existMenu.Bath = houseItem.Bath;
                existMenu.YearBuilt = houseItem.YearBuilt;
                existMenu.Garage = houseItem.Garage;
                existMenu.Parking = houseItem.Parking;
                existMenu.Garden = houseItem.Garden;
                existMenu.Price = houseItem.Price;
                existMenu.SalePrice = houseItem.SalePrice;
                existMenu.Area = houseItem.Area;
                existMenu.Balcony = houseItem.Balcony;
                existMenu.Location = houseItem.Location;
                existMenu.OwnerId = houseItem.OwnerId;
                existMenu.Category = houseItem.Category;
                existMenu.ImagePath1 = ProcessUploadedFile1(houseItem);
                existMenu.ImagePath2 = ProcessUploadedFile2(houseItem);
                existMenu.ImagePath3 = ProcessUploadedFile3(houseItem);
               
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Apartments");
            }
            else return View(houseItem);
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
