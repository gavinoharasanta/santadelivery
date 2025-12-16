using Family_Aware_Delivery_Library;

namespace Family_Aware_Delivery.xUnit
{
    public class ToyMachineTests
    {
        [Fact]
        public void HasPresents_ShouldBeFalse_WhenNoPresentsCreated()
        {
            var machine = new ToyMachine();
            Assert.False(machine.HasPresents);
        }

        [Fact]
        public void RegisterOrders_PassOrders_HasPresentsTrue()
        {
            List<Order> orders = new List<Order>();
            orders.Add(new Order(1, "family1", new Dictionary<PresentTypes, int> {  [PresentTypes.ToyBall] = 1 }));

            ToyMachine toyMachine = new ToyMachine();

            toyMachine.RegisterOrders(orders);
            toyMachine.CreatePresents();

            Assert.True(toyMachine.HasPresents);

        }

        [Fact]
        public void CreatePresents_RegisterOrdersCreateRemovePresents_HasPresentsFalse()
        {
            List<Order> orders = new List<Order>();
            orders.Add(new Order(1, "family1", new Dictionary<PresentTypes, int> { [PresentTypes.ToyBall] = 5 }));
            orders.Add(new Order(1, "family1", new Dictionary<PresentTypes, int> { [PresentTypes.ToyCar] = 2 }));

            ToyMachine toyMachine = new ToyMachine();

            toyMachine.RegisterOrders(orders);
            toyMachine.CreatePresents();

            for(var i = 0; i < 7; i++)
            {
                toyMachine.PassPresentToElf(new Elf());
            }


            Assert.True(toyMachine.HasPresents == false);
        }


    }

}
