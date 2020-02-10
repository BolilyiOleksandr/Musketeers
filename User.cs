using System;

namespace Musketeers
{
    public class User : ILocation
    {
        public User() { }
        public string Username { get; set; }
        public string Email { get; set; }
        string ILocation.City { get; set; }
        int ILocation.PostalCode { get; set; }
        string ILocation.Country { get; set; }
    }
}
