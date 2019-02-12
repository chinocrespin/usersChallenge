using System;
using System.Collections.Generic;
using System.Text;
using Core.Common.Presentation;

namespace Core.Common.Exceptions
{
    public interface ICustomException
    {
        IEnumerable<ErrorResult> GetErrors();
    }
}
