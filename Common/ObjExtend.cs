using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class ObjExtend
    {
        public static Dictionary<string, string> ToDictionary<T>(this T model) where T : class, new()
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            foreach (System.Reflection.PropertyInfo p in typeof(T).GetProperties())
            {
                pairs.Add(p.Name, p.GetValue(model).ToString());
            }
            return pairs;
        }

        public static string ObjToParam<T>(this T model, Encoding encode = null)
        {
            string url = "";
            var parms = "";
            if (encode == null) encode = Encoding.UTF8;
            foreach (System.Reflection.PropertyInfo p in typeof(T).GetProperties())
            {
                parms += string.Format("{0}={1}&", Encode(p.Name, encode), Encode(p.GetValue(model).ToString(), encode));
            }
            if (parms != "")
            {
                parms = parms.TrimEnd('&');
            }
            url += parms;
            return url;
        }

        private static string Encode(string content, Encoding encode = null)
        {
            if (encode == null) return content;

            return System.Web.HttpUtility.UrlEncode(content, Encoding.UTF8);

        }
    }
}
