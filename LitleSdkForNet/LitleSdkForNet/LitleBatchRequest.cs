using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Litle.Sdk
{
    public partial class litleBatchRequest
    {
        public string id;

        public string merchantId;

        //TODO ask what is this
        public int numAccountUpdates;
        public string reportGroup;

        public Dictionary<String, String> config;

        public string batchFilePath;
        private string tempBatchFilePath;
        private litleFile litleFile;
        private litleTime litleTime;

        private int numAuthorization;
        private int numCapture;
        private int numCredit;
        private int numSale;
        private int numAuthReversal;
        private int numEcheckCredit;
        private int numEcheckVerification;
        private int numEcheckSale;
        private int numRegisterTokenRequest;
        private int numForceCapture;
        private int numCaptureGivenAuth;
        private int numEcheckRedeposit;
        private int numUpdateCardValidationNumOnToken;

        private long sumOfAuthorization;
        private long sumOfAuthReversal;
        private long sumOfCapture;
        private long sumOfCredit;
        private long sumOfSale;
        private long sumOfForceCapture;
        private long sumOfEcheckSale;
        private long sumOfEcheckCredit;
        private long sumOfEcheckVerification;
        private long sumOfCaptureGivenAuth;

        public litleBatchRequest()
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
            config["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            config["sftpUsername"] = Properties.Settings.Default.sftpUsername;
            config["sftpPassword"] = Properties.Settings.Default.sftpPassword;
            config["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;

            litleFile = new litleFile();
            litleTime = new litleTime();

            numAuthorization = 0;
            numAuthReversal = 0;
            numCapture = 0;
            numCaptureGivenAuth = 0;
            numCredit = 0;
            numEcheckCredit = 0;
            numEcheckRedeposit = 0;
            numEcheckSale = 0;
            numEcheckVerification = 0;
            numForceCapture = 0;
            numRegisterTokenRequest = 0;
            numSale = 0;
            numUpdateCardValidationNumOnToken = 0;

            sumOfAuthorization = 0;
            sumOfAuthReversal = 0;
            sumOfCapture = 0;
            sumOfCredit = 0;
            sumOfSale = 0;
            sumOfForceCapture = 0;
            sumOfEcheckSale = 0;
            sumOfEcheckCredit = 0;
            sumOfEcheckVerification = 0;
            sumOfCaptureGivenAuth = 0;
        }

        public litleBatchRequest(Dictionary<String, String> config)
        {
            this.config = config;
        }

        public void setLitleFile(litleFile litleFile)
        {
            this.litleFile = litleFile;
        }

        public void setLitleTime(litleTime litleTime)
        {
            this.litleTime = litleTime;
        }

        public int getNumAuthorization()
        {
            return numAuthorization; 
        }

        public int getNumCapture()
        {
            return numCapture;
        }

        public int getNumCredit()
        {
            return numCapture;
        }

        public int getNumSale()
        {
            return numSale;
        }

        public int getAuthReversal()
        {
            return numAuthReversal;
        }

        public int getNumEcheckCredit()
        {
            return numEcheckCredit;
        }

        public int getNumEcheckVerification()
        {
            return numEcheckVerification;
        }

        public int getNumEcheckSale()
        {
            return numEcheckSale;
        }

        public int getNumRegisterTokenRequest()
        {
            return numRegisterTokenRequest;
        }

        public int getNumForceCapture()
        {
            return numForceCapture;
        }

        public int getNumCaptureGivenAuth()
        {
            return numCaptureGivenAuth;
        }

        public int getNumEcheckRedeposit()
        {
            return numEcheckRedeposit;
        }

        public int getNumUpdateCardValidationNumOnToken()
        {
            return numUpdateCardValidationNumOnToken;
        }

        public long getSumOfAuthorization()
        {
            return sumOfAuthorization;
        }

        public long getSumOfAuthReversal()
        {
            return sumOfAuthReversal;
        }

        public long getSumOfCapture()
        {
            return sumOfCapture;
        }

        public long getSumOfCredit()
        {
            return sumOfCredit;
        }

        public long getSumOfSale()
        {
            return sumOfSale;
        }

        public long getSumOfForceCapture()
        {
            return sumOfForceCapture;
        }

        public long getSumOfEcheckSale()
        {
            return sumOfEcheckSale;
        }

        public long getSumOfEcheckCredit()
        {
            return sumOfEcheckCredit;
        }

        public long getSumOfEcheckVerification()
        {
            return sumOfEcheckVerification;
        }

        public long getSumOfCaptureGivenAuth()
        {
            return sumOfCaptureGivenAuth;
        }

        public void addAuthorization(authorization authorization)
        {
            numAuthorization++;
            sumOfAuthorization += authorization.amount;
            fillInReportGroup(authorization);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, authorization);
        }

        public void addCapture(capture capture)
        {
            numCapture++;
            sumOfCapture += capture.amount;
            fillInReportGroup(capture);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, capture);
        }

        public void addCredit(credit credit)
        {
            numCredit++;
            sumOfCredit += credit.amount;
            fillInReportGroup(credit);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, credit);
        }

        public void addSale(sale sale)
        {
            numSale++;
            sumOfSale += sale.amount;
            fillInReportGroup(sale);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, sale);
        }

        public void addAuthReversal(authReversal authReversal)
        {
            numAuthReversal++;
            sumOfAuthReversal += authReversal.amount;
            fillInReportGroup(authReversal);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, authReversal);
        }

        public void addEcheckCredit(echeckCredit echeckCredit)
        {
            numEcheckCredit++;
            sumOfEcheckCredit += echeckCredit.amount;
            fillInReportGroup(echeckCredit);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, echeckCredit);
        }

        public void addEcheckVerification(echeckVerification echeckVerification)
        {
            numEcheckVerification++;
            sumOfEcheckVerification += echeckVerification.amount;
            fillInReportGroup(echeckVerification);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, echeckVerification);
        }

        public void addEcheckSale(echeckSale echeckSale)
        {
            numEcheckSale++;
            sumOfEcheckSale += echeckSale.amount;
            fillInReportGroup(echeckSale);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, echeckSale);
        }

        public void addRegisterTokenRequest(registerTokenRequestType registerTokenRequestType)
        {
            numRegisterTokenRequest++;
            fillInReportGroup(registerTokenRequestType);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, registerTokenRequestType);
        }

        public void addForceCapture(forceCapture forceCapture)
        {
            numForceCapture++;
            sumOfForceCapture += forceCapture.amount;
            fillInReportGroup(forceCapture);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, forceCapture);
        }

        public void addCaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            numCaptureGivenAuth++;
            sumOfCaptureGivenAuth += captureGivenAuth.amount;
            fillInReportGroup(captureGivenAuth);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, captureGivenAuth);
        }

        public void addEcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            numEcheckRedeposit++;
            fillInReportGroup(echeckRedeposit);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, echeckRedeposit);
        }

        public void addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            numUpdateCardValidationNumOnToken++;
            fillInReportGroup(updateCardValidationNumOnToken);
            tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, updateCardValidationNumOnToken);
        }

        public String Serialize()
        {
            string xmlHeader = "\r\n<batchRequest " +
                "id=\"" + id + "\"\r\n";

            if (numAuthorization != 0)
            {
                xmlHeader += "numAuths=\"" + numAuthorization + "\"\r\n";
                xmlHeader += "authAmount=\"" + sumOfAuthorization + "\"\r\n";
            }

            if (numAuthReversal != 0)
            {
                xmlHeader += "numAuthReversals=\"" + numAuthReversal + "\"\r\n";
                xmlHeader += "authReversalAmount=\"" + sumOfAuthReversal + "\"\r\n";
            }

            if (numCapture != 0)
            {
                xmlHeader += "numCaptures=\"" + numCapture + "\"\r\n";
                xmlHeader += "captureAmount=\"" + sumOfCapture + "\"\r\n";
            }

            if (numCredit != 0)
            {

                xmlHeader += "numCredits=\"" + numCredit + "\"\r\n";
                xmlHeader += "creditAmount=\"" + sumOfCredit + "\"\r\n";
            }

            if (numForceCapture != 0)
            {

                xmlHeader += "numForceCaptures=\"" + numForceCapture + "\"\r\n";
                xmlHeader += "forceCaptureAmount=\"" + sumOfForceCapture + "\"\r\n";
            }

            if (numSale != 0)
            {

                xmlHeader += "numSales=\"" + numSale + "\"\r\n";
                xmlHeader += "saleAmount=\"" + sumOfSale + "\"\r\n";
            }

            if (numCaptureGivenAuth != 0)
            {

                xmlHeader += "numCaptureGivenAuths=\"" + numCaptureGivenAuth + "\"\r\n";
                xmlHeader += "captureGivenAuthAmount=\"" + sumOfCaptureGivenAuth + "\"\r\n";
            }

            if (numEcheckSale != 0)
            {

                xmlHeader += "numEcheckSales=\"" + numEcheckSale + "\"\r\n";
                xmlHeader += "echeckSalesAmount=\"" + sumOfEcheckSale + "\"\r\n";
            }

            if (numEcheckCredit != 0)
            {

                xmlHeader += "numEcheckCredit=\"" + numEcheckCredit + "\"\r\n";
                xmlHeader += "echeckCreditAmount=\"" + sumOfEcheckCredit + "\"\r\n";
            }

            if (numEcheckVerification != 0)
            {

                xmlHeader += "numEcheckVerification=\"" + numEcheckVerification + "\"\r\n";
                xmlHeader += "echeckVerificationAmount=\"" + sumOfEcheckVerification + "\"\r\n";
            }

            if (numEcheckRedeposit != 0)
            {
                xmlHeader += "numEcheckRedeposit=\"" + numEcheckRedeposit + "\"\r\n";
            }

            xmlHeader += "numAccountUpdates=\"" + numAccountUpdates + "\"\r\n";

            if (numRegisterTokenRequest != 0)
            {
                xmlHeader += "numTokenRegistrations=\"" + numRegisterTokenRequest + "\"\r\n";
            }

            if (numUpdateCardValidationNumOnToken != 0)
            {
                xmlHeader += "numUpdateCardValidationNumOnTokens=\"" + numUpdateCardValidationNumOnToken + "\"\r\n";
            }

            xmlHeader += "merchantId=\"" + config["merchantId"] + "\">\r\n";

            string xmlFooter = "</batchRequest>\r\n";

            batchFilePath = litleFile.createRandomFile(null, litleTime, "_batchRequest.xml");

            litleFile.AppendLineToFile(batchFilePath, xmlHeader);
            litleFile.AppendFileToFile(batchFilePath, tempBatchFilePath);
            litleFile.AppendLineToFile(batchFilePath, xmlFooter);

            tempBatchFilePath = null;

            return batchFilePath;
        }

        private string saveElement(litleFile litleFile, litleTime litleTime, string filePath, transactionType element)
        {
            string fPath;
            fPath = litleFile.createRandomFile(filePath, litleTime, "_temp_batchRequest.xml");

            litleFile.AppendLineToFile(fPath, element.Serialize());

            return fPath;
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
