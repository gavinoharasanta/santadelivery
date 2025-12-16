using System.Collections.Concurrent;

namespace Family_Aware_Delivery_Library
{
    public class ToyMachine
    {
        public delegate void Notify();
        public event Notify FinishedProcessing;

        private List<ToyOrder> toyOrders;
        private readonly BlockingCollection<Present> presentsToPack;
        private Dictionary<PresentTypes, int> toysCreated = new Dictionary<PresentTypes, int>();
        public string Name { get; set; }

        public bool OrdersProcessed { get; set; }

        public List<Order> Orders { get; set; }

        public ToyMachine(string name, BlockingCollection<Present> presentsToPack, TheClauses theClauses)
        {
            toyOrders = new List<ToyOrder>();
            Name = name;
            this.presentsToPack = presentsToPack;
            theClauses.StartCreatingPresents += StartProcessingHandler;
        }

        public void StartProcessingHandler()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessOrders!), null);
        }

        public void RegisterToyOrder(ToyOrder toyOrder)
        {
            this.toyOrders.Add(toyOrder);
        }

        private void ProcessOrders(object e)
        {
            foreach (var toyOrder in toyOrders)
            {
                if (Orders.Any(o => o.Id == toyOrder.OrderId && o.IsCancelled)) // don't make cancelled orders
                { 
                    continue; 
                }

                Thread.Sleep((int)toyOrder.PresentType * SantaUrgencySettings.SantaUrgencyLevel);
                presentsToPack.Add(new Present(toyOrder.PresentType, toyOrder.OrderId));

                if (!toysCreated.ContainsKey(toyOrder.PresentType))
                {
                    toysCreated.Add(toyOrder.PresentType, 1);
                }
                else
                {
                    toysCreated[toyOrder.PresentType]++;
                }
            }

            List<string> toyCounts = new List<string>();
            foreach (var toyCreated in toysCreated)
            {
                toyCounts.Add(string.Format("{0}={1}", toyCreated.Key.ToString(), toyCreated.Value.ToString()));
            }

            //Console.WriteLine($"{Name} finished. {string.Join(",", toyCounts)} toys created...");

            OrdersProcessed = true;
            OnFinishedProcessing();
        }

        protected virtual void OnFinishedProcessing()
        {
            FinishedProcessing?.Invoke();
        }

    }

}
