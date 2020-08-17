using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using Tencent;

namespace 订阅号开发.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WxController : ControllerBase
    {
        public static readonly  string token = "xxx";
        public static readonly string sEncodingAESKey = "mRdIjkLkTJdJeOBeL9DM495UjhGwe6ekC834H0wMDj1";
        public static readonly string sAppID = "wx83d56382439a236f";

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WxController> _logger;

        public WxController(ILogger<WxController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg_signature">加密后才会有</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Post(string signature, string timestamp, string nonce,string msg_signature)
        {
            // 取出消息内容
            string content = string.Empty;
            using (Stream stream = HttpContext.Request.Body)
            {
                byte[] buffer = new byte[HttpContext.Request.ContentLength.Value];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                content = Encoding.UTF8.GetString(buffer);
            }
            // 解密得到消息明文
            string xmlMsg=string.Empty;//消息
            if (!string.IsNullOrWhiteSpace(msg_signature))
            {
                WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(token, sEncodingAESKey, sAppID);
                int ret = wxcpt.DecryptMsg(msg_signature, timestamp, nonce, content, ref xmlMsg);
                if (ret != 0)
                {
                    Console.WriteLine("ERR: Decrypt fail, ret: " + ret);
                    return "";
                }
            }
            BaseMessage msg = MessageFactory.CreateMessage(xmlMsg);
            return msg.ResText(msg.FromUserName);
        }       

        [HttpGet]
        public string Get(string signature, string timestamp, string nonce, string echostr)
        {
            string tmpStr=GetEchostr(signature, timestamp, nonce);

            if (tmpStr == signature)
            {
                return echostr;
            }
            return "校验失败";
        }


        public string GetEchostr(string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = SHA1_Encrypt(tmpStr);
            tmpStr = tmpStr.ToLower();
            return tmpStr;
        }

        public static string SHA1_Encrypt(string Source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString().ToUpper();
        }
    }
}
