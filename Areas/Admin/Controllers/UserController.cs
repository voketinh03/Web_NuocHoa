using CK_ASP_NET_CORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CK_ASP_NET_CORE.Areas.Admin.Controllers
{
	[Area("Admin")]
	//[Route("Admin/User")]
	//[Authorize(Roles = "Admin")]
	[Authorize]
	public class UserController : Controller
	{


		private readonly UserManager<AppUseModel> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public UserController(UserManager<AppUseModel> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public IActionResult Index()
		{
			var users = _userManager.Users.ToList();
			var roles = _roleManager.Roles.ToList();

			var userRoles = new Dictionary<AppUseModel, List<IdentityRole>>();

			foreach (var user in users)
			{
				var userRolesList = _userManager.GetRolesAsync(user).Result;
				var rolesList = roles.Where(r => userRolesList.Contains(r.Name)).ToList();

				userRoles[user] = rolesList;
			}

			ViewBag.UserRoles = userRoles;

			return View();
		}

		[HttpGet]
		//[Route("Create")]
		public IActionResult Create()
		{
			var roles = _roleManager.Roles.ToList();
			ViewBag.Roles = roles;
			return View();
		}
		//[Route("Create")]
		[HttpPost]
		public async Task<IActionResult> Create(AppUseModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new AppUseModel { UserName = model.UserName, Email = model.Email };

				var result = await _userManager.CreateAsync(user, model.PasswordHash);

				if (result.Succeeded)
				{
					if (!await _roleManager.RoleExistsAsync(model.Id))
					{
						var role = new IdentityRole { Name = model.UserName };
						await _roleManager.CreateAsync(role);
					}

					await _userManager.AddToRoleAsync(user, model.Id);

					return RedirectToAction("Index", "Home");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}
	}
}