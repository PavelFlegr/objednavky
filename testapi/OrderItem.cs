using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace testapi
{
    public class OrderItem
    {
        static List<Item> items;
        public OrderItem()
        {
            if(items == null)
            {
                var response1 = Global.client.Execute<List<Item>>(new RestRequest("items"));
                items = response1.Data;
            }
        }
        public int id { get; set; }
        public int amount { get; set; }
        public int price => items.First(item => item.id == id).price * amount;
        
    }
}
