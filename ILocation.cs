using System;

namespace Musketeers
{
    public interface ILocation
    {
        string City { get; set; }
        int PostalCode { get; set; }
        string Country { get; set; }
    }
}
