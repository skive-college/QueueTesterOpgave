using System;
using System.Collections.Generic;
using System.Messaging;
using System.Text;


namespace MsMQConsumer
{
    public class MQConsumer
    {


        private string QName = ".\\myQueue";
        private MessageQueue myQueue;

        private static int toRead;

        //private static int toRead;       

        public string name { get; private set; }


        public MQConsumer (string _name, int _toRead)
        {
            toRead = _toRead;
            name = _name;

            myQueue = new MessageQueue(QName)
            {

                // Set the formatter to indicate body contains an Order.
                         //Formatter = new XmlMessageFormatter(new Type[] {typeof(MSMQ2.Order)})
            };

        }


        public void ReceiveMessage()
        {   

            try
            {
                // Receive and format the message. 
                Message myMessage = myQueue.Receive();
                Console.WriteLine("Line read");
           //     Order myOrder = (Order)myMessage.Body;
           
            }

            catch (MessageQueueException)
            {
                // Handle Message Queuing exceptions.
            }

            // Handle invalid serialization format.
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            // Catch other exceptions as necessary.
            catch (Exception e)
            {
                Console.WriteLine("Error occured : " + e.Message);
            }

            return;
        }
    }
}

