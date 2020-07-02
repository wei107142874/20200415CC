using Common;
using System;
using System.Text;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1; i++)
            {
                RabbitMQHelp.Provider4("logs_direct", "info" ,(i+"===="+DateTime.Now.ToString()));
            }
            Console.WriteLine("OK");
        }
    }
}
