using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Common
{
    public class ServerControl
    {
        private Socket serverSocket;
        public List<Socket> clients = new List<Socket>();
        public ServerControl()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        public void Start()
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 12345));
            serverSocket.Listen(100);
            Console.WriteLine("服务器启动成功 ");

            //Accept会阻塞挂起线程
            var taskAccept = new Thread(Accept);
            taskAccept.IsBackground = true;
            taskAccept.Start();
            //Task.WaitAll(taskAccept);
        }

        /// <summary>
        /// 客户端连接
        /// </summary>
        private void Accept()
        {
            //接收一个客户端的连接
            Socket client = serverSocket.Accept();
            clients.Add(client);

            IPEndPoint pEndPoint = client.RemoteEndPoint as IPEndPoint;
            Console.WriteLine($"{pEndPoint.Address}【{pEndPoint.Port}】连接成功");

            Thread threadReceive = new Thread(Receive);
            threadReceive.IsBackground = true;
            threadReceive.Start(client);

            Accept();
        }


        /// <summary>
        /// 接收客户端发来的消息
        /// </summary>
        /// <param name="obj"></param>
        public void Receive(object obj)
        {
            Socket client = obj as Socket;
            IPEndPoint pEndPoint = client.RemoteEndPoint as IPEndPoint;

            try
            {
                byte[] msg = new byte[1024];
                int msgLen = client.Receive(msg);
                string strMsg = Encoding.UTF8.GetString(msg, 0, msgLen);
                Console.WriteLine($"{pEndPoint.Address}【{pEndPoint.Port}】{strMsg}");
                //向当前连接上的客户端发消息
                //client.Send(Encoding.UTF8.GetBytes(strMsg));
                //向当前连接的客户端广播消息
                foreach (var item in clients.Where(x=>x!=client))
                {
                    item.Send(Encoding.UTF8.GetBytes($"{pEndPoint.Address}【{pEndPoint.Port}】:{strMsg}"));
                }
                Receive(client);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{pEndPoint.Address}【{pEndPoint.Port}】积极断开");
                clients.Remove(client);
            }
        }
    }
}
