using System.Collections.Generic;

namespace OrderService.OrderDomain
{
	public interface IOrderRepository
	{
		void CreateOrder(Order input);

		Order GetOrderById(int id);
		IEnumerable<Order> GetOrderByState(OrderState state);

		void UpdateOrder(int id, OrderState state);
	}
}