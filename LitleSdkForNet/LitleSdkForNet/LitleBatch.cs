using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;

namespace Litle.Sdk
{
    public partial class litleRequest
    {
        private authentication authentication;
        private Dictionary<string, string> config;
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
        public litleRequest()
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
            config["onlineBatchUrl"] = Properties.Settings.Default.onlineBatchUrl;
            config["onlineBatchPort"] = Properties.Settings.Default.onlineBatchPort;
            config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            config["responseDirectory"] = Properties.Settings.Default.responseDirectory;
            config["useEncryption"] = Properties.Settings.Default.useEncryption;
            config["vantivPublicKeyId"] = Properties.Settings.Default.vantivPublicKeyId;
            config["pgpPassphrase"] = Properties.Settings.Default.pgpPassphrase;

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
        public litleRequest(Dictionary<string, string> config)
        {
            this.config = config;

            initializeRequest();
        }

        private void initializeRequest()
        {
            communication = new Communications();

            authentication = new authentication();
            authentication.user = config["username"];
            authentication.password = config["password"];

            requestDirectory = config["requestDirectory"] + "\\Requests\\";
            responseDirectory = config["responseDirectory"] + "\\Responses\\";

            litleXmlSerializer = new litleXmlSerializer();
            litleTime = new litleTime();
            litleFile = new litleFile();
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
            var requestFilePath = this.Serialize();
            var batchName = Path.GetFileName(requestFilePath);

            var responseFilePath = communication.SocketStream(requestFilePath, responseDirectory, config);

            var litleResponse = (litleResponse)litleXmlSerializer.DeserializeObjectFromFile(responseFilePath);
            return litleResponse;
        }

        public string sendToLitle()
        {
            var useEncryption =  config.ContainsKey("useEncryption")? config["useEncryption"] : "false";
            var vantivPublicKeyId = config.ContainsKey("vantivPublicKeyId")? config["vantivPublicKeyId"] : "";
            
            var requestFilePath = this.Serialize();
            var batchRequestDir = requestDirectory;
            var finalRequestFilePath = requestFilePath;
            if ("true".Equals(useEncryption))
            {
                batchRequestDir = Path.Combine(requestDirectory, "encrypted");
                Console.WriteLine(batchRequestDir);
                finalRequestFilePath =
                    Path.Combine(batchRequestDir, Path.GetFileName(requestFilePath) + ".encrypted");
                litleFile.createDirectory(finalRequestFilePath);
                PgpHelper.EncryptFile(requestFilePath, finalRequestFilePath, vantivPublicKeyId);
            }
            
            communication.FtpDropOff(batchRequestDir, Path.GetFileName(finalRequestFilePath), config);
            
            return Path.GetFileName(finalRequestFilePath);
        }


        public void blockAndWaitForResponse(string fileName, int timeOut)
        {
            communication.FtpPoll(fileName, timeOut, config);
        }

        public litleResponse receiveFromLitle(string batchFileName)
        {
            var useEncryption =  config.ContainsKey("useEncryption")? config["useEncryption"] : "false";
            var pgpPassphrase = config.ContainsKey("pgpPassphrase")? config["pgpPassphrase"] : "";
            
            litleFile.createDirectory(responseDirectory);
            
            var responseFilePath = Path.Combine(responseDirectory, batchFileName);
            var batchResponseDir = responseDirectory;
            var finalResponseFilePath = responseFilePath;

            if ("true".Equals(useEncryption))
            {
                batchResponseDir = Path.Combine(responseDirectory, "encrypted");
                finalResponseFilePath =
                    Path.Combine(batchResponseDir, batchFileName);
                litleFile.createDirectory(finalResponseFilePath);
            }
            communication.FtpPickUp(finalResponseFilePath, config, batchFileName);

            if ("true".Equals(useEncryption))
            {
                responseFilePath = responseFilePath.Replace(".encrypted", "");
                PgpHelper.DecryptFile(finalResponseFilePath, responseFilePath, pgpPassphrase);
            }

            var litleResponse = (litleResponse)litleXmlSerializer.DeserializeObjectFromFile(responseFilePath);
                        
            return litleResponse;
        }

        public string SerializeBatchRequestToFile(batchRequest litleBatchRequest, string filePath)
        {

            filePath = litleFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_litleRequest.xml", litleTime);
            var tempFilePath = litleBatchRequest.Serialize();

            litleFile.AppendFileToFile(filePath, tempFilePath);

            return filePath;
        }

        public string SerializeRFRRequestToFile(RFRRequest rfrRequest, string filePath)
        {
            filePath = litleFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_litleRequest.xml", litleTime);
            var tempFilePath = rfrRequest.Serialize();

            litleFile.AppendFileToFile(filePath, tempFilePath);

            return filePath;
        }

        public string Serialize()
        {
            var xmlHeader = "<?xml version='1.0' encoding='utf-8'?>\r\n<litleRequest version=\"11.4\"" +
             " xmlns=\"http://www.litle.com/schema\" " +
             "numBatchRequests=\"" + numOfLitleBatchRequest + "\">";

            var xmlFooter = "\r\n</litleRequest>";

            finalFilePath = litleFile.createRandomFile(requestDirectory, Path.GetFileName(finalFilePath), ".xml", litleTime);
            var filePath = finalFilePath;

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
    
    
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRoot("litleResponse", Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class litleResponse
    {
        public string id;
        public long litleBatchId;
        public long litleSessionId;
        public string merchantId;
        public string response;
        public string message;
        public string version;

        private XmlReader originalXmlReader;
        private XmlReader batchResponseReader;
        private XmlReader rfrResponseReader;
        private string filePath;

        public litleResponse()
        {
        }

        public litleResponse(string filePath)
        {
            XmlTextReader reader = new XmlTextReader(filePath);
            readXml(reader, filePath);
        }

        public litleResponse(XmlReader reader, string filePath)
        {
            readXml(reader, filePath);
        }

        public void setBatchResponseReader(XmlReader xmlReader)
        {
            this.batchResponseReader = xmlReader;
        }

        public void setRfrResponseReader(XmlReader xmlReader)
        {
            this.rfrResponseReader = xmlReader;
        }

        public void readXml(XmlReader reader, string filePath)
        {
            if (reader.ReadToFollowing("litleResponse"))
            {
                version = reader.GetAttribute("version");
                message = reader.GetAttribute("message");
                response = reader.GetAttribute("response");

                string rawLitleSessionId = reader.GetAttribute("litleSessionId");
                if (rawLitleSessionId != null)
                {
                    litleSessionId = Int64.Parse(rawLitleSessionId);
                }
            }
            else
            {
                reader.Close();
            }

            this.originalXmlReader = reader;
            this.filePath = filePath;

            this.batchResponseReader = new XmlTextReader(filePath);
            if (!batchResponseReader.ReadToFollowing("batchResponse"))
            {
                batchResponseReader.Close();
            }

            this.rfrResponseReader = new XmlTextReader(filePath);
            if (!rfrResponseReader.ReadToFollowing("RFRResponse"))
            {
                rfrResponseReader.Close();
            }

        }

        virtual public batchResponse nextBatchResponse()
        {
            if (batchResponseReader.ReadState != ReadState.Closed)
            {
                batchResponse litleBatchResponse = new batchResponse(batchResponseReader, filePath);
                if (!batchResponseReader.ReadToFollowing("batchResponse"))
                {
                    batchResponseReader.Close();
                }

                return litleBatchResponse;
            }

            return null;
        }

        virtual public RFRResponse nextRFRResponse()
        {
            if (rfrResponseReader.ReadState != ReadState.Closed)
            {
                string response = rfrResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(RFRResponse));
                StringReader reader = new StringReader(response);
                RFRResponse rfrResponse = (RFRResponse)serializer.Deserialize(reader);

                if (!rfrResponseReader.ReadToFollowing("RFRResponse"))
                {
                    rfrResponseReader.Close();
                }

                return rfrResponse;
            }

            return null;
        }
    }

    public class litleFile
    {
        public virtual string createRandomFile(string fileDirectory, string fileName, string fileExtension, litleTime litleTime)
        {
            string filePath = null;
            if (string.IsNullOrEmpty(fileName))
            {
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                fileName = litleTime.getCurrentTime("MM-dd-yyyy_HH-mm-ss-ffff_") + RandomGen.NextString(8);
                filePath = fileDirectory + fileName + fileExtension;

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                }
            }
            else
            {
                filePath = fileDirectory + fileName;
            }

            return filePath;
        }

        public virtual string AppendLineToFile(string filePath, string lineToAppend)
        {
            using (var fs = new FileStream(filePath, FileMode.Append))
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(lineToAppend);
            }

            return filePath;
        }


        public virtual string AppendFileToFile(string filePathToAppendTo, string filePathToAppend)
        {

            using (var fs = new FileStream(filePathToAppendTo, FileMode.Append))
            using (var fsr = new FileStream(filePathToAppend, FileMode.Open))
            {
                var buffer = new byte[16];

                var bytesRead = 0;

                do
                {
                    bytesRead = fsr.Read(buffer, 0, buffer.Length);
                    fs.Write(buffer, 0, bytesRead);
                }
                while (bytesRead > 0);
            }

            File.Delete(filePathToAppend);

            return filePathToAppendTo;
        }

        public virtual void createDirectory(string destinationFilePath)
        {
            var destinationDirectory = Path.GetDirectoryName(destinationFilePath);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }
        }
    }

    public static class RandomGen
    {
        private static RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();
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
                result += Convert.ToChar(NextInt() % ('Z' - 'A') + 'A');
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
