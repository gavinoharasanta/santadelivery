using Multi_Elf_Library;
using System.Collections.Concurrent;

namespace Multi_Elf.xUnit
{
    public class ElfShould
    {
        [Fact]
        public void DeliverAndPackPresentOnSleigh()
        {
            BlockingCollection<Elf> availableElfs = new BlockingCollection<Elf>();
            Sleigh sleigh = new Sleigh();
            Elf elf = new Elf(null, 1, availableElfs, sleigh);
            Present present = new Present(PresentTypes.ToyDoll, 0);
            
            elf.DeliverPresentToSleigh(present);

            Assert.Same(present, sleigh.Presents[0]);
        }

        [Fact]
        public void ReturnItselfToTheAvailableElfsCollection()
        {
            BlockingCollection<Elf> availableElfs = new BlockingCollection<Elf>();
            Elf elf = new Elf(null, 1, availableElfs, null);

            elf.ReturnToToyMachine();

            Assert.Same(elf, availableElfs.Take());
        }
    }
}
