namespace CK_ASP_NET_CORE.Models
{
	public class cartItemModel
	{
		public long ProductId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Total
		{
			get { return Quantity * Price; }
		}
		public string Image {  get; set; }
		public cartItemModel()
		{

		}
		public cartItemModel(ProductModel product)
		{
			ProductId = product.Id;
			ProductName = product.Name;
			Price = product.Price;
			Quantity = 1;
			Image = product.Image;
		}
	}
}
