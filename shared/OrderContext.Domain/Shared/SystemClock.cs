using System; 

namespace OrderContext.Domain.Shared
{
    public static class SystemClock
    {
        public static Now Now = () => DateTime.UtcNow;
    }
}
