using System;
using System.Collections.Generic;

namespace Musketeers
{
    public class User : ILocation
    {
        public User() { }

        public string Username { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public List<CarWorkshop> Appointment { get; set; }
    }
}
