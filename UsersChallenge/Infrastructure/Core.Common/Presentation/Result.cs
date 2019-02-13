using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Presentation
{
    public sealed class Result<T>
    {
        public Result()
        {
            Elements = new List<T>();
        }

        public Result(Query query)
        {
            Elements = new List<T>();
            PageNumber = query.PageNumber;
            PageSize = query.PageSize;
        }

        public IEnumerable<T> Elements { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int ElementsCount { get; set; }
        public bool TieneMasResultados => (PageSize * PageNumber) < ElementsCount;
    }
}
