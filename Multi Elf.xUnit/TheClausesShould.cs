using Multi_Elf_Library;
using System.Collections.Concurrent;

namespace Multi_Elf.xUnit
{
    public class TheClausesShould
    {
        [Fact]
        public void ConstructCorrectly()
        {

            TheClauses theClauses = new TheClauses(2);

            Assert.True(theClauses.ToyMachines.Count == 2);
        }

            [Fact]
        public void GenerateOrdersCorrectly()
        {

            TheClauses theClauses = new TheClauses(2);

            Assert.True(theClauses.ToyMachines.Count == 2);
        }


        [Fact]
        public void theClausesPackCorrectNumberOfPresentsInSleigh() 
        {

            int noOfToyMachine = 2;
            List<string> families = new List<string>() { "Family1", "Family2", "Family3" };

            TheClauses theClauses = new TheClauses(noOfToyMachine);
            theClauses.GenerateOrders(families);


            theClauses.DistributeOrders();
            theClauses.ToyMachinesStartProcessing();
            theClauses.ProcessPresentsToPack();

            Assert.True(theClauses.Sleigh.Presents.Count == 27);

        }

    }
}
