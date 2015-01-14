using System;
using System.Collections.Generic;

namespace OrderService
{
	public class Order
	{
		public Guid Id { get; set; }

		public string CustomerName { get; set; }

		public string CustomerAddress { get; set; }

		public IEnumerable<int> ItemIds { get; set; }

		public OrderState State { get; set; }
	}

	public enum OrderState
	{
		New,
		InProgress,
		Shipped,
		Error
	}
}