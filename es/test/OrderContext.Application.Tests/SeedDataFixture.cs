using ImGalaxy.ES.Core;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders;
using OrderContext.Integration.Tests;
using System; 

namespace OrderContext.Application.Tests
{
    public class SeedDataFixture : OrderContextIntegrationTestBase
    {
        public IAggregateRootRepository<OrderState> RootRepository { get; }
        public IUnitOfWork UnitOfWork { get; }
        public OrderId FakeOrderId { get; }

        public int FakeMaxStudentNumber { get; }
        public SeedDataFixture()
        {
            FakeOrderId = OrderId.New; 

            RootRepository = The<IAggregateRootRepository<OrderState>>();
            UnitOfWork = The<IUnitOfWork>();

            SeedOrder();
        }

        private void SeedOrder()
        {
            var newCourse = Order.Create(FakeOrderId, new CustomerId(Guid.NewGuid().ToString()),
                "Amsterdam", "Fake-1");

            RootRepository.AddAsync(newCourse.State, FakeOrderId)
                .GetAwaiter().GetResult();

            UnitOfWork.SaveChangesAsync()
                .GetAwaiter().GetResult();
        }
    }
}
