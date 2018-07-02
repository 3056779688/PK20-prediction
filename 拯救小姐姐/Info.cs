using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Management;
using System.Net;
using System.IO;

namespace 拯救小姐姐
{
    public partial class MainWindow : Window
    {
        public string GetInformation()
        {
            //  string LocalIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            string ip = "";
            string mac = "";

            System.Net.IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            for (int i = 0; i < addressList.Length; i++)
            {
                ip += addressList[i].ToString()+"\n";
            }

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                {
                    mac = mo["MacAddress"].ToString();
                }
            }
            string ExternalIP = "";
            try
            {
                ExternalIP = GetExternalIP();
            }
            catch
            {
                Console.WriteLine("外网失败");
            }
            string result = ip + "\n***\n" + mac + "\n***\n" + ExternalIP;
            Console.WriteLine(result);
            return result;
        }

        private string GetExternalIP()
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Encoding = System.Text.Encoding.Default;
            string reply = client.DownloadString("http://www.ip138.com/ip2city.asp");
            return reply;
        }
    }
}
