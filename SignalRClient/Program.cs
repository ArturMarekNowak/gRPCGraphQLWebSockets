using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace ClientSignalR
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Chat Room Console Client Started!");

            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/signalr")
                .WithAutomaticReconnect()
                .Build();

            connection.StartAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                    Console.WriteLine("There was an error opening the connection:{0}",
                        task.Exception.GetBaseException());

                connection.On<string>("ReceiveMessage",
                    message => { Console.WriteLine(message); });

                Console.Write("Enter your message:");
                while (true)
                {
                    var message = Console.ReadLine();
                    connection.InvokeAsync("CreateMessage", message).ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                            Console.WriteLine("There was an error calling send: {0}",
                                task.Exception?.GetBaseException());
                    });
                }
            }).Wait();

            connection.StopAsync();

            Console.ReadLine();
        }
    }
}