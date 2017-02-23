using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestActivate
    {
        private LitleOnline litle;
        private Dictionary<string, string> config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            config = new Dictionary<string, string>();
            config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "65");
            config.Add("merchantId", "101");
            config.Add("password", "TESTCASE");
            config.Add("printxml", "true");
            config.Add("proxyHost", Properties.Settings.Default.proxyHost);
            config.Add("proxyPort", Properties.Settings.Default.proxyPort);
            config.Add("logFile", Properties.Settings.Default.logFile);
            config.Add("neuterAccountNums", "true");
            litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleActivate()
        {
            activate activate = new activate();
            activate.id = "1";
            activate.reportGroup = "Planets";
            activate.orderId = "12344";
            activate.amount = 1500;
            activate.orderSource = orderSourceType.ecommerce;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.cardValidationNum = "123";
            card.expDate = "1215";
            activate.card = card;

            activateResponse response = litle.Activate(activate);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void VirtualGiftCardActivate()
        {
            activate activate = new activate();
            activate.id = "1";
            activate.reportGroup = "Planets";
            activate.orderId = "12344";
            activate.amount = 1500;
            activate.orderSource = orderSourceType.ecommerce;
            virtualGiftCardType virtualGiftCard = new virtualGiftCardType();
            virtualGiftCard.accountNumberLength = 123;
            virtualGiftCard.giftCardBin = "123";
            activate.virtualGiftCard = virtualGiftCard;

            activateResponse response = litle.Activate(activate);
            Assert.AreEqual("000", response.response);
        }

    }
}
