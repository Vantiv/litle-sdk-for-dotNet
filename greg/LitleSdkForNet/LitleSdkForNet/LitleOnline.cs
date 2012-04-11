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

        public litleOnlineResponseTransactionResponseVoidResponse DoVoid(baseRequestTransactionVoid v)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(v);

            litleOnlineResponse response = sendToLitle(request);
            litleOnlineResponseTransactionResponseVoidResponse voidResponse = (litleOnlineResponseTransactionResponseVoidResponse)response.Item;
            return voidResponse;
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
