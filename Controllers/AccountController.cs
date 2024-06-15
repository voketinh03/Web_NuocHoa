using CK_ASP_NET_CORE.Models;
using CK_ASP_NET_CORE.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CK_ASP_NET_CORE.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUseModel> _userManager;
        private SignInManager<AppUseModel> _signInManager;
        public AccountController(SignInManager<AppUseModel> signInManager, UserManager<AppUseModel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
		public IActionResult Index()
        {
            return View();
        }
		
		public IActionResult Create()
		{
			return View();
		}
        public async Task<IActionResult> Login()
        {
            return View();
        }
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserModel user)
		{
			if (ModelState.IsValid)
			{
				AppUseModel newUser = new AppUseModel { UserName = user.UserName, Email = user.Email };
				IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);

				if (result.Succeeded)
				{
					TempData["success"] = "Tạo tài khoản thành công";
					return Redirect("/account/login");
				}

				foreach (IdentityError error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}

			}

			return View(user);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);

				if (result.Succeeded)
				{
					return Redirect(loginVM.URL ?? "/");
				}

				ModelState.AddModelError("", "Invalid username or password");
			}

			return View(loginVM);
		}

		public async Task<RedirectResult> Logout(string URL = "/")
		{
			await _signInManager.SignOutAsync();

			return Redirect(URL);
		}
	}
}
