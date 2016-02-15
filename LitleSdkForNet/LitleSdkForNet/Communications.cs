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
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Litle.Sdk
{
    public class Communications
    {
        private static readonly object _synLock = new object();
        private readonly IDictionary<string, StringBuilder> _cache;

        public Communications(IDictionary<string, StringBuilder> cache)
        {
            _cache = cache;
        }

        public StringBuilder this[string key]
        {
            get
            {
                return _cache[key];
            }
        }

        public static bool ValidateServerCertificate(
             object sender,
             X509Certificate certificate,
             X509Chain chain,
             SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers. 
            return false;
        }

        public void neuterXML(ref string inputXml)
        {

            string pattern1 = "(?i)<number>.*?</number>";
            string pattern2 = "(?i)<accNum>.*?</accNum>";

            Regex rgx1 = new Regex(pattern1);
            Regex rgx2 = new Regex(pattern2);
            inputXml = rgx1.Replace(inputXml, "<number>xxxxxxxxxxxxxxxx</number>");
            inputXml = rgx2.Replace(inputXml, "<accNum>xxxxxxxxxx</accNum>");
        }
        
        public void log(String logMessage, String logFile, bool neuter)
        {
            lock (_synLock)
            {
                if (neuter)
                {
                    neuterXML(ref logMessage);
                }
                StreamWriter logWriter = new StreamWriter(logFile, true);
                DateTime time = DateTime.Now;
                logWriter.WriteLine(time.ToString());
                logWriter.WriteLine(logMessage + "\r\n");
                logWriter.Close();
            }
        }

        virtual public string HttpPost(string xmlRequest, Dictionary<String, String> config)
        {
            string logFile = null;
            if (config.ContainsKey("logFile"))
            {
                logFile = config["logFile"];
            }
            
            string uri = config["url"];
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls; 
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            
            bool neuter = false;
            if (config.ContainsKey("neuterAccountNums"))
            {
                neuter = ("true".Equals(config["neuterAccountNums"]));
            }

            bool printxml = false;
            if (config.ContainsKey("printxml"))
            {
                if("true".Equals(config["printxml"])) {
                    printxml = true;
                }
            }
            if(printxml) {
                Console.WriteLine(xmlRequest);
                Console.WriteLine(logFile);
            }

            //log request
            if (logFile != null)
            {
                log(xmlRequest,logFile, neuter);
            }

            req.ContentType = "text/xml";
            req.Method = "POST";
            req.ServicePoint.MaxIdleTime = 10000;
            req.ServicePoint.Expect100Continue = false;
            if (isProxyOn(config))
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
            if (printxml)
            {
                Console.WriteLine(xmlResponse);
            }

            //log response
            if (logFile != null)
            {
                log(xmlResponse,logFile,neuter);
            }

            return xmlResponse;
        }

        public bool isProxyOn(Dictionary<String,String> config) {
            return config.ContainsKey("proxyHost") && config["proxyHost"] != null && config["proxyHost"].Length > 0 && config.ContainsKey("proxyPort") && config["proxyPort"] != null && config["proxyPort"].Length > 0;
        }

        virtual public string socketStream(string xmlRequestFilePath, string xmlResponseDestinationDirectory, Dictionary<String, String> config)
        {
            string url = config["onlineBatchUrl"];
            int port = Int32.Parse(config["onlineBatchPort"]);
            TcpClient tcpClient = null;
            SslStream sslStream = null;

            try
            {
                tcpClient = new TcpClient(url, port);
                sslStream = new SslStream(tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
            }
            catch (SocketException e)
            {
                throw new LitleOnlineException("Error establishing a network connection", e);
            }

            try
            {
                sslStream.AuthenticateAsClient(url);
            }
            catch (AuthenticationException e)
            {
                tcpClient.Close();
                throw new LitleOnlineException("Error establishing a network connection - SSL Authencation failed", e);
            }

            if ("true".Equals(config["printxml"]))
            {
                Console.WriteLine("Using XML File: " + xmlRequestFilePath);
            }

            var memoryStream = this[xmlRequestFilePath];
            var buffer = Encoding.UTF8.GetBytes(memoryStream.ToString());
            sslStream.Write(buffer);
            sslStream.Flush();

            string batchName = Path.GetFileName(xmlRequestFilePath);
            if ("true".Equals(config["printxml"]))
            {
                Console.WriteLine("Writing to XML File: " + xmlResponseDestinationDirectory + batchName);
            }

            byte[] byteBuffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                // Read the client's test message.
                bytes = sslStream.Read(byteBuffer, 0, byteBuffer.Length);

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                var decoder = Encoding.UTF8.GetDecoder();
                var chars = new char[decoder.GetCharCount(byteBuffer, 0, bytes)];
                decoder.GetChars(byteBuffer, 0, bytes, chars, 0);
                messageData.Append(chars);
            } while (bytes != 0);
            
            _cache.Add(xmlResponseDestinationDirectory + batchName, messageData);

            tcpClient.Close();
            sslStream.Close();

            return xmlResponseDestinationDirectory + batchName;
        }

        virtual public void FtpDropOff(string fileDirectory, string fileName, Dictionary<String, String> config)
        {
            ChannelSftp channelSftp = null;
            Channel channel;

            string url = config["sftpUrl"];
            string username = config["sftpUsername"];
            string password = config["sftpPassword"];
            string knownHostsFile = config["knownHostsFile"];
            string filePath = fileDirectory + fileName;

            bool printxml = config["printxml"] == "true";
            if (printxml)
            {
                Console.WriteLine("Sftp Url: " + url);
                Console.WriteLine("Username: " + username);
                //Console.WriteLine("Password: " + password);
                Console.WriteLine("Known hosts file path: " + knownHostsFile);
            }

            JSch jsch = new JSch();
            jsch.setKnownHosts(knownHostsFile);

            Session session = jsch.getSession(username, url);
            session.setPassword(password);

            try
            {
                session.connect();

                channel = session.openChannel("sftp");
                channel.connect();
                channelSftp = (ChannelSftp)channel;
            }
            catch (SftpException e)
            {
                throw new LitleOnlineException("Error occured while attempting to establish an SFTP connection",e);
            }
            catch (JSchException e)
            {
                throw new LitleOnlineException("Error occured while attempting to establish an SFTP connection", e);
            }

            try
            {
                if (printxml)
                {
                    Console.WriteLine("Dropping off local file " + filePath + " to inbound/" + fileName + ".prg");
                }
                channelSftp.put(filePath, "inbound/" + fileName + ".prg", ChannelSftp.OVERWRITE);
                if (printxml)
                {
                    Console.WriteLine("File copied - renaming from inbound/" + fileName + ".prg to inbound/" + fileName + ".asc");
                }
                channelSftp.rename("inbound/" + fileName + ".prg", "inbound/" + fileName + ".asc");
            }
            catch (SftpException e)
            {
                throw new LitleOnlineException("Error occured while attempting to upload and save the file to SFTP", e);
            }

            channelSftp.quit();

            session.disconnect();
        }

        virtual public void FtpPoll(string fileName, int timeout, Dictionary<string, string> config)
        {
            fileName = fileName + ".asc";
            bool printxml = config["printxml"] == "true";
            if (printxml)
            {
                Console.WriteLine("Polling for outbound result file.  Timeout set to " + timeout + "ms. File to wait for is " + fileName);
            }
            ChannelSftp channelSftp = null;
            Channel channel;

            string url = config["sftpUrl"];
            string username = config["sftpUsername"];
            string password = config["sftpPassword"];
            string knownHostsFile = config["knownHostsFile"];

            JSch jsch = new JSch();
            jsch.setKnownHosts(knownHostsFile);

            Session session = jsch.getSession(username, url);
            session.setPassword(password);

            try
            {
                session.connect();

                channel = session.openChannel("sftp");
                channel.connect();
                channelSftp = (ChannelSftp)channel;
            }
            catch (SftpException e)
            {
                throw new LitleOnlineException("Error occured while attempting to establish an SFTP connection", e);
            }

            //check if file exists
            SftpATTRS sftpATTRS = null;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            do
            {
                if (printxml)
                {
                    Console.WriteLine("Elapsed time is " + stopWatch.Elapsed.TotalMilliseconds);
                }
                try
                {
                    sftpATTRS = channelSftp.lstat("outbound/" + fileName);
                    if (printxml)
                    {
                        Console.WriteLine("Attrs of file are: " + sftpATTRS.ToString());
                    }
                }
                catch (SftpException e)
                {
                    if (printxml)
                    {
                        Console.WriteLine(e.message);
                    }
                    System.Threading.Thread.Sleep(30000);
                }
            } while (sftpATTRS == null && stopWatch.Elapsed.TotalMilliseconds <= timeout);
        }

        virtual public void FtpPickUp(string destinationFilePath, Dictionary<String, String> config, string fileName)
        {
            ChannelSftp channelSftp = null;
            Channel channel;

            bool printxml = config["printxml"] == "true";

            string url = config["sftpUrl"];
            string username = config["sftpUsername"];
            string password = config["sftpPassword"];
            string knownHostsFile = config["knownHostsFile"];

            JSch jsch = new JSch();
            jsch.setKnownHosts(knownHostsFile);

            Session session = jsch.getSession(username, url);
            session.setPassword(password);

            try
            {
                session.connect();

                channel = session.openChannel("sftp");
                channel.connect();
                channelSftp = (ChannelSftp)channel;
            }
            catch (SftpException e)
            {
                throw new LitleOnlineException("Error occured while attempting to establish an SFTP connection", e);
            }

            try
            {
                if (printxml)
                {
                    Console.WriteLine("Picking up remote file outbound/" + fileName + ".asc");
                    Console.WriteLine("Putting it at " + destinationFilePath);
                }
                channelSftp.get("outbound/" + fileName + ".asc", destinationFilePath);
                if (printxml)
                {
                    Console.WriteLine("Removing remote file output/" + fileName + ".asc");
                }
                channelSftp.rm("outbound/" + fileName + ".asc");
            }
            catch (SftpException e)
            {
                throw new LitleOnlineException("Error occured while attempting to retrieve and save the file from SFTP", e);
            }

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
