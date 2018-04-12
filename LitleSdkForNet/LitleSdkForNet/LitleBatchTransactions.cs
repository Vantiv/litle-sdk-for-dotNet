using System;
using System.Collections.Generic;
using System.Security;
using System.Xml.Serialization;


namespace Litle.Sdk
{
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
    
    
    [System.Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public class accountUpdateResponse : transactionTypeWithReportGroup
    {
        public long litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public string message;

        //Optional child elements
        public cardType updatedCard;
        public cardType originalCard;
        public accountUpdateResponseCardTokenType originalToken;
        public accountUpdateResponseCardTokenType updatedToken;
    }
    
    
    [System.Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public class accountUpdateResponseCardTokenType : cardTokenType
    {
        public string bin;
    }
    
    
    public class accountUpdateFileRequestData
    {
        public string merchantId;
        public accountUpdateFileRequestData()
        {
            merchantId = Properties.Settings.Default.merchantId;
        }
        public accountUpdateFileRequestData(Dictionary<string,string> config) 
        {
            merchantId = config["merchantId"];
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute("echeckPreNoteSaleResponse", Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class echeckPreNoteSaleResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        private bool duplicateField;

        private bool duplicateFieldSpecified;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

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
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool duplicate
        {
            get
            {
                return this.duplicateField;
            }
            set
            {
                this.duplicateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool duplicateSpecified
        {
            get
            {
                return this.duplicateFieldSpecified;
            }
            set
            {
                this.duplicateFieldSpecified = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute("echeckPreNoteCreditResponse", Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class echeckPreNoteCreditResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        private bool duplicateField;

        private bool duplicateFieldSpecified;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

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
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool duplicate
        {
            get
            {
                return this.duplicateField;
            }
            set
            {
                this.duplicateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool duplicateSpecified
        {
            get
            {
                return this.duplicateFieldSpecified;
            }
            set
            {
                this.duplicateFieldSpecified = value;
            }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class submerchantCreditResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string fundsTransferIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

        /// <remarks/>
        public string fundsTransferId
        {
            get
            {
                return this.fundsTransferIdField;
            }
            set
            {
                this.fundsTransferIdField = value;
            }
        }

        /// <remarks/>
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class payFacCreditResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string fundsTransferIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

        /// <remarks/>
        public string fundsTransferId
        {
            get
            {
                return this.fundsTransferIdField;
            }
            set
            {
                this.fundsTransferIdField = value;
            }
        }

        /// <remarks/>
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class vendorCreditResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string fundsTransferIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

        /// <remarks/>
        public string fundsTransferId
        {
            get
            {
                return this.fundsTransferIdField;
            }
            set
            {
                this.fundsTransferIdField = value;
            }
        }

        /// <remarks/>
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class reserveCreditResponse : transactionTypeWithReportGroup {
    
    private long litleTxnIdField;
    
    private string fundsTransferIdField;
    
    private string responseField;
    
    private System.DateTime responseTimeField;
    
    private string messageField;
    
    /// <remarks/>
    public long litleTxnId {
        get {
            return this.litleTxnIdField;
        }
        set {
            this.litleTxnIdField = value;
        }
    }
    
    /// <remarks/>
    public string fundsTransferId {
        get {
            return this.fundsTransferIdField;
        }
        set {
            this.fundsTransferIdField = value;
        }
    }
    
    /// <remarks/>
    public string response {
        get {
            return this.responseField;
        }
        set {
            this.responseField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime responseTime {
        get {
            return this.responseTimeField;
        }
        set {
            this.responseTimeField = value;
        }
    }
    
    /// <remarks/>
    public string message {
        get {
            return this.messageField;
        }
        set {
            this.messageField = value;
        }
    }
}
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class physicalCheckCreditResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string fundsTransferIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

        /// <remarks/>
        public string fundsTransferId
        {
            get
            {
                return this.fundsTransferIdField;
            }
            set
            {
                this.fundsTransferIdField = value;
            }
        }

        /// <remarks/>
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class submerchantDebitResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string fundsTransferIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

        /// <remarks/>
        public string fundsTransferId
        {
            get
            {
                return this.fundsTransferIdField;
            }
            set
            {
                this.fundsTransferIdField = value;
            }
        }

        /// <remarks/>
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class payFacDebitResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string fundsTransferIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

        /// <remarks/>
        public string fundsTransferId
        {
            get
            {
                return this.fundsTransferIdField;
            }
            set
            {
                this.fundsTransferIdField = value;
            }
        }

        /// <remarks/>
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class vendorDebitResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string fundsTransferIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

        /// <remarks/>
        public string fundsTransferId
        {
            get
            {
                return this.fundsTransferIdField;
            }
            set
            {
                this.fundsTransferIdField = value;
            }
        }

        /// <remarks/>
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class reserveDebitResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string fundsTransferIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

        /// <remarks/>
        public string fundsTransferId
        {
            get
            {
                return this.fundsTransferIdField;
            }
            set
            {
                this.fundsTransferIdField = value;
            }
        }

        /// <remarks/>
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class physicalCheckDebitResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string fundsTransferIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        /// <remarks/>
        public long litleTxnId
        {
            get
            {
                return this.litleTxnIdField;
            }
            set
            {
                this.litleTxnIdField = value;
            }
        }

        /// <remarks/>
        public string fundsTransferId
        {
            get
            {
                return this.fundsTransferIdField;
            }
            set
            {
                this.fundsTransferIdField = value;
            }
        }

        /// <remarks/>
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    
    
    
}