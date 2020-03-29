﻿using FluentAssertions;
using MediatR;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Integration.Tests;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OrderContext.Application.Tests.Commands.PayOrderCommand
{
    public class PayOrderCommandHandler_Tests : Given_in_memory_aggregate_store
    {
        private Unit _result;
        public PayOrderCommandHandler_Tests(SeedDataFixture seedDataFixture) : base(seedDataFixture)
        {
            When(async () =>
            {
                var command = new Domain.Messages.Orders.Commands.PayOrderCommand(SeedDataFixture.FakeOrderId);

                _result = await new PayOrderCommandHandler(SeedDataFixture.AggregateStore).Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public void Then_command_should_be_succeed()
        {
            _result.Should().Be(Unit.Value);
        }
    }
    public class Given_in_memory_aggregate_store : OrderContextIntegrationTestBase,
        IClassFixture<SeedDataFixture>
    {
        protected SeedDataFixture SeedDataFixture { get; }
        public Given_in_memory_aggregate_store(SeedDataFixture seedDataFixture) =>
            SeedDataFixture = seedDataFixture;

    }
}
