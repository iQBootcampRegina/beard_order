using System;
using System.Text.RegularExpressions;
using Nancy;
using Nancy.ModelBinding;

namespace OrderService
{
	public class OrderModule : NancyModule
	{
		static readonly IOrderRepository _orderRepository = new InMemoryOrderRepository();

		public OrderModule()
		{
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
			return _orderRepository.GetOrderById(id);
		}

		object GetOrders()
		{
			var enumValue = GetStateFromFilter();

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

		OrderState? GetStateFromFilter()
		{
			var filter = Request.Query["$filter"];

			if (string.IsNullOrWhiteSpace(filter))
				return null;

			string strRegex = @"(?<Filter>" +
							"\n" + @"     (?<Resource>.+?)\s+" +
							"\n" + @"     (?<Operator>eq|ne|gt|ge|lt|le|add|sub|mul|div|mod)\s+" +
							"\n" + @"     '?(?<Value>.+?)'?" +
							"\n" + @")" +
							"\n" + @"(?:" +
							"\n" + @"    \s*$" +
							"\n" + @"   |\s+(?:or|and|not)\s+" +
							"\n" + @")" +
							"\n";

			Regex myRegex = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

			string strReplace = @"${Value}";

			var resource = myRegex.Replace(filter, "${Resource}");
			var op = myRegex.Replace(filter, "${Operator}");
			var value = myRegex.Replace(filter, "${Value}");

			if (!string.Equals(resource, "State", StringComparison.InvariantCultureIgnoreCase) || 
							!string.Equals(op, "eq", StringComparison.InvariantCultureIgnoreCase))
				return null;

			OrderState enumValue;

			var valid = Enum.TryParse(value, true, out enumValue);

			if (!valid)
				return null;

			return enumValue;
		}
	}
}