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
                IdentityResult result = await _roleManager.DeleteAsync(rol);
            }
            return RedirectToAction("AddRole");
        }

        //[HttpGet]
        //public async Task<IActionResult> Manage(string userId)
        //{
        //    ViewBag.userId = userId;
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
        //        return View("NotFound");
        //    }
        //    ViewBag.UserName = user.UserName;
        //    var model = new List<ManageUserRolesViewModel>();
        //    foreach (var role in _roleManager.Roles)
        //    {
        //        var userRolesViewModel = new ManageUserRolesViewModel
        //        {
        //            RoleId = role.Id,
        //            RoleName = role.Name
        //        };
        //        if (await _userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            userRolesViewModel.Selected = true;
        //        }
        //        else
        //        {
        //            userRolesViewModel.Selected = false;
        //        }
        //        model.Add(userRolesViewModel);
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return View();
        //    }
        //    var roles = await _userManager.GetRolesAsync(user);
        //    var result = await _userManager.RemoveFromRolesAsync(user, roles);
        //    if (!result.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Cannot remove user existing roles");
        //        return View(model);
        //    }
        //    result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
        //    if (!result.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Cannot add selected roles to user");
        //        return View(model);
        //    }
        //    return RedirectToAction("Index");
        //}



        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
