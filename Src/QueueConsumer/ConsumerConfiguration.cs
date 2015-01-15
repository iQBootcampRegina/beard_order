using IQ.Foundation.Messaging.AzureServiceBus.Configuration;
using OrderMessaging;

namespace QueueConsumer
{
	public class ConsumerConfiguration : ConventionServiceBusConfiguration
	{
		public override string ConnectionString
		{
			get { return "Endpoint=sb://iqbootcamp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DYR29cbRrvBi1ADCztJQ99vlTOyPDVFAtLOgO8yWgN8="; }
		}

		public override string ServiceIdentifier
		{
			get { return QueueNames.ORDER_QUEUE_NAME; }
		}

		protected override bool ConsumesQueue
		{
			get { return true; }
		}
	}
}