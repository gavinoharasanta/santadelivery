using Family_Aware_Delivery_Library;

namespace Family_Aware_Delivery.xUnit
{
    public class ElfTests
    {
        [Fact]
        public void DeliverPresentToSleigh_PacksPresent()
        {
            var sleigh = new Sleigh();
            var elf = new Elf();
            elf.Present = new Present(PresentTypes.ToyPhone, 5);

            elf.DeliverPresentToSleigh(sleigh);

            Assert.Single(sleigh.Presents);
            Assert.Null(elf.Present);
        }

        [Fact]
        public void ReturnToToyMachine_RaisesReadyEvent()
        {
            var elf = new Elf();
            bool wasRaised = false;

            elf.IsReadyForAnotherPresent += (s, p) => wasRaised = true;

            elf.ReturnToToyMachine();

            Assert.True(wasRaised);
        }
    }
}
