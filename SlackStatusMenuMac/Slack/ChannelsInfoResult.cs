using System;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class ChannelsInfoResult
    {
        [JsonProperty("ok")]
        public bool OK { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("channel")]
        public Group Channel { get; set; }
    }
}
