using Multi_Elf_Library;
using System.Collections.Concurrent;

namespace Multi_Elf.xUnit
{
    public class OrderShould
    {
        [Fact]
        public void ConstructCorrectly()
        {
            string familyName = "O'Hara";
            Dictionary<PresentTypes, int> toysOrdered = new Dictionary<PresentTypes, int>();

            Order order = new Order(1, familyName, toysOrdered);

            Assert.True(order.Id == 1);
            Assert.Same(familyName, order.FamilyName);
            Assert.Same(toysOrdered, order.ToysOrdered);

        }
    }
}

