using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CatalogApi.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Hosting;

namespace CatalogApi.Services
{
    public class DatabaseSeeder : BackgroundService
    {
        private readonly DocumentClient _documentClient;

        public DatabaseSeeder(DocumentClient documentClient)
        {
            _documentClient = documentClient;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var items = _documentClient.CreateDocumentQuery<Item>(
                UriFactory.CreateDocumentCollectionUri("catalog", "items")
            ).ToList();

            if(items.Any()) return Task.FromResult(0);

            // the catalog is empty, add some items
            var factory = UriFactory.CreateDocumentCollectionUri("catalog", "items");
            _documentClient.CreateDocumentAsync(factory,
                new Item {
                    Id = Guid.NewGuid().ToString(), 
                    Name = "Apple", 
                    Description = "Shiny, red, grown in California", 
                    Price = 0.65
                }
            );
            _documentClient.CreateDocumentAsync(factory,
                new Item {
                    Id = Guid.NewGuid().ToString(), 
                    Name = "Banana", 
                    Description = "The perfect food", 
                    Price = 0.89
                }
            );
            _documentClient.CreateDocumentAsync(factory,
                new Item {
                    Id = Guid.NewGuid().ToString(), 
                    Name = "Kiwi", 
                    Description = "Cute, fuzzy, interesting accent", 
                    Price = .95
                }
            );
            _documentClient.CreateDocumentAsync(factory,
                new Item {
                    Id = Guid.NewGuid().ToString(), 
                    Name = "Cashews", 
                    Description = "Great protein snack", 
                    Price = 3.99
                }
            );

            return Task.FromResult(0);
        }
    }
}