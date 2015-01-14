using System;
using System.Collections.Generic;

namespace OrderService
{
	public interface IOrderRepository
	{
		void CreateOrder(Order input);

		Order GetOrderById(Guid id);
		IEnumerable<Order> GetPendingOrders();

		void UpdateOrder(Order input);

		void DeleteOrder(Guid id);
	}
}