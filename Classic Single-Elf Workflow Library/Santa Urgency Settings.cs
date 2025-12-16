using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classic_Single_Elf_Workflow_Library
{
    public static class SantaUrgencySettings
    {

		public static int santaUrgencyLevel = 1;
		
		private static int elfDelay;

		public static int ElfDelay
		{
			get 
			{ 
				return elfDelay * santaUrgencyLevel; 
			}
		}

        static SantaUrgencySettings()
        {
            elfDelay = 1000;
        }

    }
}
