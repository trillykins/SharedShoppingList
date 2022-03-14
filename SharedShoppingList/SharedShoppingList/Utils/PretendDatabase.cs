using Newtonsoft.Json;
using SharedShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedShoppingList.Utils
{
    // Only for local debug purposes
    public class PretendDatabase
    {
        private static readonly string _pretendDatabaseFilePath = Path.Combine(Environment.CurrentDirectory, "7f6bb6e6-00d6-468a-b768-2333ae4cb6ed.json");


        public static async Task<ShoppingList> Fetch(Guid id, CancellationToken cancellationToken)
        {
            //if (System.IO.File.Exists(Path.Combine(_pretendDatabaseFilePath, $"{id}.json")))
            if (File.Exists(_pretendDatabaseFilePath))
            {

                var str = await File.ReadAllTextAsync(_pretendDatabaseFilePath, Encoding.UTF8, cancellationToken);
                return JsonConvert.DeserializeObject<ShoppingList>(str);

            }
            throw new FileNotFoundException($"Could not find database file: {_pretendDatabaseFilePath}");
        }

        public static async Task<ShoppingList> Update(Guid id, ShoppingListItem shoppingListItem, CancellationToken cancellationToken)
        {
            if (File.Exists(_pretendDatabaseFilePath))
            {
                var str = await File.ReadAllTextAsync(_pretendDatabaseFilePath, Encoding.UTF8, cancellationToken);
                var shoppingList = JsonConvert.DeserializeObject<ShoppingList>(str);
                if (shoppingList.Items.TryGetValue(shoppingListItem.Item, out var _))
                {
                    if (shoppingListItem.Quantity == 0)
                    {
                        Debug.WriteLine($"Removing {shoppingListItem.Item}");
                        shoppingList.Items.Remove(shoppingListItem.Item);
                    }
                    else
                    {
                        Debug.WriteLine($"Updating {shoppingListItem.Item} to {shoppingListItem.Quantity}");
                        shoppingList.Items[shoppingListItem.Item] = shoppingListItem.Quantity;
                    }
                }
                else
                {
                    Debug.WriteLine($"Inserting {shoppingListItem.Item} with quantatiy {shoppingListItem.Quantity}");
                    shoppingList.Items.Add(shoppingListItem.Item, shoppingListItem.Quantity);
                }

                var strShoppingList = JsonConvert.SerializeObject(shoppingList);
                await File.WriteAllTextAsync(_pretendDatabaseFilePath, strShoppingList, cancellationToken);
                return shoppingList;

            }
            throw new FileNotFoundException($"Could not find database file: {_pretendDatabaseFilePath}");

        }
    }
}
