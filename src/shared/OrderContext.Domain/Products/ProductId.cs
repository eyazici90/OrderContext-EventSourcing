using ImGalaxy.ES.Core; 

namespace OrderContext.Domain.Products
{ 
    public class ProductId : Identity<string>
    {
        public ProductId(string id) : base(id)
        {
        }

        public static implicit operator string(ProductId self) => self.Id;

        public static explicit operator ProductId(string value) => new ProductId(value);
    }
}
