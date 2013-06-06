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
        virtual public string HttpPost(string xmlRequest, Dictionary<String,String> config)
        {
            string uri = config["url"];
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.WebRequest req = System.Net.WebRequest.Create(uri);
            if("true".Equals(config["printxml"])) 
            {
                Console.WriteLine(xmlRequest);
            }
            req.ContentType = "text/xml";
            req.Method = "POST";
            if (config.ContainsKey("proxyHost") && config["proxyHost"].Length > 0 && config.ContainsKey("proxyPort") && config["proxyPort"].Length > 0)
            {
                WebProxy myproxy = new WebProxy(config["proxyHost"], int.Parse(config["proxyPort"]));
                myproxy.BypassProxyOnLocal = true;
                req.Proxy = myproxy;
            }

            // submit http request
            using (var writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.Write(xmlRequest);
            }

            // read response
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null)
            {
               return null;
            }
            string xmlResponse;
            using (var reader = new System.IO.StreamReader(resp.GetResponseStream()))
            {
                xmlResponse = reader.ReadToEnd().Trim();
            }
            if ("true".Equals(config["printxml"]))
            {
                Console.WriteLine(xmlResponse);
            }
            return xmlResponse;
        }

        virtual public Stream HttpPost(Stream xmlRequestStream, Dictionary<String, String> config)
        {
            string uri = config["url"];
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.WebRequest req = System.Net.WebRequest.Create(uri);
            if ("true".Equals(config["printxml"]))
            {
                Console.WriteLine(xmlRequestStream);
            }
            req.ContentType = "text/xml";
            req.Method = "POST";
            if (config.ContainsKey("proxyHost") && config["proxyHost"].Length > 0 && config.ContainsKey("proxyPort") && config["proxyPort"].Length > 0)
            {
                WebProxy myproxy = new WebProxy(config["proxyHost"], int.Parse(config["proxyPort"]));
                myproxy.BypassProxyOnLocal = true;
                req.Proxy = myproxy;
            }

            // submit http request
            using (var writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.Write(xmlRequestStream);
            }

            // read response
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null)
            {
                return null;
            }

            return resp.GetResponseStream();
       
        }
    }
}