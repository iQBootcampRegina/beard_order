using System;
using System.Collections.Generic;

namespace OrderService
{
	public interface IOrderRepository
	{
		void CreateOrder(Order input);

		Order GetOrderById(Guid id);
		IEnumerable<Order> GetOrderByState(OrderState state);

		void UpdateOrder(Guid id, OrderState state);

		void DeleteOrder(Guid id);
	}
}