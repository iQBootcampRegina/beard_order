using Nancy;
using Nancy.TinyIoc;
using OrderService.OrderDomain;

namespace OrderService
{
	public class NancyBootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			base.ConfigureApplicationContainer(container);

			container.Register<IOrderRepository, InMemoryOrderRepository>();
			container.Register<IMapper<string, OrderState?>, OrderStateMapper>();
		}
	}
}