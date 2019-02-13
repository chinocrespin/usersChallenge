using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Common.Presentation
{
    public abstract class Query
    {
        [Range(1, Double.PositiveInfinity)]
        public int PageNumber { get; set; }

        [Range(1, 50)]
        public int PageSize { get; set; }

        public virtual int Since => (PageNumber - 1) * PageSize;
        public virtual int To => PageNumber * PageSize + PageSize;
    }
}
