using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Litle.Sdk
{
    public partial class batchRequest
    {
        public string id;
        public string merchantId;
        public string reportGroup;

        public Dictionary<string, string> config;

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
        private int numEcheckPreNoteSale;
        private int numEcheckPreNoteCredit;
        private int numUpdateCardValidationNumOnToken;
        private int numUpdateSubscriptions;
        private int numCancelSubscriptions;
        private int numCreatePlans;
        private int numUpdatePlans;
        private int numActivates;
        private int numDeactivates;
        private int numLoads;
        private int numUnloads;
        private int numBalanceInquiries;
        private int numPayFacCredit;
        private int numSubmerchantCredit;
        private int numReserveCredit;
        private int numVendorCredit;
        private int numPhysicalCheckCredit;
        private int numPayFacDebit;
        private int numSubmerchantDebit;
        private int numReserveDebit;
        private int numVendorDebit;
        private int numPhysicalCheckDebit;

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
        private long activateAmount;
        private long loadAmount;
        private long unloadAmount;
        private long payFacCreditAmount;
        private long submerchantCreditAmount;
        private long reserveCreditAmount;
        private long vendorCreditAmount;
        private long physicalCheckCreditAmount;
        private long payFacDebitAmount;
        private long submerchantDebitAmount;
        private long reserveDebitAmount;
        private long vendorDebitAmount;
        private long physicalCheckDebitAmount;

        private const string accountUpdateErrorMessage = "Account Updates need to exist in their own batch request!";

        public batchRequest()
        {
            config = new Dictionary<string, string>();

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

        public batchRequest(Dictionary<string, string> config)
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
            numEcheckPreNoteSale = 0;
            numEcheckPreNoteCredit = 0;
            numEcheckSale = 0;
            numEcheckVerification = 0;
            numForceCapture = 0;
            numRegisterTokenRequest = 0;
            numSale = 0;
            numUpdateCardValidationNumOnToken = 0;
            numUpdateSubscriptions = 0;
            numCancelSubscriptions = 0;
            numPayFacCredit = 0;
            numSubmerchantCredit = 0;
            numReserveCredit = 0;
            numVendorCredit = 0;
            numPhysicalCheckCredit = 0;
            numPayFacDebit = 0;
            numSubmerchantDebit = 0;
            numReserveDebit = 0;
            numVendorDebit = 0;
            numPhysicalCheckDebit = 0;

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
            payFacCreditAmount = 0;
            submerchantCreditAmount = 0;
            reserveCreditAmount = 0;
            vendorCreditAmount = 0;
            physicalCheckCreditAmount = 0;
            payFacDebitAmount = 0;
            submerchantDebitAmount = 0;
            reserveDebitAmount = 0;
            vendorDebitAmount = 0;
            physicalCheckDebitAmount = 0;
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

        public int getNumEcheckPreNoteSale()
        {
            return numEcheckPreNoteSale;
        }

        public int getNumEcheckPreNoteCredit()
        {
            return numEcheckPreNoteCredit;
        }

        public int getNumUpdateCardValidationNumOnToken()
        {
            return numUpdateCardValidationNumOnToken;
        }

        public int getNumUpdateSubscriptions()
        {
            return numUpdateSubscriptions;
        }

        public int getNumCancelSubscriptions()
        {
            return numCancelSubscriptions;
        }

        public int getNumCreatePlans()
        {
            return numCreatePlans;
        }

        public int getNumUpdatePlans()
        {
            return numUpdatePlans;
        }

        public int getNumActivates()
        {
            return numActivates;
        }

        public int getNumDeactivates()
        {
            return numDeactivates;
        }

        public int getNumLoads()
        {
            return numLoads;
        }

        public int getNumUnloads()
        {
            return numUnloads;
        }

        public int getNumBalanceInquiries()
        {
            return numBalanceInquiries;
        }

        public int getNumPayFacCredit()
        {
            return numPayFacCredit;
        }

        public int getNumSubmerchantCredit()
        {
            return numSubmerchantCredit;
        }

        public int getNumReserveCredit()
        {
            return numReserveCredit;
        }

        public int getNumVendorCredit()
        {
            return numVendorCredit;
        }

        public int getNumPhysicalCheckCredit()
        {
            return numPhysicalCheckCredit;
        }

        public int getNumPayFacDebit()
        {
            return numPayFacDebit;
        }

        public int getNumSubmerchantDebit()
        {
            return numSubmerchantDebit;
        }

        public int getNumReserveDebit()
        {
            return numReserveDebit;
        }

        public int getNumVendorDebit()
        {
            return numVendorDebit;
        }

        public int getNumPhysicalCheckDebit()
        {
            return numPhysicalCheckDebit;
        }

        public long getLoadAmount()
        {
            return loadAmount;
        }

        public long getUnloadAmount()
        {
            return unloadAmount;
        }

        public long getActivateAmount()
        {
            return activateAmount;
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

        public long getPayFacCreditAmount()
        {
            return payFacCreditAmount;
        }

        public long getSubmerchantCreditAmount()
        {
            return submerchantCreditAmount;
        }

        public long getReserveCreditAmount()
        {
            return reserveCreditAmount;
        }

        public long getVendorCreditAmount()
        {
            return vendorCreditAmount;
        }

        public long getPhysicalCheckCreditAmount()
        {
            return physicalCheckCreditAmount;
        }

        public long getPayFacDebitAmount()
        {
            return payFacDebitAmount;
        }

        public long getSubmerchantDebitAmount()
        {
            return submerchantDebitAmount;
        }

        public long getReserveDebitAmount()
        {
            return reserveDebitAmount;
        }

        public long getVendorDebitAmount()
        {
            return vendorDebitAmount;
        }

        public long getPhysicalCheckDebitAmount()
        {
            return physicalCheckDebitAmount;
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

        public void addEcheckPreNoteSale(echeckPreNoteSale echeckPreNoteSale)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckPreNoteSale++;
                fillInReportGroup(echeckPreNoteSale);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, echeckPreNoteSale);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckPreNoteCredit(echeckPreNoteCredit echeckPreNoteCredit)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckPreNoteCredit++;
                fillInReportGroup(echeckPreNoteCredit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, echeckPreNoteCredit);
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

        public void addUpdateSubscription(updateSubscription updateSubscription)
        {
            if (numAccountUpdates == 0)
            {
                numUpdateSubscriptions++;
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, updateSubscription);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCancelSubscription(cancelSubscription cancelSubscription)
        {
            if (numAccountUpdates == 0)
            {
                numCancelSubscriptions++;
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, cancelSubscription);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCreatePlan(createPlan createPlan)
        {
            if (numAccountUpdates == 0)
            {
                numCreatePlans++;
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, createPlan);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addUpdatePlan(updatePlan updatePlan)
        {
            if (numAccountUpdates == 0)
            {
                numUpdatePlans++;
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, updatePlan);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addActivate(activate activate)
        {
            if (numAccountUpdates == 0)
            {
                numActivates++;
                activateAmount += activate.amount;
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, activate);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addDeactivate(deactivate deactivate)
        {
            if (numAccountUpdates == 0)
            {
                numDeactivates++;
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, deactivate);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addLoad(load load)
        {
            if (numAccountUpdates == 0)
            {
                numLoads++;
                loadAmount += load.amount;
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, load);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addUnload(unload unload)
        {
            if (numAccountUpdates == 0)
            {
                numUnloads++;
                unloadAmount += unload.amount;
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, unload);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addBalanceInquiry(balanceInquiry balanceInquiry)
        {
            if (numAccountUpdates == 0)
            {
                numBalanceInquiries++;
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, balanceInquiry);
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

        public void addSubmerchantCredit(submerchantCredit submerchantCredit)
        {
            if (numAccountUpdates == 0)
            {
                numSubmerchantCredit++;
                submerchantCreditAmount += (long)submerchantCredit.amount;
                fillInReportGroup(submerchantCredit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, submerchantCredit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addPayFacCredit(payFacCredit payFacCredit)
        {
            if (numAccountUpdates == 0)
            {
                numPayFacCredit++;
                payFacCreditAmount += (long)payFacCredit.amount;
                fillInReportGroup(payFacCredit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, payFacCredit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addReserveCredit(reserveCredit reserveCredit)
        {
            if (numAccountUpdates == 0)
            {
                numReserveCredit++;
                reserveCreditAmount += (long)reserveCredit.amount;
                fillInReportGroup(reserveCredit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, reserveCredit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addVendorCredit(vendorCredit vendorCredit)
        {
            if (numAccountUpdates == 0)
            {
                numVendorCredit++;
                vendorCreditAmount += (long)vendorCredit.amount;
                fillInReportGroup(vendorCredit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, vendorCredit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addPhysicalCheckCredit(physicalCheckCredit physicalCheckCredit)
        {
            if (numAccountUpdates == 0)
            {
                numPhysicalCheckCredit++;
                physicalCheckCreditAmount += (long)physicalCheckCredit.amount;
                fillInReportGroup(physicalCheckCredit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, physicalCheckCredit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addSubmerchantDebit(submerchantDebit submerchantDebit)
        {
            if (numAccountUpdates == 0)
            {
                numSubmerchantDebit++;
                submerchantDebitAmount += (long)submerchantDebit.amount;
                fillInReportGroup(submerchantDebit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, submerchantDebit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addPayFacDebit(payFacDebit payFacDebit)
        {
            if (numAccountUpdates == 0)
            {
                numPayFacDebit++;
                payFacDebitAmount += (long)payFacDebit.amount;
                fillInReportGroup(payFacDebit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, payFacDebit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addReserveDebit(reserveDebit reserveDebit)
        {
            if (numAccountUpdates == 0)
            {
                numReserveDebit++;
                reserveDebitAmount += (long)reserveDebit.amount;
                fillInReportGroup(reserveDebit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, reserveDebit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addVendorDebit(vendorDebit vendorDebit)
        {
            if (numAccountUpdates == 0)
            {
                numVendorDebit++;
                vendorDebitAmount += (long)vendorDebit.amount;
                fillInReportGroup(vendorDebit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, vendorDebit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addPhysicalCheckDebit(physicalCheckDebit physicalCheckDebit)
        {
            if (numAccountUpdates == 0)
            {
                numPhysicalCheckDebit++;
                physicalCheckDebitAmount += (long)physicalCheckDebit.amount;
                fillInReportGroup(physicalCheckDebit);
                tempBatchFilePath = saveElement(litleFile, litleTime, tempBatchFilePath, physicalCheckDebit);
            }
            else
            {
                throw new LitleOnlineException(accountUpdateErrorMessage);
            }
        }

        public string Serialize()
        {
            var xmlHeader = generateXmlHeader();
            var xmlFooter = generateXmlFooter();

            batchFilePath = litleFile.createRandomFile(requestDirectory, null, "_batchRequest.xml", litleTime);

            litleFile.AppendLineToFile(batchFilePath, xmlHeader);
            litleFile.AppendFileToFile(batchFilePath, tempBatchFilePath);
            litleFile.AppendLineToFile(batchFilePath, xmlFooter);

            tempBatchFilePath = null;

            return batchFilePath;
        }

        public string generateXmlHeader()
        {
            string xmlHeader = "\r\n<batchRequest id=\"" + id + "\"\r\n";

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

            if (numEcheckPreNoteSale != 0)
            {
                xmlHeader += "numEcheckPreNoteSale=\"" + numEcheckPreNoteSale + "\"\r\n";
            }

            if (numEcheckPreNoteCredit != 0)
            {
                xmlHeader += "numEcheckPreNoteCredit=\"" + numEcheckPreNoteCredit + "\"\r\n";
            }

            if (numAccountUpdates != 0)
            {
                xmlHeader += "numAccountUpdates=\"" + numAccountUpdates + "\"\r\n";
            }

            if (numRegisterTokenRequest != 0)
            {
                xmlHeader += "numTokenRegistrations=\"" + numRegisterTokenRequest + "\"\r\n";
            }

            if (numUpdateCardValidationNumOnToken != 0)
            {
                xmlHeader += "numUpdateCardValidationNumOnTokens=\"" + numUpdateCardValidationNumOnToken + "\"\r\n";
            }

            if (numUpdateSubscriptions != 0)
            {
                xmlHeader += "numUpdateSubscriptions=\"" + numUpdateSubscriptions + "\"\r\n";
            }

            if (numCancelSubscriptions != 0)
            {
                xmlHeader += "numCancelSubscriptions=\"" + numCancelSubscriptions + "\"\r\n";
            }

            if (numCreatePlans != 0)
            {
                xmlHeader += "numCreatePlans=\"" + numCreatePlans + "\"\r\n";
            }

            if (numUpdatePlans != 0)
            {
                xmlHeader += "numUpdatePlans=\"" + numUpdatePlans + "\"\r\n";
            }

            if (numActivates != 0)
            {
                xmlHeader += "numUpdateActivates=\"" + numActivates + "\"\r\n";
                xmlHeader += "activateAmount=\"" + activateAmount + "\"\r\n";
            }

            if (numDeactivates != 0)
            {
                xmlHeader += "numDeactivates=\"" + numDeactivates + "\"\r\n";
            }

            if (numLoads != 0)
            {
                xmlHeader += "numLoads=\"" + numLoads + "\"\r\n";
                xmlHeader += "loadAmount=\"" + loadAmount + "\"\r\n";
            }

            if (numUnloads != 0)
            {
                xmlHeader += "numUnloads=\"" + numUnloads + "\"\r\n";
                xmlHeader += "unloadAmount=\"" + unloadAmount + "\"\r\n";
            }

            if (numBalanceInquiries != 0)
            {
                xmlHeader += "numBalanceInquirys=\"" + numBalanceInquiries + "\"\r\n";
            }

            if (numPayFacCredit != 0)
            {

                xmlHeader += "numPayFacCredit=\"" + numPayFacCredit + "\"\r\n";
                xmlHeader += "payFacCreditAmount=\"" + payFacCreditAmount + "\"\r\n";
            }

            if (numSubmerchantCredit != 0)
            {

                xmlHeader += "numSubmerchantCredit=\"" + numSubmerchantCredit + "\"\r\n";
                xmlHeader += "submerchantCreditAmount=\"" + submerchantCreditAmount + "\"\r\n";
            }

            if (numReserveCredit != 0)
            {

                xmlHeader += "numReserveCredit=\"" + numReserveCredit + "\"\r\n";
                xmlHeader += "reserveCreditAmount=\"" + reserveCreditAmount + "\"\r\n";
            }

            if (numVendorCredit != 0)
            {

                xmlHeader += "numVendorCredit=\"" + numVendorCredit + "\"\r\n";
                xmlHeader += "vendorCreditAmount=\"" + vendorCreditAmount + "\"\r\n";
            }

            if (numPhysicalCheckCredit != 0)
            {

                xmlHeader += "numPhysicalCheckCredit=\"" + numPhysicalCheckCredit + "\"\r\n";
                xmlHeader += "physicalCheckCreditAmount=\"" + physicalCheckCreditAmount + "\"\r\n";
            }

            if (numPayFacDebit != 0)
            {

                xmlHeader += "numPayFacDebit=\"" + numPayFacDebit + "\"\r\n";
                xmlHeader += "payFacDebitAmount=\"" + payFacDebitAmount + "\"\r\n";
            }

            if (numSubmerchantDebit != 0)
            {

                xmlHeader += "numSubmerchantDebit=\"" + numSubmerchantDebit + "\"\r\n";
                xmlHeader += "submerchantDebitAmount=\"" + submerchantDebitAmount + "\"\r\n";
            }

            if (numReserveDebit != 0)
            {

                xmlHeader += "numReserveDebit=\"" + numReserveDebit + "\"\r\n";
                xmlHeader += "reserveDebitAmount=\"" + reserveDebitAmount + "\"\r\n";
            }

            if (numVendorDebit != 0)
            {

                xmlHeader += "numVendorDebit=\"" + numVendorDebit + "\"\r\n";
                xmlHeader += "vendorDebitAmount=\"" + vendorDebitAmount + "\"\r\n";
            }

            if (numPhysicalCheckDebit != 0)
            {

                xmlHeader += "numPhysicalCheckDebit=\"" + numPhysicalCheckDebit + "\"\r\n";
                xmlHeader += "physicalCheckDebitAmount=\"" + physicalCheckDebitAmount + "\"\r\n";
            }

            xmlHeader += "merchantSdk=\"DotNet;9.14\"\r\n";

            xmlHeader += "merchantId=\"" + config["merchantId"] + "\">\r\n";
            return xmlHeader;
        }

        public string generateXmlFooter()
        {
            const string xmlFooter = "</batchRequest>\r\n";

            return xmlFooter;
        }

        private string saveElement(litleFile litleFile, litleTime litleTime, string filePath, transactionRequest element)
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
                && numCapture == 0
                && numCredit == 0
                && numSale == 0
                && numAuthReversal == 0
                && numEcheckCredit == 0
                && numEcheckVerification == 0
                && numEcheckSale == 0
                && numRegisterTokenRequest == 0
                && numForceCapture == 0
                && numCaptureGivenAuth == 0
                && numEcheckRedeposit == 0
                && numEcheckPreNoteSale == 0
                && numEcheckPreNoteCredit == 0
                && numUpdateCardValidationNumOnToken == 0
                && numUpdateSubscriptions == 0
                && numCancelSubscriptions == 0
                && numCreatePlans == 0
                && numUpdatePlans == 0
                && numActivates == 0
                && numDeactivates == 0
                && numLoads == 0
                && numUnloads == 0
                && numBalanceInquiries == 0
                && numPayFacCredit == 0
                && numSubmerchantCredit == 0
                && numReserveCredit == 0
                && numVendorCredit == 0
                && numPhysicalCheckCredit == 0
                && numPayFacDebit == 0
                && numSubmerchantDebit == 0
                && numReserveDebit == 0
                && numVendorDebit == 0
                && numPhysicalCheckDebit == 0;

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

        private Dictionary<string, string> config;

        public RFRRequest()
        {
            config = new Dictionary<string, string>();

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

        public RFRRequest(Dictionary<string, string> config)
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

        public void setConfig(Dictionary<string, string> config)
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
    
    
    [System.Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public class batchResponse
    {
        public string id;
        public long litleBatchId;
        public string merchantId;

        private XmlReader originalXmlReader;
        private XmlReader accountUpdateResponseReader;
        private XmlReader authorizationResponseReader;
        private XmlReader authReversalResponseReader;
        private XmlReader captureResponseReader;
        private XmlReader captureGivenAuthResponseReader;
        private XmlReader creditResponseReader;
        private XmlReader forceCaptureResponseReader;
        private XmlReader echeckCreditResponseReader;
        private XmlReader echeckRedepositResponseReader;
        private XmlReader echeckSalesResponseReader;
        private XmlReader echeckVerificationResponseReader;
        private XmlReader saleResponseReader;
        private XmlReader registerTokenResponseReader;
        private XmlReader updateCardValidationNumOnTokenResponseReader;
        private XmlReader cancelSubscriptionResponseReader;
        private XmlReader updateSubscriptionResponseReader;
        private XmlReader createPlanResponseReader;
        private XmlReader updatePlanResponseReader;
        private XmlReader activateResponseReader;
        private XmlReader deactivateResponseReader;
        private XmlReader loadResponseReader;
        private XmlReader echeckPreNoteSaleResponseReader;
        private XmlReader echeckPreNoteCreditResponseReader;
        private XmlReader unloadResponseReader;
        private XmlReader balanceInquiryResponseReader;
        private XmlReader submerchantCreditResponseReader;
        private XmlReader payFacCreditResponseReader;
        private XmlReader vendorCreditResponseReader;
        private XmlReader reserveCreditResponseReader;
        private XmlReader physicalCheckCreditResponseReader;
        private XmlReader submerchantDebitResponseReader;
        private XmlReader payFacDebitResponseReader;
        private XmlReader vendorDebitResponseReader;
        private XmlReader reserveDebitResponseReader;
        private XmlReader physicalCheckDebitResponseReader;

        public batchResponse()
        {
        }

        public batchResponse(XmlReader xmlReader, string filePath)
        {
            readXml(xmlReader, filePath);
        }

        public void setAccountUpdateResponseReader(XmlReader xmlReader)
        {
            this.accountUpdateResponseReader = xmlReader;
        }

        public void setAuthorizationResponseReader(XmlReader xmlReader)
        {
            this.authorizationResponseReader = xmlReader;
        }

        public void setAuthReversalResponseReader(XmlReader xmlReader)
        {
            this.authReversalResponseReader = xmlReader;
        }

        public void setCaptureResponseReader(XmlReader xmlReader)
        {
            this.captureResponseReader = xmlReader;
        }

        public void setCaptureGivenAuthResponseReader(XmlReader xmlReader)
        {
            this.captureGivenAuthResponseReader = xmlReader;
        }

        public void setCreditResponseReader(XmlReader xmlReader)
        {
            this.creditResponseReader = xmlReader;
        }

        public void setForceCaptureResponseReader(XmlReader xmlReader)
        {
            this.forceCaptureResponseReader = xmlReader;
        }

        public void setEcheckCreditResponseReader(XmlReader xmlReader)
        {
            this.echeckCreditResponseReader = xmlReader;
        }

        public void setEcheckRedepositResponseReader(XmlReader xmlReader)
        {
            this.echeckRedepositResponseReader = xmlReader;
        }

        public void setEcheckSalesResponseReader(XmlReader xmlReader)
        {
            this.echeckSalesResponseReader = xmlReader;
        }

        public void setEcheckVerificationResponseReader(XmlReader xmlReader)
        {
            this.echeckVerificationResponseReader = xmlReader;
        }

        public void setSaleResponseReader(XmlReader xmlReader)
        {
            this.saleResponseReader = xmlReader;
        }

        public void setRegisterTokenResponseReader(XmlReader xmlReader)
        {
            this.registerTokenResponseReader = xmlReader;
        }

        public void setUpdateCardValidationNumOnTokenResponseReader(XmlReader xmlReader)
        {
            this.updateCardValidationNumOnTokenResponseReader = xmlReader;
        }

        public void setCancelSubscriptionResponseReader(XmlReader xmlReader)
        {
            this.cancelSubscriptionResponseReader = xmlReader;
        }

        public void setUpdateSubscriptionResponseReader(XmlReader xmlReader)
        {
            this.updateSubscriptionResponseReader = xmlReader;
        }

        public void setCreatePlanResponseReader(XmlReader xmlReader)
        {
            this.createPlanResponseReader = xmlReader;
        }

        public void setUpdatePlanResponseReader(XmlReader xmlReader)
        {
            this.updatePlanResponseReader = xmlReader;
        }

        public void setActivateResponseReader(XmlReader xmlReader)
        {
            this.activateResponseReader = xmlReader;
        }

        public void setDeactivateResponseReader(XmlReader xmlReader)
        {
            this.deactivateResponseReader = xmlReader;
        }

        public void setLoadResponseReader(XmlReader xmlReader)
        {
            this.loadResponseReader = xmlReader;
        }

        public void setEcheckPreNoteSaleResponseReader(XmlReader xmlReader)
        {
            this.echeckPreNoteSaleResponseReader = xmlReader;
        }

        public void setEcheckPreNoteCreditResponseReader(XmlReader xmlReader)
        {
            this.echeckPreNoteCreditResponseReader = xmlReader;
        }

        public void setUnloadResponseReader(XmlReader xmlReader)
        {
            this.unloadResponseReader = xmlReader;
        }

        public void setBalanceInquiryResponseReader(XmlReader xmlReader)
        {
            this.balanceInquiryResponseReader = xmlReader;
        }

        public void setSubmerchantCreditResponseReader(XmlReader xmlReader)
        {
            this.submerchantCreditResponseReader = xmlReader;
        }

        public void setPayFacCreditResponseReader(XmlReader xmlReader)
        {
            this.payFacCreditResponseReader = xmlReader;
        }

        public void setReserveCreditResponseReader(XmlReader xmlReader)
        {
            this.reserveCreditResponseReader = xmlReader;
        }

        public void setVendorCreditResponseReader(XmlReader xmlReader)
        {
            this.vendorCreditResponseReader = xmlReader;
        }

        public void setPhysicalCheckCreditResponseReader(XmlReader xmlReader)
        {
            this.physicalCheckCreditResponseReader = xmlReader;
        }

        public void setSubmerchantDebitResponseReader(XmlReader xmlReader)
        {
            this.submerchantDebitResponseReader = xmlReader;
        }

        public void setPayFacDebitResponseReader(XmlReader xmlReader)
        {
            this.payFacDebitResponseReader = xmlReader;
        }

        public void setReserveDebitResponseReader(XmlReader xmlReader)
        {
            this.reserveDebitResponseReader = xmlReader;
        }

        public void setVendorDebitResponseReader(XmlReader xmlReader)
        {
            this.vendorDebitResponseReader = xmlReader;
        }

        public void setPhysicalCheckDebitResponseReader(XmlReader xmlReader)
        {
            this.physicalCheckDebitResponseReader = xmlReader;
        }


        public void readXml(XmlReader reader, string filePath)
        {
            id = reader.GetAttribute("id");
            litleBatchId = Int64.Parse(reader.GetAttribute("litleBatchId"));
            merchantId = reader.GetAttribute("merchantId");

            originalXmlReader = reader;
            accountUpdateResponseReader = new XmlTextReader(filePath);
            authorizationResponseReader = new XmlTextReader(filePath);
            authReversalResponseReader = new XmlTextReader(filePath);
            captureResponseReader = new XmlTextReader(filePath);
            captureGivenAuthResponseReader = new XmlTextReader(filePath);
            creditResponseReader = new XmlTextReader(filePath);
            forceCaptureResponseReader = new XmlTextReader(filePath);
            echeckCreditResponseReader = new XmlTextReader(filePath);
            echeckRedepositResponseReader = new XmlTextReader(filePath);
            echeckSalesResponseReader = new XmlTextReader(filePath);
            echeckVerificationResponseReader = new XmlTextReader(filePath);
            saleResponseReader = new XmlTextReader(filePath);
            registerTokenResponseReader = new XmlTextReader(filePath);
            updateCardValidationNumOnTokenResponseReader = new XmlTextReader(filePath);
            cancelSubscriptionResponseReader = new XmlTextReader(filePath);
            updateSubscriptionResponseReader = new XmlTextReader(filePath);
            createPlanResponseReader = new XmlTextReader(filePath);
            updatePlanResponseReader = new XmlTextReader(filePath);
            activateResponseReader = new XmlTextReader(filePath);
            deactivateResponseReader = new XmlTextReader(filePath);
            loadResponseReader = new XmlTextReader(filePath);
            echeckPreNoteSaleResponseReader = new XmlTextReader(filePath);
            echeckPreNoteCreditResponseReader = new XmlTextReader(filePath);
            unloadResponseReader = new XmlTextReader(filePath);
            balanceInquiryResponseReader = new XmlTextReader(filePath);
            submerchantCreditResponseReader = new XmlTextReader(filePath);
            payFacCreditResponseReader = new XmlTextReader(filePath);
            reserveCreditResponseReader = new XmlTextReader(filePath);
            vendorCreditResponseReader = new XmlTextReader(filePath);
            physicalCheckCreditResponseReader = new XmlTextReader(filePath);
            submerchantDebitResponseReader = new XmlTextReader(filePath);
            payFacDebitResponseReader = new XmlTextReader(filePath);
            reserveDebitResponseReader = new XmlTextReader(filePath);
            vendorDebitResponseReader = new XmlTextReader(filePath);
            physicalCheckDebitResponseReader = new XmlTextReader(filePath);

            if (!accountUpdateResponseReader.ReadToFollowing("accountUpdateResponse"))
            {
                accountUpdateResponseReader.Close();
            }
            if (!authorizationResponseReader.ReadToFollowing("authorizationResponse"))
            {
                authorizationResponseReader.Close();
            }
            if (!authReversalResponseReader.ReadToFollowing("authReversalResponse"))
            {
                authReversalResponseReader.Close();
            }
            if (!captureResponseReader.ReadToFollowing("captureResponse"))
            {
                captureResponseReader.Close();
            }
            if (!captureGivenAuthResponseReader.ReadToFollowing("captureGivenAuthResponse"))
            {
                captureGivenAuthResponseReader.Close();
            }
            if (!creditResponseReader.ReadToFollowing("creditResponse"))
            {
                creditResponseReader.Close();
            }
            if (!forceCaptureResponseReader.ReadToFollowing("forceCaptureResponse"))
            {
                forceCaptureResponseReader.Close();
            }
            if (!echeckCreditResponseReader.ReadToFollowing("echeckCreditResponse"))
            {
                echeckCreditResponseReader.Close();
            }
            if (!echeckRedepositResponseReader.ReadToFollowing("echeckRedepositResponse"))
            {
                echeckRedepositResponseReader.Close();
            }
            if (!echeckSalesResponseReader.ReadToFollowing("echeckSalesResponse"))
            {
                echeckSalesResponseReader.Close();
            }
            if (!echeckVerificationResponseReader.ReadToFollowing("echeckVerificationResponse"))
            {
                echeckVerificationResponseReader.Close();
            }
            if (!saleResponseReader.ReadToFollowing("saleResponse"))
            {
                saleResponseReader.Close();
            }
            if (!registerTokenResponseReader.ReadToFollowing("registerTokenResponse"))
            {
                registerTokenResponseReader.Close();
            }
            if (!updateCardValidationNumOnTokenResponseReader.ReadToFollowing("updateCardValidationNumOnTokenResponse"))
            {
                updateCardValidationNumOnTokenResponseReader.Close();
            }
            if (!cancelSubscriptionResponseReader.ReadToFollowing("cancelSubscriptionResponse"))
            {
                cancelSubscriptionResponseReader.Close();
            }
            if (!updateSubscriptionResponseReader.ReadToFollowing("updateSubscriptionResponse"))
            {
                updateSubscriptionResponseReader.Close();
            }
            if (!createPlanResponseReader.ReadToFollowing("createPlanResponse"))
            {
                createPlanResponseReader.Close();
            }
            if (!updatePlanResponseReader.ReadToFollowing("updatePlanResponse"))
            {
                updatePlanResponseReader.Close();
            }
            if (!activateResponseReader.ReadToFollowing("activateResponse"))
            {
                activateResponseReader.Close();
            }
            if (!loadResponseReader.ReadToFollowing("loadResponse"))
            {
                loadResponseReader.Close();
            }
            if (!deactivateResponseReader.ReadToFollowing("deactivateResponse"))
            {
                deactivateResponseReader.Close();
            }
            if (!echeckPreNoteSaleResponseReader.ReadToFollowing("echeckPreNoteSaleResponse"))
            {
                echeckPreNoteSaleResponseReader.Close();
            }
            if (!echeckPreNoteCreditResponseReader.ReadToFollowing("echeckPreNoteCreditResponse"))
            {
                echeckPreNoteCreditResponseReader.Close();
            } if (!unloadResponseReader.ReadToFollowing("unloadResponse"))
            {
                unloadResponseReader.Close();
            }
            if (!balanceInquiryResponseReader.ReadToFollowing("balanceInquiryResponse"))
            {
                balanceInquiryResponseReader.Close();
            }
            if (!submerchantCreditResponseReader.ReadToFollowing("submerchantCreditResponse"))
            {
                submerchantCreditResponseReader.Close();
            }
            if (!payFacCreditResponseReader.ReadToFollowing("payFacCreditResponse"))
            {
                payFacCreditResponseReader.Close();
            }
            if (!vendorCreditResponseReader.ReadToFollowing("vendorCreditResponse"))
            {
                vendorCreditResponseReader.Close();
            }
            if (!reserveCreditResponseReader.ReadToFollowing("reserveCreditResponse"))
            {
                reserveCreditResponseReader.Close();
            }
            if (!physicalCheckCreditResponseReader.ReadToFollowing("physicalCheckCreditResponse"))
            {
                physicalCheckCreditResponseReader.Close();
            }
            if (!submerchantDebitResponseReader.ReadToFollowing("submerchantDebitResponse"))
            {
                submerchantDebitResponseReader.Close();
            }
            if (!payFacDebitResponseReader.ReadToFollowing("payFacDebitResponse"))
            {
                payFacDebitResponseReader.Close();
            }
            if (!vendorDebitResponseReader.ReadToFollowing("vendorDebitResponse"))
            {
                vendorDebitResponseReader.Close();
            }
            if (!reserveDebitResponseReader.ReadToFollowing("reserveDebitResponse"))
            {
                reserveDebitResponseReader.Close();
            }
            if (!physicalCheckDebitResponseReader.ReadToFollowing("physicalCheckDebitResponse"))
            {
                physicalCheckDebitResponseReader.Close();
            }
        }

        virtual public accountUpdateResponse nextAccountUpdateResponse()
        {
            if (accountUpdateResponseReader.ReadState != ReadState.Closed)
            {
                string response = accountUpdateResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(accountUpdateResponse));
                StringReader reader = new StringReader(response);
                accountUpdateResponse i = (accountUpdateResponse)serializer.Deserialize(reader);

                if (!accountUpdateResponseReader.ReadToFollowing("accountUpdateResponse"))
                {
                    accountUpdateResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public authorizationResponse nextAuthorizationResponse()
        {
            if (authorizationResponseReader.ReadState != ReadState.Closed)
            {
                string response = authorizationResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(authorizationResponse));
                StringReader reader = new StringReader(response);
                authorizationResponse i = (authorizationResponse)serializer.Deserialize(reader);

                if (!authorizationResponseReader.ReadToFollowing("authorizationResponse"))
                {
                    authorizationResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public authReversalResponse nextAuthReversalResponse()
        {
            if (authReversalResponseReader.ReadState != ReadState.Closed)
            {
                string response = authReversalResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(authReversalResponse));
                StringReader reader = new StringReader(response);
                authReversalResponse i = (authReversalResponse)serializer.Deserialize(reader);

                if (!authReversalResponseReader.ReadToFollowing("authReversalResponse"))
                {
                    authReversalResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public captureResponse nextCaptureResponse()
        {
            if (captureResponseReader.ReadState != ReadState.Closed)
            {
                string response = captureResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(captureResponse));
                StringReader reader = new StringReader(response);
                captureResponse i = (captureResponse)serializer.Deserialize(reader);

                if (!captureResponseReader.ReadToFollowing("captureResponse"))
                {
                    captureResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public captureGivenAuthResponse nextCaptureGivenAuthResponse()
        {
            if (captureGivenAuthResponseReader.ReadState != ReadState.Closed)
            {
                string response = captureGivenAuthResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(captureGivenAuthResponse));
                StringReader reader = new StringReader(response);
                captureGivenAuthResponse i = (captureGivenAuthResponse)serializer.Deserialize(reader);

                if (!captureGivenAuthResponseReader.ReadToFollowing("captureGivenAuthResponse"))
                {
                    captureGivenAuthResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public creditResponse nextCreditResponse()
        {
            if (creditResponseReader.ReadState != ReadState.Closed)
            {
                string response = creditResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(creditResponse));
                StringReader reader = new StringReader(response);
                creditResponse i = (creditResponse)serializer.Deserialize(reader);

                if (!creditResponseReader.ReadToFollowing("creditResponse"))
                {
                    creditResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public echeckCreditResponse nextEcheckCreditResponse()
        {
            if (echeckCreditResponseReader.ReadState != ReadState.Closed)
            {
                string response = echeckCreditResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(echeckCreditResponse));
                StringReader reader = new StringReader(response);
                echeckCreditResponse i = (echeckCreditResponse)serializer.Deserialize(reader);

                if (!echeckCreditResponseReader.ReadToFollowing("echeckCreditResponse"))
                {
                    echeckCreditResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public echeckRedepositResponse nextEcheckRedepositResponse()
        {
            if (echeckRedepositResponseReader.ReadState != ReadState.Closed)
            {
                string response = echeckRedepositResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(echeckRedepositResponse));
                StringReader reader = new StringReader(response);
                echeckRedepositResponse i = (echeckRedepositResponse)serializer.Deserialize(reader);

                if (!echeckRedepositResponseReader.ReadToFollowing("echeckRedepositResponse"))
                {
                    echeckRedepositResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public echeckSalesResponse nextEcheckSalesResponse()
        {
            if (echeckSalesResponseReader.ReadState != ReadState.Closed)
            {
                string response = echeckSalesResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(echeckSalesResponse));
                StringReader reader = new StringReader(response);
                echeckSalesResponse i = (echeckSalesResponse)serializer.Deserialize(reader);

                if (!echeckSalesResponseReader.ReadToFollowing("echeckSalesResponse"))
                {
                    echeckSalesResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public echeckVerificationResponse nextEcheckVerificationResponse()
        {
            if (echeckVerificationResponseReader.ReadState != ReadState.Closed)
            {
                string response = echeckVerificationResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(echeckVerificationResponse));
                StringReader reader = new StringReader(response);
                echeckVerificationResponse i = (echeckVerificationResponse)serializer.Deserialize(reader);

                if (!echeckVerificationResponseReader.ReadToFollowing("echeckVerificationResponse"))
                {
                    echeckVerificationResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public forceCaptureResponse nextForceCaptureResponse()
        {
            if (forceCaptureResponseReader.ReadState != ReadState.Closed)
            {
                string response = forceCaptureResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(forceCaptureResponse));
                StringReader reader = new StringReader(response);
                forceCaptureResponse i = (forceCaptureResponse)serializer.Deserialize(reader);

                if (!forceCaptureResponseReader.ReadToFollowing("forceCaptureResponse"))
                {
                    forceCaptureResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public registerTokenResponse nextRegisterTokenResponse()
        {
            if (registerTokenResponseReader.ReadState != ReadState.Closed)
            {
                string response = registerTokenResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(registerTokenResponse));
                StringReader reader = new StringReader(response);
                registerTokenResponse i = (registerTokenResponse)serializer.Deserialize(reader);

                if (!registerTokenResponseReader.ReadToFollowing("registerTokenResponse"))
                {
                    registerTokenResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public saleResponse nextSaleResponse()
        {
            if (saleResponseReader.ReadState != ReadState.Closed)
            {
                string response = saleResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(saleResponse));
                StringReader reader = new StringReader(response);
                saleResponse i = (saleResponse)serializer.Deserialize(reader);

                if (!saleResponseReader.ReadToFollowing("saleResponse"))
                {
                    saleResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public updateCardValidationNumOnTokenResponse nextUpdateCardValidationNumOnTokenResponse()
        {
            if (updateCardValidationNumOnTokenResponseReader.ReadState != ReadState.Closed)
            {
                string response = updateCardValidationNumOnTokenResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(updateCardValidationNumOnTokenResponse));
                StringReader reader = new StringReader(response);
                updateCardValidationNumOnTokenResponse i = (updateCardValidationNumOnTokenResponse)serializer.Deserialize(reader);

                if (!updateCardValidationNumOnTokenResponseReader.ReadToFollowing("updateCardValidationNumOnTokenResponse"))
                {
                    updateCardValidationNumOnTokenResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public updateSubscriptionResponse nextUpdateSubscriptionResponse()
        {
            if (updateSubscriptionResponseReader.ReadState != ReadState.Closed)
            {
                string response = updateSubscriptionResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(updateSubscriptionResponse));
                StringReader reader = new StringReader(response);
                updateSubscriptionResponse i = (updateSubscriptionResponse)serializer.Deserialize(reader);

                if (!updateSubscriptionResponseReader.ReadToFollowing("updateSubscriptionResponse"))
                {
                    updateSubscriptionResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public cancelSubscriptionResponse nextCancelSubscriptionResponse()
        {
            if (cancelSubscriptionResponseReader.ReadState != ReadState.Closed)
            {
                string response = cancelSubscriptionResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(cancelSubscriptionResponse));
                StringReader reader = new StringReader(response);
                cancelSubscriptionResponse i = (cancelSubscriptionResponse)serializer.Deserialize(reader);

                if (!cancelSubscriptionResponseReader.ReadToFollowing("cancelSubscriptionResponse"))
                {
                    cancelSubscriptionResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public createPlanResponse nextCreatePlanResponse()
        {
            if (createPlanResponseReader.ReadState != ReadState.Closed)
            {
                string response = createPlanResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(createPlanResponse));
                StringReader reader = new StringReader(response);
                createPlanResponse i = (createPlanResponse)serializer.Deserialize(reader);

                if (!createPlanResponseReader.ReadToFollowing("createPlanResponse"))
                {
                    createPlanResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public updatePlanResponse nextUpdatePlanResponse()
        {
            if (updatePlanResponseReader.ReadState != ReadState.Closed)
            {
                string response = updatePlanResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(updatePlanResponse));
                StringReader reader = new StringReader(response);
                updatePlanResponse i = (updatePlanResponse)serializer.Deserialize(reader);

                if (!updatePlanResponseReader.ReadToFollowing("updatePlanResponse"))
                {
                    updatePlanResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public activateResponse nextActivateResponse()
        {
            if (activateResponseReader.ReadState != ReadState.Closed)
            {
                string response = activateResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(activateResponse));
                StringReader reader = new StringReader(response);
                activateResponse i = (activateResponse)serializer.Deserialize(reader);

                if (!activateResponseReader.ReadToFollowing("activateResponse"))
                {
                    activateResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public deactivateResponse nextDeactivateResponse()
        {
            if (deactivateResponseReader.ReadState != ReadState.Closed)
            {
                string response = deactivateResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(deactivateResponse));
                StringReader reader = new StringReader(response);
                deactivateResponse i = (deactivateResponse)serializer.Deserialize(reader);

                if (!deactivateResponseReader.ReadToFollowing("deactivateResponse"))
                {
                    deactivateResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public echeckPreNoteSaleResponse nextEcheckPreNoteSaleResponse()
        {
            if (echeckPreNoteSaleResponseReader.ReadState != ReadState.Closed)
            {
                string response = echeckPreNoteSaleResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(echeckPreNoteSaleResponse));
                StringReader reader = new StringReader(response);
                echeckPreNoteSaleResponse i = (echeckPreNoteSaleResponse)serializer.Deserialize(reader);

                if (!echeckPreNoteSaleResponseReader.ReadToFollowing("echeckPreNoteSaleResponse"))
                {
                    echeckPreNoteSaleResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public echeckPreNoteCreditResponse nextEcheckPreNoteCreditResponse()
        {
            if (echeckPreNoteCreditResponseReader.ReadState != ReadState.Closed)
            {
                string response = echeckPreNoteCreditResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(echeckPreNoteCreditResponse));
                StringReader reader = new StringReader(response);
                echeckPreNoteCreditResponse i = (echeckPreNoteCreditResponse)serializer.Deserialize(reader);

                if (!echeckPreNoteCreditResponseReader.ReadToFollowing("echeckPreNoteCreditResponse"))
                {
                    echeckPreNoteCreditResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public loadResponse nextLoadResponse()
        {
            if (loadResponseReader.ReadState != ReadState.Closed)
            {
                string response = loadResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(loadResponse));
                StringReader reader = new StringReader(response);
                loadResponse i = (loadResponse)serializer.Deserialize(reader);

                if (!loadResponseReader.ReadToFollowing("loadResponse"))
                {
                    loadResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public unloadResponse nextUnloadResponse()
        {
            if (unloadResponseReader.ReadState != ReadState.Closed)
            {
                string response = unloadResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(unloadResponse));
                StringReader reader = new StringReader(response);
                unloadResponse i = (unloadResponse)serializer.Deserialize(reader);

                if (!unloadResponseReader.ReadToFollowing("unloadResponse"))
                {
                    unloadResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public balanceInquiryResponse nextBalanceInquiryResponse()
        {
            if (balanceInquiryResponseReader.ReadState != ReadState.Closed)
            {
                string response = balanceInquiryResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(balanceInquiryResponse));
                StringReader reader = new StringReader(response);
                balanceInquiryResponse i = (balanceInquiryResponse)serializer.Deserialize(reader);

                if (!balanceInquiryResponseReader.ReadToFollowing("balanceInquiryResponse"))
                {
                    balanceInquiryResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public submerchantCreditResponse nextSubmerchantCreditResponse()
        {
            if (submerchantCreditResponseReader.ReadState != ReadState.Closed)
            {
                string response = submerchantCreditResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(submerchantCreditResponse));
                StringReader reader = new StringReader(response);
                submerchantCreditResponse i = (submerchantCreditResponse)serializer.Deserialize(reader);

                if (!submerchantCreditResponseReader.ReadToFollowing("submerchantCreditResponse"))
                {
                    submerchantCreditResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public payFacCreditResponse nextPayFacCreditResponse()
        {
            if (payFacCreditResponseReader.ReadState != ReadState.Closed)
            {
                string response = payFacCreditResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(payFacCreditResponse));
                StringReader reader = new StringReader(response);
                payFacCreditResponse i = (payFacCreditResponse)serializer.Deserialize(reader);

                if (!payFacCreditResponseReader.ReadToFollowing("payFacCreditResponse"))
                {
                    payFacCreditResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public vendorCreditResponse nextVendorCreditResponse()
        {
            if (vendorCreditResponseReader.ReadState != ReadState.Closed)
            {
                string response = vendorCreditResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(vendorCreditResponse));
                StringReader reader = new StringReader(response);
                vendorCreditResponse i = (vendorCreditResponse)serializer.Deserialize(reader);

                if (!vendorCreditResponseReader.ReadToFollowing("vendorCreditResponse"))
                {
                    vendorCreditResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public reserveCreditResponse nextReserveCreditResponse()
        {
            if (reserveCreditResponseReader.ReadState != ReadState.Closed)
            {
                string response = reserveCreditResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(reserveCreditResponse));
                StringReader reader = new StringReader(response);
                reserveCreditResponse i = (reserveCreditResponse)serializer.Deserialize(reader);

                if (!reserveCreditResponseReader.ReadToFollowing("reserveCreditResponse"))
                {
                    reserveCreditResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public physicalCheckCreditResponse nextPhysicalCheckCreditResponse()
        {
            if (physicalCheckCreditResponseReader.ReadState != ReadState.Closed)
            {
                string response = physicalCheckCreditResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(physicalCheckCreditResponse));
                StringReader reader = new StringReader(response);
                physicalCheckCreditResponse i = (physicalCheckCreditResponse)serializer.Deserialize(reader);

                if (!physicalCheckCreditResponseReader.ReadToFollowing("physicalCheckCreditResponse"))
                {
                    physicalCheckCreditResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public submerchantDebitResponse nextSubmerchantDebitResponse()
        {
            if (submerchantDebitResponseReader.ReadState != ReadState.Closed)
            {
                string response = submerchantDebitResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(submerchantDebitResponse));
                StringReader reader = new StringReader(response);
                submerchantDebitResponse i = (submerchantDebitResponse)serializer.Deserialize(reader);

                if (!submerchantDebitResponseReader.ReadToFollowing("submerchantDebitResponse"))
                {
                    submerchantDebitResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public payFacDebitResponse nextPayFacDebitResponse()
        {
            if (payFacDebitResponseReader.ReadState != ReadState.Closed)
            {
                string response = payFacDebitResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(payFacDebitResponse));
                StringReader reader = new StringReader(response);
                payFacDebitResponse i = (payFacDebitResponse)serializer.Deserialize(reader);

                if (!payFacDebitResponseReader.ReadToFollowing("payFacDebitResponse"))
                {
                    payFacDebitResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public vendorDebitResponse nextVendorDebitResponse()
        {
            if (vendorDebitResponseReader.ReadState != ReadState.Closed)
            {
                string response = vendorDebitResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(vendorDebitResponse));
                StringReader reader = new StringReader(response);
                vendorDebitResponse i = (vendorDebitResponse)serializer.Deserialize(reader);

                if (!vendorDebitResponseReader.ReadToFollowing("vendorDebitResponse"))
                {
                    vendorDebitResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public reserveDebitResponse nextReserveDebitResponse()
        {
            if (reserveDebitResponseReader.ReadState != ReadState.Closed)
            {
                string response = reserveDebitResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(reserveDebitResponse));
                StringReader reader = new StringReader(response);
                reserveDebitResponse i = (reserveDebitResponse)serializer.Deserialize(reader);

                if (!reserveDebitResponseReader.ReadToFollowing("reserveDebitResponse"))
                {
                    reserveDebitResponseReader.Close();
                }

                return i;
            }

            return null;
        }

        virtual public physicalCheckDebitResponse nextPhysicalCheckDebitResponse()
        {
            if (physicalCheckDebitResponseReader.ReadState != ReadState.Closed)
            {
                string response = physicalCheckDebitResponseReader.ReadOuterXml();
                XmlSerializer serializer = new XmlSerializer(typeof(physicalCheckDebitResponse));
                StringReader reader = new StringReader(response);
                physicalCheckDebitResponse i = (physicalCheckDebitResponse)serializer.Deserialize(reader);

                if (!physicalCheckDebitResponseReader.ReadToFollowing("physicalCheckDebitResponse"))
                {
                    physicalCheckDebitResponseReader.Close();
                }

                return i;
            }

            return null;
        }
    
    }



    [System.Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public class RFRResponse
    {
        [XmlAttribute]
        public string response;
        [XmlAttribute]
        public string message;
    }
}