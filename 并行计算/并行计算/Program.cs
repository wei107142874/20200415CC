using System;
using System.Threading;
using System.Threading.Tasks;

namespace 并行计算
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 10, (i) => {
                Interlocked.Increment(ref i);
                Console.Write($"{i} ");
            });
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
