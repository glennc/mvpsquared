using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OrderWorker
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly DocumentClient _documentClient;

        public Worker(ILogger<Worker> logger, ISubscriptionClient subscriptionClient, DocumentClient documentClient)
        {
            _logger = logger;
            _subscriptionClient = subscriptionClient;
            _documentClient = documentClient;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Subscribing to topic at: {DateTime.Now}");
            var messageHandlerOptions = new MessageHandlerOptions(ProcessExceptionAsync)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Message received, processing");
            var bytes = message.Body;
            var text = Encoding.UTF8.GetString(bytes);
            var order = JsonConvert.DeserializeObject<Order>(text);

            // TODO - do something with this...

            await _documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("orders", "order"), order);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async Task ProcessExceptionAsync(ExceptionReceivedEventArgs e)
        {
            _logger.LogError(e.Exception, "Error During Processing");
            // TODO integrate with health chexxx. @pranavkm. 100 pranav points for doing health chex.
        }
    }
}
