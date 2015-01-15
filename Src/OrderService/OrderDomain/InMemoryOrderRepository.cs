using System.Collections.Generic;
using System.Linq;

namespace OrderService.OrderDomain
{
	public class InMemoryOrderRepository : IOrderRepository
	{
		readonly IList<Order> _orders;

		public InMemoryOrderRepository()
		{
			_orders = new List<Order>();
		}

		int GetNextId()
		{
			if (!_orders.Any())
				return 1;

			var max = _orders.Max(x => x.Id);

			return max + 1;
		}

		public void CreateOrder(Order input)
		{
			input.Id = GetNextId();

			input.State = OrderState.New;
			
			_orders.Add(input);
		}

		public Order GetOrderById(int id)
		{
			var match = _orders.SingleOrDefault(x => x.Id == id);

			return match;
		}

		public IEnumerable<Order> GetOrderByState(OrderState state)
		{
			return _orders.Where(x => x.State == state);
		}

		public void UpdateOrder(int id, OrderState state)
		{
			var match = GetOrderById(id);

			match.State = state;
		}
	}
}