using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classic_Single_Elf_Workflow_Library
{
    public class ToyMachine
    {
        public bool HasPresents
        {
            get
            {
                return presents.Count > 0;
            }
            //set { myVar = value; }
        }

        private Queue<Present> presents = new Queue<Present>();

        private List<Order> orders;

        public ToyMachine()
        {
            orders = new List<Order>();
        }

        public void RegisterOrders(List<Order> orders)
        {
            this.orders = orders;
            Console.WriteLine($"Toy Machine: Orders recieved...");

        }

        public void CreatePresents()
        {
            foreach (var order in orders)
            {
                foreach (var toyOrder in order.ToysOrdered)
                {
                    for (int i = 0; i < toyOrder.Value; i++)
                    {
                        presents.Enqueue(new Present(toyOrder.Key, order.Id));

                    }
                }
            }
            Console.WriteLine($"Toy Machine: {orders.Count} orders, {presents.Count} toys produced...");

        }

        private void Elf_IsReadyForAnotherPresent(object? sender, Present present)
        {
            Elf elf = (Elf)sender;
            elf.IsReadyForAnotherPresent -= Elf_IsReadyForAnotherPresent;
        }


        public void PassPresentToElf(Elf elf)
        {
            elf.IsReadyForAnotherPresent += Elf_IsReadyForAnotherPresent;
            elf.Present = presents.Dequeue();
        }

    }

}
