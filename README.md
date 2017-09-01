Vantiv eCommerce .NET SDK
=====================
#### WARNING:
##### All major version changes require recertification to the new version. Once certified for the use of a new version, Vantiv modifies your Merchant Profile, allowing you to submit transaction to the Production Environment using the new version. Updating your code without recertification and modification of your Merchant Profile will result in transaction declines. Please consult you Implementation Analyst for additional information about this process.
About Vantiv eCommerce
------------
[Vantiv eCommerce](https://developer.vantiv.com/community/ecommerce) powers the payment processing engines for leading companies that sell directly to consumers through  internet retail, direct response marketing (TV, radio and telephone), and online services. Vantiv eCommerce is the leading authority in card-not-present (CNP) commerce, transaction processing and merchant services.


About this SDK
--------------
The Vantiv eCommerce .NET SDK is a C# implementation of the [Vantiv eCommerce](https://developer.vantiv.com/community/ecommerce) XML API. This SDK was created to make it as easy as possible to connect and process your payments with Vantiv eCommerce. This SDK utilizes the HTTPS protocol to securely connect to Vantiv eCommerce. Using the SDK requires coordination with the Vantiv eCommerce team in order to be provided with credentials for accessing our systems.

Each .NET SDK release supports all of the functionality present in the associated Vantiv eCommerce XML version (e.g., SDK v9.3.2 supports Vantiv eCommerce XML v9.3). Please see the online copy of our XSD for Vantiv eCommerce XML to get more details on what the Vantiv eCommerce payments engine supports.

This SDK is implemented to support the .NET plaform, including C#, VB.NET and Managed C++ and was created by Vantiv eCommerce. Its intended use is for online transactions processing utilizing your account on the Vantiv eComerce payments engine.

See LICENSE file for details on using this software.

Source Code available from : https://github.com/LitleCo/litle-sdk-for-dotNet

Please contact [Vantiv eCommerce](https://developer.vantiv.com/community/ecommerce) to receive valid merchant credentials in order to run tests successfully or if you require assistance in any way.  We are reachable at sdksupport@Vantiv.com

Setup
-----

1.) To install it, just copy LitleSdkForDotNet.dll into your Visual Studio referernces. 

2.) You can configure it statically by modifying LitleSdkForDotNet.dll.config or at runtime using the LitleOnline(Dictionary) constructor. If you are just trying it out, the username, password and merchant id don't matter, and you should choose the sandbox url at https://www.testlitle.com/sandbox/communicator/online.

3.) Create a c# class similar to:  

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Litle.Sdk;

    class Example
    {
[STAThread]
        public static void Main(String[] args)
        {
            LitleOnline litle = new LitleOnline();
            sale sale = new sale();
            sale.orderId = "1";
            sale.amount = 10010;
            sale.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "John Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010000000009";
            card.expDate = "0112";
            card.cardValidationNum = "349";
            sale.card = card;

            saleResponse response = litle.Sale(sale);
            //Display Results
            Console.WriteLine("Response: " + response.response);
            Console.WriteLine("Message: " + response.message);
            Console.WriteLine("Litle Transaction Id: " + response.litleTxnId);
            Console.ReadLine();
        }
    }

```

4) Compile and run this file.  You should see the following result:

    Response: 000
    Message: Approved
    Litle Transaction ID: <your-numeric-litle-txn-id>

More examples can be found here [.Net Gists](https://gist.github.com/search?q=.net+sdk+Litle) or [Here](http://litleco.github.io/dotnet/) or in [Functional and Unit Tests] (https://github.com/LitleCo/litle-sdk-for-dotNet/tree/master/LitleSdkForNet/LitleSdkForNetTest)

Please contact Vantiv eCommerce with any further questions.   You can reach us at sdksupport@Vantiv.com.
