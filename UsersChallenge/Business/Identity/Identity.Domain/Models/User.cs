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

    public class UserResult : Comparable
    {
        public UserResult() { }

        public UserResult(User user)
        {
            IdValue = user.IdValue;
            Gender = user.Gender;
            Name = user.Name;
            Email = user.Email;
            BirthDate = user.BirthDate;
            Uuid = user.Uuid;
            UserName = user.UserName;
            Location = user.Location;
        }

        public string IdValue { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Uuid { get; set; }
        public string UserName { get; set; }
        public Location Location { get; set; }
        public bool IsSenior { get; set; }

        public override int CompareTo(object x)
        {
            if (!(x is UserResult)) return 0;
            return (((UserResult) x).BirthDate - BirthDate).Days;
        }
    }
}
