using System.Collections.Generic;

namespace OrderMessaging
{
	public class OrderWasShipped
	{
		public OrderWasShipped()
		{
			ProductsSold = new List<ProductSold>();
		}

		public IList<ProductSold> ProductsSold { get; set; }
	}

	public class ProductSold
	{
		public int ProductId { get; set; }

		public int Quantity { get; set; }
	}
}