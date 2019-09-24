using ImGalaxy.ES.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public class AddresState : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }

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
