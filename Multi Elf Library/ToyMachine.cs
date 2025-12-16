using System.Collections.Concurrent;

namespace Multi_Elf_Library
{
    public class ToyMachine
    {
        public delegate void Notify();
        public event Notify FinishedProcessing;

        private List<Order> orders;
        private readonly BlockingCollection<Present> presentsToPack;
        private Dictionary<PresentTypes, int> toysCreated = new Dictionary<PresentTypes, int>();
        public string Name { get; set; }

        public bool OrdersProcessed { get; set; }

        public ToyMachine(string name, BlockingCollection<Present> presentsToPack, TheClauses theClauses)
        {
            orders = new List<Order>();
            Name = name;
            this.presentsToPack = presentsToPack;
            theClauses.StartCreatingPresents += StartProcessingHandler;
        }

        public void StartProcessingHandler()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessOrders!), null);
        }

        public void RegisterOrder(Order order)
        {
            this.orders.Add(order);
        }

        private void ProcessOrders(object e)
        {
            foreach (var order in orders)
            {
                foreach (var toyOrder in order.ToysOrdered)
                {
                    for (int i = 0; i < toyOrder.Value; i++)
                    {
                        Thread.Sleep((int)toyOrder.Key * SantaUrgencySettings.SantaUrgencyLevel);
                        presentsToPack.Add(new Present(toyOrder.Key, order.Id));

                        if (!toysCreated.ContainsKey(toyOrder.Key))
                        {
                            toysCreated.Add(toyOrder.Key, 1);
                        }
                        else
                        {
                            toysCreated[toyOrder.Key]++;
                        }

                    }
                }
            }

            List<string> toyCounts = new List<string>();
            foreach(var toyCreated in toysCreated)
            {
                toyCounts.Add(string.Format("{0}={1}", toyCreated.Key.ToString(), toyCreated.Value.ToString()));
            }

            Console.WriteLine($"{Name} finished. {string.Join(",", toyCounts)} toys created...");

            OrdersProcessed = true;
            OnFinishedProcessing();
        }

        protected virtual void OnFinishedProcessing()
        {
            FinishedProcessing?.Invoke();
        }

    }

}
