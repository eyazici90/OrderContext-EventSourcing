using ImGalaxy.ES.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Customers
{
    public class CustomerState : AggregateRootState<CustomerState>
    {
        private CustomerId _id;
        private CustomerState()
        { 
        }


        internal CustomerState(CustomerId id) : this() =>
            EnsureValidState();


        private void EnsureValidState() =>
             _id.ThrowsIfNull(new ArgumentNullException(_id));
    }
}
