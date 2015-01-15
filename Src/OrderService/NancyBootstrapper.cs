﻿using IQ.Foundation.Messaging;
using IQ.Foundation.Messaging.AzureServiceBus;
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

			var bootstrapper = new DefaultAzureServiceBusBootstrapper(new PublisherConfiguration());

			var messageEnqueuer = bootstrapper.BuildQueueProducer();

			container.Register<IEnqueueMessages>(messageEnqueuer);
			container.Register<IOrderRepository, InMemoryOrderRepository>();
			container.Register<IMapper<string, OrderState?>, OrderStateMapper>();
		}
	}
}