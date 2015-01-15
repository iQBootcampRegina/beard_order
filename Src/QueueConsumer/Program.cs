using System;
using IQ.Foundation.Messaging.AzureServiceBus;
using OrderMessaging;

namespace QueueConsumer
{
	class Program
	{
		static void Main(string[] args)
		{
			var servicebusBootstrapper = new DefaultAzureServiceBusBootstrapper(new ConsumerConfiguration());

			servicebusBootstrapper.MessageHandlerRegisterer.Register<ProductQuantitiesChanged>(MessageHandlerFunc);

			servicebusBootstrapper.Subscribe();

			Console.WriteLine("Listening...");
			Console.ReadLine();
		}

		static void MessageHandlerFunc(ProductQuantitiesChanged message)
		{
			Console.WriteLine(message.Changes.Count);
		}
	}
}
