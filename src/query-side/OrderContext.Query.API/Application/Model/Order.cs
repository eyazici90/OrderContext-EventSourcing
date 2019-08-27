using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderContext.Query.API.Application.Model
{
    public class Order
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; } 
        public string BuyerId { get; set; } 
        public DateTime OrderDate{ get; set; }  
        public string OrderStatus { get; set; }
    }
}
