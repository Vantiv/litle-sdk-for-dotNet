using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace LitleSdkForNet
{
    public class LitleOnline
    {
        private Dictionary<String, String> config;
        private Communications communication;

        /**
         * Construct a Litle online using the configuration specified in LitleSdkForNet.dll.config
         */
        public LitleOnline()
        {
            config = new Dictionary<String, String>();
            config["url"] = Properties.Settings.Default.url;
            config["reportGroup"] = Properties.Settings.Default.reportGroup;
            config["username"] = Properties.Settings.Default.username;
            config["printxml"] = Properties.Settings.Default.printxml;
            config["version"] = Properties.Settings.Default.version;
            config["timeout"] = Properties.Settings.Default.timeout;
            config["proxyHost"] = Properties.Settings.Default.proxyHost;
            config["merchantId"] = Properties.Settings.Default.merchantId;
            config["password"] = Properties.Settings.Default.password;
            config["proxyPort"] = Properties.Settings.Default.proxyPort;
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
            request.authorization = auth;
            //request.
            //request.Item = auth;
            //Console.WriteLine(request.Item.reportGroup);

            //baseRequest baseRequest = (baseRequest)request;
            //Console.WriteLine(baseRequest.authentication.user);
            //transactionTypeWithReportGroup ttwrg = baseRequest.Item;
            //Console.WriteLine(ttwrg.reportGroup);
            //authorization auth2 = (authorization)ttwrg;
            //Console.WriteLine(auth2.amount);
            //request.Item = 


            litleOnlineResponse response = sendToLitle(request);
            authorizationResponse authResponse = (authorizationResponse)response.Item;
            return authResponse;
        }

        public authReversalResponse AuthReversal(authReversal reversal)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(reversal);

            litleOnlineResponse response = sendToLitle(request);
            authReversalResponse reversalResponse = (authReversalResponse)response.Item;
            return reversalResponse;
        }

        public captureResponse Capture(capture capture)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(capture);

            litleOnlineResponse response = sendToLitle(request);
            captureResponse captureResponse = (captureResponse)response.Item;
            return captureResponse;
        }

        public captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(captureGivenAuth);

            litleOnlineResponse response = sendToLitle(request);
            captureGivenAuthResponse captureGivenAuthResponse = (captureGivenAuthResponse)response.Item;
            return captureGivenAuthResponse;
        }

        public creditResponse Credit(credit credit)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(credit);

            litleOnlineResponse response = sendToLitle(request);
            creditResponse creditResponse = (creditResponse)response.Item;
            return creditResponse;
        }

        public echeckCreditResponse EcheckCredit(echeckCredit echeckCredit)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(echeckCredit);

            litleOnlineResponse response = sendToLitle(request);
            echeckCreditResponse echeckCreditResponse = (echeckCreditResponse)response.Item;
            return echeckCreditResponse;
        }

        public echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(echeckRedeposit);

            litleOnlineResponse response = sendToLitle(request);
            echeckRedepositResponse echeckRedepositResponse = (echeckRedepositResponse)response.Item;
            return echeckRedepositResponse;
        }

        public echeckSalesResponse EcheckSale(echeckSale echeckSale)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(echeckSale);

            litleOnlineResponse response = sendToLitle(request);
            echeckSalesResponse echeckSalesResponse = (echeckSalesResponse)response.Item;
            return echeckSalesResponse;
        }

        public echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(echeckVerification);

            litleOnlineResponse response = sendToLitle(request);
            echeckVerificationResponse echeckVerificationResponse = (echeckVerificationResponse)response.Item;
            return echeckVerificationResponse;
        }

        public forceCaptureResponse ForceCapture(forceCapture forceCapture)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(forceCapture);

            litleOnlineResponse response = sendToLitle(request);
            forceCaptureResponse forceCaptureResponse = (forceCaptureResponse)response.Item;
            return forceCaptureResponse;
        }

        public saleResponse Sale(sale sale)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(sale);

            litleOnlineResponse response = sendToLitle(request);
            saleResponse saleResponse = (saleResponse)response.Item;
            return saleResponse;
        }

        public registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(tokenRequest);

            litleOnlineResponse response = sendToLitle(request);
            registerTokenResponse registerTokenResponse = (registerTokenResponse)response.Item;
            return registerTokenResponse;
        }

        //public litleOnlineResponseTransactionResponseVoidResponse DoVoid(baseRequestTransactionVoid v)
        //{
        //    litleOnlineRequest request = createLitleOnlineRequest();
        //    fillInReportGroup(v);

        //    litleOnlineResponse response = sendToLitle(request);
        //    litleOnlineResponseTransactionResponseVoidResponse voidResponse = (litleOnlineResponseTransactionResponseVoidResponse)response.Item;
        //    return voidResponse;
        //}

        private litleOnlineRequest createLitleOnlineRequest()
        {
            litleOnlineRequest request = new litleOnlineRequest();
            request.merchantId = config["merchantId"];
            request.version = config["version"];
            authentication authentication = new authentication();
            authentication.password = config["password"];
            authentication.user = config["username"];
            request.authentication = authentication;
            return request;
        }

        private litleOnlineResponse sendToLitle(litleOnlineRequest request)
        {
            //string xmlRequest = request.Serialize();
            string xmlRequest = SerializeObject(request);
            string xmlResponse = communication.HttpPost(xmlRequest,config);
            //litleOnlineResponse response = litleOnlineResponse.Deserialize(xmlResponse);
            //litleO
            //if("1".Equals(response.response)) {
            //    throw new LitleOnlineException(response.message);
            //}
            //return response;
            litleOnlineResponse litleOnlineResponse = DeserializeObject(xmlResponse);
            return litleOnlineResponse;
        }

        public static String SerializeObject(litleOnlineRequest req)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(litleOnlineRequest));
            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, req);
            return Encoding.UTF8.GetString(ms.GetBuffer());//return string is UTF8 encoded.
        }// serialize the xml

        public static litleOnlineResponse DeserializeObject(string response)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(litleOnlineResponse));
            StringReader reader = new StringReader(response);
            litleOnlineResponse i = (litleOnlineResponse)serializer.Deserialize(reader);
            return i;

        }// deserialize the object

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
