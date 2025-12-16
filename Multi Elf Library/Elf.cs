using System.Collections.Concurrent;

namespace Multi_Elf_Library
{
    public class Elf
    {
        private readonly BlockingCollection<Elf> availableElfs;
        private readonly Sleigh sleigh;

        public string Name { get; set; }
        public int Speed { get; set; }

        public Present Present { get; set; }

        public Elf(string name, int speed, BlockingCollection<Elf> availableElfs, Sleigh sleigh)
        {
            Name = name;
            Speed = speed * SantaUrgencySettings.SantaUrgencyLevel;
            this.availableElfs = availableElfs;
            this.sleigh = sleigh;
        }

        public void DeliverPresentToSleigh(object present)
        {
            Console.WriteLine($">>>>>>>>>>> {Name} + {((Present)present).PresentType}");


            Thread.Sleep(Speed);

            sleigh.Pack((Present)present);

            ReturnToToyMachine();

        }

        public void ReturnToToyMachine()
        {
            Console.WriteLine($"<<<<<<<<< {Name}");
            
            Thread.Sleep(Speed);

            availableElfs.Add(this);

        }


    }
}
