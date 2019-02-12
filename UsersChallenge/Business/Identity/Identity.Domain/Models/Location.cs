using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.Models
{
    public class Location
    {
        public string State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
