using System; 

namespace OrderContext.Query.Shared.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
    }
}
