namespace Family_Aware_Delivery_Library
{
    public class ToyOrder
    {
        public int OrderId { get; set; }

        public PresentTypes PresentType { get; set; }

        public ToyOrder(PresentTypes presentType, int orderId)
        {
            PresentType = presentType;
            OrderId = orderId;
        }
    }
}
