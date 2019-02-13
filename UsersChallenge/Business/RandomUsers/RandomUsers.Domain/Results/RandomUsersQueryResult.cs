using System;
using System.Collections.Generic;
using System.Text;
using RandomUsers.Domain.Models;

namespace RandomUsers.Domain.Results
{
    public class RandomUsersQueryResult
    {
        public IEnumerable<RandomUser> Results { get; set; }
    }
}
