using MediatR;
using System; 

namespace OrderContext.Domain.Messages.Orders
{
    public static class Events
    {
        public class OrderCancelledEvent : INotification
        {
            public string OrderId { get; }
            public OrderCancelledEvent(string orderId)
            {
                OrderId = orderId;
            }
        }
        public class OrderItemAddedEvent : INotification
        {
            public string OrderItemId { get; }
            public string OrderId { get; }
            public string ProductId { get; }
            public decimal UnitPrice { get; }
            public decimal Discount { get; }
            public OrderItemAddedEvent(string orderItemId, string orderId, string productId,
                decimal unitPrice, decimal discount)
            {
                OrderItemId = orderItemId;
                OrderId = orderId;
                ProductId = productId;
                UnitPrice = unitPrice;
                Discount = discount;
            }

        }

        public class OrderPaidEvent : INotification
        {
            public string OrderId { get; }
            public OrderPaidEvent(string orderId)
            {
                OrderId = orderId;
            }
        }
        public class OrderShippedEvent : INotification
        {
            public string OrderId { get; }
            public OrderShippedEvent(string orderId)
            {
                OrderId = orderId;
            }
        }

        public class OrderStartedEvent : INotification
        {
            public string OrderId { get; }
            public string BuyerId { get; }
            public string City { get; }
            public string Street { get; }
            public DateTime StartedDate { get; }
            public OrderStartedEvent(string orderId, string buyerId,
                string city, string street,
                DateTime startedDate)
            {
                OrderId = orderId;
                BuyerId = buyerId;
                City = city;
                Street = street;
                StartedDate = startedDate;
            }
        }
    }
}
