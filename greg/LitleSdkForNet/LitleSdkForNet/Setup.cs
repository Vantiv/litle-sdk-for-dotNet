using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LitleSdkForNet
{
    class Setup
    {
        private static Dictionary<String,String> URL_MAP = new Dictionary<String,String>() { 
			{"sandbox", "https://www.testlitle.com/sandbox/communicator/online"},
			{"cert", "https://cert.litle.com/vap/communicator/online"},
            {"precert", "https://precert.litle.com/vap/communicator/online"},
            {"production", "https://payments.litle.com/vap/communicator/online"}
    };

        public static void Main(string[] args)
        {

        System.IO.StreamWriter configFile = new System.IO.StreamWriter("config.cs");

            Console.WriteLine("Welcome to Litle dotNet_SDK");
            Console.WriteLine("Please input your user name: ");
            configFile.WriteLine("username: " + Console.ReadLine());
            Console.WriteLine("Please input your password: ");
            configFile.WriteLine("password: " + Console.ReadLine());
            Console.WriteLine("Please input your merchantId: ");
            configFile.WriteLine("merchantId: " + Console.ReadLine());
            Console.WriteLine("Please choose Litle URL from the following list (example: 'cert') or directly input another URL: ");
            Console.WriteLine("\tsandbox => https://www.testlitle.com/sandbox/communicator/online");
		    Console.WriteLine("\tcert => https://cert.litle.com/vap/communicator/online");
		    Console.WriteLine("\tprecert => https://precert.litle.com/vap/communicator/online");
            Console.WriteLine("\tproduction => https://payments.litle.com/vap/communicator/online");
            String url;
            URL_MAP.TryGetValue(Console.ReadLine(), out url);
            configFile.WriteLine("url: " + url);
            Console.WriteLine("Please input the proxy host, if no proxy hit enter: ");
            configFile.WriteLine("proxyHost: " + Console.ReadLine());
            Console.WriteLine("Please input the proxy port, if no proxy hit enter: ");
            configFile.WriteLine("proxyPort: " + Console.ReadLine());
            configFile.WriteLine("version: " + "8.10");
            configFile.WriteLine("timeout: " + "65");
            configFile.WriteLine("reportGroup: " + "Default Report Group");
            configFile.WriteLine("printxml: " + "true");

            configFile.Close();

            Console.WriteLine("The Litle configuration file has been generated, the file is located at ");

            Console.ReadKey();



	}
    }
}


