using System.Collections.Generic;
using Newtonsoft.Json;

namespace StoryLine.Wiremock.Contracts
{
    public class RequestHeadersDictionary : Dictionary<string, object>
    {
        [JsonIgnore]
        public object ContentType
        {
            get { return this["Content-Type"]; }
            set { this["Content-Type"] = value;}
        }
    }
}