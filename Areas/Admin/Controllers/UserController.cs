using Microsoft.AspNetCore.Mvc;

namespace CK_ASP_NET_CORE.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
