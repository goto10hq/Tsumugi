using System;
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
