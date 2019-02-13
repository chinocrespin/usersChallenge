using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Common.Presentation;

namespace Identity.Domain.Queries
{
    public class UsersQuery : Query
    {
        [Range(1, 50)]
        public new int PageSize { get; set; }
    }
}
