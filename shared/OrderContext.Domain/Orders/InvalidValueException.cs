using System; 

namespace OrderContext.Domain.Orders
{ 
    public class InvalidValueException : Exception
    {
        public InvalidValueException()
        { }

        public InvalidValueException(string value)
            : base($"{value} is not valid.")
        { }
    }
}
