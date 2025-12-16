namespace Multi_Elf_Library
{
    public class Order
    {
        public int Id { get; set; }

        public string FamilyName { get; set; }

        public Dictionary<PresentTypes, int> ToysOrdered { get; set; }

        public Order(int id, string familyName, Dictionary<PresentTypes, int> toysOrdered)
        {
            Id = id;
            FamilyName = familyName;
            ToysOrdered = toysOrdered;
        }
    }
}
