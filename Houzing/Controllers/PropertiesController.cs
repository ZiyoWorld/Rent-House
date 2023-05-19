using Houzing.Data;
using Microsoft.AspNetCore.Mvc;

namespace Houzing.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }




    }
}
