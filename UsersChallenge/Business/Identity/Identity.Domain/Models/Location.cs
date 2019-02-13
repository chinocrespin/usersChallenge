using System;
using System.Collections.Generic;
using Core.Common.Data;

namespace Identity.Domain.Models
{
    public class Location : Entity
    {
        public Location()
        {
            //Users = new HashSet<User>();
        }

        public string State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        //public virtual ICollection<User> Users { get; set; }
    }
}
