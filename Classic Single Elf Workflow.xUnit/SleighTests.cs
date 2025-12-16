using Classic_Single_Elf_Workflow_Library;

namespace Classic_Single_Elf_Workflow.xUnit
{
    public class SleighTests
    {
        [Fact]
        public void Pack_PresentPassed_PresentsCountEquals1()
        {
            Sleigh sleigh = new Sleigh();
            sleigh.Pack(new Present(PresentTypes.ToyDoll, 1));

            Assert.True(sleigh.Presents.Count == 1);
        }
    }
}
