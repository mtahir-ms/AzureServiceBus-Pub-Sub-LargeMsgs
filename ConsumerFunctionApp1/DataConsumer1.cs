using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ConsumerFunctionApp1
{
    public class DataConsumer1
    {
        private readonly ILogger<DataConsumer1> _logger;

        public DataConsumer1(ILogger<DataConsumer1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(DataConsumer1))]
        public async Task Run(
            [ServiceBusTrigger("queue1", Connection = "ServiceBusConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
