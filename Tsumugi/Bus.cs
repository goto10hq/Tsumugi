using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Momo.Tokens;
using Newtonsoft.Json;

namespace Tsumugi
{
    public class Bus
    {
        private readonly string _connectionString;
        private readonly string _queue;
        private readonly string _channel;
        private readonly string _emoji;
        private readonly string _bot;
        private readonly QueueClient _queueClient;

        public Action<Exception> ErrorHandler { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="tsumugiConfiguration">Tsumugi configuration.</param>
        public Bus(ITsumugiConfiguration tsumugiConfiguration) : this(tsumugiConfiguration.ConnectionString,
            tsumugiConfiguration.Queue,
            tsumugiConfiguration.Channel,
            tsumugiConfiguration.Emoji,
            tsumugiConfiguration.Bot)
        {
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="queue">Queue.</param>
        /// <param name="channel">Channel.</param>
        /// <param name="emoji">Emoji.</param>
        /// <param name="bot">Bot.</param>
        public Bus(string connectionString,
            string queue,
            string channel,
            string emoji,
            string bot)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
            _emoji = emoji ?? throw new ArgumentNullException(nameof(emoji));
            _bot = bot ?? throw new ArgumentNullException(nameof(bot));
            _queueClient = new QueueClient(connectionString, queue);
        }               

        public async Task<bool> SendAsync(string channel, string emoji, string botName, string message, IEnumerable<Attachment> attachments = null)
        {
            var notification = new Notification(channel, emoji, botName, message) { Attachments = attachments };
            return await SendHelperAsync(notification);
        }

        public async Task<bool> SendAsync(string message)
        {
            var notification = new Notification(_channel, _emoji, _bot, message);
            return await SendHelperAsync(notification);
        }
        
        async Task<bool> SendHelperAsync(Notification notification)
        {
            try
            {
                var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(notification)))
                {
                    ContentType = "application/json",
                    Label = "Tsumugi"
                };

                await _queueClient.SendAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                ErrorHandler?.Invoke(ex);
            }

            return false;
        }
    }
}
