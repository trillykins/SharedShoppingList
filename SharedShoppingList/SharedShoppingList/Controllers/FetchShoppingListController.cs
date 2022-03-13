using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedShoppingList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FetchShoppingListController : ControllerBase
    {
        private readonly string _pretendDatabaseFilePath = Path.Combine(Environment.CurrentDirectory, "pretenddatabase.json");

        private readonly ILogger<FetchShoppingListController> _logger;

        public FetchShoppingListController(ILogger<FetchShoppingListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get(CancellationToken cancellationToken)
        {
            if (Debugger.IsAttached)
            {
                
                return Ok(await PretendDatabase(cancellationToken));
            }
            else
            {
                return Ok("Beep boop!");
            }
        }

        private async Task<string> PretendDatabase(CancellationToken cancellationToken)
        {
            var test = new List<ShoppingListItem> { new ShoppingListItem { Item = "mælk", Quantity = 2 }, new ShoppingListItem { Item = "ymer", Quantity = 1 }, new ShoppingListItem { Item = "lego vespa", Quantity = 1 } };
            Console.WriteLine(JsonConvert.SerializeObject(test));

            if (System.IO.File.Exists(_pretendDatabaseFilePath))
            {
                return await System.IO.File.ReadAllTextAsync(_pretendDatabaseFilePath, Encoding.UTF8, cancellationToken);

            }
            throw new FileNotFoundException($"Could not find database file: {_pretendDatabaseFilePath}");
        }
    }
}
