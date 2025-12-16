using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family_Aware_Delivery_Library
{
    public class FamilyToySack
    {
        public Order Order { get; set; }

        public List<Present> Presents { get; set; } = new List<Present>();

        public bool IsCompleted { get; set; }

        public FamilyToySack(Order order)
        {
            Order = order;
        }
        public void PackSack(Present present)
        {
            Presents.Add(present);
        }
    }
}
