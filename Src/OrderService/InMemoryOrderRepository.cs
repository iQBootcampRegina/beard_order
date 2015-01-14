using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService
{
	public class InMemoryOrderRepository : IOrderRepository
	{
		readonly IList<Order> _orders;

		public InMemoryOrderRepository()
		{
			_orders = new List<Order>();
		}

		public void CreateOrder(Order input)
		{
			input.Id = Guid.NewGuid();
			input.State = OrderState.New;
			
			_orders.Add(input);
		}

		public Order GetOrderById(Guid id)
		{
			var match = _orders.SingleOrDefault(x => x.Id == id);

			if (match == null)
				throw new OrderNotFoundException();

			return match;
		}

		public IEnumerable<Order> GetOrderByState(OrderState state)
		{
			return _orders.Where(x => x.State == state);
		}

		public void UpdateOrder(Guid id, OrderState state)
		{
			var match = GetOrderById(id);

			match.State = state;
		}

		public void DeleteOrder(Guid id)
		{
			var match = GetOrderById(id);

			_orders.Remove(match);
		}
	}
}