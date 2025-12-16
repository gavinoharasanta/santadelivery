using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family_Aware_Delivery_Library
{
    public class OrderEventArgs : EventArgs
    {

        public Order Order { get; set; }

        public OrderEventArgs(Order order)
        {
            Order = order;
        }

    }
}
