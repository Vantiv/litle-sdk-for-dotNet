using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security;

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

            xmlHeader += "merchantSdk=\"DotNet;9.12.3\"\r\n";

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

    public partial class echeckPreNoteCredit : transactionTypeWithReportGroup
    {

        private string orderIdField;

        private orderSourceType orderSourceField;

        private contact billToAddressField;

        private echeckType echeckField;

        private merchantDataType merchantDataField;

        /// <remarks/>
        public string orderId
        {
            get
            {
                return this.orderIdField;
            }
            set
            {
                this.orderIdField = value;
            }
        }

        /// <remarks/>
        public orderSourceType orderSource
        {
            get
            {
                return this.orderSourceField;
            }
            set
            {
                this.orderSourceField = value;
            }
        }

        /// <remarks/>
        public contact billToAddress
        {
            get
            {
                return this.billToAddressField;
            }
            set
            {
                this.billToAddressField = value;
            }
        }

        /// <remarks/>
        public echeckType echeck
        {
            get
            {
                return this.echeckField;
            }
            set
            {
                this.echeckField = value;
            }
        }

        /// <remarks/>
        public merchantDataType merchantData
        {
            get
            {
                return this.merchantDataField;
            }
            set
            {
                this.merchantDataField = value;
            }
        }

        public override string Serialize()
        {
            string xml = "\r\n<echeckPreNoteCredit ";

            if (id != null)
            {
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            }
            if (customerId != null)
            {
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            }
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";

            if (orderSource != null)
            {
                xml += "\r\n<orderSource>";
                xml += orderSource.Serialize();
                xml += "</orderSource>";
            }

            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>";
                xml += billToAddress.Serialize();
                xml += "\r\n</billToAddress>";
            }

            if (echeck != null)
            {
                xml += "\r\n<echeck>";
                xml += echeck.Serialize();
                xml += "\r\n</echeck>";
            }

            if (merchantData != null)
            {
                xml += "\r\n<merchantData>";
                xml += merchantData.Serialize();
                xml += "\r\n</merchantData>";
            }

            xml += "\r\n</echeckPreNoteCredit>";

            return xml;
        }
    }

    public partial class echeckPreNoteSale : transactionTypeWithReportGroup
    {

        private string orderIdField;

        private orderSourceType orderSourceField;

        private contact billToAddressField;

        private echeckType echeckField;

        private merchantDataType merchantDataField;

        /// <remarks/>
        public string orderId
        {
            get
            {
                return this.orderIdField;
            }
            set
            {
                this.orderIdField = value;
            }
        }

        /// <remarks/>
        public orderSourceType orderSource
        {
            get
            {
                return this.orderSourceField;
            }
            set
            {
                this.orderSourceField = value;
            }
        }

        /// <remarks/>
        public contact billToAddress
        {
            get
            {
                return this.billToAddressField;
            }
            set
            {
                this.billToAddressField = value;
            }
        }

        /// <remarks/>
        public echeckType echeck
        {
            get
            {
                return this.echeckField;
            }
            set
            {
                this.echeckField = value;
            }
        }

        /// <remarks/>
        public merchantDataType merchantData
        {
            get
            {
                return this.merchantDataField;
            }
            set
            {
                this.merchantDataField = value;
            }
        }

        public override string Serialize()
        {
            string xml = "\r\n<echeckPreNoteSale ";

            if (id != null)
            {
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            }
            if (customerId != null)
            {
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            }
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";

            if (orderSource != null)
            {
                xml += "\r\n<orderSource>";
                xml += orderSource.Serialize();
                xml += "</orderSource>";
            }

            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>";
                xml += billToAddress.Serialize();
                xml += "\r\n</billToAddress>";
            }

            if (echeck != null)
            {
                xml += "\r\n<echeck>";
                xml += echeck.Serialize();
                xml += "\r\n</echeck>";
            }

            if (merchantData != null)
            {
                xml += "\r\n<merchantData>";
                xml += merchantData.Serialize();
                xml += "\r\n</merchantData>";
            }

            xml += "\r\n</echeckPreNoteSale>";

            return xml;
        }
    }

    public partial class submerchantCredit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string submerchantName { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public echeckType accountInfo { get; set; }

        public override string Serialize()
        {
            string xml = "\r\n<submerchantCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (submerchantName != null)
                xml += "\r\n<submerchantName>" + SecurityElement.Escape(submerchantName) + "</submerchantName>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            if (accountInfo != null)
            {
                xml += "\r\n<accountInfo>";
                xml += accountInfo.Serialize();
                xml += "</accountInfo>";
            }

            xml += "\r\n</submerchantCredit>";

            return xml;
        }
    }

    public partial class payFacCredit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            string xml = "\r\n<payFacCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</payFacCredit>";

            return xml;
        }
    }

    public partial class reserveCredit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            string xml = "\r\n<reserveCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</reserveCredit>";

            return xml;
        }
    }

    public partial class vendorCredit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string vendorName { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public echeckType accountInfo { get; set; }

        public override string Serialize()
        {
            string xml = "\r\n<vendorCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (vendorName != null)
                xml += "\r\n<vendorName>" + SecurityElement.Escape(vendorName) + "</vendorName>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            if (accountInfo != null)
            {
                xml += "\r\n<accountInfo>";
                xml += accountInfo.Serialize();
                xml += "</accountInfo>";
            }

            xml += "\r\n</vendorCredit>";

            return xml;
        }
    }

    public partial class physicalCheckCredit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            string xml = "\r\n<physicalCheckCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</physicalCheckCredit>";

            return xml;
        }
    }

    public partial class submerchantDebit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string submerchantName { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public echeckType accountInfo { get; set; }

        public override string Serialize()
        {
            string xml = "\r\n<submerchantDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (submerchantName != null)
                xml += "\r\n<submerchantName>" + SecurityElement.Escape(submerchantName) + "</submerchantName>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            if (accountInfo != null)
            {
                xml += "\r\n<accountInfo>";
                xml += accountInfo.Serialize();
                xml += "</accountInfo>";
            }

            xml += "\r\n</submerchantDebit>";

            return xml;
        }
    }

    public partial class payFacDebit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            string xml = "\r\n<payFacDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</payFacDebit>";

            return xml;
        }
    }

    public partial class reserveDebit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            string xml = "\r\n<reserveDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</reserveDebit>";

            return xml;
        }
    }

    public partial class vendorDebit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string vendorName { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public echeckType accountInfo { get; set; }

        public override string Serialize()
        {
            string xml = "\r\n<vendorDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (vendorName != null)
                xml += "\r\n<vendorName>" + SecurityElement.Escape(vendorName) + "</vendorName>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            if (accountInfo != null)
            {
                xml += "\r\n<accountInfo>";
                xml += accountInfo.Serialize();
                xml += "</accountInfo>";
            }

            xml += "\r\n</vendorDebit>";

            return xml;
        }
    }

    public partial class physicalCheckDebit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            string xml = "\r\n<physicalCheckDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</physicalCheckDebit>";

            return xml;
        }
    }
}