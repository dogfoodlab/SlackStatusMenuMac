using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class GroupsListResult : AbstractResult
    {
        [JsonProperty("groups")]
        public List<Group> Groups { get; set; }
    }
}
