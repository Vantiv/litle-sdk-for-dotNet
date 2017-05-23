using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        private static readonly object SynLock = new object();
        public event EventHandler HttpAction;
        
        private void OnHttpAction(RequestType requestType, string xmlPayload, bool neuter)
        {
            if (HttpAction != null)
            {
                if (neuter)
                {
                    NeuterXml(ref xmlPayload);
                }

                HttpAction(this, new HttpActionEventArgs(requestType, xmlPayload));
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

        public void NeuterXml(ref string inputXml)
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
            lock (SynLock)
            {
                if (neuter)
                {
                    NeuterXml(ref logMessage);
                }

                using (var logWriter = new StreamWriter(logFile, true))
                {
                    var time = DateTime.Now;
                    logWriter.WriteLine(time.ToString(CultureInfo.InvariantCulture));
                    logWriter.WriteLine(logMessage + "\r\n");
                }
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls; 
            var req = (HttpWebRequest)WebRequest.Create(uri);
            
            var neuter = false;
            if (config.ContainsKey("neuterAccountNums"))
            {
                neuter = ("true".Equals(config["neuterAccountNums"]));
            }

            var printxml = false;
            if (config.ContainsKey("printxml"))
            {
                if("true".Equals(config["printxml"])) 
                {
                    printxml = true;
                }
            }

            if (printxml) 
            {
                Console.WriteLine(xmlRequest);
                Console.WriteLine(logFile);
            }

            //log request
            if (logFile != null)
            {
                Log(xmlRequest, logFile, neuter);
            }

            req.ContentType = "text/xml; charset=UTF-8";
            req.Method = "POST";
            req.ServicePoint.MaxIdleTime = 10000;
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

            OnHttpAction(RequestType.Request, xmlRequest, neuter);

            // submit http request
            using (var writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.Write(xmlRequest);
            }
            
            // read response
            var resp = req.GetResponse();
            string xmlResponse;
            using (var reader = new StreamReader(resp.GetResponseStream()))
            {
                xmlResponse = reader.ReadToEnd().Trim();
            }
            if (printxml)
            {
                Console.WriteLine(xmlResponse);
            }

            OnHttpAction(RequestType.Response, xmlResponse, neuter);

            //log response
            if (logFile != null)
            {
                Log(xmlResponse, logFile, neuter);
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
            TcpClient tcpClient;
            SslStream sslStream;

            try
            {
                tcpClient = new TcpClient(url, port);
                sslStream = new SslStream(tcpClient.GetStream(), false, ValidateServerCertificate, null);
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

            using (var readFileStream = new FileStream(xmlRequestFilePath, FileMode.Open))
            {
                int bytesRead;

                do
                {
                    var byteBuffer = new byte[1024 * sizeof(char)];
                    bytesRead = readFileStream.Read(byteBuffer, 0, byteBuffer.Length);

                    sslStream.Write(byteBuffer, 0, bytesRead);
                    sslStream.Flush();
                } while (bytesRead != 0);
            }

            var batchName = Path.GetFileName(xmlRequestFilePath);
            var destinationDirectory = Path.GetDirectoryName(xmlResponseDestinationDirectory);
            if (destinationDirectory != null && !Directory.Exists(destinationDirectory)) Directory.CreateDirectory(destinationDirectory);

            if ("true".Equals(config["printxml"]))
            {
                Console.WriteLine("Writing to XML File: " + xmlResponseDestinationDirectory + batchName);
            }

            using (var writeFileStream = new FileStream(xmlResponseDestinationDirectory + batchName, FileMode.Create))
            {
                int bytesRead;

                do
                {
                    var byteBuffer = new byte[1024 * sizeof(char)];
                    bytesRead = sslStream.Read(byteBuffer, 0, byteBuffer.Length);

                    writeFileStream.Write(byteBuffer, 0, bytesRead);
                } while (bytesRead > 0);
            }

            tcpClient.Close();
            sslStream.Close();

            return xmlResponseDestinationDirectory + batchName;
        }

        public virtual void FtpDropOff(string fileDirectory, string fileName, Dictionary<string, string> config)
        {
            ChannelSftp channelSftp;

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
                //Console.WriteLine("Password: " + password);
                Console.WriteLine("Known hosts file path: " + knownHostsFile);
            }

            var jsch = new JSch();

            if (printxml)
            {
                // grab the contents fo the knownhosts file and print
                var hostFile = File.ReadAllText(knownHostsFile);
                Console.WriteLine("known host contents: " + hostFile);
            }
            
            jsch.setKnownHosts(knownHostsFile);

            // setup for diagnostic
            // Get the KnownHosts repository from JSchs
            var hkr = jsch.getHostKeyRepository();
            var hks = hkr.getHostKey();
            HostKey hk;
            if (printxml)
            {
                // Print all knownhosts and keys  
                if (hks != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Host keys in " + hkr.getKnownHostsRepositoryID() + ":");
                    foreach (var t in hks)
                    {
                        hk = t;
                        Console.WriteLine("local HostKey host: <" + hk.getHost() + "> type: <" + hk.getType() + "> fingerprint: <" + hk.getFingerPrint(jsch) + ">");
                    }
                    Console.WriteLine("");
                }
            }
           

            var session = jsch.getSession(username, url);
            session.setPassword(password);

            try
            {
                session.connect();

                // more diagnostic code for troubleshooting sFTP connection errors
                if (printxml)
                {
                    // Print the host key info of the connected server:
                    hk = session.getHostKey();
                    Console.WriteLine("remote HostKey host: <" + hk.getHost() + "> type: <" + hk.getType() + "> fingerprint: <" + hk.getFingerPrint(jsch) + ">");
                }
                
                var channel = session.openChannel("sftp");
                channel.connect();
                channelSftp = (ChannelSftp)channel;
            }
            catch (SftpException e)
            {
                throw new LitleOnlineException("Error occured while attempting to establish an SFTP connection", e);
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
            ChannelSftp channelSftp;

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

                var channel = session.openChannel("sftp");
                channel.connect();
                channelSftp = (ChannelSftp)channel;
            }
            catch (SftpException e)
            {
                throw new LitleOnlineException("Error occured while attempting to establish an SFTP connection", e);
            }

            //check if file exists
            SftpATTRS sftpAttrs = null;
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
                    sftpAttrs = channelSftp.lstat("outbound/" + fileName);
                    if (printxml)
                    {
                        Console.WriteLine("Attrs of file are: " + sftpAttrs);
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
            } while (sftpAttrs == null && stopWatch.Elapsed.TotalMilliseconds <= timeout);
        }

        public virtual void FtpPickUp(string destinationFilePath, Dictionary<string, string> config, string fileName)
        {
            ChannelSftp channelSftp;

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

                var channel = session.openChannel("sftp");
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

    public enum RequestType
    {
        Request, Response
    }

    public class HttpActionEventArgs : EventArgs
    {
        public RequestType RequestType { get; set; }
        public string XmlPayload;

        public HttpActionEventArgs(RequestType requestType, string xmlPayload)
        {
            RequestType = requestType;
            XmlPayload = xmlPayload;
        }
    }

}
