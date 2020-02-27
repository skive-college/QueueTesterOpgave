using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZeroMQConsumer;
using ZeroMQPublicher;

namespace TesterZeroMQ
{
    class ZMQTester
    {
        //antal Consumers
        private int consumers = 2;
        //antal beskeder
        private int antal = 10000;

        private DateTime start;
        private List<double> tid = new List<double>();

        private bool wait = true;
        static void Main(string[] args)
        {
            Console.WriteLine("-----------ZMQ TESTER------------");
            ZMQTester t = new ZMQTester();
            t.testConsumer();
        }

        public void testConsumer()
        {
            
            for (int i = 0; i < consumers; i++)
            {
                ZmqConsumer c = new ZmqConsumer();
                //pull messages
                string tmp = "Consumer nr. " + (i + 1) + "/" + consumers;
                
                Task.Run(() => c.StartPull(tmp, antal)).ContinueWith(Done, c);
                
            }
            start = DateTime.Now;
            ZmqPublicher p = new ZmqPublicher();
            p.create(antal);
            Console.WriteLine("waiting fore reader to complete");
            Console.ReadLine();

        }
        private void Done(Task arg1, object c)
        {
            
            DateTime tiden = DateTime.Now;
            ZmqConsumer cons = c as ZmqConsumer;
            Console.WriteLine(cons.name + " Tiden = " + (tiden - start).TotalMilliseconds + " ms");
            Console.WriteLine(cons.name + " Tiden = " + (tiden - start).TotalSeconds + " s");
            wait = false;
        }
    }
}
