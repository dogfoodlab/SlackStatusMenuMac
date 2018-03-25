using System;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class GroupsInfoResult
    {
        [JsonProperty("ok")]
        public bool OK { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("group")]
        public Group Group { get; set; }
    }
}
