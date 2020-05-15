using System.Threading.Tasks;

namespace OrderContext.Projections.Connectors
{
    public interface ICosmosDbConnector
    {
        Task<T> ReadItemAsync<T>(string id);
        Task ReplaceAsync<T>(T state, string id);

        Task CreateAsync<T>(T state);
    }
}
