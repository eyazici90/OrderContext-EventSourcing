using OrderContext.Query.API.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderContext.Query.API.Application
{
    public static class ReadModelForOrderContext
    {
        public static object locker = new object();

        public static IList<Order> Orders;
        static ReadModelForOrderContext()
        {
            Orders = new List<Order>();
        }
         
    }

    public static class StaticExtensions
    {
        public static void Lock(this object @obj, Action act)
        {
            lock (@obj)
            {
                act();
            }
        }
    }
}
