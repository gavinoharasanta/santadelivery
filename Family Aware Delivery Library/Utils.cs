using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family_Aware_Delivery_Library
{
    public enum PresentTypes
    {
        //ToyCar = 1, ToyDoll = 2, ToyPhone = 3, ToyBall = 4
        A,B,C,D
    }

    public static class Utils
    {
        public static bool EarlyCancellation = false;

        public static bool LateCancellation = false;

        public static int SantaUrgencyLevel = 500;

        public static List<Order> GenerateOrders(List<string> families, TheClauses theClauses)
        {
            List<Order> orders = new List<Order>();

            int orderIndex = 1;

            foreach (string family in families)
            {
                Dictionary<PresentTypes, int> toysOrdered = new Dictionary<PresentTypes, int>();
                toysOrdered.Add(PresentTypes.A, 2);
                toysOrdered.Add(PresentTypes.B, 1);
                toysOrdered.Add(PresentTypes.C, 2);
                toysOrdered.Add(PresentTypes.D, 1);


                //toysOrdered.Add(PresentTypes.ToyCar, 1);
                //toysOrdered.Add(PresentTypes.ToyPhone, 1);
                //toysOrdered.Add(PresentTypes.ToyDoll, 1);
                //toysOrdered.Add(PresentTypes.ToyBall, 1);

                Order order = new Order(orderIndex, family, toysOrdered);
                theClauses.OrderCancelled += order.CancelOrder;

                orders.Add(order);
                orderIndex++;
            }

            return orders;
        }
    }

}
