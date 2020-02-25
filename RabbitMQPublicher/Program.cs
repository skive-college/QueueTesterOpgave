using System;

namespace RabbitMQPublicher
{
    public class Program
    {
        static void Main(string[] args)
        {
            Publicher p = new Publicher();
            p.Create(10, true);
        }
    }
}
