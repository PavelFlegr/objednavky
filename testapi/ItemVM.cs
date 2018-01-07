using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapi
{
    public class ItemVM
    {
        public ItemVM(Item item, int amount = 0)
        {
            this.item = item;
            this.amount = amount;
        }
        public Item item { get; set; }
        public int amount { get; set; }
    }
}
