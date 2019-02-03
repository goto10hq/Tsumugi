using Microsoft.Extensions.Configuration;
using Momo.Tokens;
using System;
using System.IO;
using System.Threading.Tasks;
using Tsumugi;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var appSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("AppSettings.json")
                .AddUserSecrets("tsumugi")
                .AddEnvironmentVariables()
                .Build();

            var configuration = new TsumugiConfiguration(appSettings["Tsumugi:ConnectionString"],
                appSettings["Tsumugi:Queue"],
                appSettings["Tsumugi:Channel"],
                appSettings["Tsumugi:Emoji"],
                appSettings["Tsumugi:Bot"]);

            var bus = new Bus(configuration);

            bus.ErrorHandler += exception => Console.WriteLine($"Error: {exception.Message}.");

            await bus.SendAsync("Aloha 2 *dudes*!");
            //bus.Send("general", ":panda_face:", "Tsumugi", "Wellp...", new List<Attachment>
            //                                                           {
            //                                                               new Attachment
            //                                                               {
            //                                                                   Fields = new List<Field>
            //                                                                            {
            //                                                                                new Field("ID", "1", true),
            //                                                                                new Field("Name", "Sushi", true)
            //                                                                            }
            //                                                               },
            //                                                               new Attachment
            //                                                               {
            //                                                                   Fields = new List<Field>
            //                                                                            {
            //                                                                                new Field("ID", "2", true),
            //                                                                                new Field("Name", "Sashimi", true)
            //                                                                            }
            //                                                               }
            //                                                           });
        }
    }
}
