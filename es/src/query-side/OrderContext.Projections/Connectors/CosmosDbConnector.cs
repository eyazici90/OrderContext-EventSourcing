using Microsoft.Azure.Cosmos;
using OrderContext.Query.Shared;
using System.Threading.Tasks;

namespace OrderContext.Projections.Connectors
{
    public class CosmosDbConnector : ICosmosDbConnector
    {
        private readonly Container _container;
        public CosmosDbConnector(CosmosClient cosmosClient) =>
            _container = cosmosClient.GetContainer(SettingConsts.DATABASE, SettingConsts.ORDER_CONTAINER);

        public async Task CreateAsync<T>(T state) =>
            await _container.CreateItemAsync(state).ConfigureAwait(false);

        public async Task<T> ReadItemAsync<T>(string id)
        {
            var result = await _container.ReadItemAsync<T>(id, PartitionKey.None).ConfigureAwait(false);
            return result.Resource;
        }

        public async Task ReplaceAsync<T>(T state, string id) =>
            await _container.ReplaceItemAsync(state, id).ConfigureAwait(false);
    }
}
