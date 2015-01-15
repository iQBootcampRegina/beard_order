﻿using IQ.Foundation.Messaging.AzureServiceBus.Configuration;
using OrderMessaging;

namespace OrderService
{
	public class PublisherConfiguration : ConventionServiceBusConfiguration
	{
		public override string ConnectionString
		{
			get { return "Endpoint=sb://iqbootcamp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DYR29cbRrvBi1ADCztJQ99vlTOyPDVFAtLOgO8yWgN8="; }
		}

		public override string ServiceIdentifier
		{
			get { return QueueNames.ORDER_QUEUE_NAME; }
		}
	}
}