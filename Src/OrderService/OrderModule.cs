using System;
using Nancy;
using Nancy.ModelBinding;

namespace OrderService
{
	public class OrderModule : NancyModule
	{
		static readonly IOrderRepository _orderRepository = new InMemoryOrderRepository();

		public OrderModule()
		{
			Get["/orders/{id}"] = x => GetOrderById(x.id);
			Get["/orders"] = x => GetOrders();
			Post["/orders"] = x => CreateOrder();
		}

		object CreateOrder()
		{
			var inputOrder = this.Bind<Order>();

			_orderRepository.CreateOrder(inputOrder);

			return Negotiate.WithStatusCode(HttpStatusCode.Created);
		}

		object GetOrderById(Guid id)
		{
			return _orderRepository.GetOrderById(id);
		}

		object GetOrders()
		{
			return _orderRepository.GetPendingOrders();
		}
	}
}