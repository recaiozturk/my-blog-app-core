using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.WebUI.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View();
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);

            if (role != null && role.Name != null)
            {
                ViewBag.Users = await _userManager.GetUsersInRoleAsync(role.Name);
                return View(role);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);

                if (role != null)
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }

                    if (role.Name != null)
                        ViewBag.Users = await _userManager.GetUsersInRoleAsync(role.Name);
                }
            }

            return View(model);
        }
    }
}
