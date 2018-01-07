using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace testapi
{
    public class Order
    {
        public int id { get; set; }
        public List<OrderItem> items { get; set; }
        public string order_date { get; set; }
        public int price => items.Sum((item) => item.price);
        public int count => items.Sum((item) => item.amount);
        Command hide;
        public Command HideCommand
        {
            get
            {
                if(hide == null)
                {
                    hide = new Command(Hide);
                }

                return hide;
            }
        }

        void Hide()
        {
            Global.client.Execute(new RestRequest("orders/" + id, Method.DELETE));
            MainWindow.window.navigation.Navigate(new Orders());
        }
    }
}
