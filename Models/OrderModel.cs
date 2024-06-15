namespace CK_ASP_NET_CORE.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string OrderCode { get; set; }
		public string tenNguoiDat { get; set; }
		public DateTime ngayDat { get; set; }
		public int trangThai { get; set; }
	}
}
