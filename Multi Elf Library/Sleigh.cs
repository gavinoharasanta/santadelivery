namespace Multi_Elf_Library
{
    public class Sleigh
    {
        static object lockObj = new object();

        public List<Present> Presents { get; set; } = new List<Present>();

        public void Pack(Present present)
        {
            Presents.Add(present);
            Console.WriteLine($"******* {present.PresentType} {Presents.Count} packed");

        }
        public static void Pack(Present present, List<Present> Presents)
        {
            lock (lockObj)
            {
                Presents.Add(present);
                Console.WriteLine($"******* {present.PresentType} {Presents.Count} packed");
            }
        }
    }
}

