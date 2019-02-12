using Newtonsoft.Json;

namespace Core.Common.Presentation
{
    public class ErrorResult
    {
        public string Error { get; set; }

        [JsonProperty(PropertyName = "error_description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }
}
