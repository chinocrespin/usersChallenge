using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Core.Common.Presentation
{
    public class HttpResult
    {
        public int Status { get; set; }

        [JsonProperty(PropertyName = "result", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Data { get; private set; }

        [JsonProperty(PropertyName = "errors", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ErrorResult> Errors { get; private set; }

        public HttpResult(int statusCode, dynamic data)
        {
            Status = statusCode;
            Data = data;
        }

        public HttpResult(int statusCode, IEnumerable<ErrorResult> errors)
        {
            Status = statusCode;
            Errors = errors;
        }

        public HttpResult(IEnumerable<ErrorResult> errors)
        {
            Errors = errors;
        }
    }
}
