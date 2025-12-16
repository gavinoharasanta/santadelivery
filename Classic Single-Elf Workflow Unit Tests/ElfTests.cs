using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classic_Single_Elf_Workflow_Unit_Tests
{
    internal class ElfTests
    {
        [Fact]
        public void DeliverPresentToSleigh_ShouldMovePresentToSleigh()
        {
            var elf = new Elf();
            var sleigh = new Sleigh();
            elf.Present = new Present("Test Toy");

            elf.DeliverPresentToSleigh(sleigh);

            Assert.Single(sleigh.Presents);
            Assert.Equal("Test Toy", sleigh.Presents.Peek().Name);
        }

        [Fact]
        public void DeliverPresentToSleigh_ShouldClearElfPresent()
        {
            var elf = new Elf();
            var sleigh = new Sleigh();
            elf.Present = new Present("Test Toy");

            elf.DeliverPresentToSleigh(sleigh);

            Assert.Null(elf.Present);
        }

        [Fact]
        public void ReturnToToyMachine_ShouldRaiseIsReadyForAnotherPresentEvent()
        {
            var elf = new Elf();
            bool eventRaised = false;

            elf.IsReadyForAnotherPresent += (sender, present) =>
            {
                eventRaised = true;
            };

            elf.ReturnToToyMachine();

            Assert.True(eventRaised);
        }
    }
}
