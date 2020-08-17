using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tencent
{
    /// <summary>
    /// 文本消息
    /// </summary>
    [Serializable]
    public class TextMessage:BaseMessage
    {
        public string Content { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }

        public override string ResText(string content)
        {
            //根据内容处理自己的逻辑
            //switch(content)
            //{
            //    case
            //}


            string xml = $@"<xml>
                              <ToUserName><![CDATA[{FromUserName}]]></ToUserName>
                              <FromUserName><![CDATA[{ToUserName}]]></FromUserName>
                              <CreateTime>{DateTimeUtil.GetTimeStamp(DateTime.Now)}</CreateTime>
                              <MsgType><![CDATA[text]]></MsgType>
                              <Content><![CDATA[{content}]]></Content>
                            </xml>";
            return xml;
        }
    }
}
