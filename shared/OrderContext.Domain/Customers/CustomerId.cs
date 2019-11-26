using ImGalaxy.ES.Core;
using System; 

namespace OrderContext.Domain.Customers
{ 
    public class CustomerId : Identity<string>
    {
        public static CustomerId New => new CustomerId(Guid.NewGuid().ToString());
        public CustomerId(string id) : base(id)
        {
        }

        public static implicit operator string(CustomerId self) => self.Id;

        public static explicit operator CustomerId(string value) => new CustomerId(value);
    }
}
