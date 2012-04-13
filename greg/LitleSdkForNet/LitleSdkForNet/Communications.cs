using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.XPath;
using System.Net;

namespace Litle.Sdk
{
    public class Communications
    {
        public string HttpPost(string xmlRequest, Dictionary<String,String> config)
        {
            string uri = config["url"];
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(xmlRequest); // get raw bytes to be sent
            System.Net.WebRequest req = System.Net.WebRequest.Create(uri);
            if("true".Equals(config["printxml"])) 
            {
                Console.WriteLine(xmlRequest);
            }
            req.ContentType = "text/xml";
            req.Method = "POST";
            WebProxy myproxy = new WebProxy(config["proxyHost"], int.Parse(config["proxyPort"]));
            myproxy.BypassProxyOnLocal = true;
            req.Proxy = myproxy;
            req.ContentLength = bytes.Length;            

            System.IO.Stream os = req.GetRequestStream();
            try
            {
                // submit http request
                os.Write(bytes, 0, bytes.Length);

                // read response
                System.Net.WebResponse resp = req.GetResponse();
                if (resp == null)
                {
                    return null;
                }
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                string xmlResponse = sr.ReadToEnd().Trim();
                if ("true".Equals(config["printxml"]))
                {
                    Console.WriteLine(xmlResponse);
                }
                return xmlResponse;
            }
            finally
            {
                os.Close();
            }
        }
    }
}