using Azure.Core;
using Houzing.Data;
using Houzing.Data.Houses;
using Houzing.Models;
using Houzing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult CreateHouseItems(HouseItemModel houseItem)
        {
            
            if (houseItem != null)
            {
                HouseItem newItem = new HouseItem()
                {
                    Name = houseItem.Name,
                    Description = houseItem.Description,
                    Room = houseItem.Room,
                    ImagePath = UploadImage(houseItem.ImageFile),
                    Bath = houseItem.Bath,
                    Garage = houseItem.Garage,
                    Area = houseItem.Area,
                    YearBuilt = houseItem.YearBuilt,
                    Parking = houseItem.Parking,
                    Price = houseItem.Price,
                    Garden = houseItem.Garden,
                    SalePrice = houseItem.SalePrice,
                    Balcony = houseItem.Balcony,
                    Location = houseItem.Location,
                    Category = houseItem.Category,
                    OwnerId = houseItem.OwnerId,
                };
                _context.HouseItems.Add(newItem);
                // сохраняем в бд все изменения
                _context.SaveChanges();
                return RedirectToAction("CreateApartments", "Apartment");
            }
            else
            {
                return RedirectToAction("CreateOwner", "Owner");
            }
        }

        private string UploadImage(IFormFile imageFile)
        {
            try
            {
                var wwwPath = this._environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                // check the alllowed exestitions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extencions are allowed", string.Join(",", allowedExtensions));
                    return msg;
                }
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return newFileName;
            }
            catch (Exception ex)
            {
                return "hass been accured";
            }
        }
        //private string UploadFile(IFormFile file)
        //{
        //    string fileName = null;
        //    if (file != null)
        //    {
        //        string uploadDir = Path.Combine(_environment.WebRootPath, "Uploads");
        //        fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        //        string filePath = Path.Combine(uploadDir, fileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            file.CopyTo(fileStream);
        //        }
        //    }
        //    return fileName;
        //string serverFolder = Path.Combine(_environment.WebRootPath, folderPath);
        //folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

        ////file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
        //using (var fileStream = new FileStream(serverFolder, FileMode.Create))
        //{
        //    file.CopyTo(fileStream);
        //}

        //return "/" + folderPath;
        //}


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
