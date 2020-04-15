using Baidu.Aip.Ocr;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace 图片文字识别
{
    class Program
    {

		// 设置APPID/AK/SK
		static string APP_ID = "18910988";
		static string API_KEY = "YtGvRU82RkdxHOBFvC1qUwIx";
		static string SECRET_KEY = "E2rr6aRaKhxsNGP1Lm9Ij3cavbIZs7y7";
		static string access_token = "";

		static void Main(string[] args)
        {
			//new Program().GeneralBasicUrlDemo();
			JObject jtoken = JsonConvert.DeserializeObject<JObject>(getAccessToken());
			access_token = jtoken["access_token"].ToString();
			string result= Idcard();//身份证识别
			Console.WriteLine(result);
			Console.ReadKey();
		}

		/// <summary>
		/// 身份证识别
		/// </summary>
		/// <returns></returns>
		public static string Idcard()
		{
			//string token = "[调用鉴权接口获取的token]";
			string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/idcard?access_token=" + access_token;
			Encoding encoding = Encoding.Default;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
			request.Method = "post";
			request.KeepAlive = true;
			// 图片的base64编码
			string base64 = getFileBase64(@"C:\Users\wy\Desktop\IMG_0410.JPG");
			String str = "id_card_side=" + "front" + "&image=" + HttpUtility.UrlEncode(base64);
			byte[] buffer = encoding.GetBytes(str);
			request.ContentLength = buffer.Length;
			request.GetRequestStream().Write(buffer, 0, buffer.Length);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
			string result = reader.ReadToEnd();
			Console.WriteLine("身份证识别:");
			Console.WriteLine(result);
			return result;
		}

		public static String getFileBase64(String fileName)
		{
			FileStream filestream = new FileStream(fileName, FileMode.Open);
			byte[] arr = new byte[filestream.Length];
			filestream.Read(arr, 0, (int)filestream.Length);
			string baser64 = Convert.ToBase64String(arr);
			filestream.Close();
			return baser64;
		}

		public static String getAccessToken()
		{
			String authHost = "https://aip.baidubce.com/oauth/2.0/token";
			HttpClient client = new HttpClient();
			List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
			paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
			paraList.Add(new KeyValuePair<string, string>("client_id", API_KEY));
			paraList.Add(new KeyValuePair<string, string>("client_secret", SECRET_KEY));

			HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
			String result = response.Content.ReadAsStringAsync().Result;
			return result;
		}

		

	   public Ocr client { get; set; } = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY) 
	   {  
		   Timeout= 60000  // 修改超时时间
	   };
		//client.Timeout = 60000; 
	
		public void GeneralBasicDemo()
		{
			var image = File.ReadAllBytes("图片文件路径");
			// 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
			var result = client.GeneralBasic(image);
			Console.WriteLine(result);
			// 如果有可选参数
			var options = new Dictionary<string, object>{
		{"language_type", "CHN_ENG"},
		{"detect_direction", "true"},
		{"detect_language", "true"},
		{"probability", "true"}
	};
			// 带参数调用通用文字识别, 图片参数为本地图片
			result = client.GeneralBasic(image, options);
			Console.WriteLine(result);
		}


		public  void GeneralBasicUrlDemo()
		{
			var url = "http://pic185.nipic.com/file/20181011/13597469_074544788000_2.jpg";

			// 调用通用文字识别, 图片参数为远程url图片，可能会抛出网络等异常，请使用try/catch捕获
			var result = client.GeneralBasicUrl(url);
			Console.WriteLine(result);
			// 如果有可选参数
			var options = new Dictionary<string, object>{
		{"language_type", "CHN_ENG"},
		{"detect_direction", "true"},
		{"detect_language", "true"},
		{"probability", "true"}
	};
			// 带参数调用通用文字识别, 图片参数为远程url图片
			result = client.GeneralBasicUrl(url, options);
			Console.WriteLine(result);
		}
	}
}
