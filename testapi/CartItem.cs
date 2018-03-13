using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapi
{
    public class CartItem
    {
        public Item item { get; set; }
        public int quantity { get; set; }

        public CartItem()
        {

        }

        public CartItem(Item item, int quantity = 0)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }
}
