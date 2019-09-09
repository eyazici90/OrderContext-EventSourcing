﻿using FluentAssertions;
using ImGalaxy.ES.TestBase;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Messages.Orders;
using OrderContext.Domain.Orders;
using OrderContext.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderContext.Domain.Tests
{
    public class OrderScens
    {
        [Fact]
        public void creating_new_order_with_valid_state_should_create_order_success()
        {
            var fakeOrderId = OrderId.New;
            var fakeBuyerId = CustomerId.New;
            var fakeCity = "Amsterdam";
            var fakeStreet = "Fake Street";

            var newOrder = Order.Create(fakeOrderId, fakeBuyerId, fakeCity, fakeStreet);

            CommandScenarioFor<OrderState>.With
                (newOrder.State) 
                .WhenNone()
                .Then(new OrderStartedEvent(fakeOrderId, fakeBuyerId, fakeCity, fakeStreet))
                .Assert();

        }

        [Fact]
        public void pay_existing_order_should_success()
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
        public void cancel_existing_order_should_success()
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
        public void add_order_item_to_existing_order_should_success()
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
        public void ship_order_without_paid_state_should_throw()
        {
            var order = FakeOrder;
            CommandScenarioFor<OrderState>.With
              (
                  order
              ) 
              .When(s => Order.ShipOrder(order))
              .Throws(typeof(OrderNotPaidYetException))
              .Assert();
        }


        public OrderState FakeOrder
        {
            get
            {
                var order = Order.Create(OrderId.New, CustomerId.New , "Amsterdam", "Fake Street").State;
                order.ClearEvents();
                return order;
            }
        }
    }
}