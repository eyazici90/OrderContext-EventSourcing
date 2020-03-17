

using FluentAssertions;
using ImGalaxy.ES.Core;
using MediatR;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders;
using OrderContext.Integration.Tests;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OrderContext.Application.Tests.Commands.CreateOrderCommand
{
    public class CreateOrderCommandHandler_Tests : OrderContextIntegrationTestBase
    {
        private readonly IAggregateStore _aggregateStore;
        public CreateOrderCommandHandler_Tests()
        {
            _aggregateStore = The<IAggregateStore>();
        }

        [Fact] 
        public async Task Create_order_command_with_valid_command_should_success()
        {
            var fakeBuyerId = CustomerId.New;

            var command = new Domain.Messages.Orders.Commands.CreateOrderCommand(fakeBuyerId, "Amsterdam", "Fake 12312");

            var result = await new CreateOrderCommandHandler(_aggregateStore)
                .Handle(command, CancellationToken.None);

            result.Should().NotBeNullOrEmpty();
        }

    }
}
