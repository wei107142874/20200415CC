using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Tencent
{
    public class MessageFactory
    {
        public static BaseMessage CreateMessage(string xml)
        {
            XElement xdoc = XElement.Parse(xml);
            var msgtype = xdoc.Element("MsgType").Value.ToUpper();
            MsgType type = (MsgType)Enum.Parse(typeof(MsgType), msgtype);
            switch (type)
            {
                case MsgType.TEXT: return ConvertObj<TextMessage>(xml);
                //case MsgType.IMAGE: return ConvertObj<ImgMessage>(xml);
                //case MsgType.VIDEO: return ConvertObj<VideoMessage>(xml);
                //case MsgType.VOICE: return ConvertObj<VoiceMessage>(xml);
                //case MsgType.LINK:
                //    return Utils.ConvertObj<LinkMessage>(xml);
                //case MsgType.LOCATION:
                //    return Utils.ConvertObj<LocationMessage>(xml);
                //case MsgType.EVENT://事件类型
                //    {

                //    }
                //    break;
                default:
                    return ConvertObj<BaseMessage>(xml);
            }
        }


        /// <summary>
        /// xml解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        public static T ConvertObj<T>(string xmlstr)
        {
            XElement xdoc = XElement.Parse(xmlstr);
            var type = typeof(T);
            var t = Activator.CreateInstance<T>();
            foreach (XElement element in xdoc.Elements())
            {
                var pr = type.GetProperty(element.Name.ToString());
                if (element.HasElements)
                {//这里主要是兼容微信新添加的菜单类型。nnd，竟然有子属性，所以这里就做了个子属性的处理
                    foreach (var ele in element.Elements())
                    {
                        pr = type.GetProperty(ele.Name.ToString());
                        pr.SetValue(t, Convert.ChangeType(ele.Value, pr.PropertyType), null);
                    }
                    continue;
                }
                if (pr.PropertyType.Name == "MsgType")//获取消息模型
                {
                    pr.SetValue(t, (MsgType)Enum.Parse(typeof(MsgType), element.Value.ToUpper()), null);
                    continue;
                }
                //if (pr.PropertyType.Name == "Event")//获取事件类型。
                //{
                //    pr.SetValue(t, (Event)Enum.Parse(typeof(EVENT), element.Value.ToUpper()), null);
                //    continue;
                //}
                pr.SetValue(t, Convert.ChangeType(element.Value, pr.PropertyType), null);
            }
            return t;
        }
    }
}
