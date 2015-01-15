using IQ.Foundation.Messaging.AzureServiceBus.Configuration;

namespace OrderService
{
	public class PublisherConfiguration : ConventionServiceBusConfiguration
	{
		public override string ConnectionString
		{
			get { return "Endpoint=sb://iqdevelopment.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=4UZc0PqgjG3KBl08WJ1VkaGdMMPZVMxgrCvnrFxg1XI="; }
		}

		public override string ServiceIdentifier
		{
			get { return "Bootcamp.BeardOrders"; }
		}

		protected override bool PublishesMessages
		{
			get { return true; }
		}
	}
}