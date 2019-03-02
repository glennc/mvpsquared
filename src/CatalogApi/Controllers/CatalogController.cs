using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
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
        public ActionResult<IEnumerable<CatalogItem>> Get()
        {
            var items = _documentClient.CreateDocumentQuery<CatalogItem>(
                UriFactory.CreateDocumentCollectionUri("catalog", "items")
            ).ToList();

            return Ok(items);
        }
    }
}
