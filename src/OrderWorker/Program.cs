using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OrderWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config => config.AddUserSecrets<Program>())
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<Worker>();

                    var connectionString = context.Configuration["servicebus:connectionString"];
                    var topicName = context.Configuration["servicebus:topicName"];
                    var subscriptionName = context.Configuration["servicebus:subscriptionName"];

                    services.AddSingleton<ISubscriptionClient>(new SubscriptionClient(
                        connectionString,
                        topicName,
                        subscriptionName,
                        ReceiveMode.PeekLock,
                        RetryPolicy.Default));

                    services.AddSingleton<DocumentClient>(new DocumentClient(
                        new Uri(context.Configuration["database:accountEndpoint"]),
                        context.Configuration["database:accountKey"]));
                });
    }
}
