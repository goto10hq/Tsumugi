using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Tsumugi
{
    public class Bus
    {
        private static readonly Lazy<string> _connection = new Lazy<string>(() => CloudConfigurationManager.GetSetting("Tsumugi.Connection"));
        private static readonly Lazy<string> _queue = new Lazy<string>(() => CloudConfigurationManager.GetSetting("Tsumugi.Queue"));
        private static readonly Lazy<string> _channel = new Lazy<string>(() => CloudConfigurationManager.GetSetting("Tsumugi.Channel"));
        private static readonly Lazy<string> _emoji = new Lazy<string>(() => CloudConfigurationManager.GetSetting("Tsumugi.Emoji"));
        private static readonly Lazy<string> _bot = new Lazy<string>(() => CloudConfigurationManager.GetSetting("Tsumugi.Bot"));
        private static readonly Lazy<QueueClient> _queueClient = new Lazy<QueueClient>(() => QueueClient.CreateFromConnectionString(_connection.Value, _queue.Value));

        public Action<Exception> ErrorHandler { get; set; }

        private static string Connection => _connection.Value;

        public Bus()
        {            
        }

        public bool Send(string channel, string emoji, string botName, string message)
        {
            var notification = new Notification(channel, emoji, botName, message);
            return SendHelper(notification);
        }

        public bool Send(string message)
        {
            var notification = new Notification(_channel.Value, _emoji.Value, _bot.Value, message);
            return SendHelper(notification);
        }

        public async Task<bool> SendAsync(string channel, string emoji, string botName, string message)
        {
            var notification = new Notification(channel, emoji, botName, message);
            return await SendHelperAsync(notification);
        }

        public async Task<bool> SendAsync(string message)
        {
            var notification = new Notification(_channel.Value, _emoji.Value, _bot.Value, message);
            return await SendHelperAsync(notification);
        }

        private bool SendHelper(Notification notification)
        {
            try
            {
                var message = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(notification))))
                {
                    ContentType = "application/json",
                    Label = "Tsumugi"                    
                };

                _queueClient.Value.Send(message);
                return true;
            }
            catch (Exception ex)
            {                  
                ErrorHandler?.Invoke(ex);              
            }

            return false;
        }

        private async Task<bool> SendHelperAsync(Notification notification)
        {
            try
            {
                var message = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(notification))))
                {
                    ContentType = "application/json",
                    Label = "Tsumugi"
                };

                await _queueClient.Value.SendAsync(message);
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
