using Xunit;

namespace OrderContext.Application.Tests.Scenarios.Given
{
    public class Given_an_order_with_an_in_memory_aggregate_store : Given_in_memory_aggregate_store,
       IClassFixture<SeedDataFixture>
    {
        protected SeedDataFixture SeedDataFixture { get; }
        public Given_an_order_with_an_in_memory_aggregate_store(SeedDataFixture seedDataFixture) =>
            SeedDataFixture = seedDataFixture;
    }
}
