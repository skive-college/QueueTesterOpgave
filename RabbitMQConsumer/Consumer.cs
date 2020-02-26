using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQConsumer
{
    public class Consumer
    {
        private static int toRead;
        
        //private static int toRead;       

        public string name { get; private set; }
        public Consumer(string _name, int _toRead)
        {
            toRead = _toRead;
            name = _name;
        }

        public void Start()
        {
            string QName = "RabbitTest";
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                
                while (toRead != 0)
                {
                    if (channel.MessageCount(QName) > 0)
                    {
                        var consumer = new EventingBasicConsumer(channel);

                        consumer.Received += (ch, ea) =>
                        {
                            var body = ea.Body;
                            Console.WriteLine(name + " læser");
                            channel.BasicAck(ea.DeliveryTag, false);
                            toRead--;
                        };

                        String consumerTag = channel.BasicConsume(QName, false, consumer);
                        Console.WriteLine(name + " : " +consumerTag);
                    }
                }
            }
        }

        public void StartPull()
        {
            string queueName = "RabbitTest";
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                Console.WriteLine(name);
                int ready = (int)channel.MessageCount(queueName);
                Console.ForegroundColor = ConsoleColor.Green;
                while (ready > 0 || toRead > 0)
                {
                   
                    bool Ack = true;
                    BasicGetResult result = channel.BasicGet(queueName, Ack);
                    if (result == null)
                    {
                        // No message available at this time.
                    }
                    else
                    {
                        byte[] body = result.Body;

                        // acknowledge receipt of the message
                        //Console.WriteLine(name + " læser");
                        Thread.Sleep(2);
                        toRead--;
                        
                    }
                    ready = (int)channel.MessageCount(queueName);
                }
            }
        }
    }
}
