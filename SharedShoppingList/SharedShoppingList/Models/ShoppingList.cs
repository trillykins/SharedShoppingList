using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharedShoppingList.Models
{
    public class ShoppingList
    {
        public Guid UserID { get; set; }

        public Dictionary<string, int> Items { get; set; }
    }
}
