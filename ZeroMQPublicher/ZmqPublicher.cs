using NetMQ;
using NetMQ.Sockets;
using RabbitMqTesterOpgave;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZeroMQPublicher
{
    public class ZmqPublicher
    {
        
        public void create(int antal)
        {
            Message m = new Message();
            //var pubSocket = new PublisherSocket();
            using (var sender = new PushSocket("@tcp://*:5556"))
            {
                Console.WriteLine("Publisher started");
                //pubSocket.Options.SendHighWatermark = 10000;
                //pubSocket.Bind("tcp://*:5556");
                //Thread.Sleep(100);
                for (var i = 0; i < antal; i++)
                {

                    //Console.WriteLine("Sending message : {0}", msg);
                    sender.SendFrame(m.Navn);
                    //Thread.Sleep(100);
                }
            }
            Console.WriteLine(antal + " Beskeder sendt");



        }
    }
}
