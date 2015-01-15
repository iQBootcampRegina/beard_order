using Nancy;
using Nancy.ModelBinding;
using OrderService.OrderDomain;

namespace OrderService
{
	public class OrderModule : NancyModule
	{
		readonly IOrderRepository _orderRepository;
		readonly IMapper<string, OrderState?> _orderStateMapper;

		const string MANY_PATH = "/orders";
		const string SINGLE_PATH = "/orders/{id}";

		public OrderModule(IOrderRepository orderRepository, IMapper<string, OrderState?> orderStateMapper)
		{
			_orderRepository = orderRepository;
			_orderStateMapper = orderStateMapper;

			Post[MANY_PATH] = x => CreateOrder();
			Get[SINGLE_PATH] = x => GetOrderById(x.id);
			Get[MANY_PATH] = x => GetOrders();
			Put[SINGLE_PATH] = x => UpdateOrder();
		}

		object CreateOrder()
		{
			var inputOrder = this.Bind<Order>();

			_orderRepository.CreateOrder(inputOrder);

			var location = Request.Url + inputOrder.Id.ToString();

			return Negotiate.WithStatusCode(HttpStatusCode.Created)
							.WithHeader("Location", location)
							.WithModel(inputOrder);
		}

		object GetOrderById(int id)
		{
			var match = _orderRepository.GetOrderById(id);

			if (match == null)
				return Negotiate.WithStatusCode(HttpStatusCode.NotFound)
								.WithModel(new { Message = "Not Found"});

			return match;
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