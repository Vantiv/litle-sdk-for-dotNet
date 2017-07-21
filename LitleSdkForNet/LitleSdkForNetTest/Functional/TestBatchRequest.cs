using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using System.IO;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestBatchRequest
    {
        private litleRequest litle;
        private Dictionary<string, string> invalidConfig;
        private Dictionary<string, string> invalidSftpConfig;

        [SetUp]
        public void setUpBeforeTest()
        {
            litle = new litleRequest();
        }

        [Test]
        public void SimpleBatch()
        {
            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.SameDayFunding(true);
            litleBatchRequest.id = "123";

            var payFacCredit = new payFacCredit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            litleBatchRequest.addPayFacCredit(payFacCredit);

            var payFacDebit = new payFacDebit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            litleBatchRequest.addPayFacDebit(payFacDebit);

            var submerchantCredit = new submerchantCredit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                submerchantName = "Test",
                fundsTransferId = "123467",
                amount = 107L,
                accountInfo = new echeckType
                {
                    accType = echeckAccountTypeEnum.Corporate,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123456789",
                    ccdPaymentInformation = "description"
                },
                customIdentifier = "abc123"
            };
            litleBatchRequest.addSubmerchantCredit(submerchantCredit);

            var submerchantDebit = new submerchantDebit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                submerchantName = "Test",
                fundsTransferId = "123467",
                amount = 107L,
                accountInfo = new echeckType
                {
                    accType = echeckAccountTypeEnum.Corporate,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123456789",
                    ccdPaymentInformation = "description"
                },
                customIdentifier = "abc123"
            };
            litleBatchRequest.addSubmerchantDebit(submerchantDebit);

            var reserveCredit = new reserveCredit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            litleBatchRequest.addReserveCredit(reserveCredit);

            var reserveDebit = new reserveDebit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            litleBatchRequest.addReserveDebit(reserveDebit);

            var vendorCredit = new vendorCredit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                vendorName = "Test",
                fundsTransferId = "123467",
                amount = 107L,
                accountInfo = new echeckType
                {
                    accType = echeckAccountTypeEnum.Corporate,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123456789",
                    ccdPaymentInformation = "description"
                }
            };
            litleBatchRequest.addVendorCredit(vendorCredit);

            var vendorDebit = new vendorDebit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                vendorName = "Test",
                fundsTransferId = "123467",
                amount = 107L,
                accountInfo = new echeckType
                {
                    accType = echeckAccountTypeEnum.Corporate,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123456789",
                    ccdPaymentInformation = "description"
                }
            };
            litleBatchRequest.addVendorDebit(vendorDebit);

            var physicalCheckCredit = new physicalCheckCredit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            litleBatchRequest.addPhysicalCheckCredit(physicalCheckCredit);

            var physicalCheckDebit = new physicalCheckDebit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            litleBatchRequest.addPhysicalCheckDebit(physicalCheckDebit);

            litle.addBatch(litleBatchRequest);

            string batchName = litle.sendToLitle();

            litle.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            litleResponse litleResponse = litle.receiveFromLitle(batchName);

            Assert.NotNull(litleResponse);
            Assert.AreEqual("0", litleResponse.response);
            Assert.AreEqual("Valid Format", litleResponse.message);

            batchResponse litleBatchResponse = litleResponse.nextBatchResponse();
            while (litleBatchResponse != null)
            {
                payFacCreditResponse payFacCreditResponse = litleBatchResponse.nextPayFacCreditResponse();
                while (payFacCreditResponse != null)
                {
                    Assert.AreEqual("000", payFacCreditResponse.response);

                    payFacCreditResponse = litleBatchResponse.nextPayFacCreditResponse();
                }

                payFacDebitResponse payFacDebitResponse = litleBatchResponse.nextPayFacDebitResponse();
                while (payFacDebitResponse != null)
                {
                    Assert.AreEqual("000", payFacDebitResponse.response);

                    payFacDebitResponse = litleBatchResponse.nextPayFacDebitResponse();
                }

                submerchantCreditResponse submerchantCreditResponse = litleBatchResponse.nextSubmerchantCreditResponse();
                while (submerchantCreditResponse != null)
                {
                    Assert.AreEqual("000", submerchantCreditResponse.response);

                    submerchantCreditResponse = litleBatchResponse.nextSubmerchantCreditResponse();
                }

                submerchantDebitResponse submerchantDebitResponse = litleBatchResponse.nextSubmerchantDebitResponse();
                while (submerchantDebitResponse != null)
                {
                    Assert.AreEqual("000", submerchantDebitResponse.response);

                    submerchantDebitResponse = litleBatchResponse.nextSubmerchantDebitResponse();
                }

                reserveCreditResponse reserveCreditResponse = litleBatchResponse.nextReserveCreditResponse();
                while (reserveCreditResponse != null)
                {
                    Assert.AreEqual("000", reserveCreditResponse.response);

                    reserveCreditResponse = litleBatchResponse.nextReserveCreditResponse();
                }

                reserveDebitResponse reserveDebitResponse = litleBatchResponse.nextReserveDebitResponse();
                while (reserveDebitResponse != null)
                {
                    Assert.AreEqual("000", reserveDebitResponse.response);

                    reserveDebitResponse = litleBatchResponse.nextReserveDebitResponse();
                }

                vendorCreditResponse vendorCreditResponse = litleBatchResponse.nextVendorCreditResponse();
                while (vendorCreditResponse != null)
                {
                    Assert.AreEqual("000", vendorCreditResponse.response);

                    vendorCreditResponse = litleBatchResponse.nextVendorCreditResponse();
                }

                vendorDebitResponse vendorDebitResponse = litleBatchResponse.nextVendorDebitResponse();
                while (vendorDebitResponse != null)
                {
                    Assert.AreEqual("000", vendorDebitResponse.response);

                    vendorDebitResponse = litleBatchResponse.nextVendorDebitResponse();
                }

                physicalCheckCreditResponse physicalCheckCreditResponse = litleBatchResponse.nextPhysicalCheckCreditResponse();
                while (physicalCheckCreditResponse != null)
                {
                    Assert.AreEqual("000", physicalCheckCreditResponse.response);

                    physicalCheckCreditResponse = litleBatchResponse.nextPhysicalCheckCreditResponse();
                }

                physicalCheckDebitResponse physicalCheckDebitResponse = litleBatchResponse.nextPhysicalCheckDebitResponse();
                while (physicalCheckDebitResponse != null)
                {
                    Assert.AreEqual("000", physicalCheckDebitResponse.response);

                    physicalCheckDebitResponse = litleBatchResponse.nextPhysicalCheckDebitResponse();
                }

                litleBatchResponse = litleResponse.nextBatchResponse();
            }
        }
        private int estimatedResponseTime(int numAuthsAndSales, int numRest)
        {
            return (int)(5 * 60 * 1000 + 2.5 * 1000 + numAuthsAndSales * (1 / 5) * 1000 + numRest * (1 / 50) * 1000) * 5;
        }
    }
}