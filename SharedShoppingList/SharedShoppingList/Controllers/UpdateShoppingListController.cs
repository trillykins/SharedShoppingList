using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedShoppingList.Models;
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
        private readonly string _pretendDatabaseFilePath = Path.Combine(Environment.CurrentDirectory, "pretenddatabase.json");

        private readonly ILogger<UpdateShoppingListController> _logger;

        public UpdateShoppingListController(ILogger<UpdateShoppingListController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> UpdateList(ShoppingList shoppingList, CancellationToken cancellationToken)
        {
            await Task.FromResult(0);
            if (Debugger.IsAttached)
            {
                return Ok(await UpdatePretendDatabase(null, cancellationToken));
            }
            else
            {
                return Ok("Beep boop!");
            }
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateItemInList([Required] Guid userId, [FromQuery] ShoppingListItem shoppingListItem, CancellationToken cancellationToken)
        {
            await Task.FromResult(0);
            if (Debugger.IsAttached)
            {
                return Ok(await UpdatePretendDatabase(shoppingListItem, cancellationToken));
            }
            else
            {
                return NotFound("Beep boop!");
            }
        }

        private async Task<string> UpdatePretendDatabase(ShoppingListItem shoppingListItem, CancellationToken cancellationToken)
        {
            if (System.IO.File.Exists(_pretendDatabaseFilePath))
            {
                var str = await System.IO.File.ReadAllTextAsync(_pretendDatabaseFilePath, Encoding.UTF8, cancellationToken);
                var list = JsonConvert.DeserializeObject<IEnumerable<ShoppingListItem>>(str);
                var map = list.ToDictionary(x => x.Item, x => x.Quantity);
                if (map.TryGetValue(shoppingListItem.Item, out var _))
                {
                    if (shoppingListItem.Quantity == 0)
                    {
                        Console.WriteLine($"Removing {shoppingListItem.Item}");
                        map.Remove(shoppingListItem.Item);
                    }
                    else
                    {
                        Console.WriteLine($"Updating {shoppingListItem.Item} to {shoppingListItem.Quantity}");
                        map[shoppingListItem.Item] = shoppingListItem.Quantity;
                    }
                }
                else
                {
                    Console.WriteLine($"Inserting {shoppingListItem.Item} with quantatiy {shoppingListItem.Quantity}");
                    map.Add(shoppingListItem.Item, shoppingListItem.Quantity);
                }

                var result = JsonConvert.SerializeObject(map.Select(x => new ShoppingListItem { Item = x.Key, Quantity = x.Value }));
                await System.IO.File.WriteAllTextAsync(_pretendDatabaseFilePath, result, cancellationToken);
                return result;

            }
            throw new FileNotFoundException($"Could not find database file: {_pretendDatabaseFilePath}");

        }
    }
}
