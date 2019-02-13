using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Data;
using Core.Common.Exceptions;
using Core.Common.Presentation;
using FluentValidation.Results;
using Identity.Domain.IRepositories;
using Identity.Domain.IServices;
using Identity.Domain.Models;
using Identity.Domain.Queries;
using Identity.Domain.Validators;
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

        public Result<UserResult> GetAll(UsersQuery query)
        {
            if(query == null) query = new UsersQuery();
            UsersQueryValidator validator = new UsersQueryValidator();
            ValidationResult validation = validator.Validate(query);
            if(!validation.IsValid)
                throw new InvalidModelException(validation.Errors.Select(x => x.ErrorMessage));

            Result<UserResult> result = new Result<UserResult>(query);
            IQueryable<User> usersQuery = _usersRepository.GetAll();
            // Should keep the count of total of elements so we can know if there are more pages to consult
            result.ElementsCount = usersQuery.Count();
            //result.Elements = usersQuery.OrderBy(x => x.Name)
            //    .Skip(query.Since)
            //    .Take(query.To);
            var users = usersQuery.OrderBy(x => x.Name)
                .Skip(query.Since)
                .Take(query.To);

            Heap<UserResult> heap = new Heap<UserResult>(query.PageSize, false);
            foreach (var usr in users)
            {
                heap.Add(new UserResult(usr));
            }
            var array = heap.GetHeap();
            array[0].IsSenior = true;
            result.Elements = array;

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
