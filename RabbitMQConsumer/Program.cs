using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQConsumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Consumer c = new Consumer("self running" , 10);
            c.Start();

            
        }
    }
}
