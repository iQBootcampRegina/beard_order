using System.Collections.Generic;

namespace OrderMessaging
{
	public class ProductQuantitiesChanged
	{
		public ProductQuantitiesChanged()
		{
			Changes = new List<ProductQuantityChange>();
		}

		public IList<ProductQuantityChange> Changes { get; set; }
	}

	public class ProductQuantityChange
	{
		public int ProductId { get; set; }

		public int Quantity { get; set; }
	}
}