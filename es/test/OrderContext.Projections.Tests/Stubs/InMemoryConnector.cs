using OrderContext.Projections.Connectors;
using OrderContext.Query.Shared.Models;
using System.Collections.Concurrent; 
using System.Threading.Tasks;

namespace OrderContext.Projections.Tests.Stubs
{
    public class InMemoryConnector : ICosmosDbConnector
    {
        private readonly static ConcurrentDictionary<string, object> _states = new ConcurrentDictionary<string, object>();
        public async Task CreateAsync<T>(T state)
        {
            _states.TryAdd((state as Order).Id, state);
        }

        public async Task<T> ReadItemAsync<T>(string id)
        {
            _states.TryGetValue(id, out var state);
            return (T)state;
        }

        public async Task ReplaceAsync<T>(T state, string id)
        {
            _states.AddOrUpdate(id, state, (_, __) => state);
        }
    }
}
