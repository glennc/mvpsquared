using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;

namespace CatalogApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private DocumentClient _documentClient;

        public CatalogController(DocumentClient documentClient)
        {
            _documentClient = documentClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get()
        {
            var items = _documentClient.CreateDocumentQuery<Item>(
                UriFactory.CreateDocumentCollectionUri("catalog", "items")
            ).ToList();

            return Ok(items);
        }
    }
}
