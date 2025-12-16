//using Classic_Single_Elf_Workflow_Library;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Classic_Single_Elf_Workflow_Unit_Tests
//{
//    internal class ToyMachineTests
//    {

//        [Fact]
//        public void Constructor_ShouldInitializeWithCorrectNumberOfPresents()
//        {
//            var machine = new ToyMachine(3);

//            Assert.True(machine.HasPresents);
//        }

//        [Fact]
//        public void PassPresentToElf_ShouldGiveElfAPresent()
//        {
//            var machine = new ToyMachine(1);
//            var elf = new Elf();

//            machine.PassPresentToElf(elf);

//            Assert.NotNull(elf.Present);
//            Assert.Equal("Toy", elf.Present.Name);
//        }

//        [Fact]
//        public void PassPresentToElf_ShouldReducePresentCount()
//        {
//            var machine = new ToyMachine(2);
//            var elf = new Elf();

//            machine.PassPresentToElf(elf);

//            Assert.True(machine.HasPresents); // one left
//        }

//        [Fact]
//        public void PassPresentToElf_ShouldSubscribeToElfEvent()
//        {
//            var machine = new ToyMachine(1);
//            var elf = new Elf();
//            bool handlerCalled = false;

//            elf.IsReadyForAnotherPresent += (s, p) => handlerCalled = true;

//            machine.PassPresentToElf(elf);

//            elf.ReturnToToyMachine(); // triggers event

//            Assert.True(handlerCalled);
//        }
//    }
//}



//}
