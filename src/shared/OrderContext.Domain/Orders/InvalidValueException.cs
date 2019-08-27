using System;
using System.Collections.Generic;
using System.Text;

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
