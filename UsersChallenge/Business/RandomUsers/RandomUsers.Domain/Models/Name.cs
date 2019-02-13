using System;
using System.Collections.Generic;
using System.Text;

namespace RandomUsers.Domain.Models
{
    public class Name
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }

        public string FullName => $"{Title} {First} {Last}".Trim();
    }
}
