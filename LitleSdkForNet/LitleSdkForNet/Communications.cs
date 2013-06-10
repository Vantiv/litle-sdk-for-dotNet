using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.XPath;
using System.Net;
using Tamir.SharpSsh.jsch;
using Tamir.SharpSsh;
using System.Timers;

namespace Litle.Sdk
{
    public class Communications
    {
        virtual public string HttpPost(string xmlRequest, Dictionary<String, String> config)
        {
            string uri = config["url"];
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.WebRequest req = System.Net.WebRequest.Create(uri);
            if ("true".Equals(config["printxml"]))
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

        virtual public void FtpDropOff(string filePath, Dictionary<String, String> config)
        {
            ChannelSftp channelSftp = null;
            Channel channel;

            string currentPath = Environment.CurrentDirectory.ToString();
            string parentPath = Directory.GetParent(currentPath).ToString();

            string url = config["sftpUrl"];
            string username = config["sftpUsername"];
            string password = config["sftpPassword"];
            string knownHostsFile = parentPath + "\\"  + config["knownHostsFile"];
            string fileName = Path.GetFileName(filePath);

            JSch jsch = new JSch();
            jsch.setKnownHosts(knownHostsFile);

            Session session = jsch.getSession(username, url);
            session.setPassword(password);

            session.connect();

            channel = session.openChannel("sftp");
            channel.connect();
            channelSftp = (ChannelSftp)channel;

            channelSftp.put(filePath, "inbound/" + fileName, ChannelSftp.OVERWRITE);
            channelSftp.rename("inbound/" + fileName, "inbound/" + fileName + ".asc");
            channelSftp.quit();

            session.disconnect();
        }

        virtual public void FtpPoll(string fileName, int timeout, Dictionary<string, string> config)
        {
            ChannelSftp channelSftp = null;
            Channel channel;

            string currentPath = Environment.CurrentDirectory.ToString();
            string parentPath = Directory.GetParent(currentPath).ToString();

            string url = config["sftpUrl"];
            string username = config["sftpUsername"];
            string password = config["sftpPassword"];
            string knownHostsFile = parentPath + "\\" + config["knownHostsFile"];

            JSch jsch = new JSch();
            jsch.setKnownHosts(knownHostsFile);

            Session session = jsch.getSession(username, url);
            session.setPassword(password);

            session.connect();

            channel = session.openChannel("sftp");
            channel.connect();
            channelSftp = (ChannelSftp)channel;

            //check if file exists
            SftpATTRS sftpATTRS = null;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            do
            {
                try
                {
                    sftpATTRS = channelSftp.lstat("outbound/" + fileName);
                }
                catch 
                {
                }
            } while (sftpATTRS == null && stopWatch.Elapsed.TotalMilliseconds <= timeout);
        }

        virtual public void FtpPickUp(string destinationFilePath, Dictionary<String, String> config, string fileName)
        {
            ChannelSftp channelSftp = null;
            Channel channel;

            string currentPath = Environment.CurrentDirectory.ToString();
            string parentPath = Directory.GetParent(currentPath).ToString();

            string url = config["sftpUrl"];
            string username = config["sftpUsername"];
            string password = config["sftpPassword"];
            string knownHostsFile = parentPath + "\\" + config["knownHostsFile"];

            JSch jsch = new JSch();
            jsch.setKnownHosts(knownHostsFile);

            Session session = jsch.getSession(username, url);
            session.setPassword(password);

            session.connect();

            channel = session.openChannel("sftp");
            channel.connect();
            channelSftp = (ChannelSftp)channel;

            channelSftp.get("outbound/" + fileName + ".asc", destinationFilePath);
            channelSftp.quit();

            session.disconnect();

        }

        public struct SshConnectionInfo
        {
            public string Host;
            public string User;
            public string Pass;
            public string IdentityFile;
        }
    }
}