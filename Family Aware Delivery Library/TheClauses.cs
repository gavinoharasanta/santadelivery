using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Family_Aware_Delivery_Library
{
    public class TheClauses
    {
        BlockingCollection<Present> presentsToPack = new BlockingCollection<Present>();
        BlockingCollection<Elf> elves = new BlockingCollection<Elf>();

        int noOfToyMachines;

        public delegate void Notify();
        public event Notify StartCreatingPresents;

        public event EventHandler<OrderEventArgs> OrderCancelled;

        public List<Order> Orders { get; set; }

        public Sleigh Sleigh { get; set; } = new Sleigh();
        public List<ToyMachine> ToyMachines { get; set; }

        public ConcurrentBag<FamilyToySack> FamilyToySacks { get; set; } = new ConcurrentBag<FamilyToySack>();

        public ConcurrentBag<Present> Bin { get; set; } = new ConcurrentBag<Present>();

        Dictionary<string, int> santasElfsSpeedByName = new Dictionary<string, int>()
        {
            ["Alabaster"] = 1,
            ["Snowball"] = 2,
            ["Bushy"] = 2,
            ["Evergreen"] = 3,
            ["Pepper"] = 2,
            //"Minstix",
            //"Shinny",
            //"Upatree",
            //"Sugarplum",
            //"Mary",
            //"Wunorse",
            //"Openslae"
        };

        public TheClauses(int noOfToyMachines)
        {
            this.noOfToyMachines = noOfToyMachines;
            GetElfs();
            ToyMachines = GetToyMachines();
        }

        public void AcceptOrders(List<Order> orders)
        {
            this.Orders = orders;

            foreach (var toyMachine in ToyMachines)
            {
                toyMachine.Orders = orders;
            }

            foreach (var elf in elves)
            {
                elf.Orders = orders;
            }
        }

        public void ProcessPresentsToPack()
        {
            var events = new List<ManualResetEvent>();
 
            int i = 0;
            foreach (Present presentToPack in presentsToPack.GetConsumingEnumerable())
            {
                Console.WriteLine($"presentsToPack.Take() {presentToPack.PresentType}{presentToPack.OrderId} {Thread.CurrentThread.ManagedThreadId}");

                bool waitingForElf = true;

                while (waitingForElf)
                {
                    if (elves.TryTake(out Elf elf, millisecondsTimeout: 25000))
                    {
                        waitingForElf = false;
                        
                        Console.WriteLine($"*************************{elf.Name}");

                        var resetEvent = new ManualResetEvent(false);
                        ThreadPool.QueueUserWorkItem(arg =>
                                                    {
                                                        elf.DeliverPresentToSleigh(presentToPack);
                                                        resetEvent.Set();
                                                    });

                        Console.WriteLine($"Queued Elf {elf.Name} {presentToPack.PresentType} Order {presentToPack.OrderId}");
                        
                        events.Add(resetEvent);
                        //Console.WriteLine($"xxxxxxxxxxxxxxxxx POst QueueUserWorkItem {elf.Name} {presentToPack.PresentType}{presentToPack.OrderId}  {Thread.CurrentThread.ManagedThreadId}");

                    }
                }


                if (Utils.LateCancellation && i == 10) // cancels late
                {
                    OnCancelOrder(new OrderEventArgs(Orders[0]));
                }

                i++;
            }

            WaitHandle.WaitAll(events.ToArray());

        }


        private void GetElfs()
        {
            foreach (KeyValuePair<string, int> elf in santasElfsSpeedByName)
            {
                elves.Add(new Elf(elf.Key, elf.Value, elves, FamilyToySacks, Sleigh, Bin));
            }
        }

        private List<ToyMachine> GetToyMachines()
        {
            List<ToyMachine> toyMachines = new List<ToyMachine>();

            for (int i = 0; i < noOfToyMachines; i++)
            {
                ToyMachine toyMachine = new ToyMachine($"Toy Machine {i.ToString()}", presentsToPack, this);
                toyMachine.FinishedProcessing += ToyMachine_FinishedProcessing;
                toyMachines.Add(toyMachine);
            }

            return toyMachines;
        }

        private void ToyMachine_FinishedProcessing()
        {
            if (ToyMachines.All(tm => tm.OrdersProcessed))
            {
                presentsToPack.CompleteAdding();
            }
        }

        public void CreatedFamilyToySacks()
        {
            foreach (Order order in Orders)
            {
                FamilyToySacks.Add(new FamilyToySack(order));
            }
        }

        public void DistributeOrders()
        {
            List<ToyOrder> toysOrders = new List<ToyOrder>();

            var validOrders = Orders.Where(o => o.IsCancelled == false);

            foreach (var order in validOrders) // flatten out into list of ToyOrders
            {
                foreach (var toyOrder in order.ToysOrdered)
                {
                    for (int j = 0; j < toyOrder.Value; j++)
                    {
                        toysOrders.Add(new ToyOrder(toyOrder.Key, order.Id));
                    }
                }
            }

            // distribute randomly to toy machines
            int i = 0;
            Random random = new Random();
            while (toysOrders.Count > 0)
            {
                int randomIndex = random.Next(0, toysOrders.Count);
                ToyOrder toyOrder = toysOrders[randomIndex];
                toysOrders.Remove(toyOrder);

                ToyMachines[i].RegisterToyOrder(toyOrder);

                if (i == ToyMachines.Count - 1)
                {
                    i = 0;
                }
                else
                {
                    i++;
                }
            }

        }

        public void ToyMachinesStartProcessing()
        {
            StartCreatingPresents?.Invoke();
        }

        public void OnCancelOrder(OrderEventArgs e)
        {
            OrderCancelled?.Invoke(this, e);
            Console.WriteLine($"Order for {e.Order.FamilyName} cancelled...");

        }
    }
}
