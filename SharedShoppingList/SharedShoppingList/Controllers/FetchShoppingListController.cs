using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedShoppingList.Models;
using SharedShoppingList.Utils;
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
        private readonly string _pretendDatabaseFilePath = Path.Combine(Environment.CurrentDirectory, "7f6bb6e6-00d6-468a-b768-2333ae4cb6ed.json");

        private readonly ILogger<FetchShoppingListController> _logger;

        public FetchShoppingListController(ILogger<FetchShoppingListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            if (Debugger.IsAttached)
            {
                
                return Ok(await PretendDatabase.Fetch(id, cancellationToken));
            }
            else
            {
                return Ok("Beep boop!");
            }
        }
    }
}
