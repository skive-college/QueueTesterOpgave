using RabbitMQConsumer;
using RabbitMQPublicher;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester
{
    class Tester
    {
        
        //antal Consumers
        private int consumers = 3;
        //antal beskeder
        private int antal = 10000;

        

        private DateTime start;
        private List<double> tid = new List<double>();
        static void Main(string[] args)
        {
            
            Tester p = new Tester();            
            p.testConsumer();


        }

        public void testConsumer()
        {            
            for(int i = 0; i < consumers; i++)
            {
                Consumer c = new Consumer("Consumer nr. "+ i + "/"+consumers, antal);
                //pull messages
                Task.Run(() => c.StartPull()).ContinueWith(Done);
                //push messages
                //Task.Run(() => c.Start()).ContinueWith(Done);
            }                    

            start = DateTime.Now;
            Publicher p = new Publicher();
            p.Create(antal, true);
        }
        private void Done(Task arg1)
        {
            DateTime tiden = DateTime.Now;
            Console.WriteLine(" Tiden = " + (tiden - start).TotalMilliseconds + " ms");
            Console.WriteLine(" Tiden = " + (tiden - start).TotalSeconds + " s");
        }
    }
}
