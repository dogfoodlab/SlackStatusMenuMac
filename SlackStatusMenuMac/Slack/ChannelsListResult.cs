using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class ChannelsListResult
    {
        [JsonProperty("ok")]
        public bool OK { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("channels")]
        public List<Channel> Channels { get; set; }
    }
}
