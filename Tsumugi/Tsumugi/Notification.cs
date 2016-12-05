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

        [JsonProperty("botName")]
        public string BotName { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; } = DateTime.Now;

        public Notification()
        {            
        }

        public Notification(string channel, string emoji, string botName, string message)
        {
            Channel = channel;
            Emoji = emoji;
            BotName = botName;
            Message = message;
        }
    }
}
