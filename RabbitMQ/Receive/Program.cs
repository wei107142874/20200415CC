using Common;
using System;

namespace Customer
{
    class Program
    {
        static void Main(string[] args)
        {
            //RabbitMQHelp.Customer1("hello");
            //RabbitMQHelp.Customer3("logs");
            RabbitMQHelp.Customer4("logs_direct","error");
            Console.ReadKey();
        }
    }
}
