using System;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace OrderContext.Projections.Extensions
{
    public static class ObjectExtensions
    {
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
