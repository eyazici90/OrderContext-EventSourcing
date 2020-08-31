namespace OrderContext.Domain.Orders
{
    public class OrderStatus
    {
        public static OrderStatus Submitted { get; } = new OrderStatus(1, nameof(Submitted).ToLowerInvariant());
        public static OrderStatus AwaitingValidation { get; } = new OrderStatus(2, nameof(AwaitingValidation).ToLowerInvariant());
        public static OrderStatus StockConfirmed { get; } = new OrderStatus(3, nameof(StockConfirmed).ToLowerInvariant());
        public static OrderStatus Paid { get; } = new OrderStatus(4, nameof(Paid).ToLowerInvariant());
        public static OrderStatus Shipped { get; } = new OrderStatus(5, nameof(Shipped).ToLowerInvariant());
        public static OrderStatus Cancelled { get; } = new OrderStatus(6, nameof(Cancelled).ToLowerInvariant());

        private readonly int _id;
        public string Name { get; }
        private OrderStatus(int id, string name)
        {
            _id = id;
            Name = name;
        }
    }
}
