using FluentAssertions;
using ImGalaxy.ES.Core;
using ImGalaxy.ES.TestBase;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderContext.Integration.Tests
{
    public class Repository_Tests : ImGalaxyIntegrationTestBase
    {
        protected override IServiceCollection ConfigureServices(IServiceCollection services) =>
            OrderContextIntegrationConfigurator.Configure(services);

        private readonly OrderId FAKE_ORDER_ID = OrderId.New;

        private readonly IAggregateRootRepository<OrderState> _aggregateRootRepository;
        public Repository_Tests()
        {
            _aggregateRootRepository = GetRequiredService<IAggregateRootRepository<OrderState>>();

            SeedOrder().ConfigureAwait(false)
                .GetAwaiter().GetResult();
        }

        private async Task SeedOrder()
        {
            var fakeOrder = Order.Create(FAKE_ORDER_ID, CustomerId.New, "Amsterdam", "FakeStreet");

            await WithUnitOfWorkAsync(async () =>
            {
                await _aggregateRootRepository.AddAsync(fakeOrder.State, FAKE_ORDER_ID);
            });

        }

        [Fact]
        public async Task create_should_success()
        { 
            var result = await WithUnitOfWorkAsync(async () =>
            {
                var fakeOrder = Order.Create(OrderId.New, CustomerId.New, "Amsterdam", "FakeStreet");
            });

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task get_existing_record_from_repository_shuold_return_record()
        {
            var existingOrder = await _aggregateRootRepository.GetAsync(FAKE_ORDER_ID);

            existingOrder.Should().NotBeNull();
        }

        [Fact]
        public async Task update_existing_record_should_update_record_success()
        {
            var existingOrder = await _aggregateRootRepository.GetAsync(FAKE_ORDER_ID); 

            var result = await WithUnitOfWorkAsync(async () =>
            {
                Order.PayOrder(existingOrder.Value);
            });

            result.IsSuccess.Should().BeTrue();

            var updatedOrder = await _aggregateRootRepository.GetAsync(FAKE_ORDER_ID);

            //updatedOrder.OrderStatus.Should().Be(OrderStatus.Paid);
        }
    }
}
