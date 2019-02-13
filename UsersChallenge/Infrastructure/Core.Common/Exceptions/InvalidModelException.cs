using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Presentation;

namespace Core.Common.Exceptions
{
    [Serializable]
    public class InvalidModelException : ApplicationException, ICustomException
    {
        public IEnumerable<string> Errors { get; }

        public InvalidModelException(string message) : base(message)
        {
            Errors = new List<string>() { message };
        }

        public InvalidModelException(IEnumerable<string> messages)
        {
            Errors = messages;
        }

        public IEnumerable<ErrorResult> GetErrors()
        {
            return Errors.Select(error => new ErrorResult { Error = error });
        }
    }
}
