using ImGalaxy.ES.Core;
using OrderContext.Integration.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Application.Tests
{
    public abstract class Given_in_memory_aggregate_store : OrderContextIntegratedTestBase
    {
        protected IAggregateStore AggregateStore { get; }
        public Given_in_memory_aggregate_store()
        {
            AggregateStore = The<IAggregateStore>();
        }
    }
}
