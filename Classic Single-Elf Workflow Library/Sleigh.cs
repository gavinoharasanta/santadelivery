using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classic_Single_Elf_Workflow_Library
{
    public class Sleigh
    {
        public List<Present> Presents { get; set; } = new List<Present>();

        public void Pack(Present present)
        { 
            Presents.Add(present);
            Console.WriteLine($"Sleigh: Pack {present.PresentType}, {Presents.Count} packed...");

        }


    }
}
