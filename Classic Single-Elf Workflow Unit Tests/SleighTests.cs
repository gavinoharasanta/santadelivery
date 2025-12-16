using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classic_Single_Elf_Workflow_Unit_Tests
{
    internal class SleighTests
    {
        [Fact]
        public void Sleigh_ShouldReceivePresents()
        {
            var sleigh = new Sleigh();
            var present = new Present("Ball");

            sleigh.Presents.Push(present);

            Assert.Single(sleigh.Presents);
        }
    }
}
}
