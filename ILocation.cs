using System;

namespace Musketeers
{
    public interface ILocation
    {
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
    }
}
