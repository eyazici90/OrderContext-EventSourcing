using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System; 

namespace OrderContext.Integration.Publisher.Functions.Extensions
{
    public static class DocumentExtensions
    {
        public static dynamic ToDynamic(this Document @event)
        {
            var eData = ((dynamic)@event).Data;
            var eType = ((dynamic)@event).Type;

            var type = Convert.ToString(eType);

            var castedEvent = JsonConvert.DeserializeObject(value: eData, type: Type.GetType(type));

            return castedEvent;
        }
    }
}
