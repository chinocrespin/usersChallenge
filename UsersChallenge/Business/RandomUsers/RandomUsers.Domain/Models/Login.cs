using System;
using System.Collections.Generic;
using System.Text;

namespace RandomUsers.Domain.Models
{
    public class Login
    {
        public string Uuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Md5 { get; set; }
        public string Sha1 { get; set; }
        public string Sha256 { get; set; }

    }
}
