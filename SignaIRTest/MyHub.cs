using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using StackExchange.Redis;

namespace SignaIRTest
{
    public class MyHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        /// <summary>
        /// 获取在线人信息
        /// </summary>
        /// <returns></returns>
        public async Task GetOnLinePersons()
        {
            using (ConnectionMultiplexer conn = await ConnectionMultiplexer.ConnectAsync("127.0.0.1:6379"))
            {
                IDatabase db = conn.GetDatabase();

                //获取所有登录的用户
                RedisValue[] uNames = await db.SetMembersAsync("SignaIRTest_UserName");

                RedisKey[] d= uNames.Select(x => (RedisKey)("SignaIRTest_UserName_OnLine" + x)).ToArray();
                RedisValue[] data = await db.StringGetAsync(d);

                Dictionary<string, bool> dic = new Dictionary<string, bool>();
                for (int i = 0; i < uNames.Length; i++)
                {
                    dic.Add(uNames[i], (bool)(data[i]));
                }
                Clients.All.onMessage(dic);
            }
        }

        public async Task Login(string uName)
        {
            string id = this.Context.ConnectionId;
            using (ConnectionMultiplexer conn = await ConnectionMultiplexer.ConnectAsync("127.0.0.1:6379"))
            {
                IDatabase db = conn.GetDatabase();
                await db.StringSetAsync("SignaIRTest_UserName_OnLine" + uName, true);


                string connectionId = this.Context.ConnectionId;
                await db.StringSetAsync("SignaIRTest_ConnectionId_UserName_" + connectionId, uName);

                await db.SetAddAsync("SignaIRTest_UserName", uName);
            }
            await GetOnLinePersons();
        }

        /// <summary>
        /// 断开连接时调用
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public async override Task OnDisconnected(bool stopCalled)
        {
            using (ConnectionMultiplexer conn = await ConnectionMultiplexer.ConnectAsync("127.0.0.1:6379"))
            {
                IDatabase db = conn.GetDatabase();
                string connectionId = this.Context.ConnectionId;

                RedisValue userName = await db.StringGetAsync("SignaIRTest_ConnectionId_UserName_" + connectionId);
                await db.StringSetAsync("SignaIRTest_UserName_OnLine" + userName, false);
            }
            await GetOnLinePersons();
            await base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// 连接时调用
        /// </summary>
        /// <returns></returns>
        public async override Task OnConnected()
        {
            await GetOnLinePersons();
            await base.OnConnected();
        }

        public object GetObj()
        {
            string a = null;
            a.ToString();
            return new { age = 12, name = "zhangsan" };
        }
    }
}