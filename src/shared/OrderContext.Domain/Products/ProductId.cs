using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Products
{ 
    public class ProductId
    {  
        public readonly string Id;
        public ProductId(string id) => Id = id;
        public override string ToString() => Id;

        public static implicit operator string(ProductId self) => self.Id;

        public static explicit operator ProductId(string value) => new ProductId(value);
    }
}
