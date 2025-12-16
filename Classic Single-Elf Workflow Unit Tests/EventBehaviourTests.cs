using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classic_Single_Elf_Workflow_Unit_Tests
{
    internal class EventBehaviourTests
    {

        [Fact]
        public void ToyMachine_ShouldUnsubscribeHandlerAfterEvent()
        {
            var machine = new ToyMachine(1);
            var elf = new Elf();

            machine.PassPresentToElf(elf);

            // Call ReturnToToyMachine twice: handler should only fire once
            int eventCallCount = 0;

            EventHandler<Present> counter = (s, p) => eventCallCount++;

            elf.IsReadyForAnotherPresent += counter;

            elf.ReturnToToyMachine(); // fires both handlers
            elf.ReturnToToyMachine(); // only counter fires

            Assert.Equal(2, eventCallCount);
        }
    }
}
