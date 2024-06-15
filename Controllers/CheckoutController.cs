using CK_ASP_NET_CORE.Models;
using CK_ASP_NET_CORE.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CK_ASP_NET_CORE.Controllers
{
	public class CheckoutController : Controller 
	{
		private readonly DataContext _dataContext;
		public CheckoutController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);//timf phien đăng nhập userEmail
			if(userEmail == null)
			{
				return RedirectToAction("Login");

			}
			else
			{
				var orderCode = Guid.NewGuid().ToString();// tạo ra chuỗi đơn hàng
				var orderIten = new OrderModel();
				orderIten.OrderCode = orderCode;
				orderIten.tenNguoiDat = userEmail;
				orderIten.ngayDat = DateTime.Now;
				_dataContext.Add(orderIten);
				_dataContext.SaveChanges();
				//Lưu nhiều sản phẩm vao 1 cái session Cart
				List<cartItemModel> cartItems = HttpContext.Session.GetJson<List<cartItemModel>>("Cart") ?? new List<cartItemModel>();
				foreach(var cart in cartItems)
				{
					var orderDetails = new OrderDetails();
					orderDetails.tenNguoiDat = userEmail;
					orderDetails.OrderCode = orderCode;
					orderDetails.IdSanPham = cart.ProductId;
					orderDetails.Gia = cart.Price;
					orderDetails.soLuong = cart.Quantity;
					_dataContext.Add(orderDetails);
					_dataContext.SaveChanges();
				}
				//Xóa session cart
				HttpContext.Session.Remove("Cart");
				TempData["success"] = "Đơn hàng đã được tạo";
				return RedirectToAction("Index", "Cart");
			}
			return View();
		}

	}
}
