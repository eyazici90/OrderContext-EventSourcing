using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public static class Address
    {
        public static AddresState Create(string street, string city, string state, string country, string zipcode) =>
            new AddresState(street, city, state, country, zipcode);
    }
}
