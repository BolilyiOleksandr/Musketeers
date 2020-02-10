using System;

namespace Musketeers
{
    public class CarWorkshop : ILocation
    {
        public CarWorkshop() { }
        public string CompanyName { get; set; }
        public string CarTrademarks { get; set; }
        string ILocation.City { get; set; }
        int ILocation.PostalCode { get; set; }
        string ILocation.Country { get; set; }
    }
}
