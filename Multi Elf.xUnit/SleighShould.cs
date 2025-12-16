using Multi_Elf_Library;

namespace Multi_Elf.xUnit
{
    public class SleighShould
    {
        [Fact]
        public void PackPassedPresent()
        {
            Sleigh sleigh = new Sleigh();
            Present present = new Present(PresentTypes.ToyDoll, 1);
            sleigh.Pack(present);

            Assert.Same(present, sleigh.Presents[0]);
        }
    }
}
