using Common;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Nest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.WaitAll(T1(), T1(), T1());
            Console.ReadKey();
        }


        private static Task<string> T1()
        {
            return Task.Factory.StartNew(() => {
                string a = DateTime.Now.ToString();
                throw new Exception(a);
                Console.WriteLine(a);
                return a;
            });
        }
    }

    public class ArrayQueue
    {
        public ArrayQueue(int maxSize)
        {
            MaxSize = maxSize;
            Array = new int[MaxSize];
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="data"></param>
        public void AddQueue(int data)
        {
            if (IsFull())
            {
                Console.WriteLine("队列满");
                return;
            }
            Rear = Rear + 1;
            Array[Rear] = data;
        }

        public void QuitQueue()
        {
            if(IsEmpty())
            {
                Console.WriteLine("队列空");
                return;
            }
            Front = Front + 1;
            Console.WriteLine(Array[Front]);
        }

        bool IsFull()
        {
            return MaxSize - 1 == Rear;
        }

        bool IsEmpty()
        {
            return Rear == Front;
        }

        /// <summary>
        /// 队列数组
        /// </summary>
        public int[] Array { get; set; }

        /// <summary>
        /// 前端(队首)
        /// </summary>
        public int Front { get; set; } = -1;

        /// <summary>
        /// 后端(队尾)
        /// </summary>
        public int Rear { get; set; } = -1;

        /// <summary>
        /// 队列最大长度
        /// </summary>
        public int MaxSize { get; set; }

    }
}
