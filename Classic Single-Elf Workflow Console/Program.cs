
using Classic_Single_Elf_Workflow_Library;

class Program
{
    static void Main()
    {
        List<string> families = new List<string>() { "Family1", "Family2", "Family3" };
        List<Order> orders = GenerateOrders(families);

        ToyMachine toyMachine = new ToyMachine();
        toyMachine.RegisterOrders(orders);
        toyMachine.CreatePresents();

        Elf elf = new Elf();
        Sleigh sleigh = new Sleigh();

        while (toyMachine.HasPresents)
        {
            toyMachine.PassPresentToElf(elf);
            elf.DeliverPresentToSleigh(sleigh);
            elf.ReturnToToyMachine();
        }

        Console.WriteLine("Process complete!");
    }

    private static List<Order> GenerateOrders(List<string> families)
    {
        List<Order> orders = new List<Order>();

        int orderIndex = 1;

        foreach (string family in families)
        {
            Dictionary<PresentTypes, int> toysOrdered = new Dictionary<PresentTypes, int>();
            toysOrdered.Add(PresentTypes.ToyCar, 3);
            toysOrdered.Add(PresentTypes.ToyPhone, 2);
            toysOrdered.Add(PresentTypes.ToyDoll, 3);
            toysOrdered.Add(PresentTypes.ToyBall, 1);

            orders.Add(new Order(orderIndex, family, toysOrdered));
            orderIndex++;
        }

        return orders;
    }
}




