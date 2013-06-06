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
            string uri = config["sftpUrl"];
            string username = config["sftpUsername"];
            string password = config["sftpPassword"];
            string fileName = Path.GetFileName(filePath);

            //UserInfo userInfo = new MyUserInfo();

            SshConnectionInfo info = new SshConnectionInfo();
            info.Host = uri;
            info.User = username;
            info.Pass = password;
            Sftp sshCp = new Sftp(uri, username);
            sshCp.Connect();

            sshCp.Put(filePath, "/inbound/" + fileName);
            sshCp.Close();


            //JSch jsch = new JSch();
            //Session session = jsch.getSession(username, uri);
            //session.connect();

            //ChannelSftp channelSftp = (ChannelSftp)session.openChannel("sftp");
            //channelSftp.connect();

            //channelSftp.put(filePath, "/inbound/", ChannelSftp.OVERWRITE);

            //channelSftp.rename("/inbound/" + fileName, "/inbound/" + fileName + ".asc");

            //channelSftp.quit();
            //session.disconnect();
            //System.Net.ServicePointManager.Expect100Continue = false;
            //System.Net.FtpWebRequest req = (System.Net.FtpWebRequest) System.Net.FtpWebRequest.Create(uri);


            //req.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            //req.Credentials = new NetworkCredential(config["sftpUsername"], config["sftpPassword"]);

            //using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            //using (Stream ftpStream = req.GetRequestStream())
            //{
            //    int bytesRead = 0;
            //    byte[] buffer = new byte[1024];

            //    do
            //    {
            //        bytesRead = fileStream.Read(buffer, 0, buffer.Length);
            //        ftpStream.Write(buffer, 0, bytesRead);
            //    }
            //    while (bytesRead > 0);
            //}

            //FtpWebResponse response = (FtpWebResponse)req.GetResponse();

            ////Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            //response.Close();

            //if ("true".Equals(config["printxml"]))

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