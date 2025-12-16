using Family_Aware_Delivery_Library;
using System;

class Program
{
    static void Main()
    {
        int noOfToyMachine = 2;
        List<string> families = new List<string>() { "Family1", "Family2", "Family3" };
        //List<string> families = new List<string>() { "Family1", "Family2" };


        TheClauses theClauses = new TheClauses(noOfToyMachine);

        theClauses.AcceptOrders(Utils.GenerateOrders(families, theClauses));

        theClauses.CreatedFamilyToySacks();

        Console.WriteLine($"The Clauses have {OrderPresentsMessage(theClauses.Orders)} presents to make...");

        theClauses.DistributeOrders();
        theClauses.ToyMachinesStartProcessing();

        if (Utils.EarlyCancellation) // cancels at a random second
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(CancelOrder), theClauses);
        }

        theClauses.ProcessPresentsToPack();


        Console.WriteLine($"Sleigh packed with {theClauses.Sleigh.FamilyToySacks.Count} family toy sacks...");
        int presentCount = 0;
        foreach (var sack in theClauses.Sleigh.FamilyToySacks)
        {
            presentCount += sack.Presents.Count;
        }
        Console.WriteLine($"Sleigh packed with {presentCount} presents...");
        Console.WriteLine($"Bin contains {theClauses.Bin.Count} presents...");

        Console.ReadLine();


    }

    static void CancelOrder(object theClauses)
    {
        // sleep fo 0 to 10s
        Random random = new Random();
        int randomIndex = random.Next(0, 10);
        Thread.Sleep(randomIndex * 1000);

        ((TheClauses)theClauses).OnCancelOrder(new OrderEventArgs(((TheClauses)theClauses).Orders[randomIndex]));

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
