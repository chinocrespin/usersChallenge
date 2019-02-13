using System;
using System.Collections.Generic;
using System.Text;

namespace RandomUsers.Domain.Models
{
    public class Location
    {
        public string State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public LocationCoordinates Coordinates { get; set; }
        public TimeZone Timezone { get; set; }
    }
}
