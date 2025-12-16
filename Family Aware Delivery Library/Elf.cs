using System.Collections.Concurrent;

namespace Family_Aware_Delivery_Library
{
    public class Elf
    {
        private readonly BlockingCollection<Elf> availableElfs;
        private readonly Sleigh sleigh;

        public string Name { get; set; }
        public ConcurrentBag<FamilyToySack> FamilyToySacks { get; }
        public int Speed { get; set; }

        //public Present Present { get; set; }

        public List<Order> Orders { get; set; }

        public ConcurrentBag<Present> Bin { get; set; }

        public Elf(string name, int speed, BlockingCollection<Elf> availableElfs, ConcurrentBag<FamilyToySack> familyToySacks, Sleigh sleigh, ConcurrentBag<Present> bin)
        {
            Name = name;
            Speed = speed * SantaUrgencySettings.SantaUrgencyLevel;
            this.availableElfs = availableElfs;
            FamilyToySacks = familyToySacks;
            this.sleigh = sleigh;
            Bin = bin;
        }

        public void DeliverPresentToSleigh(object present)
        {
            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> {Name} + {((Present)present).PresentType}{((Present)present).OrderId} {Thread.CurrentThread.ManagedThreadId}");

            //Thread.Sleep(5000);
            Thread.Sleep(Speed);

            FamilyToySack familyToySack = FamilyToySacks.Where(fts => fts.Order.Id == ((Present)present).OrderId).First();

            if (Orders.Any(o => o.Id == ((Present)present).OrderId && o.IsCancelled)) // bin cancelled orders
            {
                Bin.Add((Present)present);
            }
            else
            {
                familyToySack.PackSack((Present)present);
                Console.WriteLine($"Pack {familyToySack.Order.FamilyName} + {((Present)present).PresentType} + Order {((Present)present).OrderId}");
            }

            BinAnyCancelledOrderPresentsInFamilySacks();

            if (IsSackCompleted(familyToySack))
            {
                familyToySack.IsCompleted = true;
                sleigh.PackSack(familyToySack);

            }

            if (FamilyToySacks.All(fts => fts.IsCompleted))
            {
                BinAnyCancelledOrderPresentsInSleigh();
            }

            int presentCount = 0;
            foreach (var sack in FamilyToySacks)
            {
                presentCount += sack.Presents.Count;

            }
            //Console.WriteLine($"{presentCount} packed in family sacks");

            ReturnToToyMachine();

        }

        public void ReturnToToyMachine()
        {
            //Console.WriteLine($"<<<<<<<<< {Name}");

            Thread.Sleep(Speed);

            availableElfs.Add(this);

        }

        private bool IsSackCompleted(FamilyToySack familyToySack)
        {
            bool isCompleted = true;

            foreach (var toyTypeOrder in familyToySack.Order.ToysOrdered)
            {
                if (familyToySack.Presents.Where(p => p.PresentType == toyTypeOrder.Key).Count() < toyTypeOrder.Value)
                {
                    isCompleted = false;
                    break;
                }
            }

            return isCompleted;
        }

        private void BinAnyCancelledOrderPresentsInFamilySacks()
        {
            foreach (FamilyToySack familyToySack in FamilyToySacks.Where(fts => fts.Order.IsCancelled))
            {
                foreach (Present present in familyToySack.Presents.ToArray())
                {
                    Bin.Add(present);
                }
                familyToySack.Presents.Clear();
                familyToySack.IsCompleted = true;
            }
        }

        private void BinAnyCancelledOrderPresentsInSleigh()
        {
            ConcurrentBag<FamilyToySack> familyToySacksForDelivery = new ConcurrentBag<FamilyToySack>();

            foreach (FamilyToySack familyToySack in sleigh.FamilyToySacks)
            {
                if (familyToySack.Order.IsCancelled)
                {
                    Console.WriteLine($"Unpacking {familyToySack.Order.FamilyName} {familyToySack.Order.Id} from sleigh...");
                    foreach (Present present in familyToySack.Presents)
                    {
                        Bin.Add(present);
                    }
                    familyToySack.Presents.Clear();
                    familyToySack.IsCompleted = true;
                }
                else
                {
                    familyToySacksForDelivery.Add(familyToySack);
                }
            }

            sleigh.FamilyToySacks = familyToySacksForDelivery;

        }
    }
}
