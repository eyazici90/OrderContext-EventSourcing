
using ImGalaxy.ES.CosmosDB.Documents;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System; 

namespace OrderContext.Snapshotter.Functions.Extensions
{
    public static class DocumentExtensions
    {
        public static EventDocument ToEventDoc(this Document @event)
        {
            var e = ((dynamic)@event);

            var doc = Convert.ToString(e);

            var castedDoc = JsonConvert.DeserializeObject<EventDocument>(doc);

            return castedDoc;
        }
    }
}
