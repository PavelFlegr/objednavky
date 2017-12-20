using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapi
{
    class Order
    {
        public int id { get; set; }
        public List<OrderItem> items { get; set; }
    }
}
