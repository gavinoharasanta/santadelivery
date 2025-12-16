using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Elf_Library
{
    public enum PresentTypes
    {
        ToyCar = 1, ToyDoll = 2, ToyPhone = 3, ToyBall = 4
    }

    public static class Utils
    {
        static object lockObj = new object();

        public static void WriteMessage(string message, ConsoleColor colour)
        {
            lock (lockObj)
            {
                Console.ForegroundColor = colour;
                Console.WriteLine("\t" + message);
                Console.ForegroundColor = ConsoleColor.White;

            }
        }
    }

}
