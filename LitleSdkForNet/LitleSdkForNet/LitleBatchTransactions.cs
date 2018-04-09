using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Xml.Serialization;

namespace Litle.Sdk
{
    
    public partial class accountUpdate : transactionTypeWithReportGroup
    {
        public string orderId;
        public cardType card;
        public cardTokenType token;

        public override string Serialize()
        {
            var xml = "\r\n<accountUpdate ";

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
    public class accountUpdateResponseCardTokenType : cardTokenType
    {
        public string bin;
    }
    
    [System.Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class accountUpdateResponse : transactionTypeWithReportGroup
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
    
    public partial class accountUpdateFileRequestData
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
            var xml = "\r\n<merchantId>" + SecurityElement.Escape(merchantId) + "</merchantId>";

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
                return orderIdField;
            }
            set
            {
                orderIdField = value;
            }
        }

        /// <remarks/>
        public orderSourceType orderSource
        {
            get
            {
                return orderSourceField;
            }
            set
            {
                orderSourceField = value;
            }
        }

        /// <remarks/>
        public contact billToAddress
        {
            get
            {
                return billToAddressField;
            }
            set
            {
                billToAddressField = value;
            }
        }

        /// <remarks/>
        public echeckType echeck
        {
            get
            {
                return echeckField;
            }
            set
            {
                echeckField = value;
            }
        }

        /// <remarks/>
        public merchantDataType merchantData
        {
            get
            {
                return merchantDataField;
            }
            set
            {
                merchantDataField = value;
            }
        }

        public override string Serialize()
        {
            var xml = "\r\n<echeckPreNoteSale ";

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
                return orderIdField;
            }
            set
            {
                orderIdField = value;
            }
        }

        /// <remarks/>
        public orderSourceType orderSource
        {
            get
            {
                return orderSourceField;
            }
            set
            {
                orderSourceField = value;
            }
        }

        /// <remarks/>
        public contact billToAddress
        {
            get
            {
                return billToAddressField;
            }
            set
            {
                billToAddressField = value;
            }
        }

        /// <remarks/>
        public echeckType echeck
        {
            get
            {
                return echeckField;
            }
            set
            {
                echeckField = value;
            }
        }

        /// <remarks/>
        public merchantDataType merchantData
        {
            get
            {
                return merchantDataField;
            }
            set
            {
                merchantDataField = value;
            }
        }

        public override string Serialize()
        {
            var xml = "\r\n<echeckPreNoteCredit ";

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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute("echeckPreNoteSaleResponse", Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class echeckPreNoteSaleResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute("echeckPreNoteCreditResponse", Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class echeckPreNoteCreditResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

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