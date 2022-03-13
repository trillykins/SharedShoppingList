using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharedShoppingList.Models
{
    public class ShoppingListItem
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 1;
    }
}
