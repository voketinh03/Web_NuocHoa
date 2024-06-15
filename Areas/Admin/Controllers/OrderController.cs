using CK_ASP_NET_CORE.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CK_ASP_NET_CORE.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Controller
	{
		private readonly DataContext _dataContext;
		public OrderController(DataContext context)
		{
			_dataContext = context;

		}

		public async Task<IActionResult> Index()
		{
			return View(await _dataContext.OrderModels.OrderByDescending(p => p.Id).ToListAsync());
		}
	}
}
