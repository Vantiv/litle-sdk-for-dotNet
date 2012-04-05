using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * This code contains the methods to create objects containing the information for a sale, authorization, credit/refund and a token registration request
 * The fields can be modified 
 *  The Litle Implimentations field will provide a uniques test merchand id, username and password which will be used in testing transactions with the litle system
 *  Make sure to change the appropriate fields (merchantID, user, password), to match the ones that Litle has provided. 
 */

    class createTransaction
    {
        public static litleOnlineRequest createSaleObject()
        {
            litleOnlineRequest onlineReq = new litleOnlineRequest();
            onlineReq.merchantId = "101";
            onlineReq.version = "8.8";
            authentication authentication = new authentication();
            authentication.password = "certpass";
            authentication.user = "PHXMLTEST";
            onlineReq.authentication = authentication;

            sale sale = new sale();
            sale.orderId = "TEST";
            sale.amount = "10";
            sale.orderSource = orderSourceType.ecommerce;
            sale.reportGroup = "Planet";

            contact contact = new contact();
            contact.name = "John";
            contact.addressLine1 = "101 Main St";
            contact.city = "Boston";
            contact.state = "MA";
            contact.country = countryTypeEnum.US;
            contact.zip = "01851";
            contact.email = "dberman@phoenixProcessing.com";
            contact.phone = "781-270-1111";
            sale.billToAddress = contact;

            cardType ct = new cardType();
            methodOfPaymentTypeEnum type = methodOfPaymentTypeEnum.VI;
            string expDate = "1210";
            string number = "4005518930000000";
            object[] items = new object[3];
            items[0] = type;
            items[2] = expDate;
            items[1] = number;
            ItemsChoiceType[] itemsChoice = new ItemsChoiceType[3];
            itemsChoice[0] = ItemsChoiceType.type;
            itemsChoice[2] = ItemsChoiceType.expDate;
            itemsChoice[1] = ItemsChoiceType.number;
            ct.Items = items;
            ct.ItemsElementName = itemsChoice;
            sale.Item = ct;

            onlineReq.Item = sale;
            return onlineReq;
          
        }

        public static litleOnlineRequest createAuthObject()

    {
        litleOnlineRequest onlineReq = new litleOnlineRequest();
        onlineReq.merchantId = "000052";
        onlineReq.version = "8.8";

        authentication authentication = new authentication();
        authentication.password = "password";
        authentication.user = "XMLTESTV3";
        onlineReq.authentication = authentication;

        contact contact = new contact();
        contact.name = "John";
        contact.addressLine1 = "101 Main St";
        contact.city = "Boston";
        contact.state = "MA";
        contact.country = countryTypeEnum.US;
        contact.zip = "01851";
        contact.email = "dberman@phoenixProcessing.com";
        contact.phone = "781-270-1111";

        cardType ct = new cardType();
        methodOfPaymentTypeEnum type = methodOfPaymentTypeEnum.VI;
        string expDate = "1210";
        string number = "4005518930000000";
        object[] items = new object[3];
        items[0] = type;
        items[2] = expDate;
        items[1] = number;
        ItemsChoiceType[] itemsChoice = new ItemsChoiceType[3];
        itemsChoice[0] = ItemsChoiceType.type;
        itemsChoice[2] = ItemsChoiceType.expDate;
        itemsChoice[1] = ItemsChoiceType.number;
        ct.Items = items;
        ct.ItemsElementName = itemsChoice;

        authorization authorization = new authorization();
        authorization.reportGroup = "Refund";
        orderSourceType source = orderSourceType.ecommerce;
        string amount = "5000";
        string orderID = "123435432";
        object[] items1 = new object[5];
        items1[0] = orderID;
        items1[1] = amount;
        items1[2] = source; 
        items1[3] = contact;
        items1[4] = ct;
        //items2[0] = txn;
        ItemsChoiceType1[] itemsChoice1 = new ItemsChoiceType1[5];
        itemsChoice1[0] = ItemsChoiceType1.orderId;
        itemsChoice1[1] = ItemsChoiceType1.amount;
        itemsChoice1[2] = ItemsChoiceType1.orderSource;
        itemsChoice1[3] = ItemsChoiceType1.billToAddress;
        itemsChoice1[4] = ItemsChoiceType1.card;
   
        authorization.Items = items1;
        authorization.ItemsElementName = itemsChoice1;


        onlineReq.Item = authorization;
        return onlineReq;
    }

        public static litleOnlineRequest createCreditObject()
        {
            litleOnlineRequest onlineReq = new litleOnlineRequest();
            onlineReq.merchantId = "000052";
            onlineReq.version = "8.8";
            authentication authentication = new authentication();
            authentication.password = "password";
            authentication.user = "XMLTESTV3";
            onlineReq.authentication = authentication;
         
            contact contact = new contact();
            contact.name = "John";
            contact.addressLine1 = "101 Main St";
            contact.city = "Boston";
            contact.state = "MA";
            contact.country = countryTypeEnum.US;
            contact.zip = "01851";
            contact.email = "dberman@phoenixProcessing.com";
            contact.phone = "781-270-1111";

            cardType ct = new cardType();
            methodOfPaymentTypeEnum type = methodOfPaymentTypeEnum.VI;
            string expDate = "1210";
            string number = "4005518930000000";
            object[] items = new object[3];
            items[0] = type;
            items[2] = expDate;
            items[1] = number;
            ItemsChoiceType[] itemsChoice = new ItemsChoiceType[3];
            itemsChoice[0] = ItemsChoiceType.type;
            itemsChoice[2] = ItemsChoiceType.expDate;
            itemsChoice[1] = ItemsChoiceType.number;
            ct.Items = items;
            ct.ItemsElementName = itemsChoice;

            credit credit = new credit();
            credit.reportGroup = "REFUNDS";
            string txnID = "012345678";
            string amount = "5000";
            orderSourceType source = orderSourceType.ecommerce;

            object[] items2 = new object[5];
            items2[0] = txnID;
            items2[1] = amount;
            items2[2] = source;
            items2[3] = contact;
            items2[4] =  ct;
   
            ItemsChoiceType2[] itemsChoice2 = new ItemsChoiceType2[5];
            itemsChoice2[0] = ItemsChoiceType2.orderId;
            itemsChoice2[1] = ItemsChoiceType2.amount;
            itemsChoice2[2] = ItemsChoiceType2.orderSource;
            itemsChoice2[3] = ItemsChoiceType2.billToAddress;
            itemsChoice2[4] = ItemsChoiceType2.card;
          
            credit.Items = items2;
            credit.ItemsElementName = itemsChoice2;
    
            onlineReq.Item = credit;
            return onlineReq;
        }

        public static litleOnlineRequest createTokenObject()
        {
            litleOnlineRequest onlineReq = new litleOnlineRequest();
            onlineReq.merchantId = "000052";
            onlineReq.version = "8.8";
            authentication authentication = new authentication();
            authentication.password = "password";
            authentication.user = "XMLTESTV3";
            onlineReq.authentication = authentication;

            string accountNum = "4005518930000000";
            registerTokenRequestType token = new registerTokenRequestType();
            token.reportGroup = "PLANET";
            object Item = new object();
            Item = accountNum;
            ItemChoiceType ItemChoice = new ItemChoiceType();
            ItemChoice = ItemChoiceType.accountNumber;
            token.Item = Item;
            token.ItemElementName = ItemChoice;

            onlineReq.Item = token;
            return onlineReq;

        }

    }

