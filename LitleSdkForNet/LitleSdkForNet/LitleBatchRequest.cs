using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Litle.Sdk
{
    public partial class batchRequest
    {
        public string id;
        public string merchantId;
        public string reportGroup;

        public Dictionary<String, String> config;

        public string batchFilePath;
        private string tempBatchFilePath;
        private litleFile litleFile;
        private litleTime litleTime;
        private string requestDirectory;
        private string responseDirectory;

        private int numAuthorization;
        private int numAccountUpdates;
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

        private const string accountUpdateErrorMessage = "Account Updates need to exist in their own batch request!";

        public batchRequest()
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
            config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            config["responseDirectory"] = Properties.Settings.Default.responseDirectory;

            initializeRequest();
        }

        public batchRequest(Dictionary<String, String> config)
        {
            this.config = config;

            initializeRequest();
        }

        private void initializeRequest()
        {
            requestDirectory = config["requestDirectory"] + "\\Requests\\";
            responseDirectory = config["responseDirectory"] + "\\Responses\\";

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

        public string getResponseDirectory()
        {
            return this.responseDirectory;
        }

        public string getRequestDirectory()
        {
            return this.requestDirectory;
        }

        public void setLitleFile(litleFile litleFile)
        {
            this.litleFile = litleFile;
        }

        public litleFile getLitleFile()
        {
            return this.litleFile;
        }

        public void setLitleTime(litleTime litleTime)
        {
            this.litleTime = litleTime;
        }

        public litleTime getLitleTime()
        {
            return this.litleTime;
        }

        public int getNumAuthorization()
        {
            return numAuthorization;
        }

        public int getNumAccountUpdates()
        {
            return numAccountUpdates;
        }

        public int getNumCapture()
        {
            return numCapture;
        }

        public int getNumCredit()
        {
            return numCredit;
        }

        public int getNumSale()
        {
            return numSale;
        }

        public int getNumAuthReversal()
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
            if (numAccountUpdates == 0)
            {
                numAuthorization++;
                sumOfAuthorization += authorization.amount;
                fillInReportGroup(authorization);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, authorization);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCapture(capture capture)
        {
            if (numAccountUpdates == 0)
            {
                numCapture++;
                sumOfCapture += capture.amount;
                fillInReportGroup(capture);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, capture);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCredit(credit credit)
        {
            if (numAccountUpdates == 0)
            {
                numCredit++;
                sumOfCredit += credit.amount;
                fillInReportGroup(credit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, credit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addSale(sale sale)
        {
            if (numAccountUpdates == 0)
            {
                numSale++;
                sumOfSale += sale.amount;
                fillInReportGroup(sale);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, sale);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addAuthReversal(authReversal authReversal)
        {
            if (numAccountUpdates == 0)
            {
                numAuthReversal++;
                sumOfAuthReversal += authReversal.amount;
                fillInReportGroup(authReversal);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, authReversal);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckCredit(echeckCredit echeckCredit)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckCredit++;
                sumOfEcheckCredit += echeckCredit.amount;
                fillInReportGroup(echeckCredit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, echeckCredit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckVerification(echeckVerification echeckVerification)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckVerification++;
                sumOfEcheckVerification += echeckVerification.amount;
                fillInReportGroup(echeckVerification);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, echeckVerification);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckSale(echeckSale echeckSale)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckSale++;
                sumOfEcheckSale += echeckSale.amount;
                fillInReportGroup(echeckSale);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, echeckSale);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addRegisterTokenRequest(registerTokenRequestType registerTokenRequestType)
        {
            if (numAccountUpdates == 0)
            {
                numRegisterTokenRequest++;
                fillInReportGroup(registerTokenRequestType);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, registerTokenRequestType);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addForceCapture(forceCapture forceCapture)
        {
            if (numAccountUpdates == 0)
            {
                numForceCapture++;
                sumOfForceCapture += forceCapture.amount;
                fillInReportGroup(forceCapture);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, forceCapture);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            if (numAccountUpdates == 0)
            {
                numCaptureGivenAuth++;
                sumOfCaptureGivenAuth += captureGivenAuth.amount;
                fillInReportGroup(captureGivenAuth);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, captureGivenAuth);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckRedeposit++;
                fillInReportGroup(echeckRedeposit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, echeckRedeposit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            if (numAccountUpdates == 0)
            {
                numUpdateCardValidationNumOnToken++;
                fillInReportGroup(updateCardValidationNumOnToken);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, updateCardValidationNumOnToken);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addAccountUpdate(accountUpdate accountUpdate)
        {
            if (isOnlyAccountUpdates())
            {
                numAccountUpdates++;
                fillInReportGroup(accountUpdate);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, accountUpdate);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
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

            batchFilePath = litleFile.createRandomFile(requestDirectory, null, "_batchRequest.xml", litleTime);

            litleFile.AppendLineToFile(batchFilePath, xmlHeader);
            litleFile.AppendFileToFile(batchFilePath, tempBatchFilePath);
            litleFile.AppendLineToFile(batchFilePath, xmlFooter);

            tempBatchFilePath = null;

            return batchFilePath;
        }

        private string saveElement(litleFile litleFile, litleTime litleTime, string filePath, transactionType element)
        {
            string fPath;
            fPath = litleFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_batchRequest.xml", litleTime);

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

        private bool isOnlyAccountUpdates()
        {
            bool result = numAuthorization == 0
                && numAuthReversal == 0
                && numCapture == 0
                && numCaptureGivenAuth == 0
                && numCredit == 0
                && numEcheckCredit == 0
                && numEcheckRedeposit == 0
                && numEcheckSale == 0
                && numEcheckVerification == 0
                && numForceCapture == 0
                && numRegisterTokenRequest == 0
                && numSale == 0
                && numUpdateCardValidationNumOnToken == 0;

            return result;
        }
    }

    public class RFRRequest
    {
        public long litleSessionId;
        public accountUpdateFileRequestData accountUpdateFileRequestData;

        private litleTime litleTime;
        private litleFile litleFile;
        private string requestDirectory;
        private string responseDirectory;

        private Dictionary<String, String> config;

        public RFRRequest()
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
            config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            config["responseDirectory"] = Properties.Settings.Default.responseDirectory;

            litleTime = new litleTime();
            litleFile = new litleFile();

            requestDirectory = config["requestDirectory"] + "\\Requests\\";
            responseDirectory = config["responseDirectory"] + "\\Responses\\";
        }

        public RFRRequest(Dictionary<String, String> config)
        {
            this.config = config;

            initializeRequest();
        }

        private void initializeRequest()
        {
            requestDirectory = config["requestDirectory"] + "\\Requests\\";
            responseDirectory = config["responseDirectory"] + "\\Responses\\";

            litleFile = new litleFile();
            litleTime = new litleTime();
        }

        public string getRequestDirectory()
        {
            return this.requestDirectory;
        }

        public string getResponseDirectory()
        {
            return this.responseDirectory;
        }

        public void setConfig(Dictionary<String, String> config)
        {
            this.config = config;
        }

        public void setLitleFile(litleFile litleFile)
        {
            this.litleFile = litleFile;
        }

        public litleFile getLitleFile()
        {
            return this.litleFile;
        }

        public void setLitleTime(litleTime litleTime)
        {
            this.litleTime = litleTime;
        }

        public litleTime getLitleTime()
        {
            return this.litleTime;
        }

        public string Serialize()
        {
            string xmlHeader = "\r\n<RFRRequest xmlns=\"http://www.litle.com/schema\">";
            string xmlFooter = "\r\n</RFRRequest>";

            string filePath = litleFile.createRandomFile(requestDirectory, null, "_RFRRequest.xml", litleTime);

            string xmlBody = "";

            if (accountUpdateFileRequestData != null)
            {
                xmlBody += "\r\n<accountUpdateFileRequestData>";
                xmlBody += accountUpdateFileRequestData.Serialize();
                xmlBody += "\r\n</accountUpdateFileRequestData>";
            }
            else
            {
                xmlBody += "\r\n<litleSessionId>" + litleSessionId + "</litleSessionId>";
            }
            litleFile.AppendLineToFile(filePath, xmlHeader);
            litleFile.AppendLineToFile(filePath, xmlBody);
            litleFile.AppendLineToFile(filePath, xmlFooter);

            return filePath;
        }
    }
}