using System;

namespace Musketeers
{
    public class CarWorkshop : ILocation
    {
        public CarWorkshop() { }

        public string CompanyName { get; set; }
        public string CarTrademarks { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
    }
}
