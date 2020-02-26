using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZeroMQConsumer
{
    public class ZmqConsumer
    {
        private static int toRead;
        public string name { get; set; }
        public void StartPull(string navn, int antal)
        {
            
            toRead = antal;
            name = navn;
            Console.WriteLine(name + " started");
            using (var receiver = new PullSocket(">tcp://localhost:5556"))
            {
                
                while (toRead > 0)
                {
                    string workload = "";
                    bool b = receiver.TryReceiveFrameString(out workload);
                    if(b)
                    {
                        //Console.WriteLine(name + " Resived ");
                        Thread.Sleep(5);
                        toRead--;
                    }
                    else if(toRead != antal && !b)
                    {
                        break;
                    }
                        
                }
            }
        }
    }
}
