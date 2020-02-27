using System;
using System.Collections.Generic;
using System.Text;

namespace MsMQPublisher
{
    class Msg { 
        public Byte[] Navn { get; set; }

        public Msg()
        {
            //Navn = new Byte[10];
            Navn = new Byte[1280];
        }
    }
}
