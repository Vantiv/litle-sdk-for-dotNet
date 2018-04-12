using System;
using System.Collections;
using System.Collections.Generic;
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

        private bool duplicateField;

        private bool duplicateFieldSpecified;

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
    
    
    public partial class depositReversal : transactionTypeWithReportGroup
    {
        public string litleTxnId;

        public override string Serialize()
        {
            string xml = "\r\n<depositReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<litleTxnId>" + SecurityElement.Escape(litleTxnId) + "</litleTxnId>";
            xml += "\r\n</depositReversal>";
            return xml;
        }
    }
    
    
    public partial class refundReversal : transactionTypeWithReportGroup
    {
        public string litleTxnId;

        public override string Serialize()
        {
            string xml = "\r\n<refundReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<litleTxnId>" + SecurityElement.Escape(litleTxnId) + "</litleTxnId>";
            xml += "\r\n</refundReversal>";
            return xml;
        }
    }
    
    
    public partial class activateReversal : transactionTypeWithReportGroup
    {
        public string litleTxnId;

        public override string Serialize()
        {
            string xml = "\r\n<activateReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<litleTxnId>" + SecurityElement.Escape(litleTxnId) + "</litleTxnId>";
            xml += "\r\n</activateReversal>";
            return xml;
        }
    }
    
    
    public partial class deactivateReversal : transactionTypeWithReportGroup
    {
        public string litleTxnId;

        public override string Serialize()
        {
            string xml = "\r\n<deactivateReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<litleTxnId>" + SecurityElement.Escape(litleTxnId) + "</litleTxnId>";
            xml += "\r\n</deactivateReversal>";
            return xml;
        }
    }
    
    
    public partial class loadReversal : transactionTypeWithReportGroup
    {
        public string litleTxnId;

        public override string Serialize()
        {
            string xml = "\r\n<loadReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<litleTxnId>" + SecurityElement.Escape(litleTxnId) + "</litleTxnId>";
            xml += "\r\n</loadReversal>";
            return xml;
        }
    }
    
    
    public partial class unloadReversal : transactionTypeWithReportGroup
    {
        public string litleTxnId;

        public override string Serialize()
        {
            string xml = "\r\n<unloadReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<litleTxnId>" + SecurityElement.Escape(litleTxnId) + "</litleTxnId>";
            xml += "\r\n</unloadReversal>";
            return xml;
        }
    }
    
    
    public partial class queryTransaction : transactionTypeWithReportGroup
    {
        public string origId;
        public actionTypeEnum origActionType;
        public long origLitleTxnId;
        public string origOrderId;
        public string origAccountNumber;

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
            if (origOrderId != null) xml += "\r\n<origOrderId>" + SecurityElement.Escape(origOrderId) + "</origOrderId>";
            if (origAccountNumber != null) xml += "\r\n<origAccountNumber>" + SecurityElement.Escape(origAccountNumber) + "</origAccountNumber>";
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
        [XmlArrayItem("recurringResponse", typeof(recurringResponse))]
        [XmlArrayItem("registerTokenResponse", typeof(registerTokenResponse))]
        [XmlArrayItem("authReversalResponse", typeof(authReversalResponse))]
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
}