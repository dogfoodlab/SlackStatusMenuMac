using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class ChannelsListResult : AbstractResult
    {
        [JsonProperty("channels")]
        public List<Channel> Channels { get; set; }
    }
}
