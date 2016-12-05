using System;
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
        }
    }
}
