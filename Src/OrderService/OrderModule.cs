using System;
using System.Text.RegularExpressions;
using Nancy;
using Nancy.ModelBinding;
using OrderService.OrderDomain;

namespace OrderService
{
	public class OrderModule : NancyModule
	{
		readonly IOrderRepository _orderRepository;
		readonly IMapper<string, OrderState?> _orderStateMapper;

		public OrderModule(IOrderRepository orderRepository, IMapper<string, OrderState?> orderStateMapper)
		{
			_orderRepository = orderRepository;
			_orderStateMapper = orderStateMapper;

			Post["/orders"] = x => CreateOrder();
			Get["/orders/{id}"] = x => GetOrderById(x.id);
			Get["/orders"] = x => GetOrders();
			Put["/orders/{id}"] = x => UpdateOrder();
		}

		object CreateOrder()
		{
			var inputOrder = this.Bind<Order>();

			_orderRepository.CreateOrder(inputOrder);

			return Negotiate.WithStatusCode(HttpStatusCode.Created);
		}

		object GetOrderById(int id)
		{
			return new Order() {CustomerName = "stuff"};
//
//			var match = _orderRepository.GetOrderById(id);
//
//			if (match == null)
//				return Negotiate.WithStatusCode(HttpStatusCode.NotFound)
//								.WithModel(new { Message = "Not Found"});
//
//			return match;
		}

		object GetOrders()
		{
			OrderState? enumValue = _orderStateMapper.Map(Request.Query["$filter"]);

			if (!enumValue.HasValue)
				return Negotiate.WithStatusCode(HttpStatusCode.BadRequest);

			return _orderRepository.GetOrderByState(enumValue.Value);
		}

		object UpdateOrder()
		{
			var inputOrder = this.Bind<Order>();

			_orderRepository.UpdateOrder(inputOrder.Id, inputOrder.State);

			return Negotiate.WithStatusCode(HttpStatusCode.OK);
		}
	}
}