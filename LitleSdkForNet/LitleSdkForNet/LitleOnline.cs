using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        public event EventHandler HttpAction
        {
            add { communication.HttpAction += value; }
            remove { communication.HttpAction -= value; }
        }

        public void setCommunication(Communications communication)
        {
            this.communication = communication;
        }

        public Task<authorizationResponse> AuthorizeAsync(authorization auth, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.authorizationResponse, auth, cancellationToken);
        }

        public authorizationResponse Authorize(authorization auth)
        {
            return SendRequest(response => response.authorizationResponse, auth);
        }

        public Task<authReversalResponse> AuthReversalAsync(authReversal reversal, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.authReversalResponse, reversal, cancellationToken);
        }

        public authReversalResponse AuthReversal(authReversal reversal)
        {
            return SendRequest(response => response.authReversalResponse, reversal);
        }

        public Task<captureResponse> CaptureAsync(capture capture, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.captureResponse, capture, cancellationToken);
        }

        public captureResponse Capture(capture capture)
        {
            return SendRequest(response => response.captureResponse, capture);
        }

        public captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            return SendRequest(response => response.captureGivenAuthResponse, captureGivenAuth);
        }

        public Task<captureGivenAuthResponse> CaptureGivenAuthAsync(captureGivenAuth captureGivenAuth, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.captureGivenAuthResponse, captureGivenAuth, cancellationToken);
        }

        public creditResponse Credit(credit credit)
        {
            return SendRequest(response => response.creditResponse, credit);
        }

        public Task<creditResponse> CreditAsync(credit credit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.creditResponse, credit, cancellationToken);
        }

        public Task<echeckCreditResponse> EcheckCreditAsync(echeckCredit echeckCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.echeckCreditResponse, echeckCredit, cancellationToken);
        }

        public echeckCreditResponse EcheckCredit(echeckCredit echeckCredit)
        {
            return SendRequest(response => response.echeckCreditResponse, echeckCredit);
        }

        public echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            return SendRequest(response => response.echeckRedepositResponse, echeckRedeposit);
        }

        public Task<echeckRedepositResponse> EcheckRedepositAsync(echeckRedeposit echeckRedeposit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.echeckRedepositResponse, echeckRedeposit, cancellationToken);
        }

        public Task<echeckSalesResponse> EcheckSaleAsync(echeckSale echeckSale, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.echeckSalesResponse, echeckSale, cancellationToken);
        }

        public echeckSalesResponse EcheckSale(echeckSale echeckSale)
        {
            return SendRequest(response => response.echeckSalesResponse, echeckSale);
        }

        public echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification)
        {
            return SendRequest(response => response.echeckVerificationResponse, echeckVerification);
        }

        public Task<echeckVerificationResponse> EcheckVerificationAsync(echeckVerification echeckVerification, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.echeckVerificationResponse, echeckVerification, cancellationToken);
        }

        public forceCaptureResponse ForceCapture(forceCapture forceCapture)
        {
            return SendRequest(response => response.forceCaptureResponse, forceCapture);
        }

        public Task<forceCaptureResponse> ForceCaptureAsync(forceCapture forceCapture, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.forceCaptureResponse, forceCapture, cancellationToken);
        }

        public saleResponse Sale(sale sale)
        {
            return SendRequest(response => response.saleResponse, sale);
        }

        public Task<saleResponse> SaleAsync(sale sale, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.saleResponse, sale, cancellationToken);
        }

        public Task<registerTokenResponse> RegisterTokenAsync(registerTokenRequestType tokenRequest, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.registerTokenResponse, tokenRequest, cancellationToken);
        }

        public registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest)
        {
            return SendRequest(response => response.registerTokenResponse, tokenRequest);
        }

        public litleOnlineResponseTransactionResponseVoidResponse DoVoid(voidTxn v)
        {
            return SendRequest(response => response.voidResponse, v);
        }

        public Task<litleOnlineResponseTransactionResponseVoidResponse> DoVoidAsync(voidTxn v, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.voidResponse, v, cancellationToken);
        }

        public Task<litleOnlineResponseTransactionResponseEcheckVoidResponse> EcheckVoidAsync(echeckVoid v, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.echeckVoidResponse, v, cancellationToken);
        }

        public litleOnlineResponseTransactionResponseEcheckVoidResponse EcheckVoid(echeckVoid v)
        {
            return SendRequest(response => response.echeckVoidResponse, v);
        }

        public updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            return SendRequest(response => response.updateCardValidationNumOnTokenResponse, updateCardValidationNumOnToken);
        }

        public Task<updateCardValidationNumOnTokenResponse> UpdateCardValidationNumOnTokenAsync(updateCardValidationNumOnToken update, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.updateCardValidationNumOnTokenResponse, update, cancellationToken);
        }

        public cancelSubscriptionResponse CancelSubscription(cancelSubscription cancelSubscription)
        {
            return SendRequest(response => response.cancelSubscriptionResponse, cancelSubscription);
        }

        public updateSubscriptionResponse UpdateSubscription(updateSubscription updateSubscription)
        {
            return SendRequest(response => response.updateSubscriptionResponse, updateSubscription);
        }

        public activateResponse Activate(activate activate)
        {
            return SendRequest(response => response.activateResponse, activate);
        }

        public deactivateResponse Deactivate(deactivate deactivate)
        {
            return SendRequest(response => response.deactivateResponse, deactivate);
        }

        public loadResponse Load(load load)
        {
            return SendRequest(response => response.loadResponse, load);
        }

        public unloadResponse Unload(unload unload)
        {
            return SendRequest(response => response.unloadResponse, unload);
        }

        public Task<balanceInquiryResponse> BalanceInquiryAsync(balanceInquiry balanceInquiry, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.balanceInquiryResponse, balanceInquiry, cancellationToken);
        }

        public balanceInquiryResponse BalanceInquiry(balanceInquiry balanceInquiry)
        {
            return SendRequest(response => response.balanceInquiryResponse, balanceInquiry);
        }

        public createPlanResponse CreatePlan(createPlan createPlan)
        {
            return SendRequest(response => response.createPlanResponse, createPlan);
        }

        public updatePlanResponse UpdatePlan(updatePlan updatePlan)
        {
            return SendRequest(response => response.updatePlanResponse, updatePlan);
        }

        public refundReversalResponse RefundReversal(refundReversal refundReversal)
        {
            return SendRequest(response => response.refundReversalResponse, refundReversal);
        }

        public depositReversalResponse DepositReversal(depositReversal depositReversal)
        {
            return SendRequest(response => response.depositReversalResponse, depositReversal);
        }

        public activateReversalResponse ActivateReversal(activateReversal activateReversal)
        {
            return SendRequest(response => response.activateReversalResponse, activateReversal);
        }

        public deactivateReversalResponse DeactivateReversal(deactivateReversal deactivateReversal)
        {
            return SendRequest(response => response.deactivateReversalResponse, deactivateReversal);
        }

        public loadReversalResponse LoadReversal(loadReversal loadReversal)
        {
            return SendRequest(response => response.loadReversalResponse, loadReversal);
        }

        public unloadReversalResponse UnloadReversal(unloadReversal unloadReversal)
        {
            return SendRequest(response => response.unloadReversalResponse, unloadReversal);
        }

        public fraudCheckResponse FraudCheck(fraudCheck fraudCheck)
        {
            return SendRequest(response => response.fraudCheckResponse, fraudCheck);
        }

        private litleOnlineRequest createLitleOnlineRequest()
        {
            litleOnlineRequest request = new litleOnlineRequest();
            request.merchantId = config["merchantId"];
            request.merchantSdk = "DotNet;9.12.4";
            authentication authentication = new authentication();
            authentication.password = config["password"];
            authentication.user = config["username"];
            request.authentication = authentication;
            return request;
        }

        private async Task<litleOnlineResponse> sendToLitleAsync(litleOnlineRequest request, CancellationToken cancellationToken)
        {
            string xmlRequest = request.Serialize();
            string xmlResponse = await communication.HttpPostAsync(xmlRequest, config, cancellationToken).ConfigureAwait(false);
            return DeserializeResponse(xmlResponse);
        }

        private litleOnlineResponse sendToLitle(litleOnlineRequest request)
        {
            string xmlRequest = request.Serialize();
            string xmlResponse = communication.HttpPost(xmlRequest, config);
            return DeserializeResponse(xmlResponse);
        }

        private T SendRequest<T>(Func<litleOnlineResponse, T> getResponse, transactionRequest transaction)
        {
            var request = CreateRequest(transaction);

            litleOnlineResponse response = sendToLitle(request);
            return getResponse(response);
        }

        private async Task<T> SendRequestAsync<T>(Func<litleOnlineResponse, T> getResponse, transactionRequest transaction, CancellationToken cancellationToken)
        {
            var request = CreateRequest(transaction);

            litleOnlineResponse response = await sendToLitleAsync(request, cancellationToken).ConfigureAwait(false);
            return getResponse(response);
        }

        private litleOnlineRequest CreateRequest(transactionRequest transaction)
        {
            litleOnlineRequest request = createLitleOnlineRequest();

            if (transaction is transactionTypeWithReportGroup txn)
            {
                fillInReportGroup(txn);
            }
            else if (transaction is transactionTypeWithReportGroupAndPartial txnPartial)
            {
                fillInReportGroup(txnPartial);
            }

            switch (transaction)
            {
            case authorization auth:
                request.authorization = auth;
                break;
            case authReversal authReversal:
                request.authReversal = authReversal;
                break;
            case capture capture:
                request.capture = capture;
                break;
            case captureGivenAuth captureGivenAuth:
                request.captureGivenAuth = captureGivenAuth;
                break;
            case credit cred:
                request.credit = cred;
                break;
            case echeckCredit eCred:
                request.echeckCredit = eCred;
                break;
            case echeckRedeposit eCheckRedeposit:
                request.echeckRedeposit = eCheckRedeposit;
                break;
            case echeckSale eSale:
                request.echeckSale = eSale;
                break;
            case echeckVerification eVerify:
                request.echeckVerification = eVerify;
                break;
            case forceCapture fCapture:
                request.forceCapture = fCapture;
                break;
            case sale s:
                request.sale = s;
                break;
            case registerTokenRequestType token:
                request.registerTokenRequest = token;
                break;
            case voidTxn v:
                request.voidTxn = v;
                break;
            case echeckVoid eVoid:
                request.echeckVoid = eVoid;
                break;
            case updateCardValidationNumOnToken updateCard:
                request.updateCardValidationNumOnToken = updateCard;
                break;
            case cancelSubscription cancelSub:
                request.cancelSubscription = cancelSub;
                break;
            case updateSubscription updateSub:
                request.updateSubscription = updateSub;
                break;
            case activate act:
                request.activate = act;
                break;
            case deactivate deAct:
                request.deactivate = deAct;
                break;
            case load l:
                request.load = l;
                break;
            case unload ul:
                request.unload = ul;
                break;
            case balanceInquiry bal:
                request.balanceInquiry = bal;
                break;
            case createPlan cPlan:
                request.createPlan = cPlan;
                break;
            case updatePlan uPlan:
                request.updatePlan = uPlan;
                break;
            case refundReversal refRev:
                request.refundReversal = refRev;
                break;
            case depositReversal depRev:
                request.depositReversal = depRev;
                break;
            case activateReversal actRev:
                request.activateReversal = actRev;
                break;
            case deactivateReversal deRev:
                request.deactivateReversal = deRev;
                break;
            case loadReversal lRev:
                request.loadReversal = lRev;
                break;
            case unloadReversal ulRev:
                request.unloadReversal = ulRev;
                break;
            case fraudCheck fraud:
                request.fraudCheck = fraud;
                break;
            default:
                throw new NotImplementedException("Support for type: " + transaction.GetType().Name + " not implemented.");
            }

            return request;
        }

        private litleOnlineResponse DeserializeResponse(string xmlResponse)
        {
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
            litleOnlineResponse i = (litleOnlineResponse) serializer.Deserialize(reader);
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
        Task<authorizationResponse> AuthorizeAsync(authorization auth, CancellationToken cancellationToken);
        authReversalResponse AuthReversal(authReversal reversal);
        Task<authReversalResponse> AuthReversalAsync(authReversal reversal, CancellationToken cancellationToken);
        captureResponse Capture(capture capture);
        Task<captureResponse> CaptureAsync(capture capture, CancellationToken cancellationToken);
        captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth);
        Task<captureGivenAuthResponse> CaptureGivenAuthAsync(captureGivenAuth captureGivenAuth, CancellationToken cancellationToken);
        creditResponse Credit(credit credit);
        Task<creditResponse> CreditAsync(credit credit, CancellationToken cancellationToken);
        echeckCreditResponse EcheckCredit(echeckCredit echeckCredit);
        Task<echeckCreditResponse> EcheckCreditAsync(echeckCredit echeckCredit, CancellationToken cancellationToken);
        echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit);
        Task<echeckRedepositResponse> EcheckRedepositAsync(echeckRedeposit echeckRedeposit, CancellationToken cancellationToken);
        echeckSalesResponse EcheckSale(echeckSale echeckSale);
        Task<echeckSalesResponse> EcheckSaleAsync(echeckSale echeckSale, CancellationToken cancellationToken);
        echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification);
        Task<echeckVerificationResponse> EcheckVerificationAsync(echeckVerification echeckVerification, CancellationToken cancellationToken);
        forceCaptureResponse ForceCapture(forceCapture forceCapture);
        Task<forceCaptureResponse> ForceCaptureAsync(forceCapture forceCapture, CancellationToken cancellationToken);
        saleResponse Sale(sale sale);
        Task<saleResponse> SaleAsync(sale sale, CancellationToken cancellationToken);
        registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest);
        Task<registerTokenResponse> RegisterTokenAsync(registerTokenRequestType tokenRequest, CancellationToken cancellationToken);
        litleOnlineResponseTransactionResponseVoidResponse DoVoid(voidTxn v);
        Task<litleOnlineResponseTransactionResponseVoidResponse> DoVoidAsync(voidTxn v, CancellationToken cancellationToken);
        litleOnlineResponseTransactionResponseEcheckVoidResponse EcheckVoid(echeckVoid v);
        Task<litleOnlineResponseTransactionResponseEcheckVoidResponse> EcheckVoidAsync(echeckVoid v, CancellationToken cancellationToken);
        updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(updateCardValidationNumOnToken update);
        Task<updateCardValidationNumOnTokenResponse> UpdateCardValidationNumOnTokenAsync(updateCardValidationNumOnToken update, CancellationToken cancellationToken);
    }
}
