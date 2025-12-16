namespace Family_Aware_Delivery_Library
{
    public class Order
    {
        public int Id { get; set; }

        public string FamilyName { get; set; }

        public Dictionary<PresentTypes, int> ToysOrdered { get; set; }

        public bool IsCancelled { get; set; }

        public Order(int id, string familyName, Dictionary<PresentTypes, int> toysOrdered)
        {
            Id = id;
            FamilyName = familyName;
            ToysOrdered = toysOrdered;
        }

        public void CancelOrder(object sender, OrderEventArgs args)
        {
            IsCancelled = (this == args.Order);
        }
    }
}
