using System.Collections.Concurrent;

namespace Family_Aware_Delivery_Library
{
    public class Sleigh
    {
        public ConcurrentBag<FamilyToySack> FamilyToySacks { get; set; } = new ConcurrentBag<FamilyToySack>();

        public void PackSack(FamilyToySack familyToySack)
        {
            FamilyToySacks.Add(familyToySack);
            Console.WriteLine($"******* {familyToySack.Order.FamilyName} toy sack packed");
        }
    }
}


