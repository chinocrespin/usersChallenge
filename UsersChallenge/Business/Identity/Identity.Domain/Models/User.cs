using System;
using Core.Common.Data;

namespace Identity.Domain.Models
{
    public class User : Entity
    {
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Uuid { get; set; }
        public string UserName { get; set; }
        public virtual Location Location { get; set; }
    }
}
