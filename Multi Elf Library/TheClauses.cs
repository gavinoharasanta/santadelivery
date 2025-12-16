using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Multi_Elf_Library
{
    public class TheClauses
    {

        BlockingCollection<Present> presentsToPack = new BlockingCollection<Present>();
        BlockingCollection<Elf> availableElfs = new BlockingCollection<Elf>();

        int noOfToyMachines;

        public delegate void Notify();
        public event Notify StartCreatingPresents;

        public List<Order> Orders { get; set; }

        public Sleigh Sleigh { get; set; } = new Sleigh();
        public List<ToyMachine> ToyMachines { get; set; }

        Dictionary<string, int> santasElfsSpeedByName = new Dictionary<string, int>()
        {
            ["Alabaster"] = 1,
            ["Snowball"] = 2,
            ["Bushy"] = 2,
            ["Evergreen"] = 3,
            //"Pepper",
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

        public void ProcessPresentsToPack()
        {

            using (var finished = new CountdownEvent(1))
            {

                Present presentToPack = null;
                while (!presentsToPack.IsCompleted)
                {
                    try
                    {
                        presentToPack = presentsToPack.Take();
                        finished.AddCount();

                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Adding was completed!");
                        break;
                    }

                    Elf elf = availableElfs.Take();
                    elf.Present = presentToPack;

                    ThreadPool.QueueUserWorkItem((state) =>
                                                  {
                                                      try
                                                      {
                                                          elf.DeliverPresentToSleigh(presentToPack);
                                                      }
                                                      finally
                                                      {
                                                          finished.Signal(); // Signal that the work item is complete.
                                                      }
                                                  }
                                                  , null);
                }

                finished.Signal(); // Signal that queueing is complete.
                finished.Wait(); // Wait for all work items to complete.
            }
        }


        private void GetElfs()
        {
            foreach (KeyValuePair<string, int> elf in santasElfsSpeedByName)
            {
                availableElfs.Add(new Elf(elf.Key, elf.Value, availableElfs, Sleigh));
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

        public void GenerateOrders(List<string> families)
        {
            Orders = new List<Order>();

            int orderIndex = 1;

            foreach (string family in families)
            {
                Dictionary<PresentTypes, int> toysOrdered = new Dictionary<PresentTypes, int>();
                toysOrdered.Add(PresentTypes.ToyCar, 3);
                toysOrdered.Add(PresentTypes.ToyPhone, 2);
                toysOrdered.Add(PresentTypes.ToyDoll, 3);
                toysOrdered.Add(PresentTypes.ToyBall, 1);

                Orders.Add(new Order(orderIndex, family, toysOrdered));
                orderIndex++;
            }
        }

        public void DistributeOrders()
        {
            int i = 0;
            foreach (Order order in Orders)
            {
                ToyMachines[i].RegisterOrder(order);
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


    }
}
