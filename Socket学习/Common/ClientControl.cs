using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class ClientControl
    {
        private Socket clientSocket;
        public ClientControl()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>

        public void Connect(string ip,int port)
        {
            clientSocket.Connect(ip, port);
            Console.WriteLine($"{ip}【{port}】连接成功");

            Thread threadReceive = new Thread(Receive);
            threadReceive.IsBackground = true;
            threadReceive.Start();
        }

        /// <summary>
        /// 接收服务器发来的消息
        /// </summary>
        private void Receive()
        {
            try
            {
                byte[] msg = new byte[1024];
                int msgLen = clientSocket.Receive(msg);
                string strMsg = Encoding.UTF8.GetString(msg, 0, msgLen);
                Console.WriteLine($"{strMsg}");
                Receive();
            }
            catch (Exception)
            {
                Console.WriteLine("服务器积极拒绝");
                throw;
            }
            
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void Send(string msg)
        {
            clientSocket.Send(Encoding.UTF8.GetBytes(msg));
        }
    }
}
