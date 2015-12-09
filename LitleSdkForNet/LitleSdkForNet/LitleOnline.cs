using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Litle.Sdk
{
    public class LitleOnline : ILitleOnline
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
            config["timeout"] = Properties.Settings.Default.timeout;
            config["proxyHost"] = Properties.Settings.Default.proxyHost;
            config["merchantId"] = Properties.Settings.Default.merchantId;
            config["password"] = Properties.Settings.Default.password;
            config["proxyPort"] = Properties.Settings.Default.proxyPort;
            config["logFile"] = Properties.Settings.Default.logFile;
            config["neuterAccountNums"] = Properties.Settings.Default.neuterAccountNums;
            communication = new Communications();
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
        public LitleOnline(Dictionary<String, String> config)
        {
            this.config = config;
            communication = new Communications();
        }

        public void setCommunication(Communications communication)
        {
            this.communication = communication;
        }

        public authorizationResponse Authorize(authorization auth)
        {
            litleOnlineRequest request = createLitleOnlineRequest();          
            fillInReportGroup(auth);
            request.authorization = auth;

            litleOnlineResponse response = sendToLitle(request);
            authorizationResponse authResponse = (authorizationResponse)response.authorizationResponse;
            return authResponse;
        }

        public authReversalResponse AuthReversal(authReversal reversal)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(reversal);
            request.authReversal = reversal;

            litleOnlineResponse response = sendToLitle(request);
            authReversalResponse reversalResponse = (authReversalResponse)response.authReversalResponse;
            return reversalResponse;
        }

        public captureResponse Capture(capture capture)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(capture);
            request.capture = capture;

            litleOnlineResponse response = sendToLitle(request);
            captureResponse captureResponse = (captureResponse)response.captureResponse;
            return captureResponse;
        }

        public captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(captureGivenAuth);
            request.captureGivenAuth = captureGivenAuth;

            litleOnlineResponse response = sendToLitle(request);
            captureGivenAuthResponse captureGivenAuthResponse = (captureGivenAuthResponse)response.captureGivenAuthResponse;
            return captureGivenAuthResponse;
        }

        public creditResponse Credit(credit credit)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(credit);
            request.credit = credit;

            litleOnlineResponse response = sendToLitle(request);
            creditResponse creditResponse = (creditResponse)response.creditResponse;
            return creditResponse;
        }

        public echeckCreditResponse EcheckCredit(echeckCredit echeckCredit)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(echeckCredit);
            request.echeckCredit = echeckCredit;

            litleOnlineResponse response = sendToLitle(request);
            echeckCreditResponse echeckCreditResponse = (echeckCreditResponse)response.echeckCreditResponse;
            return echeckCreditResponse;
        }

        public echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(echeckRedeposit);
            request.echeckRedeposit = echeckRedeposit;

            litleOnlineResponse response = sendToLitle(request);
            echeckRedepositResponse echeckRedepositResponse = (echeckRedepositResponse)response.echeckRedepositResponse;
            return echeckRedepositResponse;
        }

        public echeckSalesResponse EcheckSale(echeckSale echeckSale)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(echeckSale);
            request.echeckSale = echeckSale;

            litleOnlineResponse response = sendToLitle(request);
            echeckSalesResponse echeckSalesResponse = (echeckSalesResponse)response.echeckSalesResponse;
            return echeckSalesResponse;
        }

        public echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(echeckVerification);
            request.echeckVerification = echeckVerification;

            litleOnlineResponse response = sendToLitle(request);
            echeckVerificationResponse echeckVerificationResponse = (echeckVerificationResponse)response.echeckVerificationResponse;
            return echeckVerificationResponse;
        }

        public forceCaptureResponse ForceCapture(forceCapture forceCapture)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(forceCapture);
            request.forceCapture = forceCapture;

            litleOnlineResponse response = sendToLitle(request);
            forceCaptureResponse forceCaptureResponse = (forceCaptureResponse)response.forceCaptureResponse;
            return forceCaptureResponse;
        }

        public saleResponse Sale(sale sale)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(sale);
            request.sale = sale;

            litleOnlineResponse response = sendToLitle(request);
            saleResponse saleResponse = (saleResponse)response.saleResponse;
            return saleResponse;
        }

        public registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(tokenRequest);
            request.registerTokenRequest = tokenRequest;

            litleOnlineResponse response = sendToLitle(request);
            registerTokenResponse registerTokenResponse = (registerTokenResponse)response.registerTokenResponse;
            return registerTokenResponse;
        }

        public litleOnlineResponseTransactionResponseVoidResponse DoVoid(voidTxn v)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(v);
            request.voidTxn = v;

            litleOnlineResponse response = sendToLitle(request);
            litleOnlineResponseTransactionResponseVoidResponse voidResponse = (litleOnlineResponseTransactionResponseVoidResponse)response.voidResponse;
            return voidResponse;
        }

        public litleOnlineResponseTransactionResponseEcheckVoidResponse EcheckVoid(echeckVoid v)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(v);
            request.echeckVoid = v;

            litleOnlineResponse response = sendToLitle(request);
            litleOnlineResponseTransactionResponseEcheckVoidResponse voidResponse = (litleOnlineResponseTransactionResponseEcheckVoidResponse)response.echeckVoidResponse;
            return voidResponse;
        }

        public updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            fillInReportGroup(updateCardValidationNumOnToken);
            request.updateCardValidationNumOnToken = updateCardValidationNumOnToken;

            litleOnlineResponse response = sendToLitle(request);
            updateCardValidationNumOnTokenResponse updateResponse = (updateCardValidationNumOnTokenResponse)response.updateCardValidationNumOnTokenResponse;
            return updateResponse;
        }

        public cancelSubscriptionResponse CancelSubscription(cancelSubscription cancelSubscription)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.cancelSubscription = cancelSubscription;

            litleOnlineResponse response = sendToLitle(request);
            cancelSubscriptionResponse cancelResponse = (cancelSubscriptionResponse)response.cancelSubscriptionResponse;
            return cancelResponse;
        }

        public updateSubscriptionResponse UpdateSubscription(updateSubscription updateSubscription)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.updateSubscription = updateSubscription;

            litleOnlineResponse response = sendToLitle(request);
            updateSubscriptionResponse updateResponse = (updateSubscriptionResponse)response.updateSubscriptionResponse;
            return updateResponse;
        }

        public activateResponse Activate(activate activate)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.activate = activate;

            litleOnlineResponse response = sendToLitle(request);
            activateResponse activateResponse = response.activateResponse;
            return activateResponse;
        }

        public deactivateResponse Deactivate(deactivate deactivate)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.deactivate = deactivate;

            litleOnlineResponse response = sendToLitle(request);
            deactivateResponse deactivateResponse = response.deactivateResponse;
            return deactivateResponse;
        }

        public loadResponse Load(load load)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.load = load;

            litleOnlineResponse response = sendToLitle(request);
            loadResponse loadResponse = response.loadResponse;
            return loadResponse;
        }

        public unloadResponse Unload(unload unload)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.unload = unload;

            litleOnlineResponse response = sendToLitle(request);
            unloadResponse unloadResponse = response.unloadResponse;
            return unloadResponse;
        }

        public balanceInquiryResponse BalanceInquiry(balanceInquiry balanceInquiry)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.balanceInquiry = balanceInquiry;

            litleOnlineResponse response = sendToLitle(request);
            balanceInquiryResponse balanceInquiryResponse = response.balanceInquiryResponse;
            return balanceInquiryResponse;
        }

        public createPlanResponse CreatePlan(createPlan createPlan)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.createPlan = createPlan;

            litleOnlineResponse response = sendToLitle(request);
            createPlanResponse createPlanResponse = response.createPlanResponse;
            return createPlanResponse;
        }

        public updatePlanResponse UpdatePlan(updatePlan updatePlan)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.updatePlan = updatePlan;

            litleOnlineResponse response = sendToLitle(request);
            updatePlanResponse updatePlanResponse = response.updatePlanResponse;
            return updatePlanResponse;
        }

        public refundReversalResponse RefundReversal(refundReversal refundReversal)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.refundReversal = refundReversal;

            litleOnlineResponse response = sendToLitle(request);
            refundReversalResponse refundReversalResponse = response.refundReversalResponse;
            return refundReversalResponse;
        }

        public depositReversalResponse DepositReversal(depositReversal depositReversal)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.depositReversal = depositReversal;

            litleOnlineResponse response = sendToLitle(request);
            depositReversalResponse depositReversalResponse = response.depositReversalResponse;
            return depositReversalResponse;
        }

        public activateReversalResponse ActivateReversal(activateReversal activateReversal)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.activateReversal = activateReversal;

            litleOnlineResponse response = sendToLitle(request);
            activateReversalResponse activateReversalResponse = response.activateReversalResponse;
            return activateReversalResponse;
        }

        public deactivateReversalResponse DeactivateReversal(deactivateReversal deactivateReversal)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.deactivateReversal = deactivateReversal;

            litleOnlineResponse response = sendToLitle(request);
            deactivateReversalResponse deactivateReversalResponse = response.deactivateReversalResponse;
            return deactivateReversalResponse;
        }

        public loadReversalResponse LoadReversal(loadReversal loadReversal)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.loadReversal = loadReversal;

            litleOnlineResponse response = sendToLitle(request);
            loadReversalResponse loadReversalResponse = response.loadReversalResponse;
            return loadReversalResponse;
        }

        public unloadReversalResponse UnloadReversal(unloadReversal unloadReversal)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.unloadReversal = unloadReversal;

            litleOnlineResponse response = sendToLitle(request);
            unloadReversalResponse unloadReversalResponse = response.unloadReversalResponse;
            return unloadReversalResponse;
        }

        public transactionTypeWithReportGroup queryTransaction(queryTransaction queryTransaction)
        {
            litleOnlineRequest request = createLitleOnlineRequest();
            request.queryTransaction = queryTransaction;

            litleOnlineResponse litleresponse = sendToLitle(request);
            transactionTypeWithReportGroup response = null;
            if (!(litleresponse.queryTransactionResponse == null))
            {
                response = litleresponse.queryTransactionResponse;
            }
            else if (!(litleresponse.queryTransactionUnavailableResponse == null))
            {
                response = litleresponse.queryTransactionUnavailableResponse;
            }
            return response;
        }

        private litleOnlineRequest createLitleOnlineRequest()
        {
            litleOnlineRequest request = new litleOnlineRequest();
            request.merchantId = config["merchantId"];
            request.merchantSdk = "DotNet;10.1";
            authentication authentication = new authentication();
            authentication.password = config["password"];
            authentication.user = config["username"];
            request.authentication = authentication;
            return request;
        }

        private litleOnlineResponse sendToLitle(litleOnlineRequest request)
        {
            string xmlRequest = request.Serialize();
            string xmlResponse = communication.HttpPost(xmlRequest,config);
            try
            {
                litleOnlineResponse litleOnlineResponse = DeserializeObject(xmlResponse);
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
