using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classic_Single_Elf_Workflow_Library
{
    public class Elf
    {
        public Present Present { get; set; }

        public event EventHandler<Present> IsReadyForAnotherPresent;

        public void DeliverPresentToSleigh(Sleigh sleigh)
        {
            Console.WriteLine($"Elf: Deliver present {Present.PresentType} to the Sleigh...");
            Thread.Sleep(SantaUrgencySettings.ElfDelay);

            sleigh.Pack(Present);
            Present = null;

        }

        public void ReturnToToyMachine()
        {
            Console.WriteLine($"Elf: Return to Toy Machine...");
            Thread.Sleep(SantaUrgencySettings.ElfDelay);

            OnReady();

        }

        protected virtual void OnReady()
        {
            IsReadyForAnotherPresent?.Invoke(this, Present);
        }


    }
}
