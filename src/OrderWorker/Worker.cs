using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OrderWorker
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISubscriptionClient _client;

        public Worker(ILogger<Worker> logger, ISubscriptionClient client)
        {
            _logger = logger;
            _client = client;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Subscribing to topic at: {DateTime.Now}");
            var messageHandlerOptions = new MessageHandlerOptions(ProcessExceptionAsync)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,
            };

            _client.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken cancellationToken = default)
        {
            var bytes = message.Body;
            var text = Encoding.UTF8.GetString(bytes);
            var order = JsonConvert.DeserializeObject<Order>(text);

            // TODO - do something with this...

            await _client.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async Task ProcessExceptionAsync(ExceptionReceivedEventArgs e)
        {
            _logger.LogInformation("Dragon Ball Z is awesome");
            // TODO integrate with health chexxx. @pranavkm. 100 pranav points for doing health chex.
        }
    }
}
