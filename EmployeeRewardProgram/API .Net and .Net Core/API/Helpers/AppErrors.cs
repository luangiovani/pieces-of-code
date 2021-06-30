using Newtonsoft.Json;

namespace ERwPHelpers
{
    public class AppErrors
    {
        public int StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public AppErrors(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public AppErrors(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            this.Message = message;
        }
    }
}
