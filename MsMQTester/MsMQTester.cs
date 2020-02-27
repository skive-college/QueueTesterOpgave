using System;
using System.Collections.Generic;
using System.Text;
using MsMQPublisher;
using MsMQConsumer;
using System.Threading.Tasks;

namespace MsMQTester
{
    class MsMQTester
    {
        //antal Consumers
        private int consumers = 1;
        //antal beskeder
        private int antal = 1000;
   
        private DateTime start;
        private List<double> tid = new List<double>();
        static void Main(string[] args)
        {
            MsMQTester t = new MsMQTester();
            t.testConsumer();
        }

        public void testConsumer()
        {
            for (int i = 0; i < consumers; i++)
            {
                MQConsumer c = new MQConsumer("Consumer nr. " + (i + 1) + "/" + consumers, antal);
                //pull messages
                Task.Run(() => c.ReceiveMessage()).ContinueWith(Done, c);
                //push messages
                //Task.Run(() => c.Start()).ContinueWith(Done,c);
            }

            start = DateTime.Now;
            MQPublisher p = new MQPublisher();
            p.SendMessage(antal);
        }
        private void Done(Task arg1, object c)
        {
            DateTime tiden = DateTime.Now;
            MQConsumer cons = c as MQConsumer;
            Console.WriteLine(cons.name + " Tiden = " + (tiden - start).TotalMilliseconds + " ms");
            Console.WriteLine(cons.name + " Tiden = " + (tiden - start).TotalSeconds + " s");
        }
    }
}

