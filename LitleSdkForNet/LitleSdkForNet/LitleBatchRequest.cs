using System;
using System.Collections.Generic;
using System.Text;

namespace Litle.Sdk
{
    public partial class litleBatchRequest
    {

        public string merchantId;
        public string merchantSdk;
        public List<batchRequest> listOfBatchRequest;



        public string Serialize()
        {
            string xml = "<?xml version='1.0' encoding='utf-8'?>\r\n<litleRequest merchantId=\"" + merchantId
                + "\" version=\"8.17\" merchantSdk=\"" + merchantSdk + 
                "\" xmlns=\"http://www.litle.com/schema\" " + 
                "numBatchRequests=" + listOfBatchRequest.Count + ">";

            foreach (batchRequest b in listOfBatchRequest)
            {
                xml += b.Serialize();
            }

            xml += "\r\n</litleRequest>";
            return xml;
        }
    }

    public partial class batchRequest
    {
        public string id;
        //public int numAuths;
        //public int authAmount;
        //public int numAuthReversals;
        //public int authReversalAmount;
        //public int numCaptures;
        //public int captureAmount;
        //public int numCredits;
        //public int creditAmount;
        //public int numForceCaptures;
        //public int forceCaptureAmount;
        //public int numSales;
        //public int saleAmount;
        //public int numCaptureGivenAuths;
        //public int captureGivenAuthAmount;
        //public int numEcheckSales;
        //public int echeckSaleAmount;
        //public int numEcheckCredit;
        //public int echeckCreditAmount;
        //public int numEcheckVerification;
        //public int echeckVerificationAmount;
        //public int numEcheckRedeposit;
        public int numAccountUpdates;
        //public int numTokenRegistrations;
        //public int numUpdateCardValidationNumOnTokens;
        public string merchantId;

        public List<authorization> listOfAuthorization;
        public List<capture> listOfCapture;
        public List<credit> listOfCredit;
        public List<sale> listOfSale;
        public List<authReversal> listOfAuthReversal;
        public List<echeckCredit> listOfEcheckCredit;
        public List<echeckVerification> listOfEcheckVerification;
        public List<echeckSale> listOfEcheckSale;
        public List<registerTokenRequestType> listOfRegisterTokenRequest;
        public List<forceCapture> listOfForceCapture;
        public List<captureGivenAuth> listOfCaptureGivenAuth;
        public List<echeckRedeposit> listOfEcheckRedeposit;
        public List<updateCardValidationNumOnToken> listOfUpdateCardValidationNumOnToken;

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

                xml += "numAuths=" + listOfAuthorization.Count + "\r\n";
                xml += "authAmount=" + sum + "\r\n";
            }

            if (listOfAuthReversal != null)
            {
                long sum = 0;

                foreach (authReversal ar in listOfAuthReversal)
                {
                    sum += ar.amount;
                }

                xml += "numAuthReversals=" + listOfAuthReversal.Count + "\r\n";
                xml += "authReversalAmount=" + sum + "\r\n";
            }

            if (listOfCapture != null)
            {
                long sum = 0;

                foreach (capture c in listOfCapture)
                {
                    sum += c.amount;
                }

                xml += "numCaptures=" + listOfCapture.Count + "\r\n";
                xml += "captureAmount=" + sum + "\r\n";
            }

            if (listOfCredit != null)
            {
                long sum = 0;

                foreach (credit c in listOfCredit)
                {
                    sum += c.amount;
                }

                xml += "numCredits=" + listOfCredit.Count + "\r\n";
                xml += "creditAmount=" + sum + "\r\n";
            }
            if (listOfForceCapture != null)
            {
                long sum = 0;

                foreach (forceCapture fc in listOfForceCapture)
                {
                    sum += fc.amount;
                }

                xml += "numForceCaptures=" + listOfForceCapture.Count + "\r\n";
                xml += "forceCaptureAmount=" + sum + "\r\n";
            }

            if (listOfSale != null)
            {
                long sum = 0;

                foreach (sale s in listOfSale)
                {
                    sum += s.amount;
                }

                xml += "numSales=" + listOfSale.Count + "\r\n";
                xml += "saleAmount=" + sum + "\r\n";
            }

            if (listOfCaptureGivenAuth != null)
            {
                long sum = 0;

                foreach (captureGivenAuth cga in listOfCaptureGivenAuth)
                {
                    sum += cga.amount;
                }

                xml += "numCaptureGivenAuths=" + listOfCapture.Count + "\r\n";
                xml += "captureGivenAuthAmount=" + sum + "\r\n";
            }

            if (listOfEcheckSale != null)
            {
                long sum = 0;

                foreach (echeckSale es in listOfEcheckSale)
                {
                    sum += es.amount;
                }

                xml += "numEcheckSales=" + listOfEcheckSale.Count + "\r\n";
                xml += "echeckSaleAmount=" + sum + "\r\n";
            }

            if (listOfEcheckCredit != null)
            {
                long sum = 0;

                foreach (echeckCredit ec in listOfEcheckCredit)
                {
                    sum += ec.amount;
                }

                xml += "numEcheckCredit=" + listOfEcheckCredit.Count + "\r\n";
                xml += "echeckCreditAmount=" + sum + "\r\n";
            }

            if (listOfEcheckVerification != null)
            {
                long sum = 0;

                foreach (echeckVerification ev in listOfEcheckVerification)
                {
                    sum += ev.amount;
                }

                xml += "numEcheckVerification=" + listOfEcheckVerification.Count + "\r\n";
                xml += "echeckVerificationAmount=" + sum + "\r\n";
            }

            if (listOfEcheckRedeposit != null)
            {
                xml += "numEcheckRedeposit=" + listOfEcheckRedeposit.Count + "\r\n";
            }

            // to ask about accountUpdate
            xml += "numAccountUpdate=" + numAccountUpdates + "\r\n";

            if (listOfRegisterTokenRequest != null)
            {
                xml += "numTokenRegistrations=" + listOfRegisterTokenRequest.Count + "\r\n";
            }

            if (listOfUpdateCardValidationNumOnToken != null)
            {
                xml += "numUpdateCardValidationNumOnTokens=" + listOfUpdateCardValidationNumOnToken.Count + "\r\n";
            }

            xml += "merchantId=\"" + merchantId + "\">";

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

            xml += "</batchRequest>";

            return xml;
        }


    }
}
