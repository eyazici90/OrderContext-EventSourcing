using FluentAssertions;
using MediatR;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Integration.Tests; 
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OrderContext.Application.Tests.Commands.CancelOrderCommand
{ 
    public class CancelOrderCommandHandler_Tests : OrderContextIntegrationTestBase,
        IClassFixture<SeedDataFixture>
    {
        private readonly SeedDataFixture _seedDataFixture;
        public CancelOrderCommandHandler_Tests(SeedDataFixture seedDataFixture) =>
            _seedDataFixture = seedDataFixture;


        [Fact]

        public async Task Cancel_order_command_with_valid_command_should_success()
        {
            var command = new Domain.Messages.Orders.Commands.CancelOrderCommand(_seedDataFixture.FakeOrderId);

            var result = await new CancelOrderCommandHandler(_seedDataFixture.UnitOfWork,
                _seedDataFixture.RootRepository)
                .Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);
        }
    }
}
