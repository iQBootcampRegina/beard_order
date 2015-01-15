using System.Collections.Generic;
using IQ.Foundation.Messaging.AzureServiceBus.Configuration;
using OrderMessaging;

namespace QueueConsumer.Messaging
{
	public class ConsumerConfiguration : ConventionServiceBusConfiguration
	{
		public override string ConnectionString
		{
			get { return "Endpoint=sb://iqbootcamp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DYR29cbRrvBi1ADCztJQ99vlTOyPDVFAtLOgO8yWgN8="; }
		}

		public override string ServiceIdentifier
		{
			get { return "TestConsoleConsumer"; }
		}

		protected override IEnumerable<string> SubscriptionTopics
		{
			get { yield return TopicNames.OrderNotifications; }
		}
	}
}