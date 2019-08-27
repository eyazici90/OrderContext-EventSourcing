using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderContext.Integration.Publisher.Application
{
    public interface IEventBus
    {
        Task PublishAsync(object @event);

        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class;
    }
}
