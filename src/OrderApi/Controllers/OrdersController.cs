using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Models;

namespace OrderApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private ITopicClient _topicClient;

        public OrdersController(ITopicClient client)
        {
            _topicClient = client;
        }

        [HttpPost()]
        public async Task<IActionResult> SubmitOrder(Order order)
        {
            // these are new orders so we'll generate ids in the api
            order.Id = Guid.NewGuid().ToString();
            
            var message = new Message();
            message.Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(order));
            await _topicClient.SendAsync(message);

            return Ok();
        }
    }
}
