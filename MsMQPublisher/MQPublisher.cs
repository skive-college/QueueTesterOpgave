using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using RabbitMqTesterOpgave;

namespace MsMQPublisher
{
    public class MQPublisher
    {
        private string QName = ".\\myQueue";
        private MessageQueue myQueue;

        public MQPublisher()
        {
            MQPublisher myNewQueue = new MQPublisher();

            // Create a queue on the local computer.
            myNewQueue.CreatePublicQueues();

            // Connect to the queue on the local computer.
            myQueue = new MessageQueue(QName);

         //   myNewQueue.SendMessage();
        }


        public void SendMessage(int antal)
        {
            // Create a new order and set values.
            Msg m = new Msg();


            // Connect to a queue on the local computer.
     //     MessageQueue myQueue = new MessageQueue(QName);

            // Send the Order to the queue.
            for(int i = 0; i < antal; i++)
            {
                myQueue.Send(m);
            }
            

            return;
        }

        private void CreatePublicQueues()
        {

            // Create and connect to a public Message Queuing queue.
            if (!MessageQueue.Exists(QName))
            {
                // Create the queue if it does not exist.
                MessageQueue myNewPublicQueue =
                    MessageQueue.Create(QName);

                // Send a message to the queue.
                myNewPublicQueue.Send("My message data.");
            }

            return;
        }
    }
   
}
