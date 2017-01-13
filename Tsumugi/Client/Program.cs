using System;
using System.Collections.Generic;
using Tsumugi;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new Bus();

            bus.ErrorHandler += exception => Console.WriteLine($"Error: {exception.Message}.");

            bus.Send("Aloha *dudes*!");
            bus.Send("general", ":panda_face:", "Tsumugi", "Wellp...", new List<Attachment>
                                                                       {
                                                                           new Attachment
                                                                           {
                                                                               Fields = new List<Field>
                                                                                        {
                                                                                            new Field("ID", "1", true),
                                                                                            new Field("Name", "Sushi", true)
                                                                                        }
                                                                           },
                                                                           new Attachment
                                                                           {
                                                                               Fields = new List<Field>
                                                                                        {
                                                                                            new Field("ID", "2", true),
                                                                                            new Field("Name", "Sashimi", true)
                                                                                        }
                                                                           }
                                                                       });
        }
    }
}
