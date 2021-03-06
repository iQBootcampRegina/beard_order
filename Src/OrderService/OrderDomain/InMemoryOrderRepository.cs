﻿using System.Collections.Generic;
using System.Linq;
using IQ.Foundation.Messaging;
using OrderMessaging;

namespace OrderService.OrderDomain
{
	public class InMemoryOrderRepository : IOrderRepository
	{
		readonly IPublishMessages _publishMessages;
		readonly IList<Order> _orders;

		public InMemoryOrderRepository(IPublishMessages publishMessages)
		{
			_publishMessages = publishMessages;
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

		public void UpdateOrder(int id, OrderState newState)
		{
			var match = GetOrderById(id);

			// If an order is already shipped it cannot be changed
			if (match.State == OrderState.Shipped)
				return;

			match.State = newState;

			if (match.State != OrderState.Shipped) return;

			PublishOrderShippedMessage(match);
		}

		void PublishOrderShippedMessage(Order match)
		{
			var products = match.Items.Select(x => new ProductSold {ProductId = x.Id, Quantity = x.Quantity});

			var message = new OrderWasShipped {ProductsSold = products.ToList()};

			_publishMessages.Publish(message);
		}
	}
}