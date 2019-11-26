using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderContext.Integration.Publisher.Application
{
    public class NullInstanceEventBus : IEventBus
    {
        public async Task PublishAsync(object @event) =>
            await Task.CompletedTask;

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class =>
            await Task.CompletedTask;
    }
}
