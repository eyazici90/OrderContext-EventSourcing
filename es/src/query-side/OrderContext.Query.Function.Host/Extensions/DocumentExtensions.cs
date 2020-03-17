using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderContext.Query.Function.Host.Extensions
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

        public static async Task ForEachAsync<T>(this IEnumerable<T> docList,
            Func<T, Task> func)
        {
            foreach (var doc in docList)
            {
                await func(doc);
            }
        }
    }
}
