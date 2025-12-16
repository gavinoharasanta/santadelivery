using Multi_Elf_Library;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        int noOfToyMachine = 2;
        List<string> families = new List<string>() { "Family1", "Family2", "Family3" };

        TheClauses theClauses = new TheClauses(noOfToyMachine);
        theClauses.GenerateOrders(families);

        Console.WriteLine($"The Clauses have {OrderPresentsMessage(theClauses.Orders)} presents to make...");

        theClauses.DistributeOrders();
        theClauses.ToyMachinesStartProcessing();
        theClauses.ProcessPresentsToPack();

        Console.WriteLine($"Sleigh packed with {theClauses.Sleigh.Presents.Count} presents...");


    }

    static string OrderPresentsMessage(List<Order> orders)
    {
        int toyOrderCount = 0;
        foreach (Order order in orders)
        {
            foreach (var toyOrder in order.ToysOrdered)
            {
                toyOrderCount += toyOrder.Value;
            }
        }

        return toyOrderCount.ToString();
    }

}

