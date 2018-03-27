using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class ChannelsInfoResult : AbstractResult
    {
        [JsonProperty("channel")]
        public Group Channel { get; set; }
    }
}
