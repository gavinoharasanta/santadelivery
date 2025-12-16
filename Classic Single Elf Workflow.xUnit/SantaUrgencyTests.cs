using Classic_Single_Elf_Workflow_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classic_Single_Elf_Workflow.xUnit
{
    public class SantaUrgencySettingsTests
    {
        [Fact]
        public void ElfDelay_ReturnsScaledValue()
        {
            SantaUrgencySettings.santaUrgencyLevel = 3;
            var delay = SantaUrgencySettings.ElfDelay;

            Assert.Equal(3000, delay);
        }
    }
}
