using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public class OrderNotPaidYetException : Exception
    {
        public OrderNotPaidYetException()
        { }

        public OrderNotPaidYetException(string orderId)
            : base($"{orderId} order not paid yet.")
        { }
    }
}
