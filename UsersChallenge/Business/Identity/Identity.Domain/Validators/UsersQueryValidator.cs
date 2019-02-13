using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Identity.Domain.Queries;

namespace Identity.Domain.Validators
{
    public class UsersQueryValidator : AbstractValidator<UsersQuery>
    {
        public UsersQueryValidator()
        {
            RuleFor(query => query.PageNumber).GreaterThan(0);
            RuleFor(query => query.PageSize).InclusiveBetween(1, 50);
        }
    }
}
