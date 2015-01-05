using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestClass]
    class TestEcheckVerification
    {
        
        private LitleOnline litle;

        [TestInitialize]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [TestMethod]
        public void TestMerchantData()
        {
            echeckVerification echeckVerification = new echeckVerification();
            echeckVerification.orderId = "1";
            echeckVerification.amount = 2;
            echeckVerification.orderSource = orderSourceType.ecommerce;
            echeckVerification.billToAddress = new contact();
            echeckVerification.billToAddress.addressLine1 = "900";
            echeckVerification.billToAddress.city = "ABC";
            echeckVerification.billToAddress.state = "MA";
            echeckVerification.merchantData = new merchantDataType();
            echeckVerification.merchantData.campaign = "camp";
            echeckVerification.merchantData.affiliate = "affil";
            echeckVerification.merchantData.merchantGroupingId = "mgi";
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<echeckVerification.*<orderId>1</orderId>.*<amount>2</amount.*<merchantData>.*<campaign>camp</campaign>.*<affiliate>affil</affiliate>.*<merchantGroupingId>mgi</merchantGroupingId>.*</merchantData>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.13' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckVerificationResponse><litleTxnId>123</litleTxnId></echeckVerificationResponse></litleOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.EcheckVerification(echeckVerification);
        }            
    }
}
