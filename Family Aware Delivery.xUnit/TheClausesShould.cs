using Family_Aware_Delivery_Library;

namespace Multi_Elf.xUnit
{
    public class TheClausesShould
    {

        [Fact]
        public void theClausesPackCorrectNumberOfPresentsInSleigh() 
        {

            int noOfToyMachine = 2;
            List<string> families = new List<string>() { "Family1", "Family2", "Family3" };

            TheClauses theClauses = new TheClauses(noOfToyMachine);

            theClauses.AcceptOrders(Utils.GenerateOrders(families, theClauses));
            theClauses.CreatedFamilyToySacks();

            theClauses.DistributeOrders();
            theClauses.ToyMachinesStartProcessing();

            theClauses.ProcessPresentsToPack();

            int presentCount = 0;
            foreach (var sack in theClauses.Sleigh.FamilyToySacks)
            {
                presentCount += sack.Presents.Count;
            }

            Assert.True(presentCount == 18);

        }

    }
}
