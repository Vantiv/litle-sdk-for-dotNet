using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Litle.Sdk
{
    public class LitleOnlineBatch
    {

        private Dictionary<String, String> config;
        private Communications communication;
        private List<litleBatchRequest> listOfLitleBatchRequest;

        /**
         * Construct a Litle online using the configuration specified in LitleSdkForNet.dll.config
         */
        public LitleOnlineBatch()
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
        public LitleOnlineBatch(Dictionary<String, String> config)
        {
            this.config = config;
            communication = new Communications();

            listOfLitleBatchRequest = new List<litleBatchRequest>();
        }

        public void setCommunication(Communications communication)
        {
            this.communication = communication;
        }

        public void addBatch(litleBatchRequest litleBatchRequest)
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

}
