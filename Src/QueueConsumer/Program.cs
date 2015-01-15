using System;
using IQ.Foundation.Messaging.AzureServiceBus;
using OrderMessaging;
using QueueConsumer.Messaging;

namespace QueueConsumer
{
	class Program
	{
		static void Main(string[] args)
		{
			var servicebusBootstrapper = new DefaultAzureServiceBusBootstrapper(new ConsumerConfiguration());

			servicebusBootstrapper.MessageHandlerRegisterer.Register<OrderWasShipped>(MessageHandlerFunc);

			servicebusBootstrapper.Subscribe();

			Console.WriteLine("Listening...");
			Console.ReadLine();
		}

		static void MessageHandlerFunc(OrderWasShipped message)
		{
			foreach (var productSold in message.ProductsSold)
			{
				Console.WriteLine("ProductId {0}, Quantity {1}", productSold.ProductId, productSold.Quantity);
			}
		}
	}
}
