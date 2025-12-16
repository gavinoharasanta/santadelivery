using Classic_Single_Elf_Workflow_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classic_Single_Elf_Workflow.xUnit
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
