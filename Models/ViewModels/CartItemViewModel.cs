namespace CK_ASP_NET_CORE.Models.ViewModels
{
	public class CartItemViewModel 
	{
		public List<cartItemModel> CartItems { get; set; }
		public decimal GrandTotal { get; set; }
	}
}
