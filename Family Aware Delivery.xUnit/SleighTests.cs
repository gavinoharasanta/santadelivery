using Family_Aware_Delivery_Library;

namespace Family_Aware_Delivery.xUnit
{
    public class SleighTests
    {
        [Fact]
        public void Pack_PresentPassed_PresentsCountEquals1()
        {
            Sleigh sleigh = new Sleigh();
            sleigh.Pack(new Present(PresentTypes.ToyDoll, 1));

            Assert.True(sleigh.Presents.Count == 1);
        }
    }
}
