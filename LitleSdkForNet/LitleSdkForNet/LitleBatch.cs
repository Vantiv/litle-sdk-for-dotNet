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
        private authentication authentication;
        private Dictionary<String, String> config;
        private Communications communication;
        private litleXmlSerializer litleXmlSerializer;
        private int numOfLitleBatchRequest = 0;
        public string finalFilePath = null;
        private string batchFilePath = null;
        private litleTime litleTime;
        private litleFileGenerator litleFileGenerator;

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
            config["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;

            communication = new Communications();

            authentication = new authentication();
            authentication.user = config["username"];
            authentication.password = config["password"];

            litleXmlSerializer = new litleXmlSerializer();
            litleTime = new litleTime();
            //listOfLitleBatchRequestFilePaths = new List<string>();
            //listOfLitleBatchRequest = new List<litleBatchRequest>();
            litleFileGenerator = new litleFileGenerator();
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

            authentication = new authentication();
            authentication.user = config["username"];
            authentication.password = config["password"];

            //listOfLitleBatchRequestFilePaths = new List<string>();
            //listOfLitleBatchRequest = new List<litleBatchRequest>();
        }

        public void setCommunication(Communications communication)
        {
            this.communication = communication;
        }

        public void setLitleXmlSerializer(litleXmlSerializer litleXmlSerializer)
        {
            this.litleXmlSerializer = litleXmlSerializer;
        }

        public void setLitleTime(litleTime litleTime)
        {
            this.litleTime = litleTime;
        }

        public void addBatch(litleBatchRequest litleBatchRequest)
        {
            fillInReportGroup(litleBatchRequest);

            batchFilePath = SerializeBatchRequestToFile(litleBatchRequest, batchFilePath);
            numOfLitleBatchRequest++;
        }

        public string sendToLitle()
        {
            string requestFilePath = this.Serialize();

            communication.FtpDropOff(requestFilePath, config);
            return Path.GetFileName(requestFilePath);
        }


        public void blockAndWaitForResponse(string fileName, int timeOut)
        {
            communication.FtpPoll(fileName, timeOut, config);
        }

        public litleResponse receiveFromLitle(string destinationFilePath, string batchFileName)
        {
            string destinationDirectory = Path.GetDirectoryName(destinationFilePath);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            try
            {
                communication.FtpPickUp(destinationFilePath, config, batchFileName);
            }
            catch (Tamir.SharpSsh.jsch.SftpException e)
            {
                if (e.message != null)
                {
                    throw new LitleOnlineException(e.message);
                }
                else
                {
                    throw new LitleOnlineException("Error occurred while attempting to retrieve file.");
                }
            }

            litleResponse litleResponse = (litleResponse)litleXmlSerializer.DeserializeObjectFromFile(destinationFilePath);
            return litleResponse;
        }

        public string SerializeBatchRequestToFile(litleBatchRequest litleBatchRequest, string filePath)
        {

            filePath = litleFileGenerator.createRandomFile(filePath, litleTime, "_temp_litleRequest.xml");
            string tempFilePath = litleBatchRequest.Serialize();

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

            File.Delete(tempFilePath);

            return filePath;
        }

        public string Serialize()
        {
            string xmlHeader = "<?xml version='1.0' encoding='utf-8'?>\r\n<litleRequest version=\"8.17\"" +
             " xmlns=\"http://www.litle.com/schema\" " +
             "numBatchRequests=\"" + numOfLitleBatchRequest + "\">";

            string xmlFooter = "\r\n</litleRequest>";

            string filePath;

            finalFilePath = litleFileGenerator.createRandomFile(finalFilePath, litleTime, ".xml");
            filePath = finalFilePath;

            using (FileStream fs = new FileStream(finalFilePath, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(xmlHeader);
                sw.Write(authentication.Serialize());
            }

            using (FileStream fs = new FileStream(finalFilePath, FileMode.Append))
            using (FileStream fsr = new FileStream(batchFilePath, FileMode.Open))
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

            using (FileStream fs = new FileStream(finalFilePath, FileMode.Append))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(xmlFooter);
            }

            File.Delete(batchFilePath);

            finalFilePath = null;

            return filePath;
        }

        private void fillInReportGroup(litleBatchRequest litleBatchRequest)
        {
            if (litleBatchRequest.reportGroup == null)
            {
                litleBatchRequest.reportGroup = config["reportGroup"];
            }
        }

    }

    public class litleFileGenerator
    {

        public string createRandomFile(string filePath, litleTime litleTime, string fileExtension)
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

                string fileName = litleTime.getCurrentTime("MM-dd-yyyy_HH-mm-ss-ffff_") + RandomGen.NextString(8);

                filePath = directoryPath + fileName + fileExtension;

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                }
            }

            return filePath;
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

    public class litleTime
    {
        public virtual String getCurrentTime(String format)
        {
            return DateTime.Now.ToString(format);
        }
    }

}
