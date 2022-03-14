using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedShoppingList.Models;
using SharedShoppingList.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class UpdateShoppingListController : ControllerBase
    {
        private readonly string _pretendDatabaseFilePath = Path.Combine(Environment.CurrentDirectory, "7f6bb6e6-00d6-468a-b768-2333ae4cb6ed.json");

        private readonly ILogger<UpdateShoppingListController> _logger;

        public UpdateShoppingListController(ILogger<UpdateShoppingListController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> UpdateList(Guid id, ShoppingList shoppingList, CancellationToken cancellationToken)
        {
            if (Debugger.IsAttached)
            {
                foreach (var splat in shoppingList.Items) {
                    await PretendDatabase.Update(id, new ShoppingListItem { Item = splat.Key, Quantity = splat.Value }, cancellationToken);
                }
                return Ok(shoppingList);
            }
            else
            {
                return Ok("Beep boop!");
            }
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateItemInList(/*[Required]*/ Guid id, [FromQuery] ShoppingListItem shoppingListItem, CancellationToken cancellationToken)
        {
            if (Debugger.IsAttached)
            {
                return Ok(await PretendDatabase.Update(id, shoppingListItem, cancellationToken));
            }
            else
            {
                return NotFound("Beep boop!");
            }
        }
    }
}
