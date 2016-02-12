using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;

namespace Litle.Sdk
{
    public class litleRequest
    {
        private IDictionary<string, StringBuilder> _memoryStreams; 
        private authentication authentication;
        private Dictionary<String, String> config;
        private Communications communication;
        private litleXmlSerializer litleXmlSerializer;
        private int numOfLitleBatchRequest = 0;
        private int numOfRFRRequest = 0;
        public string finalFilePath = null;
        private string batchFilePath = null;
        private string requestDirectory;
        private string responseDirectory;
        private litleTime litleTime;
        private litleFile litleFile;

        /**
         * Construct a Litle online using the configuration specified in LitleSdkForNet.dll.config
         */
        public litleRequest(IDictionary<string, StringBuilder> memoryStreams)
        {
            _memoryStreams = memoryStreams;
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
            config["onlineBatchUrl"] = Properties.Settings.Default.onlineBatchUrl;
            config["onlineBatchPort"] = Properties.Settings.Default.onlineBatchPort;
            config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            config["responseDirectory"] = Properties.Settings.Default.responseDirectory;

            initializeRequest();
        }

        /**
         * Construct a LitleOnline specifying the configuration in code.  This should be used by integration that have another way
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
         * sftpUrl
         * sftpUsername
         * sftpPassword
         * knownHostsFile
         * onlineBatchUrl
         * onlineBatchPort
         * requestDirectory
         * responseDirectory
         */
        public litleRequest(IDictionary<string, StringBuilder> memoryStreams, Dictionary<String, String> config)
        {
            _memoryStreams = memoryStreams;
            this.config = config;

            initializeRequest();
        }

        private void initializeRequest()
        {
            communication = new Communications(_memoryStreams);

            authentication = new authentication();
            authentication.user = config["username"];
            authentication.password = config["password"];

            requestDirectory = config["requestDirectory"] + "\\Requests\\";
            responseDirectory = config["responseDirectory"] + "\\Responses\\";

            litleXmlSerializer = new litleXmlSerializer();
            litleTime = new litleTime();
            litleFile = new litleFile(_memoryStreams);
        }

        public authentication getAuthenication()
        {
            return this.authentication;
        }

        public string getRequestDirectory()
        {
            return this.requestDirectory;
        }

        public string getResponseDirectory()
        {
            return this.responseDirectory;
        }

        public void setCommunication(Communications communication)
        {
            this.communication = communication;
        }

        public Communications getCommunication()
        {
            return this.communication;
        }

        public void setLitleXmlSerializer(litleXmlSerializer litleXmlSerializer)
        {
            this.litleXmlSerializer = litleXmlSerializer;
        }

        public litleXmlSerializer getLitleXmlSerializer()
        {
            return this.litleXmlSerializer;
        }

        public void setLitleTime(litleTime litleTime)
        {
            this.litleTime = litleTime;
        }

        public litleTime getLitleTime()
        {
            return this.litleTime;
        }

        public void setLitleFile(litleFile litleFile)
        {
            this.litleFile = litleFile;
        }

        public litleFile getLitleFile()
        {
            return this.litleFile;
        }

        public void addBatch(batchRequest litleBatchRequest)
        {
            if (numOfRFRRequest != 0)
            {
                throw new LitleOnlineException("Can not add a batch request to a batch with an RFRrequest!");
            }

            fillInReportGroup(litleBatchRequest);

            batchFilePath = SerializeBatchRequestToFile(litleBatchRequest, batchFilePath);
            numOfLitleBatchRequest++;
        }

        public void addRFRRequest(RFRRequest rfrRequest)
        {
            if (numOfLitleBatchRequest != 0)
            {
                throw new LitleOnlineException("Can not add an RFRRequest to a batch with requests!");
            }
            else if (numOfRFRRequest >= 1)
            {
                throw new LitleOnlineException("Can not add more than one RFRRequest to a batch!");
            }

            batchFilePath = SerializeRFRRequestToFile(rfrRequest, batchFilePath);
            numOfRFRRequest++;
        }

        public litleResponse sendToLitleWithStream()
        {
            string requestFilePath = this.Serialize();
            string batchName = Path.GetFileName(requestFilePath);

            string responseFilePath = communication.socketStream(requestFilePath, responseDirectory, config);
            
            litleResponse litleResponse = (litleResponse)litleXmlSerializer.DeserializeObjectFromFile(communication, responseFilePath);
            return litleResponse;
        }

        public string sendToLitle()
        {
            string requestFilePath = this.Serialize();

            communication.FtpDropOff(requestDirectory, Path.GetFileName(requestFilePath), config);
            return Path.GetFileName(requestFilePath);
        }


        public void blockAndWaitForResponse(string fileName, int timeOut)
        {
            communication.FtpPoll(fileName, timeOut, config);
        }

        public litleResponse receiveFromLitle(string batchFileName)
        {
            communication.FtpPickUp(responseDirectory + batchFileName, config, batchFileName);

            litleResponse litleResponse = (litleResponse)litleXmlSerializer.DeserializeObjectFromFile(communication, responseDirectory + batchFileName);
            return litleResponse;
        }

        public string SerializeBatchRequestToFile(batchRequest litleBatchRequest, string filePath)
        {

            filePath = litleFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_litleRequest.xml", litleTime);
            string tempFilePath = litleBatchRequest.Serialize();

            litleFile.AppendFileToFile(filePath, tempFilePath);

            return filePath;
        }

        public string SerializeRFRRequestToFile(RFRRequest rfrRequest, string filePath)
        {
            filePath = litleFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_litleRequest.xml", litleTime);
            string tempFilePath = rfrRequest.Serialize();

            litleFile.AppendFileToFile(filePath, tempFilePath);

            return filePath;
        }

        public string Serialize()
        {
            string xmlHeader = "<?xml version='1.0' encoding='utf-8'?>\r\n<litleRequest version=\"9.3\"" +
             " xmlns=\"http://www.litle.com/schema\" " +
             "numBatchRequests=\"" + numOfLitleBatchRequest + "\">";

            string xmlFooter = "\r\n</litleRequest>";

            string filePath;

            finalFilePath = litleFile.createRandomFile(requestDirectory, Path.GetFileName(finalFilePath), ".xml", litleTime);
            filePath = finalFilePath;

            litleFile.AppendLineToFile(finalFilePath, xmlHeader);
            var a = litleFile.ReadPosition(finalFilePath);
            litleFile.AppendLineToFile(finalFilePath, authentication.Serialize());
            a = litleFile.ReadPosition(finalFilePath);

            if (batchFilePath != null)
            {
                litleFile.AppendFileToFile(finalFilePath, batchFilePath);
                a = litleFile.ReadPosition(finalFilePath);
            }
            else
            {
                throw new LitleOnlineException("No batch was added to the LitleBatch!");
            }

            litleFile.AppendLineToFile(finalFilePath, xmlFooter);
            a = litleFile.ReadPosition(finalFilePath);
            finalFilePath = null;

            return filePath;
        }

        private void fillInReportGroup(batchRequest litleBatchRequest)
        {
            if (litleBatchRequest.reportGroup == null)
            {
                litleBatchRequest.reportGroup = config["reportGroup"];
            }
        }

    }

    public class litleFile
    {
        private readonly IDictionary<string, StringBuilder> _cache;

        public litleFile(IDictionary<string, StringBuilder> cache)
        {
            _cache = cache;
        }

        public StringBuilder this[string name] { get { return _cache[name]; } } 
        public virtual string createRandomFile(string fileDirectory, string fileName, string fileExtension, litleTime litleTime)
        {
            string filePath = null;
            if (fileName == null || fileName == String.Empty)
            {
                fileName = litleTime.getCurrentTime("MM-dd-yyyy_HH-mm-ss-ffff_") + RandomGen.NextString(8);
                filePath = fileDirectory + fileName + fileExtension;
            }
            else
            {
                filePath = fileDirectory + fileName;
            }
            if (_cache.ContainsKey(filePath))
            {
                _cache[filePath] = new StringBuilder();
            }
            else
            {
                _cache.Add(filePath, new StringBuilder());
            }
            return filePath;
        }

        public virtual string AppendLineToFile(string filePath, string lineToAppend)
        {
            StringBuilder ms = _cache[filePath];
            ms.Append(lineToAppend);
            return filePath;
        }
        public virtual string ReadPosition(string filepath)
        {
            var s = _cache[filepath];
            return s.ToString();
        }

        public virtual string AppendFileToFile(string filePathToAppendTo, string filePathToAppend)
        {
            var fs = _cache[filePathToAppendTo];
            StringBuilder fsr = null;
            if (filePathToAppend != null)
            {
                fsr = _cache[filePathToAppend];
                fs.Append(fsr);
            }

            return filePathToAppendTo;
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
