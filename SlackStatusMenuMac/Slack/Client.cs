using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    public class Client
    {
        private const string API = "https://slack.com/api/";
        private string token = string.Empty;


        private Client() { }


        public Client(string token) : base()
        {
            this.token = token;
        }


        private WebClient build()
        {
            var client = new WebClient();
            client.BaseAddress = API;
            client.QueryString.Add(nameof(token), this.token);
            return client;
        }


        public ChannelsListResult ChannelsList(
            bool exclude_archived = false,
            bool exclude_members = false)
        {
            var client = this.build();
            client.QueryString.Add(nameof(exclude_archived), exclude_archived.ToString());
            client.QueryString.Add(nameof(exclude_members), exclude_members.ToString());

            var response = client.DownloadString("channels.list");
            var list = JsonConvert.DeserializeObject<ChannelsListResult>(response);

            return list;
        }


        public ChannelsInfoResult ChannelsInfo(
            string channel,
            bool include_locale = false)
        {
            var client = this.build();
            client.QueryString.Add(nameof(channel), channel);
            client.QueryString.Add(nameof(include_locale), include_locale.ToString());

            var response = client.DownloadString("channels.info");
            var info = JsonConvert.DeserializeObject<ChannelsInfoResult>(response);

            return info;
        }


        public ChannelsHistoryResult ChannelHistory(
            string channel,
            int count = 100,
            bool inclusive = false,
            string latest = "now",
            string oldest = "0",
            bool unreads = false
        )
        {
            var client = this.build();
            client.QueryString.Add(nameof(channel), channel);
            client.QueryString.Add(nameof(count), count.ToString());
            //client.QueryString.Add(nameof(inclusive), inclusive.ToString());
            //client.QueryString.Add(nameof(latest), latest);
            //client.QueryString.Add(nameof(oldest), oldest);
            client.QueryString.Add(nameof(unreads), unreads.ToString());

            var response = client.DownloadString("channels.history");
            var history = JsonConvert.DeserializeObject<ChannelsHistoryResult>(response);

            return history;
        }


        public GroupsListResult GroupsList(
            bool exclude_archived = false,
            bool exclude_members = false)
        {
            var client = this.build();
            client.QueryString.Add(nameof(exclude_archived), exclude_archived.ToString());
            client.QueryString.Add(nameof(exclude_members), exclude_members.ToString());

            var response = client.DownloadString("groups.list");
            var list = JsonConvert.DeserializeObject<GroupsListResult>(response);

            return list;
        }


        public GroupsInfoResult GroupsInfo(
            string channel,
            bool include_locale = false)
        {
            var client = this.build();
            client.QueryString.Add(nameof(channel), channel);
            client.QueryString.Add(nameof(include_locale), include_locale.ToString());

            var response = client.DownloadString("groups.info");
            var info = JsonConvert.DeserializeObject<GroupsInfoResult>(response);

            return info;
        }


        public GroupsHistoryResult GroupsHistory(
            string channel,
            int count = 100,
            bool inclusive = false,
            string latest = "now",
            string oldest = "0",
            bool unreads = false
        )
        {
            var client = this.build();
            client.QueryString.Add(nameof(channel), channel);
            client.QueryString.Add(nameof(count), count.ToString());
            //client.QueryString.Add(nameof(inclusive), inclusive.ToString());
            //client.QueryString.Add(nameof(latest), latest);
            //client.QueryString.Add(nameof(oldest), oldest);
            client.QueryString.Add(nameof(unreads), unreads.ToString());

            var response = client.DownloadString("groups.history");
            var history = JsonConvert.DeserializeObject<GroupsHistoryResult>(response);

            return history;
        }


        public UsersInfoResult UsersInfo(
            string user,
            bool include_locale = true
        )
        {
            var client = this.build();
            client.QueryString.Add(nameof(user), user);
            client.QueryString.Add(nameof(include_locale), include_locale.ToString());

            var response = client.DownloadString("users.info");
            var info = JsonConvert.DeserializeObject<UsersInfoResult>(response);

            return info;
        }


    }
}
