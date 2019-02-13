using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Presentation
{
    public abstract class Query
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public virtual int Since => (PageNumber - 1) * PageSize;
        public virtual int To => PageNumber * PageSize + PageSize;
    }
}
