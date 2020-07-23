using Common;
using System;
using System.Diagnostics;
using System.IO;

namespace DataCollect.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = FileHelp.GetFilesByDirectory(@"D:\youdaoyun", "*.md");
            Random random = new Random(DateTime.Now.Second);
            int r= random.Next(files.Length - 1);
            string md = File.ReadAllText(files[r]);
            string html = MarkDownHelp.MarkdownToHtml(md);
            File.WriteAllText(@"D:\1.html", html);
            //Process.Start(@"d:\1.html");
            System.Console.WriteLine(html);
            System.Console.WriteLine("Hello World!");
        }
    }
}
