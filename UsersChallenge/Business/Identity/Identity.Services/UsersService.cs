﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Presentation;
using Identity.Domain.IRepositories;
using Identity.Domain.IServices;
using Identity.Domain.Models;
using Identity.Domain.Queries;
using RandomUsers.Domain.Http;

namespace Identity.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRandomUsersHttpClient _randomUsersHttpClient;

        public UsersService(IRandomUsersHttpClient randomUsersHttpClient, IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _randomUsersHttpClient = randomUsersHttpClient;
        }

        public User GetById(string id)
        {
            return _usersRepository.GetById(id);
        }

        public bool Update(User user)
        {
            return _usersRepository.Update(user);
        }

        public bool Delete(string id)
        {
            User user = GetById(id);
            return _usersRepository.Delete(user);
        }

        public Result<User> GetAll(UsersQuery query)
        {
            if(query == null) query = new UsersQuery();
            Result<User> result = new Result<User>(query);
            IQueryable<User> usersQuery = _usersRepository.GetAll();
            // Should keep the count of total of elements so we can know if there are more pages to consult
            result.ElementsCount = usersQuery.Count();
            result.Elements = usersQuery.OrderBy(x => x.Name)
                .Skip(query.Since)
                .Take(query.To);
            return result;
        }

        public async Task<IEnumerable<User>> GetRandomUsers()
        {
            var randomUsers = await _randomUsersHttpClient.GetRandomUsers();
            return randomUsers.Select(x => new User
            {
                Gender = x.Gender,
                BirthDate = x.DateOfBirth.Date,
                Email = x.Email,
                Location = x.Location != null ? new Location
                {
                    City = x.Location.City,
                    PostCode = x.Location.PostCode,
                    State = x.Location.State,
                    Street = x.Location.Street
                } : null,
                Name = x.Name?.FullName,
                UserName = x.Login?.Username,
                Uuid = x.Login?.Uuid
            });
        }
    }
}
