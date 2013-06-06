using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;

namespace Litle.Sdk
{
    public class LitleBatch
    {

        private Dictionary<String, String> config;
        private Communications communication;
        private List<litleBatchRequest> listOfLitleBatchRequest;
        private int numOfLitleBatchRequest = 0;
        private string fPath = null;

        /**
         * Construct a Litle online using the configuration specified in LitleSdkForNet.dll.config
         */
        public LitleBatch()
        {
            config = new Dictionary<string, string>();

            config["url"] = Properties.Settings.Default.url;
            config["reportGroup"] = Properties.Settings.Default.reportGroup;
            config["username"] = Properties.Settings.Default.username;
            config["printxml"] = Properties.Settings.Default.printxml;
            config["timeout"] = Properties.Settings.Default.timeout;
            config["proxyHost"] = Properties.Settings.Default.proxyHost;
            config["merchantId"] = Properties.Settings.Default.merchantId;
            config["password"] = Properties.Settings.Default.password;
            config["proxyPort"] = Properties.Settings.Default.proxyPort;
            config["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            config["sftpUsername"] = Properties.Settings.Default.sftpUsername;
            config["sftpPassword"] = Properties.Settings.Default.sftpPassword;

            communication = new Communications();

            listOfLitleBatchRequest = new List<litleBatchRequest>();
        }

        /**
         * Construct a LitleOnline specifying the configuration i ncode.  This should be used by integration that have another way
         * to specify their configuration settings or where different configurations are needed for different instances of LitleOnline.
         * 
         * Properties that *must* be set are:
         * url (eg https://payments.litle.com/vap/communicator/online)
         * reportGroup (eg "Default Report Group")
         * username
         * merchantId
         * password
         * timeout (in seconds)
         * Optional properties are:
         * proxyHost
         * proxyPort
         * printxml (possible values "true" and "false" - defaults to false)
         */
        public LitleBatch(Dictionary<String, String> config)
        {
            this.config = config;
            communication = new Communications();

            listOfLitleBatchRequest = new List<litleBatchRequest>();
        }

        public void setCommunication(Communications communication)
        {
            this.communication = communication;
        }

        public string addBatch(litleBatchRequest litleBatchRequest)
        {
            fillInReportGroup(litleBatchRequest);

            if (!litleBatchRequest.config.ContainsKey("username")) litleBatchRequest.config["username"] = config["username"];
            if (!litleBatchRequest.config.ContainsKey("password")) litleBatchRequest.config["password"] = config["password"];
            if (!litleBatchRequest.config.ContainsKey("merchantId")) litleBatchRequest.config["merchantId"] = config["merchantId"];
            if (!litleBatchRequest.config.ContainsKey("reportGroup"))
            {
                litleBatchRequest.config["reportGroup"] = config["reportGroup"];
                litleBatchRequest.updateReportGroup();
            }

            listOfLitleBatchRequest.Add(litleBatchRequest);
            fPath = SerializeBatchRequestToFile(litleBatchRequest, fPath);
            numOfLitleBatchRequest++;

            return fPath;
        }

        public litleResponse sendToLitle()
        {
            string xmlRequest = this.Serialize();
            string xmlResponse = communication.HttpPost(xmlRequest, config);
            try
            {
                litleResponse litleResponse = DeserializeObject(xmlResponse);
                if ("1".Equals(litleResponse))
                {
                    throw new LitleOnlineException(litleResponse.message);
                }

                return litleResponse;
            }
            catch (InvalidOperationException ioe)
            {
                throw new LitleOnlineException("Error validating xml data against the schema", ioe);
            }
        }

        public litleResponse sendToLitle_File()
        {
            string requestFileName = this.SerializeToFile(fPath);

            //string responseFileName = communication.FtpSend(requestFileName, config);
            //try
            //{
            //    litleResponse litleResponse = DeserializeObject(xmlResponse);
            //    if ("1".Equals(litleResponse))
            //    {
            //        throw new LitleOnlineException(litleResponse.message);
            //    }
            //    return litleResponse;
            //}
            //catch (InvalidOperationException ioe)
            //{
            //    throw new LitleOnlineException("Error validating xml data against the schema", ioe);
            //}

            return null;
        }

        public string SerializeBatchRequestToFile(litleBatchRequest litleBatchRequest, string filePath)
        {
            if (filePath == null)
            {
                string currentPath = Environment.CurrentDirectory.ToString();
                string parentPath = Directory.GetParent(currentPath).ToString();
                string directoryPath = parentPath + "/batches/";

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }


                string fileName = DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss-ffff_") + RandomGen.NextString(8);
                fileName += "_temp.xml";

                filePath = directoryPath + fileName;

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                }
            }


            using (FileStream fs = new FileStream(filePath, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(litleBatchRequest.Serialize());
                }
            }


            return filePath;
        }

        public string SerializeToFile(string tempFilePath)
        {
            string xmlHeader = "<?xml version='1.0' encoding='utf-8'?>\r\n<litleRequest version=\"8.17\"" +
             " xmlns=\"http://www.litle.com/schema\" " +
             "numBatchRequests=\"" + numOfLitleBatchRequest + "\">";

            string xmlFooter = "\r\n</litleRequest>";

            string filePath;
            filePath = tempFilePath;
            filePath = filePath.Replace("_temp.xml", ".xml");

            Console.WriteLine(tempFilePath);
            Console.WriteLine(filePath);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(xmlHeader);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Append))
            using (FileStream fsr = new FileStream(tempFilePath, FileMode.Open))
            {
                byte[] buffer = new byte[16];

                int bytesRead = 0;

                do
                {
                    bytesRead = fsr.Read(buffer, 0, buffer.Length);
                    fs.Write(buffer, 0, bytesRead);
                }
                while (bytesRead > 0);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Append))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(xmlFooter);
            }

            File.Delete(tempFilePath);

            fPath = null;

            return filePath;
        }

        public string Serialize()
        {
            string xml = "<?xml version='1.0' encoding='utf-8'?>\r\n<litleRequest version=\"8.17\"" +
                " xmlns=\"http://www.litle.com/schema\" " +
                "numBatchRequests=\"" + listOfLitleBatchRequest.Count + "\">";

            foreach (litleBatchRequest b in listOfLitleBatchRequest)
            {
                xml += b.Serialize();
            }

            xml += "\r\n</litleRequest>";
            return xml;
        }

        public static String SerializeObject(litleOnlineRequest req)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(litleOnlineRequest));
            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, req);
            return Encoding.UTF8.GetString(ms.GetBuffer());//return string is UTF8 encoded.
        }// serialize the xml

        public static String SerializeObjectToFile(litleOnlineRequest req)
        {

            return "filename";
        }

        public static litleResponse DeserializeObject(string response)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(litleResponse));
            StringReader reader = new StringReader(response);
            litleResponse i = (litleResponse)serializer.Deserialize(reader);
            return i;

        }// deserialize the object

        private void fillInReportGroup(litleBatchRequest litleBatchRequest)
        {
            if (litleBatchRequest.reportGroup == null)
            {
                litleBatchRequest.reportGroup = config["reportGroup"];
            }
        }

    }


    public static class RandomGen
    {
        private static RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();
        private static Random _local;
        public static int NextInt()
        {
            Random inst = _local;
            if (inst == null)
            {
                byte[] buffer = new byte[8];
                _global.GetBytes(buffer);
                _local = inst = new Random(BitConverter.ToInt32(buffer, 0));
            }
         
            return _local.Next();
        }

        public static string NextString(int length)
        {
            string result = "";

            for (int i = 0; i < length; i++)
            {
                result += Convert.ToChar(NextInt() % ('Z' - 'A') + 'A');
            }

            return result;
        }
    }

}
