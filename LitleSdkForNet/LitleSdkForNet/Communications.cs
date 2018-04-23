using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net;
using Tamir.SharpSsh.jsch;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Litle.Sdk
{
    public class Communications
    {
        private static readonly object _synLock = new object();
        public static string ContentTypeTextXmlUTF8 = "text/xml; charset=UTF-8";

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

        public void NeuterXML(ref string inputXml)
        {
            const string pattern1 = "(?i)<number>.*?</number>";
            const string pattern2 = "(?i)<accNum>.*?</accNum>";
            const string pattern3 = "(?i)<track>.*?</track>";

            var rgx1 = new Regex(pattern1);
            var rgx2 = new Regex(pattern2);
            var rgx3 = new Regex(pattern3);
            inputXml = rgx1.Replace(inputXml, "<number>xxxxxxxxxxxxxxxx</number>");
            inputXml = rgx2.Replace(inputXml, "<accNum>xxxxxxxxxx</accNum>");
            inputXml = rgx3.Replace(inputXml, "<track>xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx</track>");
        }
        
        public void Log(string logMessage, string logFile, bool neuter)
        {
            lock (_synLock)
            {
                if (neuter)
                {
                    NeuterXML(ref logMessage);
                }
                var logWriter = new StreamWriter(logFile, true);
                var time = DateTime.Now;
                logWriter.WriteLine(time.ToString());
                logWriter.WriteLine(logMessage + "\r\n");
                logWriter.Close();
            }
        }

        public virtual string HttpPost(string xmlRequest, Dictionary<string, string> config)
        {
            string logFile = null;
            if (config.ContainsKey("logFile"))
            {
                logFile = config["logFile"];
            }
            
            var uri = config["url"];
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11; 
            var req = (HttpWebRequest)WebRequest.Create(uri);
            
            var neuter = false;
            if (config.ContainsKey("neuterAccountNums"))
            {
                neuter = ("true".Equals(config["neuterAccountNums"]));
            }

            var printxml = false;
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
                Log(xmlRequest,logFile, neuter);
            }

            req.ContentType = ContentTypeTextXmlUTF8;
            req.Method = "POST";
            req.ServicePoint.MaxIdleTime = 8000;
            req.ServicePoint.Expect100Continue = false;
            req.KeepAlive = false;
            if (IsProxyOn(config))
            {
                var myproxy = new WebProxy(config["proxyHost"], int.Parse(config["proxyPort"]))
                {
                    BypassProxyOnLocal = true
                };
                req.Proxy = myproxy;
            }

            // submit http request
            using (var writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.Write(xmlRequest);
            }



            // read response
            var resp = req.GetResponse();
            if (resp == null)
            {
                return null;
            }
            string xmlResponse;
            using (var reader = new StreamReader(resp.GetResponseStream()))
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
                Log(xmlResponse,logFile,neuter);
            }

            return xmlResponse;
        }

        public bool IsProxyOn(Dictionary<string,string> config) {
            return config.ContainsKey("proxyHost") && config["proxyHost"] != null && config["proxyHost"].Length > 0 && config.ContainsKey("proxyPort") && config["proxyPort"] != null && config["proxyPort"].Length > 0;
        }

        public virtual string SocketStream(string xmlRequestFilePath, string xmlResponseDestinationDirectory, Dictionary<string, string> config)
        {
            var url = config["onlineBatchUrl"];
            var port = int.Parse(config["onlineBatchPort"]);
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
                sslStream.AuthenticateAsClient(url, null, GetBestProtocol(), true);
            }
            catch (AuthenticationException e)
            {
                tcpClient.Close();
                throw new LitleOnlineException("Error establishing a network connection - SSL Authentication failed", e);
            }

            if ("true".Equals(config["printxml"]))
            {
                Console.WriteLine("Using XML File: " + xmlRequestFilePath);
            }

            using (var readFileStream = new FileStream(xmlRequestFilePath, FileMode.Open))
            {
                var bytesRead = -1;
                byte[] byteBuffer;

                do
                {
                    byteBuffer = new byte[1024 * sizeof(char)];
                    bytesRead = readFileStream.Read(byteBuffer, 0, byteBuffer.Length);

                    sslStream.Write(byteBuffer, 0, bytesRead);
                    sslStream.Flush();
                } while (bytesRead != 0);
            }

            var batchName = Path.GetFileName(xmlRequestFilePath);
            var destinationDirectory = Path.GetDirectoryName(xmlResponseDestinationDirectory);
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            if ("true".Equals(config["printxml"]))
            {
                Console.WriteLine("Writing to XML File: " + xmlResponseDestinationDirectory + batchName);
            }

            using (var writeFileStream = new FileStream(xmlResponseDestinationDirectory + batchName, FileMode.Create))
            {
                char[] charBuffer;
                byte[] byteBuffer;
                var bytesRead = 0;

                do
                {
                    charBuffer = new char[1024];
                    byteBuffer = new byte[1024 * sizeof(char)];
                    bytesRead = sslStream.Read(byteBuffer, 0, byteBuffer.Length);
                    charBuffer = Encoding.UTF8.GetChars(byteBuffer);

                    writeFileStream.Write(byteBuffer, 0, bytesRead);
                } while (bytesRead > 0);
            }

            tcpClient.Close();
            sslStream.Close();

            return xmlResponseDestinationDirectory + batchName;
        }
        
        public SslProtocols GetBestProtocol()
        {
            var protocols = Enum.GetValues(typeof(SslProtocols)).Cast<SslProtocols>().ToList();
            var bestProtocol = protocols[protocols.Count - 1];
            Console.WriteLine("Best protocal found is " + bestProtocol);
            return bestProtocol;
        }

        public virtual void FtpDropOff(string fileDirectory, string fileName, Dictionary<string, string> config)
        {
            ChannelSftp channelSftp = null;
            Channel channel;

            var url = config["sftpUrl"];
            var username = config["sftpUsername"];
            var password = config["sftpPassword"];
            var knownHostsFile = config["knownHostsFile"];
            var filePath = fileDirectory + fileName;

            var printxml = config["printxml"] == "true";
            if (printxml)
            {
                Console.WriteLine("Sftp Url: " + url);
                Console.WriteLine("Username: " + username);
                Console.WriteLine("Known hosts file path: " + knownHostsFile);
            }

            var jsch = new JSch();
            jsch.setKnownHosts(knownHostsFile);

            var session = jsch.getSession(username, url);
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

        public virtual void FtpPoll(string fileName, int timeout, Dictionary<string, string> config)
        {
            fileName = fileName + ".asc";
            var printxml = config["printxml"] == "true";
            if (printxml)
            {
                Console.WriteLine("Polling for outbound result file.  Timeout set to " + timeout + "ms. File to wait for is " + fileName);
            }
            ChannelSftp channelSftp = null;
            Channel channel;

            var url = config["sftpUrl"];
            var username = config["sftpUsername"];
            var password = config["sftpPassword"];
            var knownHostsFile = config["knownHostsFile"];

            var jsch = new JSch();
            jsch.setKnownHosts(knownHostsFile);

            var session = jsch.getSession(username, url);
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
            var stopWatch = new Stopwatch();
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

        public virtual void FtpPickUp(string destinationFilePath, Dictionary<string, string> config, string fileName)
        {
            ChannelSftp channelSftp = null;
            Channel channel;

            var printxml = config["printxml"] == "true";

            var url = config["sftpUrl"];
            var username = config["sftpUsername"];
            var password = config["sftpPassword"];
            var knownHostsFile = config["knownHostsFile"];

            var jsch = new JSch();
            jsch.setKnownHosts(knownHostsFile);

            var session = jsch.getSession(username, url);
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
