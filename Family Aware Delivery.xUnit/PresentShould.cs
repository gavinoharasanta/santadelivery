using Family_Aware_Delivery_Library;

namespace Multi_Elf.xUnit
{
    public class PresentShould
    {
        [Fact]
        public void ConstructCorrectly()
        {
            var present = new Present(PresentTypes.ToyBall, 42);

            Assert.Equal(PresentTypes.ToyBall, present.PresentType);
            Assert.Equal(42, present.OrderId);
        }
    }
}
