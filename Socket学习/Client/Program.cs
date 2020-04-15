using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientControl client = new ClientControl();
            client.Connect("127.0.0.1", 12345);
            string msg = Console.ReadLine();
            while (msg != "quit")
            {
                client.Send(msg);
                msg = Console.ReadLine();
            }
            Console.ReadKey();
        }
    }
}
