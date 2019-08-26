using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Customers
{ 
    public class CustomerId
    {
        public static CustomerId New => new CustomerId(Guid.NewGuid().ToString());

        public readonly string Id;
        public CustomerId(string id) => Id = id;
        public override string ToString() => Id;

        public static implicit operator string(CustomerId self) => self.Id;

        public static explicit operator CustomerId(string value) => new CustomerId(value);
    }
}
