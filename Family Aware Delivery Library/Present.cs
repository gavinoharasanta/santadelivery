using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family_Aware_Delivery_Library
{
    public class Present
    {
        public int OrderId { get; set; }

        public PresentTypes PresentType { get; set; }

        public Present(PresentTypes presentType, int orderId)
        {
            PresentType = presentType;
            OrderId = orderId;
        }
    }
}
