using System;
using System.Collections.Generic;
using Nancy;

namespace OrderService
{
	public enum OrderState
	{
		New,
		InProgress,
		Shipped,
		Error
	}

	public class Order
	{
		public Guid Id { get; set; }

		public string CustomerName { get; set; }

		public string CustomerAddress { get; set; }

		public IEnumerable<int> ItemIds { get; set; }

		public OrderState State { get; set; }
	}

	public class OrderModule : NancyModule
	{
		public OrderModule()
		{
			Get["/orders/{id}"] = x => new Order() { Id = x.id, CustomerName = "Bob Smith"};
		}
	}
}