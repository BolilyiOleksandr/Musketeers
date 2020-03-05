using System;
using System.Collections.Generic;

namespace Musketeers
{
    public class Appointment
    {
        public Appointment()
        {
        }

        public Dictionary<string, Tuple<string, string, DateTime>> Appointments { get; set; }
    }
}
