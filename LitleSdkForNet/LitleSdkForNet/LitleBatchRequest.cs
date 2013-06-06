using System;
using System.Collections.Generic;
using System.Text;

namespace Litle.Sdk
{
    public partial class litleBatchRequest
    {

        public string id;

        public string merchantId;
        public string merchantSdk;

        public int numAccountUpdates;
        public string reportGroup;


        public Dictionary<String, String> config;

        private authentication authentication;

        private List<authorization> listOfAuthorization;
        private List<capture> listOfCapture;
        private List<credit> listOfCredit;
        private List<sale> listOfSale;
        private List<authReversal> listOfAuthReversal;
        private List<echeckCredit> listOfEcheckCredit;
        private List<echeckVerification> listOfEcheckVerification;
        private List<echeckSale> listOfEcheckSale;
        private List<registerTokenRequestType> listOfRegisterTokenRequest;
        private List<forceCapture> listOfForceCapture;
        private List<captureGivenAuth> listOfCaptureGivenAuth;
        private List<echeckRedeposit> listOfEcheckRedeposit;
        private List<updateCardValidationNumOnToken> listOfUpdateCardValidationNumOnToken;

        public litleBatchRequest()
        {
            config = new Dictionary<String, String>();

            authentication = new authentication();

            listOfAuthorization = new List<authorization>();
            listOfCapture = new List<capture>();
            listOfCredit = new List<credit>();
            listOfSale = new List<sale>();
            listOfAuthReversal = new List<authReversal>();
            listOfEcheckCredit = new List<echeckCredit>();
            listOfEcheckVerification = new List<echeckVerification>();
            listOfEcheckSale = new List<echeckSale>();
            listOfRegisterTokenRequest = new List<registerTokenRequestType>();
            listOfForceCapture = new List<forceCapture>();
            listOfCaptureGivenAuth = new List<captureGivenAuth>();
            listOfEcheckRedeposit = new List<echeckRedeposit>();
            listOfUpdateCardValidationNumOnToken = new List<updateCardValidationNumOnToken>();
        }

        public void addAuthorization(authorization authorization)
        {
            listOfAuthorization.Add(authorization);
        }

        public void addCapture(capture capture)
        {
            listOfCapture.Add(capture);
        }

        public void addCredit(credit credit)
        {
            listOfCredit.Add(credit);
        }

        public void addSale(sale sale)
        {
            listOfSale.Add(sale);
        }

        public void addAuthReversal(authReversal authReversal)
        {
            listOfAuthReversal.Add(authReversal);
        }

        public void addEcheckCredit(echeckCredit echeckCredit)
        {
            listOfEcheckCredit.Add(echeckCredit);
        }

        public void addEcheckVerification(echeckVerification echeckVerification)
        {
            listOfEcheckVerification.Add(echeckVerification);
        }

        public void addEcheckSale(echeckSale echeckSale)
        {
            listOfEcheckSale.Add(echeckSale);
        }

        public void addRegisterTokenRequest(registerTokenRequestType registerTokenRequestType)
        {
            listOfRegisterTokenRequest.Add(registerTokenRequestType);
        }

        public void addForceCapture(forceCapture forceCapture)
        {
            listOfForceCapture.Add(forceCapture);
        }

        public void addCaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            listOfCaptureGivenAuth.Add(captureGivenAuth);
        }

        public void addEcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            listOfEcheckRedeposit.Add(echeckRedeposit);
        }

        public void addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            listOfUpdateCardValidationNumOnToken.Add(updateCardValidationNumOnToken);
        }

        public String Serialize()
        {
            string xml = "\r\n<batchRequest " +
                "id=\"" + id + "\"\r\n";

            if (listOfAuthorization != null)
            {
                long sum = 0;

                foreach (authorization a in listOfAuthorization)
                {
                    sum += a.amount;

                }

                xml += "numAuths=\"" + listOfAuthorization.Count + "\"\r\n";
                xml += "authAmount=\"" + sum + "\"\r\n";
            }

            if (listOfAuthReversal != null)
            {
                long sum = 0;

                foreach (authReversal ar in listOfAuthReversal)
                {
                    sum += ar.amount;
                }

                xml += "numAuthReversals=\"" + listOfAuthReversal.Count + "\"\r\n";
                xml += "authReversalAmount=\"" + sum + "\"\r\n";
            }

            if (listOfCapture != null)
            {
                long sum = 0;

                foreach (capture c in listOfCapture)
                {
                    sum += c.amount;
                }

                xml += "numCaptures=\"" + listOfCapture.Count + "\"\r\n";
                xml += "captureAmount=\"" + sum + "\"\r\n";
            }

            if (listOfCredit != null)
            {
                long sum = 0;

                foreach (credit c in listOfCredit)
                {
                    sum += c.amount;
                }

                xml += "numCredits=\"" + listOfCredit.Count + "\"\r\n";
                xml += "creditAmount=\"" + sum + "\"\r\n";
            }
            if (listOfForceCapture != null)
            {
                long sum = 0;

                foreach (forceCapture fc in listOfForceCapture)
                {
                    sum += fc.amount;
                }

                xml += "numForceCaptures=\"" + listOfForceCapture.Count + "\"\r\n";
                xml += "forceCaptureAmount=\"" + sum + "\"\r\n";
            }

            if (listOfSale != null)
            {
                long sum = 0;

                foreach (sale s in listOfSale)
                {
                    sum += s.amount;
                }

                xml += "numSales=\"" + listOfSale.Count + "\"\r\n";
                xml += "saleAmount=\"" + sum + "\"\r\n";
            }

            if (listOfCaptureGivenAuth != null)
            {
                long sum = 0;

                foreach (captureGivenAuth cga in listOfCaptureGivenAuth)
                {
                    sum += cga.amount;
                }

                xml += "numCaptureGivenAuths=\"" + listOfCapture.Count + "\"\r\n";
                xml += "captureGivenAuthAmount=\"" + sum + "\"\r\n";
            }

            if (listOfEcheckSale != null)
            {
                long sum = 0;

                foreach (echeckSale es in listOfEcheckSale)
                {
                    sum += es.amount;
                }

                xml += "numEcheckSales=\"" + listOfEcheckSale.Count + "\"\r\n";
                xml += "echeckSaleAmount=\"" + sum + "\"\r\n";
            }

            if (listOfEcheckCredit != null)
            {
                long sum = 0;

                foreach (echeckCredit ec in listOfEcheckCredit)
                {
                    sum += ec.amount;
                }

                xml += "numEcheckCredit=\"" + listOfEcheckCredit.Count + "\"\r\n";
                xml += "echeckCreditAmount=\"" + sum + "\"\r\n";
            }

            if (listOfEcheckVerification != null)
            {
                long sum = 0;

                foreach (echeckVerification ev in listOfEcheckVerification)
                {
                    sum += ev.amount;
                }

                xml += "numEcheckVerification=\"" + listOfEcheckVerification.Count + "\"\r\n";
                xml += "echeckVerificationAmount=\"" + sum + "\"\r\n";
            }

            if (listOfEcheckRedeposit != null)
            {
                xml += "numEcheckRedeposit=\"" + listOfEcheckRedeposit.Count + "\"\r\n";
            }

            // to ask about accountUpdate
            xml += "numAccountUpdate=\"" + numAccountUpdates + "\"\r\n";

            if (listOfRegisterTokenRequest != null)
            {
                xml += "numTokenRegistrations=\"" + listOfRegisterTokenRequest.Count + "\"\r\n";
            }

            if (listOfUpdateCardValidationNumOnToken != null)
            {
                xml += "numUpdateCardValidationNumOnTokens=\"" + listOfUpdateCardValidationNumOnToken.Count + "\"\r\n";
            }

            xml += "merchantId=\"" + config["merchantId"] + "\"\r\n";
            xml += "merchantSdk=\"" + merchantSdk + "\">\r\n";

            authentication.user = config["username"];
            authentication.password = config["password"];

            xml += authentication.Serialize();

            if (listOfAuthorization != null)
            {
                foreach (authorization a in listOfAuthorization)
                {
                    xml += a.Serialize();
                }
            }

            if (listOfAuthReversal != null)
            {
                foreach (authReversal ar in listOfAuthReversal)
                {
                    xml += ar.Serialize();
                }
            }

            if (listOfCapture != null)
            {
                foreach (capture c in listOfCapture)
                {
                    xml += c.Serialize();
                }
            }

            if (listOfCaptureGivenAuth != null)
            {
                foreach (captureGivenAuth cga in listOfCaptureGivenAuth)
                {
                    xml += cga.Serialize();
                }
            }

            if (listOfCredit != null)
            {
                foreach (credit c in listOfCredit)
                {
                    xml += c.Serialize();
                }
            }

            if (listOfEcheckCredit != null)
            {
                foreach (echeckCredit ec in listOfEcheckCredit)
                {
                    xml += ec.Serialize();
                }
            }

            if (listOfEcheckRedeposit != null)
            {
                foreach (echeckRedeposit er in listOfEcheckRedeposit)
                {
                    xml += er.Serialize();
                }
            }

            if (listOfEcheckSale != null)
            {
                foreach (echeckSale es in listOfEcheckSale)
                {
                    xml += es.Serialize();
                }
            }

            if (listOfEcheckVerification != null)
            {
                foreach (echeckVerification ev in listOfEcheckVerification)
                {
                    xml += ev.Serialize();
                }
            }

            if (listOfForceCapture != null)
            {
                foreach (forceCapture f in listOfForceCapture)
                {
                    xml += f.Serialize();
                }
            }

            if (listOfRegisterTokenRequest != null)
            {
                foreach (registerTokenRequestType r in listOfRegisterTokenRequest)
                {
                    xml += r.Serialize();
                }
            }

            if (listOfSale != null)
            {
                foreach (sale s in listOfSale)
                {
                    xml += s.Serialize();
                }
            }

            if (listOfUpdateCardValidationNumOnToken != null)
            {
                foreach (updateCardValidationNumOnToken u in listOfUpdateCardValidationNumOnToken)
                {
                    xml += u.Serialize();
                }
            }

            xml += "</batchRequest>\r\n";

            return xml;
        }

        public void updateReportGroup()
        {
            foreach (authorization a in listOfAuthorization)
            {
                fillInReportGroup(a);
            }

            foreach (capture c in listOfCapture)
            {
                fillInReportGroup(c);
            }

            foreach (sale s in listOfSale)
            {
                fillInReportGroup(s);
            }

            foreach (authReversal ar in listOfAuthReversal)
            {
                fillInReportGroup(ar);
            }

            foreach (echeckCredit e in listOfEcheckCredit)
            {
                fillInReportGroup(e);
            }

            foreach (echeckVerification e in listOfEcheckVerification)
            {
                fillInReportGroup(e);
            }

            foreach (echeckSale e in listOfEcheckSale)
            {
                fillInReportGroup(e);
            }

            foreach (registerTokenRequestType r in listOfRegisterTokenRequest)
            {
                fillInReportGroup(r);
            }

            foreach (forceCapture f in listOfForceCapture)
            {
                fillInReportGroup(f);
            }

            foreach (captureGivenAuth c in listOfCaptureGivenAuth)
            {
                fillInReportGroup(c);
            }

            foreach (echeckRedeposit e in listOfEcheckRedeposit)
            {
                fillInReportGroup(e);
            }

            foreach (updateCardValidationNumOnToken u in listOfUpdateCardValidationNumOnToken)
            {
                fillInReportGroup(u);
            }
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
