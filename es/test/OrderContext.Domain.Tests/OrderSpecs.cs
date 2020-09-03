using ImGalaxy.ES.TestBase;
using Moq;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders;
using OrderContext.Domain.Products;
using OrderContext.Domain.Shared;
using System;
using Xunit;
using static OrderContext.Domain.Messages.Orders.Events;

namespace OrderContext.Domain.Tests
{
    public class OrderSpecs
    {
        [Fact]
        public void Should_success_when_order_created()
        {
            var fakeOrderId = OrderId.New;
            var fakeBuyerId = CustomerId.New;
            var fakeCity = "Amsterdam";
            var fakeStreet = "Fake Street";
            DateTime now = SystemClock.Now();

            var newOrder = Order.Create(fakeOrderId, fakeBuyerId, fakeCity, fakeStreet, () => now);

            CommandScenarioFor<OrderState>.With
                (newOrder.State)
                .WhenNone()
                .Then(new OrderStartedEvent(fakeOrderId, fakeBuyerId, fakeCity, fakeStreet, now))
                .Assert();

        }

        [Fact]
        public void Should_succes_when_order_paid()
        {
            var existingOrder = FakeOrder;
            CommandScenarioFor<OrderState>.With
              (
                  existingOrder
              )
              .When(s => Order.PayOrder(s))
              .Then(new OrderPaidEvent(existingOrder.Id.ToString()))
              .Assert();
        }

        [Fact]
        public void Should_succes_when_order_cancelled()
        {
            var existingOrder = FakeOrder;
            CommandScenarioFor<OrderState>.With
              (
                  existingOrder
              )
              .When(s => Order.CancelOrder(s))
              .Then(new OrderCancelledEvent(existingOrder.Id.ToString()))
              .Assert();
        }

        [Fact]
        public void Should_succes_when_order_item_added()
        {
            var existingOrder = FakeOrder;
            var fakeProductId = new ProductId(Guid.NewGuid().ToString());
            var fakeOrderItemId = OrderItemId.New;
            var unitPrice = 100;
            var discount = 10;

            CommandScenarioFor<OrderState>.With
              (
                  existingOrder
              )
              .When(s => Order.AddOrderItem(s, fakeOrderItemId, fakeProductId, unitPrice, discount))
              .Then(new OrderItemAddedEvent(fakeOrderItemId, existingOrder.Id, fakeProductId, unitPrice, discount))
              .Assert();
        }

        [Fact]
        public void Should_throw_when_order_shipped_without_paid_state()
        {
            var order = FakeOrder;
            var moq = new Mock<IOrderPolicy>();

            moq.Setup(p => p.Apply(It.IsAny<OrderShouldBePaidBeforeShip>()))
                .Throws<OrderNotPaidYetException>();

            IOrderPolicy policy = moq.Object;

            CommandScenarioFor<OrderState>.With
              (
                  order
              )
              .When(s => Order.ShipOrder(order, policy))
              .Throws(typeof(OrderNotPaidYetException))
              .Assert();
        }


        private OrderState FakeOrder
        {
            get
            {
                var order = Order.Create(OrderId.New, CustomerId.New, "Amsterdam", "Fake Street", SystemClock.Now).State;
                order.ClearEvents();
                return order;
            }
        }
    }
}
