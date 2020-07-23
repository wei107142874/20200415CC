using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{
   public class MarkDownHelp
    {
        public static string MarkdownToHtml(string markdown)
        {
            using (var reader = new StringReader(markdown))
            {
                using (var writer = new StringWriter())
                {
                    CommonMark.CommonMarkConverter.Convert(reader, writer);
                    //writer.ToString()即为转换好的html
                   return  writer.ToString();
                    //Frame.Navigate(typeof(WebPage), new string[] { Md_Title, writer.ToString() });
                }
            }
        }
    }
}
