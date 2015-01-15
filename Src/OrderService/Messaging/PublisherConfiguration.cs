using IQ.Foundation.Messaging.AzureServiceBus.Configuration;
using OrderMessaging;

namespace OrderService.Messaging
{
	public class PublisherConfiguration : ConventionServiceBusConfiguration
	{
		public override string ConnectionString
		{
			get { return "Endpoint=sb://iqbootcamp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DYR29cbRrvBi1ADCztJQ99vlTOyPDVFAtLOgO8yWgN8="; }
		}

		public override string ServiceIdentifier
		{
			get { return TopicNames.OrderNotifications; }
		}

		protected override bool PublishesMessages
		{
			get { return true; }
		}
	}
}