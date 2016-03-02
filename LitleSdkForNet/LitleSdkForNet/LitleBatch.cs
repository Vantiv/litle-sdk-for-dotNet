using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Litle.Sdk.Properties;

namespace Litle.Sdk
{
    public class litleRequest
    {
        private readonly IDictionary<string, StringBuilder> _cache;
        private authentication authentication;
        private readonly Dictionary<string, string> config;
        private Communications communication;
        private litleXmlSerializer litleXmlSerializer;
        private int numOfLitleBatchRequest;
        private int numOfRFRRequest;
        public string finalFilePath;
        private string batchFilePath;
        private string requestDirectory;
        private string responseDirectory;
        private litleTime litleTime;
        private litleFile litleFile;

        /**
         * Construct a Litle online using the configuration specified in LitleSdkForNet.dll.config
         */

        public litleRequest(IDictionary<string, StringBuilder> cache)
        {
            _cache = cache;
            config = new Dictionary<string, string>();

            config["url"] = Settings.Default.url;
            config["reportGroup"] = Settings.Default.reportGroup;
            config["username"] = Settings.Default.username;
            config["printxml"] = Settings.Default.printxml;
            config["timeout"] = Settings.Default.timeout;
            config["proxyHost"] = Settings.Default.proxyHost;
            config["merchantId"] = Settings.Default.merchantId;
            config["password"] = Settings.Default.password;
            config["proxyPort"] = Settings.Default.proxyPort;
            config["sftpUrl"] = Settings.Default.sftpUrl;
            config["sftpUsername"] = Settings.Default.sftpUsername;
            config["sftpPassword"] = Settings.Default.sftpPassword;
            config["knownHostsFile"] = Settings.Default.knownHostsFile;
            config["onlineBatchUrl"] = Settings.Default.onlineBatchUrl;
            config["onlineBatchPort"] = Settings.Default.onlineBatchPort;
            config["requestDirectory"] = Settings.Default.requestDirectory;
            config["responseDirectory"] = Settings.Default.responseDirectory;

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

        public litleRequest(IDictionary<string, StringBuilder> cache, Dictionary<string, string> config)
        {
            _cache = cache;
            this.config = config;

            initializeRequest();
        }

        private void initializeRequest()
        {
            communication = new Communications(_cache);

            authentication = new authentication();
            authentication.user = config["username"];
            authentication.password = config["password"];

            requestDirectory = config["requestDirectory"] + "\\Requests\\";
            responseDirectory = config["responseDirectory"] + "\\Responses\\";

            litleXmlSerializer = new litleXmlSerializer();
            litleTime = new litleTime();
            litleFile = new litleFile(_cache);
        }

        public authentication getAuthenication()
        {
            return authentication;
        }

        public string getRequestDirectory()
        {
            return requestDirectory;
        }

        public string getResponseDirectory()
        {
            return responseDirectory;
        }

        public void setCommunication(Communications communication)
        {
            this.communication = communication;
        }

        public Communications getCommunication()
        {
            return communication;
        }

        public void setLitleXmlSerializer(litleXmlSerializer litleXmlSerializer)
        {
            this.litleXmlSerializer = litleXmlSerializer;
        }

        public litleXmlSerializer getLitleXmlSerializer()
        {
            return litleXmlSerializer;
        }

        public void setLitleTime(litleTime litleTime)
        {
            this.litleTime = litleTime;
        }

        public litleTime getLitleTime()
        {
            return litleTime;
        }

        public void setLitleFile(litleFile litleFile)
        {
            this.litleFile = litleFile;
        }

        public litleFile getLitleFile()
        {
            return litleFile;
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
            if (numOfRFRRequest >= 1)
            {
                throw new LitleOnlineException("Can not add more than one RFRRequest to a batch!");
            }

            batchFilePath = SerializeRFRRequestToFile(rfrRequest, batchFilePath);
            numOfRFRRequest++;
        }

        public litleResponse sendToLitleWithStream()
        {
            var requestFilePath = Serialize();
            var batchName = Path.GetFileName(requestFilePath);

            var responseFilePath = communication.socketStream(requestFilePath, responseDirectory, config);
            var stringBuilder = _cache[responseFilePath];
            var litleResponse = litleXmlSerializer.DeserializeObjectFromString(stringBuilder.ToString());
            return litleResponse;
        }

        public string sendToLitle()
        {
            var requestFilePath = Serialize();

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

            var stringBuilder = _cache[responseDirectory + batchFileName];
            var litleResponse = litleXmlSerializer.DeserializeObjectFromString(stringBuilder.ToString());
            return litleResponse;
        }

        public string SerializeBatchRequestToFile(batchRequest litleBatchRequest, string filePath)
        {
            filePath = litleFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_litleRequest.xml",
                litleTime);
            var tempFilePath = litleBatchRequest.Serialize();

            litleFile.AppendFileToFile(filePath, tempFilePath);

            return filePath;
        }

        public string SerializeRFRRequestToFile(RFRRequest rfrRequest, string filePath)
        {
            filePath = litleFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_litleRequest.xml",
                litleTime);
            var tempFilePath = rfrRequest.Serialize();

            litleFile.AppendFileToFile(filePath, tempFilePath);

            return filePath;
        }

        public string Serialize()
        {
            var xmlHeader = "<?xml version='1.0' encoding='utf-8'?>\r\n<litleRequest version=\"9.3\"" +
                            " xmlns=\"http://www.litle.com/schema\" " +
                            "numBatchRequests=\"" + numOfLitleBatchRequest + "\">";

            var xmlFooter = "\r\n</litleRequest>";

            string filePath;

            finalFilePath = litleFile.createRandomFile(requestDirectory, Path.GetFileName(finalFilePath), ".xml",
                litleTime);
            filePath = finalFilePath;

            litleFile.AppendLineToFile(finalFilePath, xmlHeader);
            litleFile.AppendLineToFile(finalFilePath, authentication.Serialize());

            if (batchFilePath != null)
            {
                litleFile.AppendFileToFile(finalFilePath, batchFilePath);
            }
            else
            {
                throw new LitleOnlineException("No batch was added to the LitleBatch!");
            }

            litleFile.AppendLineToFile(finalFilePath, xmlFooter);
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

        public virtual string createRandomFile(string fileDirectory, string fileName, string fileExtension,
            litleTime litleTime)
        {
            string filePath = null;
            if (fileName == null || fileName == string.Empty)
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
            var ms = _cache[filePath];
            ms.Append(lineToAppend);
            return filePath;
        }

        public virtual string AppendFileToFile(string filePathToAppendTo, string filePathToAppend)
        {
            var fs = _cache[filePathToAppendTo];
            if (filePathToAppend != null)
            {
                var fsr = _cache[filePathToAppend];
                fs.Append(fsr);
            }

            return filePathToAppendTo;
        }
    }

    public static class RandomGen
    {
        private static readonly RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();
        private static Random _local;

        public static int NextInt()
        {
            var inst = _local;
            if (inst == null)
            {
                var buffer = new byte[8];
                _global.GetBytes(buffer);
                _local = inst = new Random(BitConverter.ToInt32(buffer, 0));
            }

            return _local.Next();
        }

        public static string NextString(int length)
        {
            var result = "";

            for (var i = 0; i < length; i++)
            {
                result += Convert.ToChar(NextInt()%('Z' - 'A') + 'A');
            }

            return result;
        }
    }

    public class litleTime
    {
        public virtual string getCurrentTime(string format)
        {
            return DateTime.Now.ToString(format);
        }
    }
}
