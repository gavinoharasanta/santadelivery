using Family_Aware_Delivery_Library;

namespace Family_Aware_Delivery.xUnit
{
    public class PresentTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var present = new Present(PresentTypes.ToyBall, 42);

            Assert.Equal(PresentTypes.ToyBall, present.PresentType);
            Assert.Equal(42, present.OrderId);
        }
    }
}
