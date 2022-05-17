﻿using RabbitMQ.Client;
using System.Text;

class Send
{
    public static void Main()
    {
                   
        string message;
        Console.WriteLine("Type a message to send with RabbitMq: ");
        message = Console.ReadLine() ?? "";
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };
        using(var connection =  factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(
                queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "", 
                routingKey: "hello",
                basicProperties: null,
                body: body
            );
            Console.WriteLine("[x] Sent [0]", message);
        }
        Console.WriteLine("Press [enter] to exit");
        Console.ReadLine();
    }
}
