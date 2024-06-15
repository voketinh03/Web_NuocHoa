using CK_ASP_NET_CORE.Models;
using CK_ASP_NET_CORE.Models.ViewModels;
using CK_ASP_NET_CORE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CK_ASP_NET_CORE.Controllers
{
    public class CartController : Controller
	{
		private readonly DataContext _dataContext;
		public CartController(DataContext _context)
		{
			_dataContext = _context;
		}
		public IActionResult Index()
		{
			//Chứa card Item
			List<cartItemModel> cartItems = HttpContext.Session.GetJson<List<cartItemModel>>("Cart") ?? new List<cartItemModel>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
			};
			return View(cartVM);
		}

		public IActionResult Checkout() { return View("~/Views/Checkout/Index.cshtml"); }
		//phuong thuc bat dong bo
		public async Task<IActionResult> Add(int Id)
		{
			ProductModel product = await _dataContext.Products.FindAsync(Id);
			List<cartItemModel> cart = HttpContext.Session.GetJson<List<cartItemModel>>("Cart") ?? new List<cartItemModel>();
			cartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();
			if (cartItems == null)
			{
				cart.Add(new cartItemModel(product));
			}
			else
			{
				cartItems.Quantity += 1;
			}
			//Lưu trữ cart và session cart
			HttpContext.Session.SetJson("Cart", cart);

			TempData["success"] = "Thêm sản phẩm vào giỏ hàng thành công";
			return Redirect(Request.Headers["Referer"].ToString());// tra ve trang hien tai
		}
		public async Task<IActionResult> Decrease(int Id)
		{
			List<cartItemModel> cart = HttpContext.Session.GetJson<List<cartItemModel>>("Cart");
			cartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();
			if (cartItems.Quantity > 1)
			{
				--cartItems.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}

			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Trừ sản phẩm trong giỏ hàng thành công";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Increase(int Id)
		{
			List<cartItemModel> cart = HttpContext.Session.GetJson<List<cartItemModel>>("Cart");
			cartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();
			if (cartItems.Quantity >= 1)
			{
				++cartItems.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}

			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Cộng sản phẩm trong giỏ hàng thành công";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Remove(int Id)
		{
			List<cartItemModel> cart = HttpContext.Session.GetJson<List<cartItemModel>>("Cart");
			cart.RemoveAll(p => p.ProductId == Id);
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Xóa sản phẩm trong giỏ hàng thành công";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Clear()
		{

			HttpContext.Session.Remove("Cart");
			TempData["success"] = "Xóa giỏ hàng thành công";
			return RedirectToAction("Index");
		}
	}
}
