using System;
using System.Linq;
using Identity.Domain.Models;
using Xunit;

namespace Identity.Services.Test
{
    public class UsersServiceTest
    {
        private readonly UsersService _usersService;

        public UsersServiceTest()
        {
            _usersService = new UsersService(null, null);
        }

        [Fact]
        public void TestAsignSeniorUser()
        {
            var users = new[]
            {
                new User{ BirthDate = new DateTime(2017, 10, 15), Name = "User 1" },
                new User{ BirthDate = new DateTime(1942, 7, 28), Name = "User 2" },
                new User{ BirthDate = new DateTime(1985, 5, 8), Name = "User 3" },
                new User{ BirthDate = new DateTime(2005, 1, 1), Name = "User 4" },
                new User{ BirthDate = new DateTime(2002, 7, 7), Name = "User 5" },
                new User{ BirthDate = new DateTime(2000, 9, 15), Name = "User 6" },
                new User{ BirthDate = new DateTime(1952, 5, 14), Name = "User 7" },
                new User{ BirthDate = new DateTime(1962, 4, 1), Name = "User 8" },
                new User{ BirthDate = new DateTime(2019, 1, 22), Name = "User 9" },
                new User{ BirthDate = new DateTime(1933, 5, 5), Name = "User 10" },
                new User{ BirthDate = new DateTime(1933, 6, 6), Name = "User 11" },
            };

            var orderedUsers = _usersService.AsignSeniorUser(users, users.Length).ToArray();
            UserResult senior = orderedUsers[0];
            Assert.True(!orderedUsers.Any(x => x.BirthDate < senior.BirthDate));
        }
    }
}
