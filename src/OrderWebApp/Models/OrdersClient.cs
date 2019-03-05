using System.Net.Http;
using System.Threading.Tasks;
using OrderWebApp.Models;

namespace OrderWebApp
{
    public class OrdersClient
    {
        private readonly HttpClient client;

        public OrdersClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task PlaceOrderAsync(Order order)
        {
            var response = await client.PostAsJsonAsync("/orders", order).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
