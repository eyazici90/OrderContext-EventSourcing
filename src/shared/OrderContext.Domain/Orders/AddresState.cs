using ImGalaxy.ES.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public class AddresState : ValueObject
    {
        public readonly string Street;
        public readonly string City;
        public readonly string State;
        public readonly string Country;
        public readonly string ZipCode;

        private AddresState() { }

        internal AddresState(string street, string city, string state, string country, string zipcode) : this()
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
