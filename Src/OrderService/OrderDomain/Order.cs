using System.Collections.Generic;

namespace OrderService.OrderDomain
{
	public class Order
	{
		public Order()
		{
			Items = new List<OrderItem>();
		}
		public int Id { get; set; }

		public string CustomerName { get; set; }

		public string CustomerAddress { get; set; }

		public IEnumerable<OrderItem> Items { get; set; }

		public OrderState State { get; set; }
	}

	public class OrderItem
	{
		public int Id { get; set; }

		public int Quantity { get; set; }
	}

	public enum OrderState
	{
		New,
		InProgress,
		Shipped
	}
}