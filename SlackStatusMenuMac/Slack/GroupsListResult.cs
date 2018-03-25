using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class GroupsListResult
    {
        [JsonProperty("ok")]
        public bool OK { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("groups")]
        public List<Group> Groups { get; set; }
    }
}
