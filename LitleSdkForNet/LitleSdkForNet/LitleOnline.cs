using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Litle.Sdk.Properties;

namespace Litle.Sdk
{
    public class LitleOnline : ILitleOnline
    {
        private readonly Dictionary<string, string> config;
        private Communications communication;
        private readonly IDictionary<string, StringBuilder> _cache;

        /**
         * Construct a Litle online using the configuration specified in LitleSdkForNet.dll.config
         */

        public LitleOnline(IDictionary<string, StringBuilder> cache)
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
            config["logFile"] = Settings.Default.logFile;
            config["neuterAccountNums"] = Settings.Default.neuterAccountNums;
            communication = new Communications(_cache);
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
         */

        public LitleOnline(IDictionary<string, StringBuilder> cache, Dictionary<string, string> config)
        {
            _cache = cache;
            this.config = config;
            communication = new Communications(_cache);
        }

        public void setCommunication(Communications communication)
        {
            this.communication = communication;
        }

        public authorizationResponse Authorize(authorization auth)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(auth);
            request.authorization = auth;

            var response = sendToLitle(request);
            var authResponse = response.authorizationResponse;
            return authResponse;
        }

        public authReversalResponse AuthReversal(authReversal reversal)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(reversal);
            request.authReversal = reversal;

            var response = sendToLitle(request);
            var reversalResponse = response.authReversalResponse;
            return reversalResponse;
        }

        public captureResponse Capture(capture capture)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(capture);
            request.capture = capture;

            var response = sendToLitle(request);
            var captureResponse = response.captureResponse;
            return captureResponse;
        }

        public captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(captureGivenAuth);
            request.captureGivenAuth = captureGivenAuth;

            var response = sendToLitle(request);
            var captureGivenAuthResponse = response.captureGivenAuthResponse;
            return captureGivenAuthResponse;
        }

        public creditResponse Credit(credit credit)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(credit);
            request.credit = credit;

            var response = sendToLitle(request);
            var creditResponse = response.creditResponse;
            return creditResponse;
        }

        public echeckCreditResponse EcheckCredit(echeckCredit echeckCredit)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(echeckCredit);
            request.echeckCredit = echeckCredit;

            var response = sendToLitle(request);
            var echeckCreditResponse = response.echeckCreditResponse;
            return echeckCreditResponse;
        }

        public echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(echeckRedeposit);
            request.echeckRedeposit = echeckRedeposit;

            var response = sendToLitle(request);
            var echeckRedepositResponse = response.echeckRedepositResponse;
            return echeckRedepositResponse;
        }

        public echeckSalesResponse EcheckSale(echeckSale echeckSale)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(echeckSale);
            request.echeckSale = echeckSale;

            var response = sendToLitle(request);
            var echeckSalesResponse = response.echeckSalesResponse;
            return echeckSalesResponse;
        }

        public echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(echeckVerification);
            request.echeckVerification = echeckVerification;

            var response = sendToLitle(request);
            var echeckVerificationResponse = response.echeckVerificationResponse;
            return echeckVerificationResponse;
        }

        public forceCaptureResponse ForceCapture(forceCapture forceCapture)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(forceCapture);
            request.forceCapture = forceCapture;

            var response = sendToLitle(request);
            var forceCaptureResponse = response.forceCaptureResponse;
            return forceCaptureResponse;
        }

        public saleResponse Sale(sale sale)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(sale);
            request.sale = sale;

            var response = sendToLitle(request);
            var saleResponse = response.saleResponse;
            return saleResponse;
        }

        public registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(tokenRequest);
            request.registerTokenRequest = tokenRequest;

            var response = sendToLitle(request);
            var registerTokenResponse = response.registerTokenResponse;
            return registerTokenResponse;
        }

        public litleOnlineResponseTransactionResponseVoidResponse DoVoid(voidTxn v)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(v);
            request.voidTxn = v;

            var response = sendToLitle(request);
            var voidResponse = response.voidResponse;
            return voidResponse;
        }

        public litleOnlineResponseTransactionResponseEcheckVoidResponse EcheckVoid(echeckVoid v)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(v);
            request.echeckVoid = v;

            var response = sendToLitle(request);
            var voidResponse = response.echeckVoidResponse;
            return voidResponse;
        }

        public updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(
            updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            var request = createLitleOnlineRequest();
            fillInReportGroup(updateCardValidationNumOnToken);
            request.updateCardValidationNumOnToken = updateCardValidationNumOnToken;

            var response = sendToLitle(request);
            var updateResponse = response.updateCardValidationNumOnTokenResponse;
            return updateResponse;
        }

        public cancelSubscriptionResponse CancelSubscription(cancelSubscription cancelSubscription)
        {
            var request = createLitleOnlineRequest();
            request.cancelSubscription = cancelSubscription;

            var response = sendToLitle(request);
            var cancelResponse = response.cancelSubscriptionResponse;
            return cancelResponse;
        }

        public updateSubscriptionResponse UpdateSubscription(updateSubscription updateSubscription)
        {
            var request = createLitleOnlineRequest();
            request.updateSubscription = updateSubscription;

            var response = sendToLitle(request);
            var updateResponse = response.updateSubscriptionResponse;
            return updateResponse;
        }

        public activateResponse Activate(activate activate)
        {
            var request = createLitleOnlineRequest();
            request.activate = activate;

            var response = sendToLitle(request);
            var activateResponse = response.activateResponse;
            return activateResponse;
        }

        public deactivateResponse Deactivate(deactivate deactivate)
        {
            var request = createLitleOnlineRequest();
            request.deactivate = deactivate;

            var response = sendToLitle(request);
            var deactivateResponse = response.deactivateResponse;
            return deactivateResponse;
        }

        public loadResponse Load(load load)
        {
            var request = createLitleOnlineRequest();
            request.load = load;

            var response = sendToLitle(request);
            var loadResponse = response.loadResponse;
            return loadResponse;
        }

        public unloadResponse Unload(unload unload)
        {
            var request = createLitleOnlineRequest();
            request.unload = unload;

            var response = sendToLitle(request);
            var unloadResponse = response.unloadResponse;
            return unloadResponse;
        }

        public balanceInquiryResponse BalanceInquiry(balanceInquiry balanceInquiry)
        {
            var request = createLitleOnlineRequest();
            request.balanceInquiry = balanceInquiry;

            var response = sendToLitle(request);
            var balanceInquiryResponse = response.balanceInquiryResponse;
            return balanceInquiryResponse;
        }

        public createPlanResponse CreatePlan(createPlan createPlan)
        {
            var request = createLitleOnlineRequest();
            request.createPlan = createPlan;

            var response = sendToLitle(request);
            var createPlanResponse = response.createPlanResponse;
            return createPlanResponse;
        }

        public updatePlanResponse UpdatePlan(updatePlan updatePlan)
        {
            var request = createLitleOnlineRequest();
            request.updatePlan = updatePlan;

            var response = sendToLitle(request);
            var updatePlanResponse = response.updatePlanResponse;
            return updatePlanResponse;
        }

        public refundReversalResponse RefundReversal(refundReversal refundReversal)
        {
            var request = createLitleOnlineRequest();
            request.refundReversal = refundReversal;

            var response = sendToLitle(request);
            var refundReversalResponse = response.refundReversalResponse;
            return refundReversalResponse;
        }

        public depositReversalResponse DepositReversal(depositReversal depositReversal)
        {
            var request = createLitleOnlineRequest();
            request.depositReversal = depositReversal;

            var response = sendToLitle(request);
            var depositReversalResponse = response.depositReversalResponse;
            return depositReversalResponse;
        }

        public activateReversalResponse ActivateReversal(activateReversal activateReversal)
        {
            var request = createLitleOnlineRequest();
            request.activateReversal = activateReversal;

            var response = sendToLitle(request);
            var activateReversalResponse = response.activateReversalResponse;
            return activateReversalResponse;
        }

        public deactivateReversalResponse DeactivateReversal(deactivateReversal deactivateReversal)
        {
            var request = createLitleOnlineRequest();
            request.deactivateReversal = deactivateReversal;

            var response = sendToLitle(request);
            var deactivateReversalResponse = response.deactivateReversalResponse;
            return deactivateReversalResponse;
        }

        public loadReversalResponse LoadReversal(loadReversal loadReversal)
        {
            var request = createLitleOnlineRequest();
            request.loadReversal = loadReversal;

            var response = sendToLitle(request);
            var loadReversalResponse = response.loadReversalResponse;
            return loadReversalResponse;
        }

        public unloadReversalResponse UnloadReversal(unloadReversal unloadReversal)
        {
            var request = createLitleOnlineRequest();
            request.unloadReversal = unloadReversal;

            var response = sendToLitle(request);
            var unloadReversalResponse = response.unloadReversalResponse;
            return unloadReversalResponse;
        }

        private litleOnlineRequest createLitleOnlineRequest()
        {
            var request = new litleOnlineRequest();
            request.merchantId = config["merchantId"];
            request.merchantSdk = "DotNet;9.3.2";
            var authentication = new authentication();
            authentication.password = config["password"];
            authentication.user = config["username"];
            request.authentication = authentication;
            return request;
        }

        private litleOnlineResponse sendToLitle(litleOnlineRequest request)
        {
            var xmlRequest = request.Serialize();
            var xmlResponse = communication.HttpPost(xmlRequest, config);
            try
            {
                var litleOnlineResponse = DeserializeObject(xmlResponse);
                if ("1".Equals(litleOnlineResponse.response))
                {
                    throw new LitleOnlineException(litleOnlineResponse.message);
                }
                return litleOnlineResponse;
            }
            catch (InvalidOperationException ioe)
            {
                throw new LitleOnlineException("Error validating xml data against the schema", ioe);
            }
        }

        public static string SerializeObject(litleOnlineRequest req)
        {
            var serializer = new XmlSerializer(typeof (litleOnlineRequest));
            var ms = new MemoryStream();
            serializer.Serialize(ms, req);
            return Encoding.UTF8.GetString(ms.GetBuffer()); //return string is UTF8 encoded.
        } // serialize the xml

        public static litleOnlineResponse DeserializeObject(string response)
        {
            var serializer = new XmlSerializer(typeof (litleOnlineResponse));
            var reader = new StringReader(response);
            var i = (litleOnlineResponse) serializer.Deserialize(reader);
            return i;
        } // deserialize the object

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

    public interface ILitleOnline
    {
        authorizationResponse Authorize(authorization auth);
        authReversalResponse AuthReversal(authReversal reversal);
        captureResponse Capture(capture capture);
        captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth);
        creditResponse Credit(credit credit);
        echeckCreditResponse EcheckCredit(echeckCredit echeckCredit);
        echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit);
        echeckSalesResponse EcheckSale(echeckSale echeckSale);
        echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification);
        forceCaptureResponse ForceCapture(forceCapture forceCapture);
        saleResponse Sale(sale sale);
        registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest);
        litleOnlineResponseTransactionResponseVoidResponse DoVoid(voidTxn v);
        litleOnlineResponseTransactionResponseEcheckVoidResponse EcheckVoid(echeckVoid v);
        updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(updateCardValidationNumOnToken update);
    }
}
