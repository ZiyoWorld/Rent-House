using Houzing.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Houzing.Controllers
{
    [Authorize]
    public class UsersRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
       
        public UsersRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleMgr)
        {
            _userManager = userManager;
            _roleManager = roleMgr;

        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach(ApplicationUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public IActionResult AddRole()
        {
            var role = _roleManager.Roles.ToList();
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("AddRole");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var rol = await _roleManager.FindByIdAsync(Id);
            if (rol != null)
            {
                ViewData["Message"] = "Customer record deleted.";
                IdentityResult result = await _roleManager.DeleteAsync(rol);
            }
            else
            {
                ViewData["Message"] = "Customer not found.";
            }

            return RedirectToAction("AddRole");
        }

        /// <summary>
        /// Manage button we can change our roles of users
        /// 
        /// </summary>
        public async Task<IActionResult> Manage(string UserId)
        {
            // получаем пользователя
            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var model = new ChangeUsersRoles
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        

        
        [HttpPost]
        public async Task<IActionResult> Manage(string UserId, List<string> roles)
        {
            // taking users
            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                // taking list that is roles of users
                var userRoles = await _userManager.GetRolesAsync(user);
                // taking all rules
                var allRoles = _roleManager.Roles.ToList();
                // taking list that had added
                var addedRoles = roles.Except(userRoles);
                // taking roles that had deleted
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                ViewData["Message"] = "Customer record deleted.";
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            else
            {
                ViewData["Message"] = "Customer not found.";
            }
            return RedirectToAction("Index");
        }
    }
}
