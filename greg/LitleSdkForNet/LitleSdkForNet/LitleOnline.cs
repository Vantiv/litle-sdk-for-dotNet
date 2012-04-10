using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitleXSDGenerated;

namespace LitleSdkForNet
{
    public class LitleOnline
    {
        private Dictionary<String, String> config;
        private Communications communication;

        /**
         * Construct a Litle online using the configuration specified in %HOME%/.litle_SDK_config.properties
         */
        public LitleOnline()
        {
            config = new Dictionary<String, String>();
             communication = new Communications();
            //TODO load config from file
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
         * version (eg 8.10)
         * timeout (in seconds)
         * Optional properties are:
         * proxyHost
         * proxyPort
         * printxml (possible values "true" and "false" - defaults to false)
         */
        public LitleOnline(Dictionary<String, String> config)
        {
            this.config = config;
            communication = new Communications();
        }

        public authorizationResponse Authorize(authorization auth)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(auth);

            litleOnlineResponse response = sendToLitle(request);
            authorizationResponse authResponse = (authorizationResponse)response.Item;
            return authResponse;
        }

        private litleOnlineRequest createLitleOnlineRequest()
        {
            litleOnlineRequest request = new litleOnlineRequest();
            request.merchantId = config["merchantId"];
            request.version = config["version"];
            authentication authentication = new authentication();
            authentication.password = config["password"];
            authentication.user = config["user"];
            request.authentication = authentication;
            return request;
        }

        private litleOnlineResponse sendToLitle(litleOnlineRequest request)
        {
            string xmlRequest = request.Serialize();
            string xmlResponse = communication.HttpPost(xmlRequest,config);
            litleOnlineResponse response = litleOnlineResponse.Deserialize(xmlResponse);
            if("1".Equals(response.response)) {
                throw new LitleOnlineException(response.message);
            }
            return response;
        }

        private void fillInReportGroup(transactionTypeWithReportGroup txn)
        {
            if (txn.reportGroup == null)
            {
                txn.reportGroup = config["reportGroup"];
            }
        }

        private void fillInReportGroup(transactionTypeWithReportGroupAndPartial txn)
        {
            if (txn.reportGroup == null)
            {
                txn.reportGroup = config["reportGroup"];
            }
        }
    }
}
