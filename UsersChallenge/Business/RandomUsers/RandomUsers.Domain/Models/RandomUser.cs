using Newtonsoft.Json;

namespace RandomUsers.Domain.Models
{
    public class RandomUser
    {
        public string Gender { get; set; }
        public Name Name { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public Login Login { get; set; }

        [JsonProperty("dob")]
        public DateOfBirth DateOfBirth { get; set; }

        public DateOfBirth Registered { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public Id Id { get; set; }
        public Picture Picture { get; set; }

        [JsonProperty("nat")]
        public string Nationality { get; set; }
    }
}
