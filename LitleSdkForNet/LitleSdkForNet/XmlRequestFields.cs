using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Xml.Serialization;

namespace Litle.Sdk
{
    public partial class litleOnlineRequest
    {

        public string merchantId;
        public string merchantSdk;
        public authentication authentication;
        public authorization authorization;
        public capture capture;
        public giftCardCapture giftCardCapture;
        public credit credit;
        public giftCardCredit giftCardCredit;
        public voidTxn voidTxn;
        public sale sale;
        public authReversal authReversal;
        public giftCardAuthReversal giftCardAuthReversal;
        public echeckCredit echeckCredit;
        public echeckVerification echeckVerification;
        public echeckSale echeckSale;
        public registerTokenRequestType registerTokenRequest;
        public forceCapture forceCapture;
        public captureGivenAuth captureGivenAuth;
        public echeckRedeposit echeckRedeposit;
        public echeckVoid echeckVoid;
        public updateCardValidationNumOnToken updateCardValidationNumOnToken;
        public updateSubscription updateSubscription;
        public cancelSubscription cancelSubscription;
        public activate activate;
        public deactivate deactivate;
        public load load;
        public unload unload;
        public balanceInquiry balanceInquiry;
        public createPlan createPlan;
        public updatePlan updatePlan;
        public refundReversal refundReversal;
        public loadReversal loadReversal;
        public depositReversal depositReversal;
        public activateReversal activateReversal;
        public deactivateReversal deactivateReversal;
        public unloadReversal unloadReversal;
        public queryTransaction queryTransaction;
        public fraudCheck fraudCheck;

        public string Serialize()
        {
            string xml = "<?xml version='1.0' encoding='utf-8'?>\r\n<litleOnlineRequest merchantId=\"" + merchantId + "\" version=\"11.0\" merchantSdk=\"" + merchantSdk + "\" xmlns=\"http://www.litle.com/schema\">"
                + authentication.Serialize();

            if (authorization != null) xml += authorization.Serialize();
            else if (capture != null) xml += capture.Serialize();
            else if (credit != null) xml += credit.Serialize();
            else if (voidTxn != null) xml += voidTxn.Serialize();
            else if (sale != null) xml += sale.Serialize();
            else if (authReversal != null) xml += authReversal.Serialize();
            else if (echeckCredit != null) xml += echeckCredit.Serialize();
            else if (echeckVerification != null) xml += echeckVerification.Serialize();
            else if (echeckSale != null) xml += echeckSale.Serialize();
            else if (registerTokenRequest != null) xml += registerTokenRequest.Serialize();
            else if (forceCapture != null) xml += forceCapture.Serialize();
            else if (captureGivenAuth != null) xml += captureGivenAuth.Serialize();
            else if (echeckRedeposit != null) xml += echeckRedeposit.Serialize();
            else if (echeckVoid != null) xml += echeckVoid.Serialize();
            else if (updateCardValidationNumOnToken != null) xml += updateCardValidationNumOnToken.Serialize();
            else if (updateSubscription != null) xml += updateSubscription.Serialize();
            else if (cancelSubscription != null) xml += cancelSubscription.Serialize();
            else if (activate != null) xml += activate.Serialize();
            else if (deactivate != null) xml += deactivate.Serialize();
            else if (load != null) xml += load.Serialize();
            else if (unload != null) xml += unload.Serialize();
            else if (balanceInquiry != null) xml += balanceInquiry.Serialize();
            else if (createPlan != null) xml += createPlan.Serialize();
            else if (updatePlan != null) xml += updatePlan.Serialize();
            else if (refundReversal != null) xml += refundReversal.Serialize();
            else if (loadReversal != null) xml += loadReversal.Serialize();
            else if (depositReversal != null) xml += depositReversal.Serialize();
            else if (activateReversal != null) xml += activateReversal.Serialize();
            else if (deactivateReversal != null) xml += deactivateReversal.Serialize();
            else if (unloadReversal != null) xml += unloadReversal.Serialize();
            else if (queryTransaction != null) xml += queryTransaction.Serialize();
            else if (fraudCheck != null) xml += fraudCheck.Serialize();
            else if (giftCardAuthReversal != null) xml += giftCardAuthReversal.Serialize();
            else if (giftCardCapture != null) xml += giftCardCapture.Serialize();
            else if (giftCardCredit != null) xml += giftCardCredit.Serialize();
            xml += "\r\n</litleOnlineRequest>";

            return xml;
        }
    }


    public partial class authentication
    {
        public string user;
        public string password;
        public String Serialize()
        {
            return "\r\n<authentication>\r\n<user>" + SecurityElement.Escape(user) + "</user>\r\n<password>" + SecurityElement.Escape(password) + "</password>\r\n</authentication>";
        }
    }

    public partial class customerInfo
    {

        public string ssn;

        public DateTime dob;

        public DateTime customerRegistrationDate;

        private customerInfoCustomerType customerTypeField;
        private bool customerTypeSet;
        public customerInfoCustomerType customerType
        {
            get { return this.customerTypeField; }
            set { this.customerTypeField = value; customerTypeSet = true; }
        }

        private long incomeAmountField;
        private bool incomeAmountSet;
        public long incomeAmount
        {
            get { return this.incomeAmountField; }
            set { this.incomeAmountField = value; incomeAmountSet = true; }
        }

        private currencyCodeEnum incomeCurrencyField;
        private bool incomeCurrencySet;
        public currencyCodeEnum incomeCurrency
        {
            get { return this.incomeCurrencyField; }
            set { this.incomeCurrencyField = value; incomeCurrencySet = true; }
        }

        private bool customerCheckingAccountField;
        private bool customerCheckingAccountSet;
        public bool customerCheckingAccount
        {
            get { return this.customerCheckingAccountField; }
            set { this.customerCheckingAccountField = value; customerCheckingAccountSet = true; }
        }

        private bool customerSavingAccountField;
        private bool customerSavingAccountSet;
        public bool customerSavingAccount
        {
            get { return this.customerSavingAccountField; }
            set { this.customerSavingAccountField = value; customerSavingAccountSet = true; }
        }

        public string employerName;

        public string customerWorkTelephone;

        private customerInfoResidenceStatus residenceStatusField;
        private bool residenceStatusSet;
        public customerInfoResidenceStatus residenceStatus
        {
            get { return this.residenceStatusField; }
            set { this.residenceStatusField = value; residenceStatusSet = true; }
        }

        private int yearsAtResidenceField;
        private bool yearsAtResidenceSet;
        public int yearsAtResidence
        {
            get { return this.yearsAtResidenceField; }
            set { this.yearsAtResidenceField = value; yearsAtResidenceSet = true; }
        }

        private int yearsAtEmployerField;
        private bool yearsAtEmployerSet;
        public int yearsAtEmployer
        {
            get
            {
                return this.yearsAtEmployerField;
            }
            set
            {
                this.yearsAtEmployerField = value;
                this.yearsAtEmployerSet = true;
            }
        }


        public customerInfo()
        {
            this.incomeCurrency = currencyCodeEnum.USD;
        }

        public string Serialize()
        {
            string xml = "";
            if (ssn != null)
            {
                xml += "\r\n<ssn>" + SecurityElement.Escape(ssn) + "</ssn>";
            }
            if (dob != null)
            {
                xml += "\r\n<dob>" + XmlUtil.toXsdDate(dob) + "</dob>";
            }
            if (customerRegistrationDate != null)
            {
                xml += "\r\n<customerRegistrationDate>" + XmlUtil.toXsdDate(customerRegistrationDate) + "</customerRegistrationDate>";
            }
            if (customerTypeSet)
            {
                xml += "\r\n<customerType>" + customerTypeField + "</customerType>";
            }
            if (incomeAmountSet)
            {
                xml += "\r\n<incomeAmount>" + incomeAmountField + "</incomeAmount>";
            }
            if (incomeCurrencySet)
            {
                xml += "\r\n<incomeCurrency>" + incomeCurrencyField + "</incomeCurrency>";
            }
            if (customerCheckingAccountSet)
            {
                xml += "\r\n<customerCheckingAccount>" + customerCheckingAccountField.ToString().ToLower() + "</customerCheckingAccount>";
            }
            if (customerSavingAccountSet)
            {
                xml += "\r\n<customerSavingAccount>" + customerSavingAccountField.ToString().ToLower() + "</customerSavingAccount>";
            }
            if (employerName != null)
            {
                xml += "\r\n<employerName>" + SecurityElement.Escape(employerName) + "</employerName>";
            }
            if (customerWorkTelephone != null)
            {
                xml += "\r\n<customerWorkTelephone>" + SecurityElement.Escape(customerWorkTelephone) + "</customerWorkTelephone>";
            }
            if (residenceStatusSet)
            {
                xml += "\r\n<residenceStatus>" + residenceStatusField + "</residenceStatus>";
            }
            if (yearsAtResidenceSet)
            {
                xml += "\r\n<yearsAtResidence>" + yearsAtResidenceField + "</yearsAtResidence>";
            }
            if (yearsAtEmployerSet)
            {
                xml += "\r\n<yearsAtEmployer>" + yearsAtEmployerField + "</yearsAtEmployer>";
            }
            return xml;
        }


    }

    public enum customerInfoCustomerType
    {

        /// <remarks/>
        New,
        Existing,
    }

    public enum currencyCodeEnum
    {

        /// <remarks/>
        AUD,
        CAD,
        CHF,
        DKK,
        EUR,
        GBP,
        HKD,
        JPY,
        NOK,
        NZD,
        SEK,
        SGD,
        USD,
    }

    public enum customerInfoResidenceStatus
    {

        /// <remarks/>
        Own,
        Rent,
        Other,
    }

    public partial class enhancedData
    {
        public string customerReference;
        private long salesTaxField;
        private bool salesTaxSet;
        public long salesTax
        {
            get { return this.salesTaxField; }
            set { this.salesTaxField = value; this.salesTaxSet = true; }
        }
        private enhancedDataDeliveryType deliveryTypeField;
        private bool deliveryTypeSet;
        public enhancedDataDeliveryType deliveryType
        {
            get { return this.deliveryTypeField; }
            set { this.deliveryTypeField = value; this.deliveryTypeSet = true; }
        }
        public bool taxExemptField;
        public bool taxExemptSet;
        public bool taxExempt
        {
            get { return this.taxExemptField; }
            set { this.taxExemptField = value; this.taxExemptSet = true; }
        }
        private long discountAmountField;
        private bool discountAmountSet;
        public long discountAmount
        {
            get { return this.discountAmountField; }
            set { this.discountAmountField = value; this.discountAmountSet = true; }
        }
        private long shippingAmountField;
        private bool shippingAmountSet;
        public long shippingAmount
        {
            get { return this.shippingAmountField; }
            set { this.shippingAmountField = value; this.shippingAmountSet = true; }
        }
        private long dutyAmountField;
        private bool dutyAmountSet;
        public long dutyAmount
        {
            get { return this.dutyAmountField; }
            set { this.dutyAmountField = value; this.dutyAmountSet = true; }
        }
        public string shipFromPostalCode;
        public string destinationPostalCode;
        private countryTypeEnum destinationCountryCodeField;
        private bool destinationCountryCodeSet;
        public countryTypeEnum destinationCountry
        {
            get { return this.destinationCountryCodeField; }
            set { this.destinationCountryCodeField = value; this.destinationCountryCodeSet = true; }
        }
        public string invoiceReferenceNumber;
        private DateTime orderDateField;
        private bool orderDateSet;
        public DateTime orderDate
        {
            get { return this.orderDateField; }
            set { this.orderDateField = value; this.orderDateSet = true; }
        }
        public List<detailTax> detailTaxes;
        public List<lineItemData> lineItems;

        public enhancedData()
        {
            lineItems = new List<lineItemData>();
            detailTaxes = new List<detailTax>();
        }

        public string Serialize()
        {
            string xml = "";
            if (customerReference != null) xml += "\r\n<customerReference>" + SecurityElement.Escape(customerReference) + "</customerReference>";
            if (salesTaxSet) xml += "\r\n<salesTax>" + salesTaxField + "</salesTax>";
            if (deliveryTypeSet) xml += "\r\n<deliveryType>" + deliveryTypeField + "</deliveryType>";
            if (taxExemptSet) xml += "\r\n<taxExempt>" + taxExemptField.ToString().ToLower() + "</taxExempt>";
            if (discountAmountSet) xml += "\r\n<discountAmount>" + discountAmountField + "</discountAmount>";
            if (shippingAmountSet) xml += "\r\n<shippingAmount>" + shippingAmountField + "</shippingAmount>";
            if (dutyAmountSet) xml += "\r\n<dutyAmount>" + dutyAmountField + "</dutyAmount>";
            if (shipFromPostalCode != null) xml += "\r\n<shipFromPostalCode>" + SecurityElement.Escape(shipFromPostalCode) + "</shipFromPostalCode>";
            if (destinationPostalCode != null) xml += "\r\n<destinationPostalCode>" + SecurityElement.Escape(destinationPostalCode) + "</destinationPostalCode>";
            if (destinationCountryCodeSet) xml += "\r\n<destinationCountryCode>" + destinationCountryCodeField + "</destinationCountryCode>";
            if (invoiceReferenceNumber != null) xml += "\r\n<invoiceReferenceNumber>" + SecurityElement.Escape(invoiceReferenceNumber) + "</invoiceReferenceNumber>";
            if (orderDateSet) xml += "\r\n<orderDate>" + XmlUtil.toXsdDate(orderDateField) + "</orderDate>";
            foreach (detailTax detailTax in detailTaxes)
            {
                xml += "\r\n<detailTax>" + detailTax.Serialize() + "\r\n</detailTax>";
            }
            foreach (lineItemData lineItem in lineItems)
            {
                xml += "\r\n<lineItemData>" + lineItem.Serialize() + "\r\n</lineItemData>";
            }
            return xml;
        }
    }

    public partial class voidTxn : transactionTypeWithReportGroup
    {
        public long litleTxnId;
        public processingInstructions processingInstructions;

        public override string Serialize()
        {
            string xml = "\r\n<void";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";
            xml += "\r\n<litleTxnId>" + litleTxnId + "</litleTxnId>";
            if (processingInstructions != null) xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
            xml += "\r\n</void>";

            return xml;
        }

    }

    public partial class lineItemData
    {
        private int itemSeqenceNumberField;
        private bool itemSequenceNumberSet;
        public int itemSequenceNumber
        {
            get { return this.itemSeqenceNumberField; }
            set { this.itemSeqenceNumberField = value; this.itemSequenceNumberSet = true; }
        }
        public string itemDescription;
        public string productCode;
        public string quantity;
        public string unitOfMeasure;
        private long taxAmountField;
        private bool taxAmountSet;
        public long taxAmount
        {
            get { return this.taxAmountField; }
            set { this.taxAmountField = value; this.taxAmountSet = true; }
        }
        private long lineItemTotalField;
        private bool lineItemTotalSet;
        public long lineItemTotal
        {
            get { return this.lineItemTotalField; }
            set { this.lineItemTotalField = value; this.lineItemTotalSet = true; }
        }
        private long lineItemTotalWithTaxField;
        private bool lineItemTotalWithTaxSet;
        public long lineItemTotalWithTax
        {
            get { return this.lineItemTotalWithTaxField; }
            set { this.lineItemTotalWithTaxField = value; this.lineItemTotalWithTaxSet = true; }
        }
        private long itemDiscountAmountField;
        private bool itemDiscountAmountSet;
        public long itemDiscountAmount
        {
            get { return this.itemDiscountAmountField; }
            set { this.itemDiscountAmountField = value; this.itemDiscountAmountSet = true; }
        }
        public string commodityCode;
        public string unitCost;
        public List<detailTax> detailTaxes;

        public lineItemData()
        {
            detailTaxes = new List<detailTax>();
        }

        public string Serialize()
        {
            string xml = "";
            if (itemSequenceNumberSet) xml += "\r\n<itemSequenceNumber>" + itemSeqenceNumberField + "</itemSequenceNumber>";
            if (itemDescription != null) xml += "\r\n<itemDescription>" + SecurityElement.Escape(itemDescription) + "</itemDescription>";
            if (productCode != null) xml += "\r\n<productCode>" + SecurityElement.Escape(productCode) + "</productCode>";
            if (quantity != null) xml += "\r\n<quantity>" + SecurityElement.Escape(quantity) + "</quantity>";
            if (unitOfMeasure != null) xml += "\r\n<unitOfMeasure>" + SecurityElement.Escape(unitOfMeasure) + "</unitOfMeasure>";
            if (taxAmountSet) xml += "\r\n<taxAmount>" + taxAmountField + "</taxAmount>";
            if (lineItemTotalSet) xml += "\r\n<lineItemTotal>" + lineItemTotalField + "</lineItemTotal>";
            if (lineItemTotalWithTaxSet) xml += "\r\n<lineItemTotalWithTax>" + lineItemTotalWithTaxField + "</lineItemTotalWithTax>";
            if (itemDiscountAmountSet) xml += "\r\n<itemDiscountAmount>" + itemDiscountAmountField + "</itemDiscountAmount>";
            if (commodityCode != null) xml += "\r\n<commodityCode>" + SecurityElement.Escape(commodityCode) + "</commodityCode>";
            if (unitCost != null) xml += "\r\n<unitCost>" + SecurityElement.Escape(unitCost) + "</unitCost>";
            foreach (detailTax detailTax in detailTaxes)
            {
                if (detailTax != null) xml += "\r\n<detailTax>" + detailTax.Serialize() + "</detailTax>";
            }
            return xml;
        }

    }


    public partial class detailTax
    {
        private bool taxIncludedInTotalField;
        private bool taxIncludedInTotalSet;
        public bool taxIncludedInTotal
        {
            get { return this.taxIncludedInTotalField; }
            set { this.taxIncludedInTotalField = value; this.taxIncludedInTotalSet = true; }
        }
        private long taxAmountField;
        private bool taxAmountSet;
        public long taxAmount
        {
            get { return this.taxAmountField; }
            set { this.taxAmountField = value; this.taxAmountSet = true; }
        }
        public string taxRate;
        private taxTypeIdentifierEnum taxTypeIdentifierField;
        private bool taxTypeIdentifierSet;
        public taxTypeIdentifierEnum taxTypeIdentifier
        {
            get { return this.taxTypeIdentifierField; }
            set { this.taxTypeIdentifierField = value; this.taxTypeIdentifierSet = true; }
        }
        public string cardAcceptorTaxId;

        public string Serialize()
        {
            string xml = "";
            if (taxIncludedInTotalSet) xml += "\r\n<taxIncludedInTotal>" + taxIncludedInTotalField.ToString().ToLower() + "</taxIncludedInTotal>";
            if (taxAmountSet) xml += "\r\n<taxAmount>" + taxAmountField + "</taxAmount>";
            if (taxRate != null) xml += "\r\n<taxRate>" + SecurityElement.Escape(taxRate) + "</taxRate>";
            if (taxTypeIdentifierSet) xml += "\r\n<taxTypeIdentifier>" + taxTypeIdentifierField + "</taxTypeIdentifier>";
            if (cardAcceptorTaxId != null) xml += "\r\n<cardAcceptorTaxId>" + SecurityElement.Escape(cardAcceptorTaxId) + "</cardAcceptorTaxId>";
            return xml;
        }
    }

    public partial class transactionTypeWithReportGroupAndPartial : transactionType
    {
        public string reportGroup;
        private bool partialField;
        protected bool partialSet;
        public bool partial
        {
            get { return this.partialField; }
            set { this.partialField = value; partialSet = true; }
        }
    }

    public partial class capture : transactionTypeWithReportGroupAndPartial
    {
        public long litleTxnId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return this.amountField; }
            set { this.amountField = value; amountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return this.surchargeAmountField; }
            set { this.surchargeAmountField = value; this.surchargeAmountSet = true; }
        }
        public enhancedData enhancedData;
        public processingInstructions processingInstructions;
        private bool payPalOrderCompleteField;
        private bool payPalOrderCompleteSet;
        public bool payPalOrderComplete
        {
            get { return this.payPalOrderCompleteField; }
            set { this.payPalOrderCompleteField = value; payPalOrderCompleteSet = true; }
        }
        public string payPalNotes;
        public customBilling customBilling;
        public string pin;

        public override string Serialize()
        {
            string xml = "\r\n<capture";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            if (partialSet)
            {
                xml += " partial=\"" + partial.ToString().ToLower() + "\"";
            }
            xml += ">";
            xml += "\r\n<litleTxnId>" + litleTxnId + "</litleTxnId>";
            if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
            if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            if (enhancedData != null) xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            if (processingInstructions != null) xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
            if (payPalOrderCompleteSet) xml += "\r\n<payPalOrderComplete>" + payPalOrderCompleteField.ToString().ToLower() + "</payPalOrderComplete>";
            if (payPalNotes != null) xml += "\r\n<payPalNotes>" + SecurityElement.Escape(payPalNotes) + "</payPalNotes>";
            if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "\r\n</customBilling>";
            if (pin != null) xml += "\r\n<pin>" + pin + "</pin>";
            xml += "\r\n</capture>";

            return xml;
        }
    }

    public partial class giftCardCapture : transactionTypeWithReportGroupAndPartial
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
                this.litleTxnIdSet = true;
            }
        }
        public long captureAmount;
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;

        public override String Serialize()
        {
            string xml = "\r\n<giftCardCapture";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            if (partialSet)
            {
                xml += " partial=\"" + partial.ToString().ToLower() + "\"";
            }
            xml += ">";
            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnIdField + "</litleTxnId>";
            }

            xml += "\r\n<captureAmount>" + captureAmount + "</captureAmount>";
            
            if (card != null)
            {
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            if (originalRefCode != null)
            {
                xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            }
            // Do I need to turn original amount long into a boolean because it cannot be compared to null
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";

            if (originalTxnTime != null)
            {
                xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            }
            xml += "\r\n</giftCardCapture>";
            return xml;
        }
    }

    public partial class echeckCredit : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get { return this.litleTxnIdField; }
            set { this.litleTxnIdField = value; litleTxnIdSet = true; }
        }
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return this.amountField; }
            set { this.amountField = value; amountSet = true; }
        }
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return this.secondaryAmountField; }
            set { this.secondaryAmountField = value; this.secondaryAmountSet = true; }
        }
        public customBilling customBilling;
        public string orderId;
        public orderSourceType orderSource;
        public contact billToAddress;
        public echeckType echeck;

        [Obsolete()]
        public echeckTokenType token
        {
            get { return echeckToken; }
            set { echeckToken = value; }
        }

        public echeckTokenType echeckToken;

        public merchantDataType merchantData;

        public override string Serialize()
        {
            string xml = "\r\n<echeckCredit";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnIdField + "</litleTxnId>";
                if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
            }
            else
            {
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                xml += "\r\n<amount>" + amountField + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
                if (billToAddress != null) xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "</billToAddress>";
                if (echeck != null) xml += "\r\n<echeck>" + echeck.Serialize() + "</echeck>";
                else if (echeckToken != null) xml += "\r\n<echeckToken>" + echeckToken.Serialize() + "</echeckToken>";
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
                if (merchantData != null) xml += "\r\n<merchantData>" + merchantData.Serialize() + "</merchantData>";
            }
            xml += "\r\n</echeckCredit>";
            return xml;
        }
    }

    public partial class echeckSale : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get { return this.litleTxnIdField; }
            set { this.litleTxnIdField = value; litleTxnIdSet = true; }
        }
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return this.amountField; }
            set { this.amountField = value; amountSet = true; }
        }
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return this.secondaryAmountField; }
            set { this.secondaryAmountField = value; this.secondaryAmountSet = true; }
        }
        public customBilling customBilling;
        public string orderId;
        private bool verifyField;
        private bool verifySet;
        public bool verify
        {
            get { return this.verifyField; }
            set { this.verifyField = value; verifySet = true; }
        }
        public orderSourceType orderSource;
        public contact billToAddress;
        public contact shipToAddress;
        public echeckType echeck;
        public echeckTokenType token;
        public merchantDataType merchantData;

        public override string Serialize()
        {
            string xml = "\r\n<echeckSale";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnIdField + "</litleTxnId>";
                if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
                // let sandbox do the validation for secondaryAmount
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
            }
            else
            {
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                if (verifySet) xml += "\r\n<verify>" + (verifyField ? "true" : "false") + "</verify>";
                xml += "\r\n<amount>" + amountField + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
                if (billToAddress != null) xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "</billToAddress>";
                if (shipToAddress != null) xml += "\r\n<shipToAddress>" + shipToAddress.Serialize() + "</shipToAddress>";
                if (echeck != null) xml += "\r\n<echeck>" + echeck.Serialize() + "</echeck>";
                else if (token != null) xml += "\r\n<echeckToken>" + token.Serialize() + "</echeckToken>";
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
                if (merchantData != null) xml += "\r\n<merchantData>" + merchantData.Serialize() + "</merchantData>";
            }
            xml += "\r\n</echeckSale>";
            return xml;
        }
    }


    public partial class echeckVerification : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get { return this.litleTxnIdField; }
            set { this.litleTxnIdField = value; litleTxnIdSet = true; }
        }
        public string orderId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return this.amountField; }
            set { this.amountField = value; amountSet = true; }
        }
        public orderSourceType orderSource;
        public contact billToAddress;
        public echeckType echeck;
        public echeckTokenType token;
        public merchantDataType merchantData;

        public override string Serialize()
        {
            string xml = "\r\n<echeckVerification";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            if (litleTxnIdSet) xml += "\r\n<litleTxnId>" + litleTxnIdField + "</litleTxnId>";
            xml += "\r\n<orderId>" + orderId + "</orderId>";
            if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
            if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            if (billToAddress != null) xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "</billToAddress>";
            if (echeck != null) xml += "\r\n<echeck>" + echeck.Serialize() + "</echeck>";
            else if (token != null) xml += "\r\n<echeckToken>" + token.Serialize() + "</echeckToken>";
            if (merchantData != null) xml += "\r\n<merchantData>" + merchantData.Serialize() + "</merchantData>";
            xml += "\r\n</echeckVerification>";
            return xml;
        }
    }

    public partial class registerTokenRequestType : transactionTypeWithReportGroup
    {
        public string orderId;
        public string accountNumber;
        public echeckForTokenType echeckForToken;
        public string paypageRegistrationId;
        public string cardValidationNum;
        public applepayType applepay;

        public override string Serialize()
        {
            string xml = "\r\n<registerTokenRequest";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            xml += "\r\n<orderId>" + orderId + "</orderId>";
            if (accountNumber != null) xml += "\r\n<accountNumber>" + accountNumber + "</accountNumber>";
            else if (echeckForToken != null) xml += "\r\n<echeckForToken>" + echeckForToken.Serialize() + "</echeckForToken>";
            else if (paypageRegistrationId != null) xml += "\r\n<paypageRegistrationId>" + paypageRegistrationId + "</paypageRegistrationId>";
            else if (applepay != null) xml += "\r\n<applepay>" + applepay.Serialize() + "\r\n</applepay>";
            if (cardValidationNum != null) xml += "\r\n<cardValidationNum>" + cardValidationNum + "</cardValidationNum>";
            xml += "\r\n</registerTokenRequest>";
            return xml;
        }
    }

    public partial class updateCardValidationNumOnToken : transactionTypeWithReportGroup
    {
        public string orderId;
        public string litleToken;
        public string cardValidationNum;

        public override string Serialize()
        {
            string xml = "\r\n<updateCardValidationNumOnToken";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            if (orderId != null) xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            if (litleToken != null) xml += "\r\n<litleToken>" + SecurityElement.Escape(litleToken) + "</litleToken>";
            if (cardValidationNum != null) xml += "\r\n<cardValidationNum>" + SecurityElement.Escape(cardValidationNum) + "</cardValidationNum>";
            xml += "\r\n</updateCardValidationNumOnToken>";
            return xml;
        }
    }

    public partial class echeckForTokenType
    {
        public string accNum;
        public string routingNum;

        public string Serialize()
        {
            string xml = "";
            if (accNum != null) xml += "\r\n<accNum>" + SecurityElement.Escape(accNum) + "</accNum>";
            if (routingNum != null) xml += "\r\n<routingNum>" + SecurityElement.Escape(routingNum) + "</routingNum>";
            return xml;
        }
    }


    public partial class credit : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get { return this.litleTxnIdField; }
            set { this.litleTxnIdField = value; litleTxnIdSet = true; }
        }
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return this.amountField; }
            set { this.amountField = value; amountSet = true; }
        }
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return this.secondaryAmountField; }
            set { this.secondaryAmountField = value; this.secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return this.surchargeAmountField; }
            set { this.surchargeAmountField = value; this.surchargeAmountSet = true; }
        }
        public customBilling customBilling;
        public enhancedData enhancedData;
        public processingInstructions processingInstructions;
        public string orderId;
        public orderSourceType orderSource;
        public contact billToAddress;
        public cardType card;
        public mposType mpos;
        public cardTokenType token;
        public cardPaypageType paypage;
        public payPal paypal;
        private taxTypeIdentifierEnum taxTypeField;
        private bool taxTypeSet;
        public taxTypeIdentifierEnum taxType
        {
            get { return this.taxTypeField; }
            set { this.taxTypeField = value; taxTypeSet = true; }
        }
        public billMeLaterRequest billMeLaterRequest;
        public pos pos;
        private string pinField;
        private bool pinSet;
        public string pin
        {
            get { return this.pinField; }
            set { this.pinField = value; pinSet = true; }
        }
        public amexAggregatorData amexAggregatorData;
        public merchantDataType merchantData;
        public string payPalNotes;
        public string actionReason;

        public override string Serialize()
        {
            string xml = "\r\n<credit";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnIdField + "</litleTxnId>";
                if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
                if (enhancedData != null) xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "</enhancedData>";
                if (processingInstructions != null) xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "</processingInstructions>";
                if (pos != null) xml += "\r\n<pos>" + pos.Serialize() + "</pos>";
                if (pinSet) xml += "\r\n<pin>" + pinField + "</pin>";
            }
            else
            {
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                xml += "\r\n<amount>" + amountField + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
                if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
                if (billToAddress != null) xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "</billToAddress>";
                if (card != null) xml += "\r\n<card>" + card.Serialize() + "</card>";
                else if (token != null) xml += "\r\n<token>" + token.Serialize() + "</token>";
                else if (mpos != null) xml += "\r\n<mpos>" + mpos.Serialize() + "</mpos>";
                else if (paypage != null) xml += "\r\n<paypage>" + paypage.Serialize() + "</paypage>";
                else if (paypal != null)
                {
                    xml += "\r\n<paypal>";
                    if (paypal.payerId != null) xml += "\r\n<payerId>" + SecurityElement.Escape(paypal.payerId) + "</payerId>";
                    else if (paypal.payerEmail != null) xml += "\r\n<payerEmail>" + SecurityElement.Escape(paypal.payerEmail) + "</payerEmail>";
                    xml += "\r\n</paypal>";
                }
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
                if (taxTypeSet) xml += "\r\n<taxType>" + taxTypeField + "</taxType>";
                if (billMeLaterRequest != null) xml += "\r\n<billMeLaterRequest>" + billMeLaterRequest.Serialize() + "</billMeLaterRequest>";
                if (enhancedData != null) xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "</enhancedData>";
                if (processingInstructions != null) xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "</processingInstructions>";
                if (pos != null) xml += "\r\n<pos>" + pos.Serialize() + "</pos>";
                if (amexAggregatorData != null) xml += "\r\n<amexAggregatorData>" + amexAggregatorData.Serialize() + "</amexAggregatorData>";
                if (merchantData != null) xml += "\r\n<merchantData>" + merchantData.Serialize() + "</merchantData>";
            }
            if (payPalNotes != null) xml += "\r\n<payPalNotes>" + SecurityElement.Escape(payPalNotes) + "</payPalNotes>";
            if (actionReason != null) xml += "\r\n<actionReason>" + SecurityElement.Escape(actionReason) + "</actionReason>";
            xml += "\r\n</credit>";
            return xml;
        }
    }

    public partial class giftCardCredit : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get { return this.litleTxnIdField; }
            set { this.litleTxnIdField = value; this.litleTxnIdSet = true; }
        }
        public long creditAmount;
        public giftCardCardType card;
        public string orderId;
        public orderSourceType orderSource;

        public override string Serialize()
        {
            string xml = "\r\n<giftCardCredit";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";

            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnIdField + "</litleTxnId>";
                xml += "\r\n<creditAmount>" + creditAmount + "</creditAmount>";
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            else
            {
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                xml += "\r\n<creditAmount>" + creditAmount + "</creditAmount>";
                xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            xml += "\r\n</giftCardCredit>";
            return xml;
        }
    }

    public partial class echeckType
    {
        private echeckAccountTypeEnum accTypeField;
        private bool accTypeSet;
        public echeckAccountTypeEnum accType
        {
            get { return this.accTypeField; }
            set { this.accTypeField = value; accTypeSet = true; }
        }

        public string accNum;
        public string routingNum;
        public string checkNum;
        public string ccdPaymentInformation;

        public string Serialize()
        {
            string xml = "";
            string accTypeName = accTypeField.ToString();
            XmlEnumAttribute[] attributes = 
                (XmlEnumAttribute[])typeof(echeckAccountTypeEnum).GetMember(accTypeField.ToString())[0].GetCustomAttributes(typeof(XmlEnumAttribute), false);
            if (attributes.Length > 0) accTypeName = attributes[0].Name;
            if (accTypeSet) xml += "\r\n<accType>" + accTypeName + "</accType>";
            if (accNum != null) xml += "\r\n<accNum>" + SecurityElement.Escape(accNum) + "</accNum>";
            if (routingNum != null) xml += "\r\n<routingNum>" + SecurityElement.Escape(routingNum) + "</routingNum>";
            if (checkNum != null) xml += "\r\n<checkNum>" + SecurityElement.Escape(checkNum) + "</checkNum>";
            if (ccdPaymentInformation != null) xml += "\r\n<ccdPaymentInformation>" + SecurityElement.Escape(ccdPaymentInformation) + "</ccdPaymentInformation>";
            return xml;
        }
    }

    public partial class echeckTokenType
    {
        public string litleToken;
        public string routingNum;
        private echeckAccountTypeEnum accTypeField;
        private bool accTypeSet;
        public echeckAccountTypeEnum accType
        {
            get { return this.accTypeField; }
            set { this.accTypeField = value; accTypeSet = true; }
        }
        public string checkNum;

        public string Serialize()
        {
            string xml = "";
            if (litleToken != null) xml += "\r\n<litleToken>" + SecurityElement.Escape(litleToken) + "</litleToken>";
            if (routingNum != null) xml += "\r\n<routingNum>" + SecurityElement.Escape(routingNum) + "</routingNum>";
            if (accTypeSet) xml += "\r\n<accType>" + accTypeField + "</accType>";
            if (checkNum != null) xml += "\r\n<checkNum>" + SecurityElement.Escape(checkNum) + "</checkNum>";
            return xml;
        }

    }


    public partial class pos
    {
        private posCapabilityTypeEnum capabilityField;
        private bool capabilitySet;
        public posCapabilityTypeEnum capability
        {
            get { return this.capabilityField; }
            set { this.capabilityField = value; capabilitySet = true; }
        }

        private posEntryModeTypeEnum entryModeField;
        private bool entryModeSet;
        public posEntryModeTypeEnum entryMode
        {
            get { return this.entryModeField; }
            set { this.entryModeField = value; entryModeSet = true; }
        }

        private posCardholderIdTypeEnum cardholderIdField;
        private bool cardholderIdSet;
        public posCardholderIdTypeEnum cardholderId
        {
            get { return this.cardholderIdField; }
            set { this.cardholderIdField = value; cardholderIdSet = true; }
        }
        public string terminalId;

        private posCatLevelEnum catLevelField;
        private bool catLevelSet;
        public posCatLevelEnum catLevel
        {
            get { return this.catLevelField; }
            set { this.catLevelField = value; catLevelSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (capabilitySet) xml += "\r\n<capability>" + capabilityField + "</capability>";
            if (entryModeSet) xml += "\r\n<entryMode>" + entryModeField + "</entryMode>";
            if (cardholderIdSet) xml += "\r\n<cardholderId>" + cardholderIdField + "</cardholderId>";
            if (terminalId != null) xml += "\r\n<terminalId>" + SecurityElement.Escape(terminalId) + "</terminalId>";
            if (catLevelSet) xml += "\r\n<catLevel>" + catLevelField.Serialize() +"</catLevel>";
            return xml;
        }

    }

    public partial class payPal
    {
        public string payerId;
        public string payerEmail;
        public string token;
        public string transactionId;

        public string Serialize()
        {
            string xml = "";
            if (payerId != null) xml += "\r\n<payerId>" + SecurityElement.Escape(payerId) + "</payerId>";
            if (payerEmail != null) xml += "\r\n<payerEmail>" + SecurityElement.Escape(payerEmail) + "</payerEmail>";
            if (token != null) xml += "\r\n<token>" + SecurityElement.Escape(token) + "</token>";
            if (transactionId != null) xml += "\r\n<transactionId>" + SecurityElement.Escape(transactionId) + "</transactionId>";
            return xml;
        }
    }

    public partial class merchantDataType
    {
        public string campaign;
        public string affiliate;
        public string merchantGroupingId;

        public string Serialize()
        {
            string xml = "";
            if (campaign != null) xml += "\r\n<campaign>" + SecurityElement.Escape(campaign) + "</campaign>";
            if (affiliate != null) xml += "\r\n<affiliate>" + SecurityElement.Escape(affiliate) + "</affiliate>";
            if (merchantGroupingId != null) xml += "\r\n<merchantGroupingId>" + SecurityElement.Escape(merchantGroupingId) + "</merchantGroupingId>";
            return xml;
        }
    }

    public partial class cardTokenType
    {
        public string litleToken;
        public string expDate;
        public string cardValidationNum;
        private methodOfPaymentTypeEnum typeField;
        private bool typeSet;
        public methodOfPaymentTypeEnum type
        {
            get { return this.typeField; }
            set { this.typeField = value; typeSet = true; }
        }

        public string Serialize()
        {
            string xml = "\r\n<litleToken>" + SecurityElement.Escape(litleToken) + "</litleToken>";
            if (expDate != null) xml += "\r\n<expDate>" + SecurityElement.Escape(expDate) + "</expDate>";
            if (cardValidationNum != null) xml += "\r\n<cardValidationNum>" + SecurityElement.Escape(cardValidationNum) + "</cardValidationNum>";
            if (typeSet) xml += "\r\n<type>" + methodOfPaymentSerializer.Serialize(typeField) + "</type>";
            return xml;
        }
    }

    public partial class cardPaypageType
    {
        public string paypageRegistrationId;
        public string expDate;
        public string cardValidationNum;
        private methodOfPaymentTypeEnum typeField;
        private bool typeSet;
        public methodOfPaymentTypeEnum type
        {
            get { return this.typeField; }
            set { this.typeField = value; typeSet = true; }
        }

        public string Serialize()
        {
            string xml = "\r\n<paypageRegistrationId>" + SecurityElement.Escape(paypageRegistrationId) + "</paypageRegistrationId>";
            if (expDate != null) xml += "\r\n<expDate>" + SecurityElement.Escape(expDate) + "</expDate>";
            if (cardValidationNum != null) xml += "\r\n<cardValidationNum>" + SecurityElement.Escape(cardValidationNum) + "</cardValidationNum>";
            if (typeSet) xml += "\r\n<type>" + methodOfPaymentSerializer.Serialize(typeField) + "</type>";
            return xml;
        }
    }

    public partial class billMeLaterRequest
    {
        private long bmlMerchantIdField;
        private bool bmlMerchantIdSet;
        public long bmlMerchantId
        {
            get { return this.bmlMerchantIdField; }
            set { this.bmlMerchantIdField = value; bmlMerchantIdSet = true; }
        }
        private long bmlProductTypeField;
        private bool bmlProductTypeSet;
        public long bmlProductType
        {
            get { return this.bmlProductTypeField; }
            set { this.bmlProductTypeField = value; bmlProductTypeSet = true; }
        }
        private int termsAndConditionsField;
        private bool termsAndConditionsSet;
        public int termsAndConditions
        {
            get { return this.termsAndConditionsField; }
            set { this.termsAndConditionsField = value; termsAndConditionsSet = true; }
        }
        public string preapprovalNumber;
        private int merchantPromotionalCodeField;
        private bool merchantPromotionalCodeSet;
        public int merchantPromotionalCode
        {
            get { return this.merchantPromotionalCodeField; }
            set { this.merchantPromotionalCodeField = value; merchantPromotionalCodeSet = true; }
        }
        public string virtualAuthenticationKeyPresenceIndicator;
        public string virtualAuthenticationKeyData;
        private int itemCategoryCodeField;
        private bool itemCategoryCodeSet;
        public int itemCategoryCode
        {
            get { return this.itemCategoryCodeField; }
            set { this.itemCategoryCodeField = value; itemCategoryCodeSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (bmlMerchantIdSet) xml += "\r\n<bmlMerchantId>" + bmlMerchantIdField + "</bmlMerchantId>";
            if (bmlProductTypeSet) xml += "\r\n<bmlProductType>" + bmlProductTypeField + "</bmlProductType>";
            if (termsAndConditionsSet) xml += "\r\n<termsAndConditions>" + termsAndConditionsField + "</termsAndConditions>";
            if (preapprovalNumber != null) xml += "\r\n<preapprovalNumber>" + SecurityElement.Escape(preapprovalNumber) + "</preapprovalNumber>";
            if (merchantPromotionalCodeSet) xml += "\r\n<merchantPromotionalCode>" + merchantPromotionalCodeField + "</merchantPromotionalCode>";
            if (virtualAuthenticationKeyPresenceIndicator != null) xml += "\r\n<virtualAuthenticationKeyPresenceIndicator>" + SecurityElement.Escape(virtualAuthenticationKeyPresenceIndicator) + "</virtualAuthenticationKeyPresenceIndicator>";
            if (virtualAuthenticationKeyData != null) xml += "\r\n<virtualAuthenticationKeyData>" + SecurityElement.Escape(virtualAuthenticationKeyData) + "</virtualAuthenticationKeyData>";
            if (itemCategoryCodeSet) xml += "\r\n<itemCategoryCode>" + itemCategoryCodeField + "</itemCategoryCode>";
            return xml;
        }

    }

    public partial class customBilling
    {
        public string phone;
        public string city;
        public string url;
        public string descriptor;
        public string Serialize()
        {
            string xml = "";
            if (phone != null) xml += "\r\n<phone>" + SecurityElement.Escape(phone) + "</phone>";
            else if (city != null) xml += "\r\n<city>" + SecurityElement.Escape(city) + "</city>";
            else if (url != null) xml += "\r\n<url>" + SecurityElement.Escape(url) + "</url>";
            if (descriptor != null) xml += "\r\n<descriptor>" + SecurityElement.Escape(descriptor) + "</descriptor>";
            return xml;
        }
    }

    public partial class amexAggregatorData
    {
        public string sellerId;
        public string sellerMerchantCategoryCode;
        public string Serialize()
        {
            string xml = "";
            xml += "\r\n<sellerId>" + SecurityElement.Escape(sellerId) + "</sellerId>";
            xml += "\r\n<sellerMerchantCategoryCode>" + SecurityElement.Escape(sellerMerchantCategoryCode) + "</sellerMerchantCategoryCode>";
            return xml;
        }

    }

    public partial class processingInstructions
    {
        private bool bypassVelocityCheckField;
        private bool bypassVelocityCheckSet;
        public bool bypassVelocityCheck
        {
            get { return this.bypassVelocityCheckField; }
            set { this.bypassVelocityCheckField = value; this.bypassVelocityCheckSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (bypassVelocityCheckSet) xml += "\r\n<bypassVelocityCheck>" + bypassVelocityCheckField.ToString().ToLower() + "</bypassVelocityCheck>";
            return xml;
        }
    }

    public partial class echeckRedeposit : baseRequestTransactionEcheckRedeposit
    {
        //litleTxnIdField and set are in super
        public echeckType echeck;
        public echeckTokenType token;
        public merchantDataType merchantData;

        public override string Serialize()
        {
            string xml = "\r\n<echeckRedeposit";
            xml += " id=\"" + id + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + customerId + "\"";
            }
            xml += " reportGroup=\"" + reportGroup + "\">";
            if (litleTxnIdSet) xml += "\r\n<litleTxnId>" + litleTxnIdField + "</litleTxnId>";
            if (echeck != null) xml += "\r\n<echeck>" + echeck.Serialize() + "</echeck>";
            else if (token != null) xml += "\r\n<echeckToken>" + token.Serialize() + "</echeckToken>";
            if (merchantData != null) { xml += "\r\n<merchantData>" + merchantData.Serialize() + "\r\n</merchantData>"; }
            xml += "\r\n</echeckRedeposit>";
            return xml;
        }
    }

    public partial class authorization : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
                this.litleTxnIdSet = true;
            }
        }
        public string orderId;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return this.secondaryAmountField; }
            set { this.secondaryAmountField = value; this.secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return this.surchargeAmountField; }
            set { this.surchargeAmountField = value; this.surchargeAmountSet = true; }
        }
        public orderSourceType orderSource;
        public customerInfo customerInfo;
        public contact billToAddress;
        public contact shipToAddress;
        public cardType card;
        public mposType mpos;
        public payPal paypal;
        public cardTokenType token;
        public cardPaypageType paypage;
        public applepayType applepay;
        public billMeLaterRequest billMeLaterRequest;
        public fraudCheckType cardholderAuthentication;
        public processingInstructions processingInstructions;
        public pos pos;
        public customBilling customBilling;
        private govtTaxTypeEnum taxTypeField;
        private bool taxTypeSet;
        public govtTaxTypeEnum taxType
        {
            get { return this.taxTypeField; }
            set { this.taxTypeField = value; this.taxTypeSet = true; }
        }
        private processingTypeEnum processingTypeField;
        private bool processingTypeSet;
        public processingTypeEnum processingType
        {
            get { return this.processingTypeField; }
            set { this.processingTypeField = value;  this.processingTypeSet = true; }
        }
        public enhancedData enhancedData;
        public amexAggregatorData amexAggregatorData;
        private bool allowPartialAuthField;
        private bool allowPartialAuthSet;
        public bool allowPartialAuth
        {
            get
            {
                return this.allowPartialAuthField;
            }
            set
            {
                this.allowPartialAuthField = value;
                this.allowPartialAuthSet = true;
            }
        }
        public healthcareIIAS healthcareIIAS;
        public filteringType filtering;
        public merchantDataType merchantData;
        public recyclingRequestType recyclingRequest;
        private bool fraudFilterOverrideField;
        private bool fraudFilterOverrideSet;
        public bool fraudFilterOverride
        {
            get
            {
                return this.fraudFilterOverrideField;
            }
            set
            {
                this.fraudFilterOverrideField = value;
                this.fraudFilterOverrideSet = true;
            }
        }
        public recurringRequest recurringRequest;
        private bool debtRepaymentField;
        private bool debtRepaymentSet;
        public bool debtRepayment
        {
            get
            {
                return this.debtRepaymentField;
            }
            set
            {
                this.debtRepaymentField = value;
                this.debtRepaymentSet = true;
            }
        }
        public advancedFraudChecksType advancedFraudChecks;
        public wallet wallet;
        private string originalNetworkTransactionIdField;
        private bool originalNetworkTransactionIdSet;
        public string originalNetworkTransactionId
        {
            get
            {
                return this.originalNetworkTransactionIdField;
            }
            set
            {
                this.originalNetworkTransactionIdField = value;
                this.originalNetworkTransactionIdSet = true;
            }
        }
        private long originalTransactionAmountField;
        private bool originalTransactionAmountSet;
        public long originalTransactionAmount
        {
            get
            {
                return this.originalTransactionAmountField;
            }
            set
            {
                this.originalTransactionAmountField = value;
                this.originalTransactionAmountSet = true;
            }
        }

        public override String Serialize()
        {
            string xml = "\r\n<authorization";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnIdField + "</litleTxnId>";
            }
            else
            {
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                xml += "\r\n<amount>" + amount + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
                if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";

                if (customerInfo != null)
                {
                    xml += "\r\n<customerInfo>" + customerInfo.Serialize() + "\r\n</customerInfo>";
                }
                if (billToAddress != null)
                {
                    xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>";
                }
                if (shipToAddress != null)
                {
                    xml += "\r\n<shipToAddress>" + shipToAddress.Serialize() + "\r\n</shipToAddress>";
                }
                if (card != null)
                {
                    xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
                }
                else if (paypal != null)
                {
                    xml += "\r\n<paypal>" + paypal.Serialize() + "\r\n</paypal>";
                }
                else if (mpos != null)
                {
                    xml += "\r\n<mpos>" + mpos.Serialize() + "\r\n</mpos>";
                }
                else if (token != null)
                {
                    xml += "\r\n<token>" + token.Serialize() + "\r\n</token>";
                }
                else if (paypage != null)
                {
                    xml += "\r\n<paypage>" + paypage.Serialize() + "\r\n</paypage>";
                }
                else if (applepay != null)
                {
                    xml += "\r\n<applepay>" + applepay.Serialize() + "\r\n</applepay>";
                }
                if (billMeLaterRequest != null)
                {
                    xml += "\r\n<billMeLaterRequest>" + billMeLaterRequest.Serialize() + "\r\n</billMeLaterRequest>";
                }
                if (cardholderAuthentication != null)
                {
                    xml += "\r\n<cardholderAuthentication>" + cardholderAuthentication.Serialize() + "\r\n</cardholderAuthentication>";
                }
                if (processingInstructions != null)
                {
                    xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
                }
                if (pos != null)
                {
                    xml += "\r\n<pos>" + pos.Serialize() + "\r\n</pos>";
                }
                if (customBilling != null)
                {
                    xml += "\r\n<customBilling>" + customBilling.Serialize() + "\r\n</customBilling>";
                }
                if (taxTypeSet)
                {
                    xml += "\r\n<taxType>" + taxTypeField + "</taxType>";
                }
                if (enhancedData != null)
                {
                    xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
                }
                if (amexAggregatorData != null)
                {
                    xml += "\r\n<amexAggregatorData>" + amexAggregatorData.Serialize() + "\r\n</amexAggregatorData>";
                }
                if (allowPartialAuthSet)
                {
                    xml += "\r\n<allowPartialAuth>" + allowPartialAuthField.ToString().ToLower() + "</allowPartialAuth>";
                }
                if (healthcareIIAS != null)
                {
                    xml += "\r\n<healthcareIIAS>" + healthcareIIAS.Serialize() + "\r\n</healthcareIIAS>";
                }
                if (filtering != null)
                {
                    xml += "\r\n<filtering>" + filtering.Serialize() + "\r\n</filtering>";
                }
                if (merchantData != null)
                {
                    xml += "\r\n<merchantData>" + merchantData.Serialize() + "\r\n</merchantData>";
                }
                if (recyclingRequest != null)
                {
                    xml += "\r\n<recyclingRequest>" + recyclingRequest.Serialize() + "\r\n</recyclingRequest>";
                }
                if (fraudFilterOverrideSet) xml += "\r\n<fraudFilterOverride>" + fraudFilterOverrideField.ToString().ToLower() + "</fraudFilterOverride>";
                if (recurringRequest != null)
                {
                    xml += "\r\n<recurringRequest>" + recurringRequest.Serialize() + "\r\n</recurringRequest>";
                }
                if (debtRepaymentSet) xml += "\r\n<debtRepayment>" + debtRepayment.ToString().ToLower() + "</debtRepayment>";
                if (advancedFraudChecks != null)
                {
                    xml += "\r\n<advancedFraudChecks>" + advancedFraudChecks.Serialize() + "\r\n</advancedFraudChecks>";
                }
                if (wallet != null)
                {
                    xml += "\r\n<wallet>" + wallet.Serialize() + "\r\n</wallet>";
                }
                if (processingTypeSet)
                {
                    xml += "\r\n<processingType>" + processingType + "</processingType>";
                }
                if (originalNetworkTransactionIdSet)
                {
                    xml += "\r\n<originalNetworkTransactionId>" + originalNetworkTransactionId + "</originalNetworkTransactionId>";
                }
                if (originalTransactionAmountSet)
                {
                    xml += "\r\n<originalTransactionAmount>" + originalTransactionAmount + "</originalTransactionAmount>";
                }

            }
            
            xml += "\r\n</authorization>";
            return xml;
        }
    }

    public partial class sale : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
                this.litleTxnIdSet = true;
            }
        }
        public string orderId;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return this.secondaryAmountField; }
            set { this.secondaryAmountField = value; this.secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return this.surchargeAmountField; }
            set { this.surchargeAmountField = value; this.surchargeAmountSet = true; }
        }
        public orderSourceType orderSource;
        public customerInfo customerInfo;
        public contact billToAddress;
        public contact shipToAddress;
        public cardType card;
        public mposType mpos;
        public payPal paypal;
        public cardTokenType token;
        public cardPaypageType paypage;
        public applepayType applepay;
        public sepaDirectDebitType sepaDirectDebit;
        public billMeLaterRequest billMeLaterRequest;
        public fraudCheckType cardholderAuthentication;
        public customBilling customBilling;
        private govtTaxTypeEnum taxTypeField;
        private bool taxTypeSet;
        public govtTaxTypeEnum taxType
        {
            get { return this.taxTypeField; }
            set { this.taxTypeField = value; this.taxTypeSet = true; }
        }
        public enhancedData enhancedData;
        public processingInstructions processingInstructions;
        public pos pos;
        private bool payPalOrderCompleteField;
        private bool payPalOrderCompleteSet;
        public bool payPalOrderComplete
        {
            get { return this.payPalOrderCompleteField; }
            set { this.payPalOrderCompleteField = value; this.payPalOrderCompleteSet = true; }
        }
        public string payPalNotes;
        public amexAggregatorData amexAggregatorData;
        private bool allowPartialAuthField;
        private bool allowPartialAuthSet;
        public bool allowPartialAuth
        {
            get
            {
                return this.allowPartialAuthField;
            }
            set
            {
                this.allowPartialAuthField = value;
                this.allowPartialAuthSet = true;
            }
        }
        public healthcareIIAS healthcareIIAS;
        public filteringType filtering;
        public merchantDataType merchantData;
        public recyclingRequestType recyclingRequest;
        private bool fraudFilterOverrideField;
        private bool fraudFilterOverrideSet;
        public bool fraudFilterOverride
        {
            get
            {
                return this.fraudFilterOverrideField;
            }
            set
            {
                this.fraudFilterOverrideField = value;
                this.fraudFilterOverrideSet = true;
            }
        }
        public recurringRequest recurringRequest;
        public litleInternalRecurringRequest litleInternalRecurringRequest;
        private bool debtRepaymentField;
        private bool debtRepaymentSet;
        public bool debtRepayment
        {
            get
            {
                return this.debtRepaymentField;
            }
            set
            {
                this.debtRepaymentField = value;
                this.debtRepaymentSet = true;
            }
        }
        public advancedFraudChecksType advancedFraudChecks;
        public wallet wallet;
        private processingTypeEnum processingTypeField;
        private bool processingTypeSet;
        public processingTypeEnum processingType
        {
            get { return this.processingTypeField; }
            set { this.processingTypeField = value; this.processingTypeSet = true; }
        }
        private string originalNetworkTransactionIdField;
        private bool originalNetworkTransactionIdSet;
        public string originalNetworkTransactionId
        {
            get
            {
                return this.originalNetworkTransactionIdField;
            }
            set
            {
                this.originalNetworkTransactionIdField = value;
                this.originalNetworkTransactionIdSet = true;
            }
        }
        private long originalTransactionAmountField;
        private bool originalTransactionAmountSet;
        public long originalTransactionAmount
        {
            get
            {
                return this.originalTransactionAmountField;
            }
            set
            {
                this.originalTransactionAmountField = value;
                this.originalTransactionAmountSet = true;
            }
        }

        public override String Serialize()
        {
            string xml = "\r\n<sale";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (litleTxnIdSet) xml += "\r\n<litleTxnId>" + litleTxnIdField + "</litleTxnId>";
            xml += "\r\n<orderId>" + orderId + "</orderId>";
            xml += "\r\n<amount>" + amount + "</amount>";
            if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
            if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            if (customerInfo != null)
            {
                xml += "\r\n<customerInfo>" + customerInfo.Serialize() + "\r\n</customerInfo>";
            }
            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>";
            }
            if (shipToAddress != null)
            {
                xml += "\r\n<shipToAddress>" + shipToAddress.Serialize() + "\r\n</shipToAddress>";
            }
            if (card != null)
            {
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            else if (paypal != null)
            {
                xml += "\r\n<paypal>" + paypal.Serialize() + "\r\n</paypal>";
            }
            else if (token != null)
            {
                xml += "\r\n<token>" + token.Serialize() + "\r\n</token>";
            }
            else if (mpos != null)
            {
                xml += "\r\n<mpos>" + mpos.Serialize() + "</mpos>";
            }
            else if (paypage != null)
            {
                xml += "\r\n<paypage>" + paypage.Serialize() + "\r\n</paypage>";
            }
            else if (applepay != null)
            {
                xml += "\r\n<applepay>" + applepay.Serialize() + "\r\n</applepay>";
            }
            else if (sepaDirectDebit != null)
            {
                xml += "\r\n<sepaDirectDebit>" + sepaDirectDebit.Serialize() + "\r\n</sepaDirectDebit>";
            }
            if (billMeLaterRequest != null)
            {
                xml += "\r\n<billMeLaterRequest>" + billMeLaterRequest.Serialize() + "\r\n</billMeLaterRequest>";
            }
            if (cardholderAuthentication != null)
            {
                xml += "\r\n<cardholderAuthentication>" + cardholderAuthentication.Serialize() + "\r\n</cardholderAuthentication>";
            }
            if (customBilling != null)
            {
                xml += "\r\n<customBilling>" + customBilling.Serialize() + "\r\n</customBilling>";
            }
            if (taxTypeSet)
            {
                xml += "\r\n<taxType>" + taxTypeField + "</taxType>";
            }
            if (enhancedData != null)
            {
                xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            }
            if (processingInstructions != null)
            {
                xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
            }
            if (pos != null)
            {
                xml += "\r\n<pos>" + pos.Serialize() + "\r\n</pos>";
            }
            if (payPalOrderCompleteSet) xml += "\r\n<payPalOrderCompleteSet>" + payPalOrderCompleteField.ToString().ToLower() + "</payPalOrderCompleteSet>";
            if (payPalNotes != null) xml += "\r\n<payPalNotes>" + SecurityElement.Escape(payPalNotes) + "</payPalNotes>";
            if (amexAggregatorData != null)
            {
                xml += "\r\n<amexAggregatorData>" + amexAggregatorData.Serialize() + "\r\n</amexAggregatorData>";
            }
            if (allowPartialAuthSet)
            {
                xml += "\r\n<allowPartialAuth>" + allowPartialAuthField.ToString().ToLower() + "</allowPartialAuth>";
            }
            if (healthcareIIAS != null)
            {
                xml += "\r\n<healthcareIIAS>" + healthcareIIAS.Serialize() + "\r\n</healthcareIIAS>";
            }
            if (filtering != null)
            {
                xml += "\r\n<filtering>" + filtering.Serialize() + "\r\n</filtering>";
            }
            if (merchantData != null)
            {
                xml += "\r\n<merchantData>" + merchantData.Serialize() + "\r\n</merchantData>";
            }
            if (recyclingRequest != null)
            {
                xml += "\r\n<recyclingRequest>" + recyclingRequest.Serialize() + "\r\n</recyclingRequest>";
            }
            if (fraudFilterOverrideSet) xml += "\r\n<fraudFilterOverride>" + fraudFilterOverrideField.ToString().ToLower() + "</fraudFilterOverride>";
            if (recurringRequest != null)
            {
                xml += "\r\n<recurringRequest>" + recurringRequest.Serialize() + "\r\n</recurringRequest>";
            }
            if (litleInternalRecurringRequest != null)
            {
                xml += "\r\n<litleInternalRecurringRequest>" + litleInternalRecurringRequest.Serialize() + "\r\n</litleInternalRecurringRequest>";
            }
            if (debtRepaymentSet) xml += "\r\n<debtRepayment>" + debtRepayment.ToString().ToLower() + "</debtRepayment>";
            if (advancedFraudChecks != null) xml += "\r\n<advancedFraudChecks>" + advancedFraudChecks.Serialize() + "\r\n</advancedFraudChecks>";
            if (wallet != null)
            {
                xml += "\r\n<wallet>" + wallet.Serialize() + "\r\n</wallet>";
            }
            if (processingTypeSet)
            {
                xml += "\r\n<processingType>" + processingType + "</processingType>";
            }
            if (originalNetworkTransactionIdSet)
            {
                xml += "\r\n<originalNetworkTransactionId>" + originalNetworkTransactionId + "</originalNetworkTransactionId>";
            }
            if (originalTransactionAmountSet)
            {
                xml += "\r\n<originalTransactionAmount>" + originalTransactionAmount + "</originalTransactionAmount>";
            }
            xml += "\r\n</sale>";
            return xml;
        }
    }

    public partial class forceCapture : transactionTypeWithReportGroup
    {
        public string orderId;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return this.secondaryAmountField; }
            set { this.secondaryAmountField = value; this.secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return this.surchargeAmountField; }
            set { this.surchargeAmountField = value; this.surchargeAmountSet = true; }
        }
        public orderSourceType orderSource;
        public contact billToAddress;
        public cardType card;
        public mposType mpos;
        public cardTokenType token;
        public cardPaypageType paypage;
        public customBilling customBilling;
        private govtTaxTypeEnum taxTypeField;
        private bool taxTypeSet;
        public govtTaxTypeEnum taxType
        {
            get { return this.taxTypeField; }
            set { this.taxTypeField = value; this.taxTypeSet = true; }
        }
        public enhancedData enhancedData;
        public processingInstructions processingInstructions;
        public pos pos;
        public amexAggregatorData amexAggregatorData;
        public merchantDataType merchantData;
        private bool debtRepaymentField;
        private bool debtRepaymentSet;
        public bool debtRepayment
        {
            get
            {
                return this.debtRepaymentField;
            }
            set
            {
                this.debtRepaymentField = value;
                this.debtRepaymentSet = true;
            }
        }
        private processingTypeEnum processingTypeField;
        private bool processingTypeSet;
        public processingTypeEnum processingType
        {
            get { return this.processingTypeField; }
            set { this.processingTypeField = value; this.processingTypeSet = true; }
        }

        public override String Serialize()
        {
            string xml = "\r\n<forceCapture";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<amount>" + amount + "</amount>";
            if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
            if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>";
            }
            if (card != null)
            {
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            else if (token != null)
            {
                xml += "\r\n<token>" + token.Serialize() + "\r\n</token>";
            }
            else if (mpos != null)
            {
                xml += "\r\n<mpos>" + mpos.Serialize() + "</mpos>";
            }
            else if (paypage != null)
            {
                xml += "\r\n<paypage>" + paypage.Serialize() + "\r\n</paypage>";
            }
            if (customBilling != null)
            {
                xml += "\r\n<customBilling>" + customBilling.Serialize() + "\r\n</customBilling>";
            }
            if (taxTypeSet)
            {
                xml += "\r\n<taxType>" + taxTypeField + "</taxType>";
            }
            if (enhancedData != null)
            {
                xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            }
            if (processingInstructions != null)
            {
                xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
            }
            if (pos != null)
            {
                xml += "\r\n<pos>" + pos.Serialize() + "\r\n</pos>";
            }
            if (amexAggregatorData != null)
            {
                xml += "\r\n<amexAggregatorData>" + amexAggregatorData.Serialize() + "\r\n</amexAggregatorData>";
            }
            if (merchantData != null)
            {
                xml += "\r\n<merchantData>" + merchantData.Serialize() + "\r\n</merchantData>";
            }
            if (debtRepaymentSet)
            {
                xml += "\r\n<debtRepayment>" + debtRepayment.ToString().ToLower() + "</debtRepayment>";
            }
            if (processingTypeSet)
            {
                xml += "\r\n<processingType>" + processingType + "</processingType>";
            }
            xml += "\r\n</forceCapture>";
            return xml;
        }
    }

    public partial class captureGivenAuth : transactionTypeWithReportGroup
    {
        public string orderId;
        public authInformation authInformation;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return this.secondaryAmountField; }
            set { this.secondaryAmountField = value; this.secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return this.surchargeAmountField; }
            set { this.surchargeAmountField = value; this.surchargeAmountSet = true; }
        }
        public orderSourceType orderSource;
        public contact billToAddress;
        public contact shipToAddress;
        public cardType card;
        public mposType mpos;
        public cardTokenType token;
        public cardPaypageType paypage;
        public customBilling customBilling;
        private govtTaxTypeEnum taxTypeField;
        private bool taxTypeSet;
        public govtTaxTypeEnum taxType
        {
            get { return this.taxTypeField; }
            set { this.taxTypeField = value; this.taxTypeSet = true; }
        }
        public billMeLaterRequest billMeLaterRequest;
        public enhancedData enhancedData;
        public processingInstructions processingInstructions;
        public pos pos;
        public amexAggregatorData amexAggregatorData;
        public merchantDataType merchantData;
        private bool debtRepaymentField;
        private bool debtRepaymentSet;
        public bool debtRepayment
        {
            get
            {
                return this.debtRepaymentField;
            }
            set
            {
                this.debtRepaymentField = value;
                this.debtRepaymentSet = true;
            }
        }
        private processingTypeEnum processingTypeField;
        private bool processingTypeSet;
        public processingTypeEnum processingType
        {
            get { return this.processingTypeField; }
            set { this.processingTypeField = value; this.processingTypeSet = true; }
        }
        private string originalNetworkTransactionIdField;
        private bool originalNetworkTransactionIdSet;
        public string originalNetworkTransactionId
        {
            get
            {
                return this.originalNetworkTransactionIdField;
            }
            set
            {
                this.originalNetworkTransactionIdField = value;
                this.originalNetworkTransactionIdSet = true;
            }
        }
        private long originalTransactionAmountField;
        private bool originalTransactionAmountSet;
        public long originalTransactionAmount
        {
            get
            {
                return this.originalTransactionAmountField;
            }
            set
            {
                this.originalTransactionAmountField = value;
                this.originalTransactionAmountSet = true;
            }
        }

        public override String Serialize()
        {
            string xml = "\r\n<captureGivenAuth";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            if (authInformation != null) xml += "\r\n<authInformation>" + authInformation.Serialize() + "\r\n</authInformation>";
            xml += "\r\n<amount>" + amount + "</amount>";
            if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
            if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>";
            }
            if (shipToAddress != null)
            {
                xml += "\r\n<shipToAddress>" + shipToAddress.Serialize() + "\r\n</shipToAddress>";
            }
            if (card != null)
            {
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            else if (token != null)
            {
                xml += "\r\n<token>" + token.Serialize() + "\r\n</token>";
            }
            else if (mpos != null)
            {
                xml += "\r\n<mpos>" + mpos.Serialize() + "</mpos>";
            }
            else if (paypage != null)
            {
                xml += "\r\n<paypage>" + paypage.Serialize() + "\r\n</paypage>";
            }
            if (customBilling != null)
            {
                xml += "\r\n<customBilling>" + customBilling.Serialize() + "\r\n</customBilling>";
            }
            if (taxTypeSet)
            {
                xml += "\r\n<taxType>" + taxTypeField + "</taxType>";
            }
            if (billMeLaterRequest != null)
            {
                xml += "\r\n<billMeLaterRequest>" + billMeLaterRequest.Serialize() + "\r\n</billMeLaterRequest>";
            }
            if (enhancedData != null)
            {
                xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            }
            if (processingInstructions != null)
            {
                xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
            }
            if (pos != null)
            {
                xml += "\r\n<pos>" + pos.Serialize() + "\r\n</pos>";
            }
            if (amexAggregatorData != null)
            {
                xml += "\r\n<amexAggregatorData>" + amexAggregatorData.Serialize() + "\r\n</amexAggregatorData>";
            }
            if (merchantData != null)
            {
                xml += "\r\n<merchantData>" + merchantData.Serialize() + "\r\n</merchantData>";
            }
            if (debtRepaymentSet)
            {
                xml += "\r\n<debtRepayment>" + debtRepayment.ToString().ToLower() + "</debtRepayment>";
            }
            if (processingTypeSet)
            {
                xml += "\r\n<processingType>" + processingType + "</processingType>";
            }
            if (originalNetworkTransactionIdSet)
            {
                xml += "\r\n<originalNetworkTransactionId>" + originalNetworkTransactionId + "</originalNetworkTransactionId>";
            }
            if (originalTransactionAmountSet)
            {
                xml += "\r\n<originalTransactionAmount>" + originalTransactionAmount + "</originalTransactionAmount>";
            }
            xml += "\r\n</captureGivenAuth>";
            return xml;
        }
    }

    public partial class cancelSubscription : recurringTransactionType
    {
        private long subscriptionIdField;
        private bool subscriptionIdSet;
        public long subscriptionId
        {
            get
            {
                return this.subscriptionIdField;
            }
            set
            {
                this.subscriptionIdField = value;
                this.subscriptionIdSet = true;
            }
        }

        public override String Serialize()
        {
            string xml = "\r\n<cancelSubscription>";
            if (subscriptionIdSet) xml += "\r\n<subscriptionId>" + subscriptionIdField + "</subscriptionId>";
            xml += "\r\n</cancelSubscription>";
            return xml;
        }
    }

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum intervalType
    {
        ANNUAL,
        SEMIANNUAL,
        QUARTERLY,
        MONTHLY,
        WEEKLY
    }

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum trialIntervalType
    {
        MONTH,
        DAY
    }

    public partial class createPlan : recurringTransactionType
    {
        public string planCode;
        public string name;

        private string descriptionField;
        private bool descriptionSet;
        public string description
        {
            get { return this.descriptionField; }
            set { this.descriptionField = value; this.descriptionSet = true; }
        }

        public intervalType intervalType;
        public long amount;

        public int numberOfPaymentsField;
        public bool numberOfPaymentsSet;
        public int numberOfPayments
        {
            get { return this.numberOfPaymentsField; }
            set { this.numberOfPaymentsField = value; this.numberOfPaymentsSet = true; }
        }

        public int trialNumberOfIntervalsField;
        public bool trialNumberOfIntervalsSet;
        public int trialNumberOfIntervals
        {
            get { return this.trialNumberOfIntervalsField; }
            set { this.trialNumberOfIntervalsField = value; this.trialNumberOfIntervalsSet = true; }
        }

        private trialIntervalType trialIntervalTypeField;
        private bool trialIntervalTypeSet;
        public trialIntervalType trialIntervalType 
        {
            get { return this.trialIntervalTypeField; }
            set { this.trialIntervalTypeField = value; this.trialIntervalTypeSet = true; }
        }

        private bool activeField;
        private bool activeSet;
        public bool active
        {
            get { return this.activeField; }
            set { this.activeField = value; this.activeSet = true; }
        }

        public override String Serialize()
        {
            string xml = "\r\n<createPlan>";
            xml += "\r\n<planCode>" + SecurityElement.Escape(planCode) + "</planCode>";
            xml += "\r\n<name>" + SecurityElement.Escape(name) + "</name>";
            if (descriptionSet) xml += "\r\n<description>" + SecurityElement.Escape(descriptionField) + "</description>";
            xml += "\r\n<intervalType>" + intervalType + "</intervalType>";
            xml += "\r\n<amount>" + amount + "</amount>";
            if (numberOfPaymentsSet) xml += "\r\n<numberOfPayments>" + numberOfPaymentsField + "</numberOfPayments>";
            if (trialNumberOfIntervalsSet) xml += "\r\n<trialNumberOfIntervals>" + trialNumberOfIntervalsField + "</trialNumberOfIntervals>";
            if (trialIntervalTypeSet) xml += "\r\n<trialIntervalType>" + trialIntervalTypeField + "</trialIntervalType>";
            if (activeSet) xml += "\r\n<active>" + activeField.ToString().ToLower() + "</active>";
            xml += "\r\n</createPlan>";
            return xml;
        }
    }

    public partial class updatePlan : recurringTransactionType
    {
        public string planCode;

        private bool activeField;
        private bool activeSet;
        public bool active
        {
            get { return this.activeField; }
            set { this.activeField = value; this.activeSet = true; }
        }

        public override String Serialize()
        {
            string xml = "\r\n<updatePlan>";
            xml += "\r\n<planCode>" + SecurityElement.Escape(planCode) + "</planCode>";
            if (activeSet) xml += "\r\n<active>" + activeField.ToString().ToLower() + "</active>";
            xml += "\r\n</updatePlan>";
            return xml;
        }
    }
    public partial class updateSubscription : recurringTransactionType
    {
        private long subscriptionIdField;
        private bool subscriptionIdSet;
        public long subscriptionId
        {
            get
            {
                return this.subscriptionIdField;
            }
            set
            {
                this.subscriptionIdField = value;
                this.subscriptionIdSet = true;
            }
        }

        public string planCode;
        public contact billToAddress;
        public cardType card;
        public cardTokenType token;
        public cardPaypageType paypage;
        private DateTime billingDateField;
        private bool billingDateSet;
        public DateTime billingDate
        {
            get
            {
                return this.billingDateField;
            }
            set
            {
                this.billingDateField = value;
                this.billingDateSet = true;
            }
        }

        public List<createDiscount> createDiscounts;
        public List<updateDiscount> updateDiscounts;
        public List<deleteDiscount> deleteDiscounts;
        public List<createAddOn> createAddOns;
        public List<updateAddOn> updateAddOns;
        public List<deleteAddOn> deleteAddOns;

        public updateSubscription()
        {
            createDiscounts = new List<createDiscount>();
            updateDiscounts = new List<updateDiscount>();
            deleteDiscounts = new List<deleteDiscount>();
            createAddOns = new List<createAddOn>();
            updateAddOns = new List<updateAddOn>();
            deleteAddOns = new List<deleteAddOn>();
        }

        public override String Serialize()
        {
            string xml = "\r\n<updateSubscription>";
            if (subscriptionIdSet) xml += "\r\n<subscriptionId>" + subscriptionIdField + "</subscriptionId>";
            if (planCode != null) xml += "\r\n<planCode>" + SecurityElement.Escape(planCode) + "</planCode>";
            if (billToAddress != null) xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>";
            if (card != null) xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            else if (token != null) xml += "\r\n<token>" + token.Serialize() + "\r\n</token>";
            else if (paypage != null) xml += "\r\n<paypage>" + paypage.Serialize() + "\r\n</paypage>";
            if (billingDateSet) xml += "\r\n<billingDate>" + XmlUtil.toXsdDate(billingDateField) + "</billingDate>";
            foreach (createDiscount createDiscount in createDiscounts) 
            {
                xml += "\r\n<createDiscount>" + createDiscount.Serialize() + "\r\n</createDiscount>";
            }
            foreach (updateDiscount updateDiscount in updateDiscounts)
            {
                xml += "\r\n<updateDiscount>" + updateDiscount.Serialize() + "\r\n</updateDiscount>";
            }
            foreach (deleteDiscount deleteDiscount in deleteDiscounts)
            {
                xml += "\r\n<deleteDiscount>" + deleteDiscount.Serialize() + "\r\n</deleteDiscount>";
            }
            foreach (createAddOn createAddOn in createAddOns)
            {
                xml += "\r\n<createAddOn>" + createAddOn.Serialize() + "\r\n</createAddOn>";
            }
            foreach (updateAddOn updateAddOn in updateAddOns)
            {
                xml += "\r\n<updateAddOn>" + updateAddOn.Serialize() + "\r\n</updateAddOn>";
            }
            foreach (deleteAddOn deleteAddOn in deleteAddOns)
            {
                xml += "\r\n<deleteAddOn>" + deleteAddOn.Serialize() + "\r\n</deleteAddOn>";
            }
            xml += "\r\n</updateSubscription>";
            return xml;
        }
    }

    public partial class fraudResult
    {
        public string Serialize()
        {
            string xml = "";
            if (avsResult != null) xml += "\r\n<avsResult>" + SecurityElement.Escape(avsResult) + "</avsResult>";
            if (cardValidationResult != null) xml += "\r\n<cardValidationResult>" + SecurityElement.Escape(cardValidationResult) + "</cardValidationResult>";
            if (authenticationResult != null) xml += "\r\n<authenticationResult>" + SecurityElement.Escape(authenticationResult) + "</authenticationResult>";
            if (advancedAVSResult != null) xml += "\r\n<advancedAVSResult>" + SecurityElement.Escape(advancedAVSResult) + "</advancedAVSResult>";
            return xml;
        }
    }

    public partial class authInformation
    {
        public DateTime authDate;
        public string authCode;
        public fraudResult fraudResult;
        private long authAmountField;
        private bool authAmountSet;
        public long authAmount
        {
            get { return this.authAmountField; }
            set { this.authAmountField = value; this.authAmountSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (authDate != null) xml += "\r\n<authDate>" + XmlUtil.toXsdDate(authDate) + "</authDate>";
            if (authCode != null) xml += "\r\n<authCode>" + SecurityElement.Escape(authCode) + "</authCode>";
            if (fraudResult != null) xml += "\r\n<fraudResult>" + fraudResult.Serialize() + "</fraudResult>";
            if (authAmountSet) xml += "\r\n<authAmount>" + authAmountField + "</authAmount>";
            return xml;
        }
    }

    public class XmlUtil
    {
        public static string toXsdDate(DateTime dateTime)
        {
            string year = dateTime.Year.ToString();
            string month = dateTime.Month.ToString();
            if (dateTime.Month < 10)
            {
                month = "0" + month;
            }
            string day = dateTime.Day.ToString();
            if (dateTime.Day < 10)
            {
                day = "0" + day;
            }
            return year + "-" + month + "-" + day;
        }
    }

    public partial class recyclingRequestType
    {
        private recycleByTypeEnum recycleByField;
        private bool recycleBySet;
        public recycleByTypeEnum recycleBy
        {
            get { return this.recycleByField; }
            set { this.recycleByField = value; this.recycleBySet = true; }
        }
        public string recycleId;

        public string Serialize()
        {
            string xml = "";
            if (recycleBySet) xml += "\r\n<recycleBy>" + recycleByField + "</recycleBy>";
            if (recycleId != null) xml += "\r\n<recycleId>" + SecurityElement.Escape(recycleId) + "</recycleId>";
            return xml;
        }
    }

    public partial class litleInternalRecurringRequest
    {
        public string subscriptionId;
        public string recurringTxnId;

        private bool finalPaymentField;
        private bool finalPaymentSet;
        public bool finalPayment
        {
            get { return this.finalPaymentField; }
            set { this.finalPaymentField = value; this.finalPaymentSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (subscriptionId != null) xml += "\r\n<subscriptionId>" + SecurityElement.Escape(subscriptionId) + "</subscriptionId>";
            if (recurringTxnId != null) xml += "\r\n<recurringTxnId>" + SecurityElement.Escape(recurringTxnId) + "</recurringTxnId>";
            if(finalPaymentSet) xml += "\r\n<finalPayment>" + finalPaymentField.ToString().ToLower() + "</finalPayment>";
            return xml;
        }
    }

    public partial class createDiscount
    {
        public string discountCode;
        public string name;
        public long amount;
        public DateTime startDate;
        public DateTime endDate;

        public string Serialize()
        {
            string xml = "";
            xml += "\r\n<discountCode>" + SecurityElement.Escape(discountCode) + "</discountCode>";
            xml += "\r\n<name>" + SecurityElement.Escape(name) + "</name>";
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<startDate>" + XmlUtil.toXsdDate(startDate) + "</startDate>";
            xml += "\r\n<endDate>" + XmlUtil.toXsdDate(endDate) + "</endDate>";
            return xml;
        }
    }

    public partial class updateDiscount
    {
        public string discountCode;

        private string nameField;
        private bool nameSet;
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; this.nameSet = true; }
        }

        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return this.amountField; }
            set { this.amountField = value; this.amountSet = true; }
        }

        private DateTime startDateField;
        private bool startDateSet;
        public DateTime startDate
        {
            get { return this.startDateField; }
            set { this.startDateField = value; this.startDateSet = true; }
        }

        private DateTime endDateField;
        private bool endDateSet;
        public DateTime endDate
        {
            get { return this.endDateField; }
            set { this.endDateField = value; this.endDateSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            xml += "\r\n<discountCode>" + SecurityElement.Escape(discountCode) + "</discountCode>";
            if (nameSet) xml += "\r\n<name>" + SecurityElement.Escape(nameField) + "</name>";
            if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
            if (startDateSet) xml += "\r\n<startDate>" + XmlUtil.toXsdDate(startDateField) + "</startDate>";
            if (endDateSet) xml += "\r\n<endDate>" + XmlUtil.toXsdDate(endDateField) + "</endDate>";
            return xml;
        }
    }

    public partial class deleteDiscount
    {
        public string discountCode;

        public string Serialize()
        {
            string xml = "";
            xml += "\r\n<discountCode>" + SecurityElement.Escape(discountCode) + "</discountCode>";
            return xml;
        }
    }

    public partial class createAddOn
    {
        public string addOnCode;
        public string name;
        public long amount;
        public DateTime startDate;
        public DateTime endDate;

        public string Serialize()
        {
            string xml = "";
            xml += "\r\n<addOnCode>" + SecurityElement.Escape(addOnCode) + "</addOnCode>";
            xml += "\r\n<name>" + SecurityElement.Escape(name) + "</name>";
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<startDate>" + XmlUtil.toXsdDate(startDate) + "</startDate>";
            xml += "\r\n<endDate>" + XmlUtil.toXsdDate(endDate) + "</endDate>";
            return xml;
        }
    }

    public partial class updateAddOn
    {
        public string addOnCode;

        private string nameField;
        private bool nameSet;
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; this.nameSet = true; }
        }

        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return this.amountField; }
            set { this.amountField = value; this.amountSet = true; }
        }

        private DateTime startDateField;
        private bool startDateSet;
        public DateTime startDate
        {
            get { return this.startDateField; }
            set { this.startDateField = value; this.startDateSet = true; }
        }

        private DateTime endDateField;
        private bool endDateSet;
        public DateTime endDate
        {
            get { return this.endDateField; }
            set { this.endDateField = value; this.endDateSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            xml += "\r\n<addOnCode>" + SecurityElement.Escape(addOnCode) + "</addOnCode>";
            if (nameSet) xml += "\r\n<name>" + SecurityElement.Escape(nameField) + "</name>";
            if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
            if (startDateSet) xml += "\r\n<startDate>" + XmlUtil.toXsdDate(startDateField) + "</startDate>";
            if (endDateSet) xml += "\r\n<endDate>" + XmlUtil.toXsdDate(endDateField) + "</endDate>";
            return xml;
        }
    }

    public partial class deleteAddOn
    {
        public string addOnCode;

        public string Serialize()
        {
            string xml = "";
            xml += "\r\n<addOnCode>" + SecurityElement.Escape(addOnCode) + "</addOnCode>";
            return xml;
        }
    }


    public partial class subscription
    {
        public string planCode;
        private bool numberOfPaymentsSet;
        private int numberOfPaymentsField;
        public int numberOfPayments
        {
            get { return this.numberOfPaymentsField; }
            set { this.numberOfPaymentsField = value; this.numberOfPaymentsSet = true; }
        }
        private bool startDateSet;
        private DateTime startDateField;
        public DateTime startDate
        {
            get { return this.startDateField; }
            set { this.startDateField = value; this.startDateSet = true; }
        }
        private bool amountSet;
        private long amountField;
        public long amount
        {
            get { return this.amountField; }
            set { this.amountField = value; this.amountSet = true; }
        }

        public List<createDiscount> createDiscounts;
        public List<createAddOn> createAddOns;

        public subscription()
        {
            createDiscounts = new List<createDiscount>();
            createAddOns = new List<createAddOn>();
        }


        public string Serialize()
        {
            string xml = "";
            xml += "\r\n<planCode>" + planCode + "</planCode>";
            if(numberOfPaymentsSet) xml += "\r\n<numberOfPayments>" + numberOfPayments + "</numberOfPayments>";
            if (startDateSet) xml += "\r\n<startDate>" + XmlUtil.toXsdDate(startDateField) + "</startDate>";
            if(amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
            foreach(createDiscount createDiscount in createDiscounts) 
            {
                xml += "\r\n<createDiscount>" + createDiscount.Serialize() + "\r\n</createDiscount>";
            }
            foreach (createAddOn createAddOn in createAddOns)
            {
                xml += "\r\n<createAddOn>" + createAddOn.Serialize() + "\r\n</createAddOn>";
            }

            return xml;
        }
    }





    public partial class filteringType
    {
        private bool prepaidField;
        private bool prepaidSet;
        public bool prepaid
        {
            get { return this.prepaidField; }
            set { this.prepaidField = value; this.prepaidSet = true; }
        }

        private bool internationalField;
        private bool internationalSet;
        public bool international
        {
            get { return this.internationalField; }
            set { this.internationalField = value; this.internationalSet = true; }
        }

        private bool chargebackField;
        private bool chargebackSet;
        public bool chargeback
        {
            get { return this.chargebackField; }
            set { this.chargebackField = value; this.chargebackSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (prepaidSet) xml += "\r\n<prepaid>" + prepaidField.ToString().ToLower() + "</prepaid>";
            if (internationalSet) xml += "\r\n<international>" + internationalField.ToString().ToLower() + "</international>";
            if (chargebackSet) xml += "\r\n<chargeback>" + chargebackField.ToString().ToLower() + "</chargeback>";
            return xml;
        }

    }

    public partial class healthcareIIAS
    {
        public healthcareAmounts healthcareAmounts;
        private IIASFlagType IIASFlagField;
        private bool IIASFlagSet;
        public IIASFlagType IIASFlag
        {
            get { return this.IIASFlagField; }
            set { this.IIASFlagField = value; this.IIASFlagSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (healthcareAmounts != null) xml += "\r\n<healthcareAmounts>" + healthcareAmounts.Serialize() + "</healthcareAmounts>";
            if (IIASFlagSet) xml += "\r\n<IIASFlag>" + IIASFlagField + "</IIASFlag>";
            return xml;
        }
    }

    public partial class recurringRequest
    {
        public subscription subscription;

        public string Serialize()
        {
            string xml = "";
            if (subscription != null) xml += "\r\n<subscription>" + subscription.Serialize() + "\r\n</subscription>";
            return xml;
        }
    }


    public partial class healthcareAmounts
    {
        private int totalHealthcareAmountField;
        private bool totalHealthcareAmountSet;
        public int totalHealthcareAmount
        {
            get { return this.totalHealthcareAmountField; }
            set { this.totalHealthcareAmountField = value; this.totalHealthcareAmountSet = true; }
        }

        private int RxAmountField;
        private bool RxAmountSet;
        public int RxAmount
        {
            get { return this.RxAmountField; }
            set { this.RxAmountField = value; this.RxAmountSet = true; }
        }

        private int visionAmountField;
        private bool visionAmountSet;
        public int visionAmount
        {
            get { return this.visionAmountField; }
            set { this.visionAmountField = value; this.visionAmountSet = true; }
        }

        private int clinicOtherAmountField;
        private bool clinicOtherAmountSet;
        public int clinicOtherAmount
        {
            get { return this.clinicOtherAmountField; }
            set { this.clinicOtherAmountField = value; this.clinicOtherAmountSet = true; }
        }

        private int dentalAmountField;
        private bool dentalAmountSet;
        public int dentalAmount
        {
            get { return this.dentalAmountField; }
            set { this.dentalAmountField = value; this.dentalAmountSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (totalHealthcareAmountSet) xml += "\r\n<totalHealthcareAmount>" + totalHealthcareAmountField + "</totalHealthcareAmount>";
            if (RxAmountSet) xml += "\r\n<RxAmount>" + RxAmountField + "</RxAmount>";
            if (visionAmountSet) xml += "\r\n<visionAmount>" + visionAmountField + "</visionAmount>";
            if (clinicOtherAmountSet) xml += "\r\n<clinicOtherAmount>" + clinicOtherAmountField + "</clinicOtherAmount>";
            if (dentalAmountSet) xml += "\r\n<dentalAmount>" + dentalAmountField + "</dentalAmount>";
            return xml;
        }
    }

    public sealed class orderSourceType
    {
        public static readonly orderSourceType ecommerce = new orderSourceType("ecommerce");
        public static readonly orderSourceType installment = new orderSourceType("installment");
        public static readonly orderSourceType mailorder = new orderSourceType("mailorder");
        public static readonly orderSourceType recurring = new orderSourceType("recurring");
        public static readonly orderSourceType retail = new orderSourceType("retail");
        public static readonly orderSourceType telephone = new orderSourceType("telephone");
        public static readonly orderSourceType item3dsAuthenticated = new orderSourceType("3dsAuthenticated");
        public static readonly orderSourceType item3dsAttempted = new orderSourceType("3dsAttempted");
        public static readonly orderSourceType recurringtel = new orderSourceType("recurringtel");
        public static readonly orderSourceType echeckppd = new orderSourceType("echeckppd");
        public static readonly orderSourceType applepay = new orderSourceType("applepay");
        public static readonly orderSourceType androidpay = new orderSourceType("androidpay");

        private orderSourceType(String value) { this.value = value; }
        public string Serialize() { return value; }
        private string value;
    }

    public partial class contact
    {

        public string name;
        public string firstName;
        public string middleInitial;
        public string lastName;
        public string companyName;
        public string addressLine1;
        public string addressLine2;
        public string addressLine3;
        public string city;
        public string state;
        public string zip;
        private countryTypeEnum countryField;
        private bool countrySpecified;
        public countryTypeEnum country
        {
            get { return this.countryField; }
            set { this.countryField = value; countrySpecified = true; }
        }
        public string email;
        public string phone;

        public string Serialize()
        {
            string xml = "";
            if (name != null) xml += "\r\n<name>" + SecurityElement.Escape(name) + "</name>";
            if (firstName != null) xml += "\r\n<firstName>" + SecurityElement.Escape(firstName) + "</firstName>";
            if (middleInitial != null) xml += "\r\n<middleInitial>" + SecurityElement.Escape(middleInitial) + "</middleInitial>";
            if (lastName != null) xml += "\r\n<lastName>" + SecurityElement.Escape(lastName) + "</lastName>";
            if (companyName != null) xml += "\r\n<companyName>" + SecurityElement.Escape(companyName) + "</companyName>";
            if (addressLine1 != null) xml += "\r\n<addressLine1>" + SecurityElement.Escape(addressLine1) + "</addressLine1>";
            if (addressLine2 != null) xml += "\r\n<addressLine2>" + SecurityElement.Escape(addressLine2) + "</addressLine2>";
            if (addressLine3 != null) xml += "\r\n<addressLine3>" + SecurityElement.Escape(addressLine3) + "</addressLine3>";
            if (city != null) xml += "\r\n<city>" + SecurityElement.Escape(city) + "</city>";
            if (state != null) xml += "\r\n<state>" + SecurityElement.Escape(state) + "</state>";
            if (zip != null) xml += "\r\n<zip>" + SecurityElement.Escape(zip) + "</zip>";
            if (countrySpecified) xml += "\r\n<country>" + countryField + "</country>";
            if (email != null) xml += "\r\n<email>" + SecurityElement.Escape(email) + "</email>";
            if (phone != null) xml += "\r\n<phone>" + SecurityElement.Escape(phone) + "</phone>";
            return xml;
        }
    }

    public enum countryTypeEnum
    {

        /// <remarks/>
        USA,
        AF,
        AX,
        AL,
        DZ,
        AS,
        AD,
        AO,
        AI,
        AQ,
        AG,
        AR,
        AM,
        AW,
        AU,
        AT,
        AZ,
        BS,
        BH,
        BD,
        BB,
        BY,
        BE,
        BZ,
        BJ,
        BM,
        BT,
        BO,
        BQ,
        BA,
        BW,
        BV,
        BR,
        IO,
        BN,
        BG,
        BF,
        BI,
        KH,
        CM,
        CA,
        CV,
        KY,
        CF,
        TD,
        CL,
        CN,
        CX,
        CC,
        CO,
        KM,
        CG,
        CD,
        CK,
        CR,
        CI,
        HR,
        CU,
        CW,
        CY,
        CZ,
        DK,
        DJ,
        DM,
        DO,
        TL,
        EC,
        EG,
        SV,
        GQ,
        ER,
        EE,
        ET,
        FK,
        FO,
        FJ,
        FI,
        FR,
        GF,
        PF,
        TF,
        GA,
        GM,
        GE,
        DE,
        GH,
        GI,
        GR,
        GL,
        GD,
        GP,
        GU,
        GT,
        GG,
        GN,
        GW,
        GY,
        HT,
        HM,
        HN,
        HK,
        HU,
        IS,
        IN,
        ID,
        IR,
        IQ,
        IE,
        IM,
        IL,
        IT,
        JM,
        JP,
        JE,
        JO,
        KZ,
        KE,
        KI,
        KP,
        KR,
        KW,
        KG,
        LA,
        LV,
        LB,
        LS,
        LR,
        LY,
        LI,
        LT,
        LU,
        MO,
        MK,
        MG,
        MW,
        MY,
        MV,
        ML,
        MT,
        MH,
        MQ,
        MR,
        MU,
        YT,
        MX,
        FM,
        MD,
        MC,
        MN,
        MS,
        MA,
        MZ,
        MM,
        NA,
        NR,
        NP,
        NL,
        AN,
        NC,
        NZ,
        NI,
        NE,
        NG,
        NU,
        NF,
        MP,
        NO,
        OM,
        PK,
        PW,
        PS,
        PA,
        PG,
        PY,
        PE,
        PH,
        PN,
        PL,
        PT,
        PR,
        QA,
        RE,
        RO,
        RU,
        RW,
        BL,
        KN,
        LC,
        MF,
        VC,
        WS,
        SM,
        ST,
        SA,
        SN,
        SC,
        SL,
        SG,
        SX,
        SK,
        SI,
        SB,
        SO,
        ZA,
        GS,
        ES,
        LK,
        SH,
        PM,
        SD,
        SR,
        SJ,
        SZ,
        SE,
        CH,
        SY,
        TW,
        TJ,
        TZ,
        TH,
        TG,
        TK,
        TO,
        TT,
        TN,
        TR,
        TM,
        TC,
        TV,
        UG,
        UA,
        AE,
        GB,
        US,
        UM,
        UY,
        UZ,
        VU,
        VA,
        VE,
        VN,
        VG,
        VI,
        WF,
        EH,
        YE,
        ZM,
        ZW,
        RS,
        ME,
    }

    public partial class fraudCheckType
    {
        public String authenticationValue;
        public String authenticationTransactionId;
        public String customerIpAddress;
        private bool authenticatedByMerchantField;
        private bool authenticatedByMerchantSet;
        public bool authenticatedByMerchant
        {
            get { return this.authenticatedByMerchantField; }
            set { this.authenticatedByMerchantField = value; authenticatedByMerchantSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (authenticationValue != null) xml += "\r\n<authenticationValue>" + SecurityElement.Escape(authenticationValue) + "</authenticationValue>";
            if (authenticationTransactionId != null) xml += "\r\n<authenticationTransactionId>" + SecurityElement.Escape(authenticationTransactionId) + "</authenticationTransactionId>";
            if (customerIpAddress != null) xml += "\r\n<customerIpAddress>" + SecurityElement.Escape(customerIpAddress) + "</customerIpAddress>";
            if (authenticatedByMerchantSet) xml += "\r\n<authenticatedByMerchant>" + authenticatedByMerchantField + "</authenticatedByMerchant>";
            return xml;
        }
    }

    public partial class advancedFraudChecksType
    {
        public string threatMetrixSessionId;
        private string customAttribute1Field;
        private bool customAttribute1Set;
        public string customAttribute1 { 
            get { return this.customAttribute1Field; } 
            set { this.customAttribute1Field = value; customAttribute1Set = true; } 
        }
        private string customAttribute2Field;
        private bool customAttribute2Set;
        public string customAttribute2
        {
            get { return this.customAttribute2Field; }
            set { this.customAttribute2Field = value; customAttribute2Set = true; }
        }
        private string customAttribute3Field;
        private bool customAttribute3Set;
        public string customAttribute3
        {
            get { return this.customAttribute3Field; }
            set { this.customAttribute3Field = value; customAttribute3Set = true; }
        }
        private string customAttribute4Field;
        private bool customAttribute4Set;
        public string customAttribute4
        {
            get { return this.customAttribute4Field; }
            set { this.customAttribute4Field = value; customAttribute4Set = true; }
        }
        private string customAttribute5Field;
        private bool customAttribute5Set;
        public string customAttribute5
        {
            get { return this.customAttribute5Field; }
            set { this.customAttribute5Field = value; customAttribute5Set = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (threatMetrixSessionId != null) xml += "\r\n<threatMetrixSessionId>" + SecurityElement.Escape(threatMetrixSessionId) + "</threatMetrixSessionId>";
            if (customAttribute1Set) xml += "\r\n<customAttribute1>" + SecurityElement.Escape(customAttribute1Field) + "</customAttribute1>";
            if (customAttribute2Set) xml += "\r\n<customAttribute2>" + SecurityElement.Escape(customAttribute2Field) + "</customAttribute2>";
            if (customAttribute3Set) xml += "\r\n<customAttribute3>" + SecurityElement.Escape(customAttribute3Field) + "</customAttribute3>";
            if (customAttribute4Set) xml += "\r\n<customAttribute4>" + SecurityElement.Escape(customAttribute4Field) + "</customAttribute4>";
            if (customAttribute5Set) xml += "\r\n<customAttribute5>" + SecurityElement.Escape(customAttribute5Field) + "</customAttribute5>";
            return xml;
        }
    }

    public partial class mposType {
        public string ksn;
        public string formatId;
        public string encryptedTrack;
        public int track1Status;
        public int track2Status;

        public string Serialize()
        {
            string xml = "";
            if (ksn != null)
            {
                xml += "\r\n<ksn>" + ksn + "</ksn>";
            }
            if (formatId != null)
            {
                xml += "\r\n<formatId>" + formatId + "</formatId>";
            }
            if (encryptedTrack != null)
            {
                xml += "\r\n<encryptedTrack>" + SecurityElement.Escape(encryptedTrack) + "</encryptedTrack>";
            }
            if (track1Status == 0 || track1Status == 1 )
            {
                xml += "\r\n<track1Status>" + track1Status + "</track1Status>";
            }
            if (track2Status == 0 || track2Status == 1)
            {
                xml += "\r\n<track2Status>" + track2Status + "</track2Status>";
            }

            return xml;
        }
    }

    public partial class cardType
    {
        public methodOfPaymentTypeEnum type;
        public string number;
        public string expDate;
        public string track;
        public string cardValidationNum;
        public string pin;

        public string Serialize()
        {
            string xml = "";
            if (track == null)
            {
                xml += "\r\n<type>" + methodOfPaymentSerializer.Serialize(type) + "</type>";
                if (number != null)
                {
                    xml += "\r\n<number>" + SecurityElement.Escape(number) + "</number>";
                }
                if (expDate != null)
                {
                    xml += "\r\n<expDate>" + SecurityElement.Escape(expDate) + "</expDate>";
                }
            }
            else
            {
                xml += "\r\n<track>" + SecurityElement.Escape(track) + "</track>";
            }
            if (cardValidationNum != null)
            {
                xml += "\r\n<cardValidationNum>" + SecurityElement.Escape(cardValidationNum) + "</cardValidationNum>";
            }
            if (pin != null)
            {
                xml += "\r\n<pin>" + pin + "</pin>";
            }
            return xml;
        }
    }

    public partial class giftCardCardType
    {
        public methodOfPaymentTypeEnum type;
        public string number;
        public string expDate;
        public string track;
        public string cardValidationNum;
        public string pin;

        public string Serialize()
        {
            string xml = "";
            if (track == null)
            {
                xml += "\r\n<type>" + methodOfPaymentSerializer.Serialize(type) + "</type>";
                if (number != null)
                {
                    xml += "\r\n<number>" + SecurityElement.Escape(number) + "</number>";
                }
                if (expDate != null)
                {
                    xml += "\r\n<expDate>" + SecurityElement.Escape(expDate) + "</expDate>";
                }
            }
            else
            {
                xml += "\r\n<track>" + SecurityElement.Escape(track) + "</track>";
            }
            if (cardValidationNum != null)
            {
                xml += "\r\n<cardValidationNum>" + SecurityElement.Escape(cardValidationNum) + "</cardValidationNum>";
            }
            if (pin != null)
            {
                xml += "\r\n<pin>" + pin + "</pin>";
            }
            return xml;
        }
    }

    public partial class virtualGiftCardType
    {
        public int accountNumberLength
        {
            get { return this.accountNumberLengthField; }
            set { this.accountNumberLengthField = value; accountNumberLengthSet = true; }
        }
        private int accountNumberLengthField;
        private bool accountNumberLengthSet;

        public string giftCardBin;

        public String Serialize()
        {
            String xml = "";
            if (accountNumberLengthSet) xml += "\r\n<accountNumberLength>" + accountNumberLengthField + "</accountNumberLength>";
            if (giftCardBin != null) xml += "\r\n<giftCardBin>" + SecurityElement.Escape(giftCardBin) + "</giftCardBin>";
            return xml;
        }

    }

    public partial class authReversal : transactionTypeWithReportGroup
    {
        public long litleTxnId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return this.amountField; }
            set { this.amountField = value; amountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return this.surchargeAmountField; }
            set { this.surchargeAmountField = value; this.surchargeAmountSet = true; }
        }
        public string payPalNotes;
        public string actionReason;

        public override string Serialize()
        {
            string xml = "\r\n<authReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<litleTxnId>" + litleTxnId + "</litleTxnId>";
            if (amountSet)
            {
                xml += "\r\n<amount>" + amountField + "</amount>";
            }
            if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            if (payPalNotes != null)
            {
                xml += "\r\n<payPalNotes>" + SecurityElement.Escape(payPalNotes) + "</payPalNotes>";
            }
            if (actionReason != null)
            {
                xml += "\r\n<actionReason>" + SecurityElement.Escape(actionReason) + "</actionReason>";
            }
            xml += "\r\n</authReversal>";
            return xml;
        }

    }

    public partial class giftCardAuthReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
                this.litleTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        private int originalSystemTraceIdField;
        private bool originalSystemTraceIdSet;
        public int originalSystemTraceId
        {
            get
            {
                return this.originalSystemTraceIdField;
            }
            set
            {
                this.originalSystemTraceIdField = value;
                this.originalSystemTraceIdSet = true;
            }
        }
        public string originalSequenceNumber;

        public override String Serialize()
        {
            string xml = "\r\n<giftCardAuthReversal ";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnIdField + "</litleTxnId>";
            }
            if (card != null)
            {
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            if (originalRefCode != null)
            {
                xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            }
            // Do I need to turn original amount long into a boolean because it cannot be compared to null
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";

            if (originalTxnTime != null)
            {
                xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            }
            // I turned this into a boolean so the serialixing methos was in a boolean form
            if (originalSystemTraceIdSet)
            {
                xml += "\r\n<originalSystemTraceId>" + originalSystemTraceIdField + "</originalSystemTraceId>";
            }
            if (originalSequenceNumber != null)
            {
                xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";
            }
            xml += "\r\n</giftCardAuthReversal>";
            return xml;
        }
    }

    public partial class echeckVoid : transactionTypeWithReportGroup
    {
        public long litleTxnId;

        public override string Serialize()
        {
            string xml = "\r\n<echeckVoid";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<litleTxnId>" + litleTxnId + "</litleTxnId>";
            xml += "\r\n</echeckVoid>";
            return xml;
        }

    }

    public class accountUpdate : transactionTypeWithReportGroup
    {
        public string orderId;
        public cardType card;
        public cardTokenType token;

        public override string Serialize()
        {
            string xml = "\r\n<accountUpdate ";

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

            if (card != null)
            {
                xml += "\r\n<card>";
                xml += card.Serialize();
                xml += "\r\n</card>";
            }
            else if (token != null)
            {
                xml += "\r\n<token>";
                xml += token.Serialize();
                xml += "\r\n</token>";
            }

            xml += "\r\n</accountUpdate>";

            return xml;
        }
    }

    public class accountUpdateFileRequestData
    {
        public string merchantId;
        public accountUpdateFileRequestData()
        {
            merchantId = Properties.Settings.Default.merchantId;
        }
        public accountUpdateFileRequestData(Dictionary<String,String> config) 
        {
            this.merchantId = config["merchantId"];
        }
        public DateTime postDay; //yyyy-MM-dd

        public string Serialize()
        {
            string xml = "\r\n<merchantId>" + SecurityElement.Escape(merchantId) + "</merchantId>";

            if (postDay != null)
            {
                xml += "\r\n<postDay>" + postDay.ToString("yyyy-MM-dd") + "</postDay>";
            }

            return xml;
        }
    }

    public partial class activate : transactionTypeWithReportGroup
    {
        public string orderId;
        public long amount;
        public orderSourceType orderSource;
        public cardType card;
        public virtualGiftCardType virtualGiftCard;

        public override string Serialize()
        {
            string xml = "\r\n<activate";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            if (card != null) xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            else if (virtualGiftCard != null) xml += "\r\n<virtualGiftCard>" + virtualGiftCard.Serialize() + "\r\n</virtualGiftCard>";
            xml += "\r\n</activate>";
            return xml;
        }
    }

    public partial class deactivate : transactionTypeWithReportGroup
    {
        public string orderId;
        public orderSourceType orderSource;
        public cardType card;

        public override string Serialize()
        {
            string xml = "\r\n<deactivate";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n</deactivate>";
            return xml;
        }
    }

    public partial class load : transactionTypeWithReportGroup
    {
        public string orderId;
        public long amount;
        public orderSourceType orderSource;
        public cardType card;

        public override string Serialize()
        {
            string xml = "\r\n<load";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n</load>";
            return xml;
        }
    }

    public partial class unload : transactionTypeWithReportGroup
    {
        public string orderId;
        public long amount;
        public orderSourceType orderSource;
        public cardType card;

        public override string Serialize()
        {
            string xml = "\r\n<unload";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n</unload>";
            return xml;
        }
    }

    public partial class balanceInquiry : transactionTypeWithReportGroup
    {
        public string orderId;
        public orderSourceType orderSource;
        public cardType card;

        public override string Serialize()
        {
            string xml = "\r\n<balanceInquiry";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n</balanceInquiry>";
            return xml;
        }
    }

    public partial class loadReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
                this.litleTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            string xml = "\r\n<loadReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnId + "</litleTxnId>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</loadReversal>";
            return xml;
        }
    }

    public partial class unloadReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
                this.litleTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            string xml = "\r\n<unloadReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnId + "</litleTxnId>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</unloadReversal>";
            return xml;
        }
    }

    public partial class deactivateReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
                this.litleTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            string xml = "\r\n<deactivateReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnId + "</litleTxnId>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</deactivateReversal>";
            return xml;
        }
    }

    public partial class activateReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
                this.litleTxnIdSet = true;
            }
        }
        private string virtualGiftCardBinField;
        private bool virtualGiftCardBinSet;
        public string virtualGiftCardBin
        {
            get
            {
                return this.virtualGiftCardBinField;
            }
            set
            {
                this.virtualGiftCardBinField = value;
                this.virtualGiftCardBinSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            string xml = "\r\n<activateReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnId + "</litleTxnId>";
            }
            if (virtualGiftCardBinSet)
            {
                xml += "\r\n<virtualGiftCardBin>" + virtualGiftCardBin + "</virtualGiftCardBin>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</activateReversal>";
            return xml;
        }
    }

    public partial class refundReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
                this.litleTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            string xml = "\r\n<refundReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnId + "</litleTxnId>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</refundReversal>";
            return xml;
        }
    }

    public partial class depositReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
                this.litleTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            string xml = "\r\n<depositReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (litleTxnIdSet)
            {
                xml += "\r\n<litleTxnId>" + litleTxnId + "</litleTxnId>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</depositReversal>";
            return xml;
        }
    }

    public partial class applepayType
    {
        public string data;
        public applepayHeaderType header;
        public string signature;
        public string version;

        public string Serialize()
        {
            string xml = "";
            if (data != null) xml += "\r\n<data>" + SecurityElement.Escape(data) + "</data>";
            if (header != null) xml += "\r\n<header>" + header.Serialize() + "</header>";
            if (signature != null) xml += "\r\n<signature>" + SecurityElement.Escape(signature) + "</signature>";
            if (version != null) xml += "\r\n<version>" + SecurityElement.Escape(version) + "</version>";
            return xml;
        }
    }

    public partial class applepayHeaderType
    {
        public string applicationData;
        public string ephemeralPublicKey;
        public string publicKeyHash;
        public string transactionId;

        public string Serialize()
        {
            string xml = "";
            if (applicationData != null) xml += "\r\n<applicationData>" + SecurityElement.Escape(applicationData) + "</applicationData>";
            if (ephemeralPublicKey != null) xml += "\r\n<ephemeralPublicKey>" + SecurityElement.Escape(ephemeralPublicKey) + "</ephemeralPublicKey>";
            if (publicKeyHash != null) xml += "\r\n<publicKeyHash>" + SecurityElement.Escape(publicKeyHash) + "</publicKeyHash>";
            if (transactionId != null) xml += "\r\n<transactionId>" + SecurityElement.Escape(transactionId) + "</transactionId>";
            return xml;
        }
    }

    public partial class wallet
    {
        public walletWalletSourceType walletSourceType;
        public string walletSourceTypeId;

        public string Serialize()
        {
            string xml = "";
            xml += "\r\n<walletSourceType>" + walletSourceType + "</walletSourceType>";
            if (walletSourceTypeId != null) xml += "\r\n<walletSourceTypeId>" + SecurityElement.Escape(walletSourceTypeId) + "</walletSourceTypeId>";
            return xml;
        }
    }

    public enum walletWalletSourceType
    {
        MasterPass,
        VisaCheckout
    }

    

    public partial class fraudCheck : transactionTypeWithReportGroup
    {

        public advancedFraudChecksType advancedFraudChecks;

        private contact billToAddressField;
        private bool billToAddressSet;
        public contact billToAddress
        {
            get
            {
                return this.billToAddressField;
            }
            set
            {
                this.billToAddressField = value; this.billToAddressSet = true;
            }
        }

        private contact shipToAddressField;
        private bool shipToAddressSet;
        public contact shipToAddress
        {
            get
            {
                return this.shipToAddressField;
            }
            set
            {
                this.shipToAddressField = value; this.shipToAddressSet = true;
            }
        }

        private int amountField;
        private bool amountSet;
        public int amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value; this.amountSet = true;
            }
        }

        public override string Serialize()
        {
            string xml = "\r\n<fraudCheck";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
                if (customerId != null)
                {
                    xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
                }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (advancedFraudChecks != null) xml += "\r\n<advancedFraudChecks>" + advancedFraudChecks.Serialize() + "\r\n</advancedFraudChecks>";
            if (billToAddressSet) xml += "\r\n<billToAddress>" + billToAddressField.Serialize() + "</billToAddress>";
            if (shipToAddressSet) xml += "\r\n<shipToAddress>" + shipToAddressField.Serialize() + "</shipToAddress>";
            if (amountSet) xml += "\r\n<amount>" + amountField.ToString() + "</amount>";
            xml += "\r\n</fraudCheck>";
            return xml;
        }
    }

    public enum processingTypeEnum
    {
        accountFunding,
        initialRecurring,
        initialInstallment
        
    }

    public enum mandateProviderType
    {
        Merchant,
        Vantiv
    }

    public enum sequenceTypeType
    {
            OneTime,
            FirstRecurring,
            SubsequentRecurring,
            FinalRecurring
     }

public partial class sepaDirectDebitType
    {
        public mandateProviderType mandateProvider;
        public sequenceTypeType sequenceType;
        public string mandateReferenceField;
        public bool mandateReferenceSet;
        public string mandateReference
        {
            get
            {
                return this.mandateReferenceField;
            }
            set
            {
                this.mandateReferenceField = value;
                this.mandateReferenceSet = true;
            }
        }
        public string mandateUrlField;
        public bool mandateUrlSet;
        public string mandateUrl
        {
            get
            {
                return this.mandateUrlField;
            }
            set
            {
                this.mandateUrlField = value;
                this.mandateUrlSet = true;
            }
        }
        // CES does this work
        public DateTime mandateSignatureDateField;
        public bool mandateSignatureDateSet;
        public DateTime mandateSignatureDate
        {
            get
            {
                return this.mandateSignatureDateField;
            }
            set
            {
                this.mandateSignatureDateField = value;
                this.mandateSignatureDateSet = true;
            }
        }
        public string iban;
        public countryTypeEnum preferredLanguageField;
        public bool preferredLanguageSet;
        public countryTypeEnum preferredLanguage
        {
            get
            {
                return this.preferredLanguageField;
            }
            set
            {
                this.preferredLanguageField = value;
                this.preferredLanguageSet = true;
            }
        }
        public string Serialize()
        {
            string xml = "";
            xml += "\r\n<mandateProvider>" + mandateProvider + "</mandateProvider>";
            xml += "\r\n<sequenceType>" + sequenceType + "</sequenceType>";
            if (mandateReferenceSet)
            {
                xml += "\r\n<mandateReference>" + mandateReference + "</mandateReference>";
            }
            if (mandateUrlSet)
            {
                xml += "\r\n<mandateUrl>" + mandateUrl + "</mandateUrl>";
            }
            if (mandateSignatureDateSet)
            {
                xml += "\r\n<mandateSignatureDate>" + mandateSignatureDate + "</mandateSignatureDate>";
            }
            if (iban != null)
            {
                xml += "\r\n<iban>" + iban + "</iban>";
            }
            if (preferredLanguageSet)
            {
                xml += "\r\n<preferredLanguage>" + preferredLanguage + "</preferredLanguage>";
            }
            return xml;
        }
    }

    public partial class queryTransaction : transactionTypeWithReportGroup
    {
        public string origId;
        public actionTypeEnum origActionType;
        public long origLitleTxnId;
        public string origOrderIdField;
        private bool origOrderIdSet;
        public string origAccountNumberField;
        private bool origAccountNumberSet;

        public string origOrderId
        {
            get
            {
                return this.origOrderIdField;
            }
            set
            {
                this.origOrderIdField = value; this.origOrderIdSet = true;
            }
        }

        public string origAccountNumber
        {
            get
            {
                return this.origAccountNumberField;
            }
            set
            {
                this.origAccountNumberField = value; this.origAccountNumberSet = true;
            }
        }


        public override string Serialize()
        {

            string xml = "\r\n<queryTransaction";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";


            xml += "\r\n<origId>" + SecurityElement.Escape(origId) + "</origId>";
            xml += "\r\n<origActionType>" + origActionType + "</origActionType>";
            if (origLitleTxnId != 0) xml += "\r\n<origLitleTxnId>" + origLitleTxnId + "</origLitleTxnId>";
            if (origOrderIdSet) xml += "\r\n<origOrderId>" + SecurityElement.Escape(origOrderIdField) + "</origOrderId>";
            if (origAccountNumberSet) xml += "\r\n<origAccountNumber>" + SecurityElement.Escape(origAccountNumberField) + "</origAccountNumber>";

            xml += "\r\n</queryTransaction>";
            return xml;
        }

    }

    public enum actionTypeEnum
    {
        A,
        D,
        R,
        AR,
        G,
        I,
        J,
        L,
        LR,
        P,
        RR,
        S,
        T,
        UR,
        V,
        W,
        X
       
    }
}
