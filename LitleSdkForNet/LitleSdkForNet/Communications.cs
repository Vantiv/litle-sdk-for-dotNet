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

namespace Litle.Sdk
{
    public class Communications
    {

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

        virtual public string socketStream(string xmlRequestFilePath, string xmlResponseDestinationDirectory, Dictionary<String, String> config)
        {
            string url = config["onlineBatchUrl"];
            int port = Int32.Parse(config["onlineBatchPort"]);
            TcpClient tcpClient;
            SslStream sslStream;

            //PROXY?
            if (config.ContainsKey("proxyHost") && config["proxyHost"].Length > 0 && config.ContainsKey("proxyPort") && config["proxyPort"].Length > 0)
            {
                WebProxy myproxy = new WebProxy(config["proxyHost"], int.Parse(config["proxyPort"]));
                myproxy.BypassProxyOnLocal = true;

                var webRequest = WebRequest.Create(url);
                webRequest.Proxy = myproxy;

                var webResponse = webRequest.GetResponse();
                var resposeStream = webResponse.GetResponseStream();

                const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

                var rsType = resposeStream.GetType();
                var connectionProperty = rsType.GetProperty("Connection", flags);

                var connection = connectionProperty.GetValue(resposeStream, null);
                var connectionType = connection.GetType();
                var networkStreamProperty = connectionType.GetProperty("NetworkStream", flags);

                var networkStream = networkStreamProperty.GetValue(connection, null);
                var nsType = networkStream.GetType();
                var socketProperty = nsType.GetProperty("Socket", flags);
                var socket = (Socket)socketProperty.GetValue(networkStream, null);
                try
                {
                    tcpClient = new TcpClient { Client = socket };
                    sslStream = new SslStream(tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                }
                catch (SocketException e)
                {
                    throw new LitleOnlineException("Error establishing a network connection", e);
                }

            }
            else
            {
                try
                {
                    tcpClient = new TcpClient(url, port);
                    sslStream = new SslStream(tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                }
                catch (SocketException e)
                {
                    throw new LitleOnlineException("Error establishing a network connection", e);
                }
            }

            try
            {
                sslStream.AuthenticateAsClient(url);
            }
            catch (AuthenticationException)
            {
                tcpClient.Close();
                throw new LitleOnlineException("Error establishing a network connection - SSL Authencation failed");
            }

            if ("true".Equals(config["printxml"]))
            {
                Console.WriteLine("Using XML File: " + xmlRequestFilePath);
            }

            using (FileStream readFileStream = new FileStream(xmlRequestFilePath, FileMode.Open))
            {
                int bytesRead = -1;
                byte[] byteBuffer;

                do
                {
                    byteBuffer = new byte[1024 * sizeof(char)];
                    bytesRead = readFileStream.Read(byteBuffer, 0, byteBuffer.Length);

                    sslStream.Write(byteBuffer, 0, bytesRead);
                    sslStream.Flush();
                } while (bytesRead != 0);
            }

            string batchName = Path.GetFileName(xmlRequestFilePath);
            string destinationDirectory = Path.GetDirectoryName(xmlResponseDestinationDirectory);
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            if ("true".Equals(config["printxml"]))
            {
                Console.WriteLine("Writing to XML File: " + xmlResponseDestinationDirectory);
            }

            using (FileStream writeFileStream = new FileStream(xmlResponseDestinationDirectory + batchName, FileMode.Create))
            {
                char[] charBuffer;
                byte[] byteBuffer;
                int bytesRead = 0;

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

            return xmlResponseDestinationDirectory + batchName;
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
            string knownHostsFile = parentPath + "\\" + config["knownHostsFile"];
            string fileName = Path.GetFileName(filePath);

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
                if (e.message != null)
                {
                    throw new LitleOnlineException(e.message);
                }
                else
                {
                    throw new LitleOnlineException("Error occured while attempting to establish an SFTP connection");
                }
            }

            try
            {
                channelSftp.put(filePath, "inbound/" + fileName, ChannelSftp.OVERWRITE);
                channelSftp.rename("inbound/" + fileName, "inbound/" + fileName + ".asc");
            }
            catch (SftpException e)
            {
                if (e.message != null)
                {
                    throw new LitleOnlineException(e.message);
                }
                else
                {
                    throw new LitleOnlineException("Error occured while attempting to upload and save the file to SFTP");
                }
            }

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

            try
            {
                session.connect();

                channel = session.openChannel("sftp");
                channel.connect();
                channelSftp = (ChannelSftp)channel;
            }
            catch (SftpException e)
            {
                if (e.message != null)
                {
                    throw new LitleOnlineException(e.message);
                }
                else
                {
                    throw new LitleOnlineException("Error occured while attempting to establish an SFTP connection");
                }
            }

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

            try
            {
                session.connect();

                channel = session.openChannel("sftp");
                channel.connect();
                channelSftp = (ChannelSftp)channel;
            }
            catch (SftpException e)
            {
                if (e.message != null)
                {
                    throw new LitleOnlineException(e.message);
                }
                else
                {
                    throw new LitleOnlineException("Error occured while attempting to establish an SFTP connection");
                }
            }

            try
            {
                channelSftp.get("outbound/" + fileName + ".asc", destinationFilePath);
                channelSftp.rm("outbound/" + fileName + ".asc");
            }
            catch (SftpException e)
            {
                if (e.message != null)
                {
                    throw new LitleOnlineException(e.message);
                }
                else
                {
                    throw new LitleOnlineException("Error occured while attempting to retrieve and save the file from SFTP");
                }
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