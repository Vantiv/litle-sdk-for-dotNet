using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Litle.Sdk
{
    public class LitleOnline : ILitleOnline
    {
        private Dictionary<string, string> _config;
        private Communications _communication;

        /**
         * Construct a Litle online using the configuration specified in LitleSdkForNet.dll.config
         */
        public LitleOnline()
        {
            _config = new Dictionary<string, string>();
            _config["url"] = Properties.Settings.Default.url;
            _config["reportGroup"] = Properties.Settings.Default.reportGroup;
            _config["username"] = Properties.Settings.Default.username;
            _config["printxml"] = Properties.Settings.Default.printxml;
            _config["timeout"] = Properties.Settings.Default.timeout;
            _config["proxyHost"] = Properties.Settings.Default.proxyHost;
            _config["merchantId"] = Properties.Settings.Default.merchantId;
            _config["password"] = Properties.Settings.Default.password;
            _config["proxyPort"] = Properties.Settings.Default.proxyPort;
            _config["logFile"] = Properties.Settings.Default.logFile;
            _config["neuterAccountNums"] = Properties.Settings.Default.neuterAccountNums;
            _communication = new Communications();
            
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
        public LitleOnline(Dictionary<string, string> config)
        {
            this._config = config;
            _communication = new Communications();
        }

        public event EventHandler HttpAction
        {
            add { _communication.HttpAction += value; }
            remove { _communication.HttpAction -= value; }
        }

        public void setCommunication(Communications communication)
        {
            this._communication = communication;
        }

        public authorizationResponse Authorize(authorization auth)
        {
            var request = CreateLitleOnlineRequest();          
            fillInReportGroup(auth);
            request.authorization = auth;

            var response = sendToLitle(request);
            var authResponse = response.authorizationResponse;
            return authResponse;
        }

        public authReversalResponse AuthReversal(authReversal reversal)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(reversal);
            request.authReversal = reversal;

            var response = sendToLitle(request);
            var reversalResponse = response.authReversalResponse;
            return reversalResponse;
        }

        public giftCardAuthReversalResponse GiftCardAuthReversal(giftCardAuthReversal giftCard)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(giftCard);
            request.giftCardAuthReversal = giftCard;

            var response = sendToLitle(request);
            var giftCardAuthReversalResponse = response.giftCardAuthReversalResponse;
            return giftCardAuthReversalResponse;
        }

        public captureResponse Capture(capture capture)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(capture);
            request.capture = capture;

            var response = sendToLitle(request);
            var captureResponse = response.captureResponse;
            return captureResponse;
        }

        public giftCardCaptureResponse GiftCardCapture(giftCardCapture giftCardCapture)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(giftCardCapture);
            request.giftCardCapture = giftCardCapture;

            var response = sendToLitle(request);
            var giftCardCaptureResponse = response.giftCardCaptureResponse;
            return giftCardCaptureResponse;
        }

        public captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(captureGivenAuth);
            request.captureGivenAuth = captureGivenAuth;

            var response = sendToLitle(request);
            var captureGivenAuthResponse = response.captureGivenAuthResponse;
            return captureGivenAuthResponse;
        }

        public creditResponse Credit(credit credit)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(credit);
            request.credit = credit;

            var response = sendToLitle(request);
            var creditResponse = response.creditResponse;
            return creditResponse;
        }

        public giftCardCreditResponse GiftCardCredit(giftCardCredit giftCardCredit)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(giftCardCredit);
            request.giftCardCredit = giftCardCredit;

            var response = sendToLitle(request);
            var giftCardCreditResponse = response.giftCardCreditResponse;
            return giftCardCreditResponse;
        }

        public echeckCreditResponse EcheckCredit(echeckCredit echeckCredit)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(echeckCredit);
            request.echeckCredit = echeckCredit;

            var response = sendToLitle(request);
            var echeckCreditResponse = response.echeckCreditResponse;
            return echeckCreditResponse;
        }

        public echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(echeckRedeposit);
            request.echeckRedeposit = echeckRedeposit;

            var response = sendToLitle(request);
            var echeckRedepositResponse = response.echeckRedepositResponse;
            return echeckRedepositResponse;
        }

        public echeckSalesResponse EcheckSale(echeckSale echeckSale)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(echeckSale);
            request.echeckSale = echeckSale;

            var response = sendToLitle(request);
            var echeckSalesResponse = response.echeckSalesResponse;
            return echeckSalesResponse;
        }

        public echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(echeckVerification);
            request.echeckVerification = echeckVerification;

            var response = sendToLitle(request);
            var echeckVerificationResponse = response.echeckVerificationResponse;
            return echeckVerificationResponse;
        }

        public forceCaptureResponse ForceCapture(forceCapture forceCapture)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(forceCapture);
            request.forceCapture = forceCapture;

            var response = sendToLitle(request);
            var forceCaptureResponse = response.forceCaptureResponse;
            return forceCaptureResponse;
        }

        public saleResponse Sale(sale sale)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(sale);
            request.sale = sale;

            var response = sendToLitle(request);
            var saleResponse = response.saleResponse;
            return saleResponse;
        }

        public registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(tokenRequest);
            request.registerTokenRequest = tokenRequest;

            var response = sendToLitle(request);
            var registerTokenResponse = response.registerTokenResponse;
            return registerTokenResponse;
        }

        public litleOnlineResponseTransactionResponseVoidResponse DoVoid(voidTxn v)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(v);
            request.voidTxn = v;

            var response = sendToLitle(request);
            var voidResponse = response.voidResponse;
            return voidResponse;
        }

        public litleOnlineResponseTransactionResponseEcheckVoidResponse EcheckVoid(echeckVoid v)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(v);
            request.echeckVoid = v;

            var response = sendToLitle(request);
            var voidResponse = response.echeckVoidResponse;
            return voidResponse;
        }

        public updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(updateCardValidationNumOnToken);
            request.updateCardValidationNumOnToken = updateCardValidationNumOnToken;

            var response = sendToLitle(request);
            var updateResponse = response.updateCardValidationNumOnTokenResponse;
            return updateResponse;
        }

        public cancelSubscriptionResponse CancelSubscription(cancelSubscription cancelSubscription)
        {
            var request = CreateLitleOnlineRequest();
            request.cancelSubscription = cancelSubscription;

            var response = sendToLitle(request);
            var cancelResponse = response.cancelSubscriptionResponse;
            return cancelResponse;
        }

        public updateSubscriptionResponse UpdateSubscription(updateSubscription updateSubscription)
        {
            var request = CreateLitleOnlineRequest();
            request.updateSubscription = updateSubscription;

            var response = sendToLitle(request);
            var updateResponse = response.updateSubscriptionResponse;
            return updateResponse;
        }

        public activateResponse Activate(activate activate)
        {
            var request = CreateLitleOnlineRequest();
            request.activate = activate;

            var response = sendToLitle(request);
            var activateResponse = response.activateResponse;
            return activateResponse;
        }

        public deactivateResponse Deactivate(deactivate deactivate)
        {
            var request = CreateLitleOnlineRequest();
            request.deactivate = deactivate;

            var response = sendToLitle(request);
            var deactivateResponse = response.deactivateResponse;
            return deactivateResponse;
        }

        public loadResponse Load(load load)
        {
            var request = CreateLitleOnlineRequest();
            request.load = load;

            var response = sendToLitle(request);
            var loadResponse = response.loadResponse;
            return loadResponse;
        }

        public unloadResponse Unload(unload unload)
        {
            var request = CreateLitleOnlineRequest();
            request.unload = unload;

            var response = sendToLitle(request);
            var unloadResponse = response.unloadResponse;
            return unloadResponse;
        }

        public balanceInquiryResponse BalanceInquiry(balanceInquiry balanceInquiry)
        {
            var request = CreateLitleOnlineRequest();
            request.balanceInquiry = balanceInquiry;

            var response = sendToLitle(request);
            var balanceInquiryResponse = response.balanceInquiryResponse;
            return balanceInquiryResponse;
        }

        public createPlanResponse CreatePlan(createPlan createPlan)
        {
            var request = CreateLitleOnlineRequest();
            request.createPlan = createPlan;

            var response = sendToLitle(request);
            var createPlanResponse = response.createPlanResponse;
            return createPlanResponse;
        }

        public updatePlanResponse UpdatePlan(updatePlan updatePlan)
        {
            var request = CreateLitleOnlineRequest();
            request.updatePlan = updatePlan;

            var response = sendToLitle(request);
            var updatePlanResponse = response.updatePlanResponse;
            return updatePlanResponse;
        }

        public refundReversalResponse RefundReversal(refundReversal refundReversal)
        {
            var request = CreateLitleOnlineRequest();
            request.refundReversal = refundReversal;

            var response = sendToLitle(request);
            var refundReversalResponse = response.refundReversalResponse;
            return refundReversalResponse;
        }

        public depositReversalResponse DepositReversal(depositReversal depositReversal)
        {
            var request = CreateLitleOnlineRequest();
            request.depositReversal = depositReversal;

            var response = sendToLitle(request);
            var depositReversalResponse = response.depositReversalResponse;
            return depositReversalResponse;
        }

        public activateReversalResponse ActivateReversal(activateReversal activateReversal)
        {
            var request = CreateLitleOnlineRequest();
            request.activateReversal = activateReversal;

            var response = sendToLitle(request);
            var activateReversalResponse = response.activateReversalResponse;
            return activateReversalResponse;
        }

        public deactivateReversalResponse DeactivateReversal(deactivateReversal deactivateReversal)
        {
            var request = CreateLitleOnlineRequest();
            request.deactivateReversal = deactivateReversal;

            var response = sendToLitle(request);
            var deactivateReversalResponse = response.deactivateReversalResponse;
            return deactivateReversalResponse;
        }

        public loadReversalResponse LoadReversal(loadReversal loadReversal)
        {
            var request = CreateLitleOnlineRequest();
            request.loadReversal = loadReversal;

            var response = sendToLitle(request);
            var loadReversalResponse = response.loadReversalResponse;
            return loadReversalResponse;
        }

        public unloadReversalResponse UnloadReversal(unloadReversal unloadReversal)
        {
            var request = CreateLitleOnlineRequest();
            request.unloadReversal = unloadReversal;

            var response = sendToLitle(request);
            var unloadReversalResponse = response.unloadReversalResponse;
            return unloadReversalResponse;
        }

        public transactionTypeWithReportGroup queryTransaction(queryTransaction queryTransaction)
        {
            var request = CreateLitleOnlineRequest();
            request.queryTransaction = queryTransaction;

            var litleresponse = sendToLitle(request);
            transactionTypeWithReportGroup response = null;
            if (litleresponse.queryTransactionResponse != null)
            {
                response = litleresponse.queryTransactionResponse;
            }
            else if (litleresponse.queryTransactionUnavailableResponse != null)
            {
                response = litleresponse.queryTransactionUnavailableResponse;
            }
            return response;
        }

        public fraudCheckResponse FraudCheck(fraudCheck fraudCheck)
        {
            var request = CreateLitleOnlineRequest();
            fillInReportGroup(fraudCheck);
            request.fraudCheck = fraudCheck;

            var response = sendToLitle(request);
            var fraudCheckResponse = response.fraudCheckResponse;
            return fraudCheckResponse;
        }

        private litleOnlineRequest CreateLitleOnlineRequest()
        {
            var request = new litleOnlineRequest();
            request.merchantId = _config["merchantId"];
            request.merchantSdk = "DotNet;11.0.2";
            var authentication = new authentication();
            authentication.password = _config["password"];
            authentication.user = _config["username"];
            request.authentication = authentication;
            return request;
        }

        private litleOnlineResponse sendToLitle(litleOnlineRequest request)
        {
            var xmlRequest = request.Serialize();
            var xmlResponse = _communication.HttpPost(xmlRequest,_config);
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
            var serializer = new XmlSerializer(typeof(litleOnlineRequest));
            var ms = new MemoryStream();
            serializer.Serialize(ms, req);
            return Encoding.UTF8.GetString(ms.GetBuffer());//return string is UTF8 encoded.
        }// serialize the xml

        public static litleOnlineResponse DeserializeObject(string response)
        {
            var serializer = new XmlSerializer(typeof(litleOnlineResponse));
            var reader = new StringReader(response);
            var i = (litleOnlineResponse)serializer.Deserialize(reader);
            return i;

        }// deserialize the object

        private void fillInReportGroup(transactionTypeWithReportGroup txn)
        {
            if (txn.reportGroup == null)
            {
                txn.reportGroup = _config["reportGroup"];
            }
        }

        private void fillInReportGroup(transactionTypeWithReportGroupAndPartial txn)
        {
            if (txn.reportGroup == null)
            {
                txn.reportGroup = _config["reportGroup"];
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
