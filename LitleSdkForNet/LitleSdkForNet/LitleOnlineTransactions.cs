using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Xml.Serialization;

namespace Litle.Sdk
{
    
    public partial class voidTxn : transactionTypeWithReportGroup
    {
        public long litleTxnId;
        public processingInstructions processingInstructions;

        public override string Serialize()
        {
            var xml = "\r\n<void";
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute("voidResponse", Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class litleOnlineResponseTransactionResponseVoidResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private System.DateTime postDateField;

        private string messageField;

        private voidRecyclingResponseType recyclingField;

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
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime postDate
        {
            get
            {
                return this.postDateField;
            }
            set
            {
                this.postDateField = value;
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

        
        //private voidRecyclingResponseType recycling;
        public voidRecyclingResponseType recycling
        {
            get
            {
                return this.recyclingField;
            }
            set
            {
                this.recyclingField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class voidRecyclingResponseType
    {
        private long creditLitleTxnIdField;

        public long creditLitleTxnId
        {
            get
            {
                return this.creditLitleTxnIdField;
            }
            set
            {
                this.creditLitleTxnIdField = value;
            }
        }
    }
    
    public partial class echeckVoid : transactionTypeWithReportGroup
    {
        public long litleTxnId;

        public override string Serialize()
        {
            var xml = "\r\n<echeckVoid";
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute("echeckVoidResponse", Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class litleOnlineResponseTransactionResponseEcheckVoidResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private System.DateTime postDateField;

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
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime postDate
        {
            get
            {
                return this.postDateField;
            }
            set
            {
                this.postDateField = value;
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
    
    public partial class depositReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return litleTxnIdField;
            }
            set
            {
                litleTxnIdField = value;
                litleTxnIdSet = true;
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
            var xml = "\r\n<depositReversal";
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
    
    public partial class refundReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return litleTxnIdField;
            }
            set
            {
                litleTxnIdField = value;
                litleTxnIdSet = true;
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
            var xml = "\r\n<refundReversal";
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
    
    public partial class activateReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return litleTxnIdField;
            }
            set
            {
                litleTxnIdField = value;
                litleTxnIdSet = true;
            }
        }
        private string virtualGiftCardBinField;
        private bool virtualGiftCardBinSet;
        public string virtualGiftCardBin
        {
            get
            {
                return virtualGiftCardBinField;
            }
            set
            {
                virtualGiftCardBinField = value;
                virtualGiftCardBinSet = true;
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
            var xml = "\r\n<activateReversal";
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
    
    public partial class deactivateReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return litleTxnIdField;
            }
            set
            {
                litleTxnIdField = value;
                litleTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            var xml = "\r\n<deactivateReversal";
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
    
    public partial class loadReversal : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get
            {
                return litleTxnIdField;
            }
            set
            {
                litleTxnIdField = value;
                litleTxnIdSet = true;
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
            var xml = "\r\n<loadReversal";
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
                return litleTxnIdField;
            }
            set
            {
                litleTxnIdField = value;
                litleTxnIdSet = true;
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
            var xml = "\r\n<unloadReversal";
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
    
    public partial class queryTransaction : transactionTypeWithReportGroup
    {
        public string origId;
        public actionTypeEnum origActionType;
        public long origLitleTxnId;

        public override string Serialize()
        {

            var xml = "\r\n<queryTransaction";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<origId>" + SecurityElement.Escape(origId) + "</origId>";
            xml += "\r\n<origActionType>" + origActionType + "</origActionType>";
            if (origLitleTxnId != 0) xml += "\r\n<origLitleTxnId>" + origLitleTxnId + "</origLitleTxnId>";
            xml += "\r\n</queryTransaction>";
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
    public partial class queryTransactionResponse : transactionTypeWithReportGroup
    {


        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        private string matchCountField;

        private ArrayList results_max10Field;

        /// <remarks/>
        public string matchCount
        {
            get
            {
                return this.matchCountField;
            }
            set
            {
                this.matchCountField = value;
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
        [XmlArray("results_max10")]
        [XmlArrayItem("authorizationResponse", typeof(authorizationResponse))]
        [XmlArrayItem("captureResponse", typeof(captureResponse))]
        [XmlArrayItem("giftCardCaptureResponse", typeof(giftCardCaptureResponse))]
        [XmlArrayItem("recurringResponse", typeof(recurringResponse))]
        [XmlArrayItem("registerTokenResponse", typeof(registerTokenResponse))]
        [XmlArrayItem("authReversalResponse", typeof(authReversalResponse))]
        [XmlArrayItem("giftCardAuthReversalResponse", typeof(giftCardAuthReversalResponse))]
        [XmlArrayItem("captureGivenAuthResponse", typeof(captureGivenAuthResponse))]
        [XmlArrayItem("updateCardValidationNumOnTokenResponse", typeof(updateCardValidationNumOnTokenResponse))]
        [XmlArrayItem("cancelSubscriptionResponse", typeof(cancelSubscriptionResponse))]
        [XmlArrayItem("updateSubscriptionResponse", typeof(updateSubscriptionResponse))]
        [XmlArrayItem("createPlanResponse", typeof(createPlanResponse))]
        [XmlArrayItem("updatePlanResponse", typeof(updatePlanResponse))]
        [XmlArrayItem("activateResponse", typeof(activateResponse))]
        [XmlArrayItem("deactivateResponse", typeof(deactivateResponse))]
        [XmlArrayItem("loadResponse", typeof(loadResponse))]
        [XmlArrayItem("echeckPreNoteSaleResponse", typeof(echeckPreNoteSaleResponse))]
        [XmlArrayItem("echeckPreNoteCreditResponse", typeof(echeckPreNoteCreditResponse))]
        [XmlArrayItem("unloadResponse", typeof(unloadResponse))]
        [XmlArrayItem("balanceInquiryResponse", typeof(balanceInquiryResponse))]
        [XmlArrayItem("payFacCreditResponse", typeof(payFacCreditResponse))]
        [XmlArrayItem("vendorDebitResponse", typeof(vendorDebitResponse))]
        [XmlArrayItem("reserveDebitResponse", typeof(reserveDebitResponse))]
        [XmlArrayItem("creditResponse", typeof(creditResponse))]
        [XmlArrayItem("giftCardCreditResponse", typeof(giftCardCreditResponse))]
        [XmlArrayItem("forceCaptureResponse", typeof(forceCaptureResponse))]
        [XmlArrayItem("echeckCreditResponse", typeof(echeckCreditResponse))]
        [XmlArrayItem("echeckRedepositResponse", typeof(echeckRedepositResponse))]
        [XmlArrayItem("echeckSalesResponse", typeof(echeckSalesResponse))]
        [XmlArrayItem("saleResponse", typeof(saleResponse))] 

        public ArrayList results_max10
        {
            get
            {
                return this.results_max10Field;
            }
            set
            {
                results_max10Field = value;
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
    public partial class queryTransactionUnavailableResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string responseField;

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
    
    // Service Status Request.
    
    // Service Status Response.
    
    // Fast Access Funding Transaction.
    public partial class fastAccessFunding : transactionTypeWithReportGroup
    {
        public string fundingSubmerchantId;
        public string submerchantName;
        public string fundsTransferId;
        public int amount;
        public cardType card;
        public cardTokenType token;
        public cardPaypageType paypage;

        public override string Serialize()
        {
            var xml = "\r\n<fastAccessFunding";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            
            // The first element of a sequence xml element  represent the sequence element
            if (fundingSubmerchantId != null)
            {
                xml += "\r\n<fundingSubmerchantId>" + fundingSubmerchantId + "</fundingSubmerchantId>";
                xml += "\r\n<submerchantName>" + submerchantName + "</submerchantName>";
                xml += "\r\n<fundsTransferId>" + fundsTransferId + "</fundsTransferId>";
                xml += "\r\n<amount>" + amount + "</amount>";
                if (card != null) xml += "\r\n<card>" + card.Serialize() + "</card>";
                else if (token != null) xml += "\r\n<token>" + token.Serialize() + "</token>";
                else xml += "\r\n<paypage>" + paypage.Serialize() + "</paypage>";
            }
            xml += "\r\n</fastAccessFunding>";
            return xml;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.litle.com/schema", IsNullable=false)]
    public partial class fastAccessFundingResponse : transactionTypeWithReportGroup {
        
        private long litleTxnIdField;
        
        private string fundsTransferIdField;
        
        private string responseField;
        
        private System.DateTime responseTimeField;
        
        private System.DateTime postDateField;
        
        private bool postDateFieldSpecified;
        
        private string messageField;
        
        private tokenResponseType tokenResponseField;
        
        private bool duplicateField;
        
        private bool duplicateFieldSpecified;
        
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
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime postDate {
            get {
                return this.postDateField;
            }
            set {
                this.postDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified {
            get {
                return this.postDateFieldSpecified;
            }
            set {
                this.postDateFieldSpecified = value;
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
        
        /// <remarks/>
        public tokenResponseType tokenResponse {
            get {
                return this.tokenResponseField;
            }
            set {
                this.tokenResponseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool duplicate {
            get {
                return this.duplicateField;
            }
            set {
                this.duplicateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool duplicateSpecified {
            get {
                return this.duplicateFieldSpecified;
            }
            set {
                this.duplicateFieldSpecified = value;
            }
        }
    }
}