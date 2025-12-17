using Family_Aware_Delivery_Library;
using System.Collections.Concurrent;

namespace Multi_Elf.xUnit
{
    public class ToyMachineShould
    {
        [Fact]
        public void ConstructCorrectly()
        {
            string name = "Gavin";

        }

        [Fact]
        public void RegisterAndProcessPassedOrder()
        {
            string familyName = "O'Hara";
            Dictionary<PresentTypes, int> toysOrdered = new Dictionary<PresentTypes, int>();
            toysOrdered.Add(PresentTypes.ToyPhone, 1);
            Order order = new Order(1, familyName, toysOrdered);

            BlockingCollection<Present> presentsToPack = new BlockingCollection<Present>();

            ToyMachine toyMachine = new ToyMachine(null, presentsToPack, new TheClauses(2));

            toyMachine.RegisterOrder(order);
            toyMachine.StartProcessingHandler();
            Thread.Sleep(5000);


            Assert.Contains(presentsToPack, p => p.PresentType == PresentTypes.ToyPhone);
            Assert.True(presentsToPack.Count == 1);

        }

    }

}
