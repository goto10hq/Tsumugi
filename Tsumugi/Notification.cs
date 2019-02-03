using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tsumugi
{
    public class Notification
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("emoji")]
        public string Emoji { get; set; }

        [JsonProperty("bot")]
        public string Bot { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("attachments")]
        public IEnumerable<Attachment> Attachments;

        public Notification()
        {            
        }

        public Notification(string channel, string emoji, string bot, string message)
        {
            Channel = channel;
            Emoji = emoji;
            Bot = bot;
            Message = message;
        }
    }
}
