using RabbitMQ.Client;
using RabbitMqTesterOpgave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace RabbitMQPublicher
{
    public class Publicher
    {
        private IModel Channel { get;  set; }
        private string QName = "RabbitTest";
        private ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            Port = 5672,
            RequestedConnectionTimeout = 3000, // milliseconds
        };

        public Publicher()
        {
            IConnection connection = connectionFactory.CreateConnection();
            Channel = connection.CreateModel();
            
            Channel.QueueDeclare(
                                queue: QName,
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);



        }

        public void Create(int antal, bool persistent)
        {
            Console.WriteLine("Publisher started");
            Message m = new Message();
            var properties = Channel.CreateBasicProperties();
            properties.Persistent = persistent;
            for (int i = 0; i < antal; i++)
            {
                Channel.BasicPublish(
                                exchange: string.Empty,
                                routingKey: QName,
                                basicProperties: properties,
                                body: m.Navn);
            }
            Console.WriteLine(antal + " Beskeder sendt");
        }        
    }
}
