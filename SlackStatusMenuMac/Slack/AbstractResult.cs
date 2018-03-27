using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public abstract class AbstractResult
    {
        [JsonProperty("ok")]
        public bool OK { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
