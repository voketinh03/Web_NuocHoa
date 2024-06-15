using CK_ASP_NET_CORE.Models;
using CK_ASP_NET_CORE.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CK_ASP_NET_CORE.Controllers
{
    public class BrandController : Controller
	{
		private readonly DataContext _dataContext;
		public BrandController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(string Slug = "")
		{
			BrandModel  brand = _dataContext.Brands.Where(c => c.Slug == Slug).FirstOrDefault();
			if (brand == null) return RedirectToAction("Index");
			var productsByBrand = _dataContext.Products.Where(c => c.BrandId == brand.Id);
			//OrderByDescending sản phẩm nào thêm sau hiển thị đâu 
			return View(await productsByBrand.OrderByDescending(p => p.Id).ToListAsync());
		}
	}
}
