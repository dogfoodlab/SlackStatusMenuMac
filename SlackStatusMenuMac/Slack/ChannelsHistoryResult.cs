using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class ChannelsHistoryResult : AbstractResult
    {
        [JsonProperty("latest")]
        public string Latest { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("unread_count_display")]
        public int UnreadCountDisplay { get; set; }
    }
}
