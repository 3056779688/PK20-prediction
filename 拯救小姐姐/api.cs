using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Baidu.Aip.Ocr;
using System.Windows;
using System.IO;
using Newtonsoft.Json;

namespace 拯救小姐姐
{
    public partial class MainWindow : Window
    {
        // 设置APPID/AK/SK
        string APP_ID = "10754280";
        static string API_KEY = "9x9P3mRm0uFCOm5Hhp0Hzpm0";
        static string SECRET_KEY = "yihZsDSQrZphjbM8acyxmXdhoLafoGQ1";

        Form form = new Baidu.Aip.Ocr.Form(API_KEY, SECRET_KEY);
        Ocr client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);

        public string GeneralBasicDemo(string filename)
        {
            var image = File.ReadAllBytes(filename);
            form.DebugLog = false; // 是否开启调试日志
            string RequestId = GetRequestId(image);
            if (RequestId != null)
            {
                string numbers = GetFormResult(RequestId);
                return RequestId;
            }
            else
            {
                Console.WriteLine("error!");
                return null;
            }
        }

        public string GetRequestId(byte[] image)
        {
            var result = form.BeginRecognition(image);

            Console.WriteLine(result);
            string request_id = GetWordsByLine(result.ToString());
            return request_id;
        }

        public string GetFormResult(string RequestId)
        {
            var options = new Dictionary<string, object>()
            {
                {"result_type", "json"}
            };
            Newtonsoft.Json.Linq.JObject result = null;
            string ret_msg = "";
            do
            {
                result = form.GetRecognitionResult(RequestId, options);
                Console.Write(result);
                ret_msg = GetWordsByMsg(result.ToString());
            }
            while (ret_msg != "已完成");


            return result.ToString();
        }

        public string GetWordsByLine(string json)
        {
            string result = "";
            string ret = "";
            // string result = "";
            JsonReader reader = new JsonTextReader(new StringReader(json));
            while (reader.Read())
            {
                
                if (reader.Path.Contains("request_id") && !reader.Value.ToString().Contains("request_id") )
                {
                    return reader.Value.ToString();
                }
                //  Console.WriteLine(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value);
            }

            //  this.text = result;
            return null;
        }

        public string GetWordsByMsg(string json)
        {
            string result = "";
            string ret = "";
            // string result = "";
            JsonReader reader = new JsonTextReader(new StringReader(json));
            while (reader.Read())
            {

                if (reader.Path.Contains("ret_msg") && !reader.Value.ToString().Contains("ret_msg"))
                {
                    return reader.Value.ToString();
                }
                //  Console.WriteLine(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value);
            }

            //  this.text = result;
            return null;
        }
    }
}
