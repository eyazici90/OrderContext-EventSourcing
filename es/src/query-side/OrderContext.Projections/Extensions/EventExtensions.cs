using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System; 

namespace OrderContext.Projections.Extensions
{
    public static class EventExtensions
    {
        public static dynamic DeserializeToEvent(this Document @event)
        {
            var eData = ((dynamic)@event).Data;
            var eType = ((dynamic)@event).Type;

            var type = Convert.ToString(eType);

            return JsonConvert.DeserializeObject(value: eData, type: Type.GetType(type));
        }
    }
}
