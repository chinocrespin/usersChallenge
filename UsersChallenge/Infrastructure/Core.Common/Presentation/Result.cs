using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Core.Common.Presentation
{
    public class Result
    {
        public int Status { get; set; }

        [JsonProperty(PropertyName = "result", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Data { get; private set; }

        [JsonProperty(PropertyName = "errors", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ErrorResult> Errors { get; private set; }

        public Result(int statusCode, dynamic data)
        {
            Status = statusCode;
            Data = data;
        }

        public Result(int statusCode, IEnumerable<ErrorResult> errors)
        {
            Status = statusCode;
            Errors = errors;
        }

        public Result(IEnumerable<ErrorResult> errors)
        {
            Errors = errors;
        }
    }
}
