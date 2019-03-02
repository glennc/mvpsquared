using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using OrderApi.Models;

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

            return Ok();
        }
    }
}
