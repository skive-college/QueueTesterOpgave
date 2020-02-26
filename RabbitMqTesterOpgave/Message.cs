using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqTesterOpgave
{
    public class Message
    {
        public Byte[] Navn { get; set; }

        public Message()
        {
            Navn = new Byte[1280];
        }
    }
}
