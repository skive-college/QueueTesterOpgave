﻿using RabbitMQConsumer;
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
        private int consumers = 1;
        //antal beskeder
        private int antal = 10000;
        //presist eller memmory
        private bool presist = true;
        

        private DateTime start;
        private List<double> tid = new List<double>();
        static void Main(string[] args)
        {            
            Tester t = new Tester();
            t.testConsumerRabbit();
        }

        public void testConsumerRabbit()
        {            
            for(int i = 0; i < consumers; i++)
            {
                Consumer c = new Consumer("Consumer nr. "+ (i+1) + "/"+consumers, antal);
                //pull messages
                Task.Run(() => c.StartPull()).ContinueWith(DoneRabbit,c);
                //push messages
                //Task.Run(() => c.Start()).ContinueWith(Done,c);
            }

            start = DateTime.Now;
            Publicher p = new Publicher();
            p.Create(antal, presist);
        }
        private void DoneRabbit(Task arg1, object c)
        {
            DateTime tiden = DateTime.Now;
            Consumer cons = c as Consumer;
            Console.WriteLine(cons.name + " Tiden = " + (tiden - start).TotalMilliseconds + " ms");
            Console.WriteLine(cons.name + " Tiden = " + (tiden - start).TotalSeconds + " s");
        }
    }
}
