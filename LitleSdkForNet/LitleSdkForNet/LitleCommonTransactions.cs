using System;
using System.Collections.Generic;
using System.Security;
using System.Xml.Serialization;

namespace Litle.Sdk
{
    
    #region Litle Common
    
    public class authentication
    {
        public string user;
        public string password;
        public string Serialize()
        {
            return "\r\n<authentication>\r\n<user>" + SecurityElement.Escape(user) + "</user>\r\n<password>" + SecurityElement.Escape(password) + "</password>\r\n</authentication>";
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class transactionRequest
    {
        public virtual string Serialize()
        {
            throw new NotImplementedException();
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class transactionResponse
    {
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum methodOfPaymentTypeEnum
    {

        /// <remarks/>
        MC,

        /// <remarks/>
        VI,

        /// <remarks/>
        AX,

        /// <remarks/>
        DC,

        /// <remarks/>
        DI,

        /// <remarks/>
        PP,

        /// <remarks/>
        JC,

        /// <remarks/>
        BL,

        /// <remarks/>
        EC,

        /// <remarks/>
        GC,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("")]
        Item,
    }
    
    public abstract class methodOfPaymentSerializer
    {
        public static String Serialize(methodOfPaymentTypeEnum mop)
        {
            if (mop == methodOfPaymentTypeEnum.Item)
            {
                return "";
            }
            else
            {
                return mop.ToString();

            }
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum govtTaxTypeEnum
    {

        /// <remarks/>
        payment,

        /// <remarks/>
        fee,
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
            get { return countryField; }
            set { countryField = value; countrySpecified = true; }
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
    
    
    public class cardType
    {
        public methodOfPaymentTypeEnum type;
        public string number;
        public string expDate;
        public string track;
        public string cardValidationNum;
        public string pin;

        public string Serialize()
        {
            var xml = "";
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
    
    
    public partial class cardTokenType
    {
        public string litleToken;
        public string expDate;
        public string cardValidationNum;
        private methodOfPaymentTypeEnum typeField;
        private bool typeSet;
        public methodOfPaymentTypeEnum type
        {
            get { return typeField; }
            set { typeField = value; typeSet = true; }
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
            get { return typeField; }
            set { typeField = value; typeSet = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class tokenResponseType
    {

        private string litleTokenField;

        private string tokenResponseCodeField;

        private string tokenMessageField;

        private methodOfPaymentTypeEnum typeField;

        private bool typeFieldSpecified;

        private string binField;

        private string eCheckAccountSuffixField;

        /// <remarks/>
        public string litleToken
        {
            get
            {
                return this.litleTokenField;
            }
            set
            {
                this.litleTokenField = value;
            }
        }

        /// <remarks/>
        public string tokenResponseCode
        {
            get
            {
                return this.tokenResponseCodeField;
            }
            set
            {
                this.tokenResponseCodeField = value;
            }
        }

        /// <remarks/>
        public string tokenMessage
        {
            get
            {
                return this.tokenMessageField;
            }
            set
            {
                this.tokenMessageField = value;
            }
        }

        /// <remarks/>
        public methodOfPaymentTypeEnum type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeSpecified
        {
            get
            {
                return this.typeFieldSpecified;
            }
            set
            {
                this.typeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string bin
        {
            get
            {
                return this.binField;
            }
            set
            {
                this.binField = value;
            }
        }

        /// <remarks/>
        public string eCheckAccountSuffix
        {
            get
            {
                return this.eCheckAccountSuffixField;
            }
            set
            {
                this.eCheckAccountSuffixField = value;
            }
        }
    }
    
    
    public partial class advancedFraudChecksType
    {
        public string threatMetrixSessionId;
        private string customAttribute1Field;
        private bool customAttribute1Set;
        public string customAttribute1 { 
            get { return customAttribute1Field; } 
            set { customAttribute1Field = value; customAttribute1Set = true; } 
        }
        private string customAttribute2Field;
        private bool customAttribute2Set;
        public string customAttribute2
        {
            get { return customAttribute2Field; }
            set { customAttribute2Field = value; customAttribute2Set = true; }
        }
        private string customAttribute3Field;
        private bool customAttribute3Set;
        public string customAttribute3
        {
            get { return customAttribute3Field; }
            set { customAttribute3Field = value; customAttribute3Set = true; }
        }
        private string customAttribute4Field;
        private bool customAttribute4Set;
        public string customAttribute4
        {
            get { return customAttribute4Field; }
            set { customAttribute4Field = value; customAttribute4Set = true; }
        }
        private string customAttribute5Field;
        private bool customAttribute5Set;
        public string customAttribute5
        {
            get { return customAttribute5Field; }
            set { customAttribute5Field = value; customAttribute5Set = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class advancedFraudResultsType
    {

        private string deviceReviewStatusField;

        private int deviceReputationScoreField;

        private bool deviceReputationScoreFieldSpecified;

        private string[] triggeredRuleField;

        /// <remarks/>
        public string deviceReviewStatus
        {
            get
            {
                return this.deviceReviewStatusField;
            }
            set
            {
                this.deviceReviewStatusField = value;
            }
        }

        /// <remarks/>
        public int deviceReputationScore
        {
            get
            {
                return this.deviceReputationScoreField;
            }
            set
            {
                this.deviceReputationScoreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deviceReputationScoreSpecified
        {
            get
            {
                return this.deviceReputationScoreFieldSpecified;
            }
            set
            {
                this.deviceReputationScoreFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("triggeredRule")]
        public string[] triggeredRule
        {
            get
            {
                return this.triggeredRuleField;
            }
            set
            {
                this.triggeredRuleField = value;
            }
        }
    }
    
    #endregion
    
    #region Litle Recurring
    
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class recurringTransactionType : transactionRequest
    {
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class recurringTransactionResponseType : transactionResponse
    {
        private string litleTxnIdField;

        /// <remarks/>
        public string litleTxnId
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

        private string responseField;

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

        private string messageField;

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

        private System.DateTime responseTimeField;

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
    }
    
    
    public partial class cancelSubscription : recurringTransactionType
    {
        private long subscriptionIdField;
        private bool subscriptionIdSet;
        public long subscriptionId
        {
            get
            {
                return subscriptionIdField;
            }
            set
            {
                subscriptionIdField = value;
                subscriptionIdSet = true;
            }
        }

        public override string Serialize()
        {
            string xml = "\r\n<cancelSubscription>";
            if (subscriptionIdSet) xml += "\r\n<subscriptionId>" + subscriptionIdField + "</subscriptionId>";
            xml += "\r\n</cancelSubscription>";
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
                return subscriptionIdField;
            }
            set
            {
                subscriptionIdField = value;
                subscriptionIdSet = true;
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
                return billingDateField;
            }
            set
            {
                billingDateField = value;
                billingDateSet = true;
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

        public override string Serialize()
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
    
    
    public partial class createPlan : recurringTransactionType
    {
        public string planCode;
        public string name;

        private string descriptionField;
        private bool descriptionSet;
        public string description
        {
            get { return descriptionField; }
            set { descriptionField = value; descriptionSet = true; }
        }

        public intervalType intervalType;
        public long amount;

        public int numberOfPaymentsField;
        public bool numberOfPaymentsSet;
        public int numberOfPayments
        {
            get { return numberOfPaymentsField; }
            set { numberOfPaymentsField = value; numberOfPaymentsSet = true; }
        }

        public int trialNumberOfIntervalsField;
        public bool trialNumberOfIntervalsSet;
        public int trialNumberOfIntervals
        {
            get { return trialNumberOfIntervalsField; }
            set { trialNumberOfIntervalsField = value; trialNumberOfIntervalsSet = true; }
        }

        private trialIntervalType trialIntervalTypeField;
        private bool trialIntervalTypeSet;
        public trialIntervalType trialIntervalType 
        {
            get { return trialIntervalTypeField; }
            set { trialIntervalTypeField = value; trialIntervalTypeSet = true; }
        }

        private bool activeField;
        private bool activeSet;
        public bool active
        {
            get { return activeField; }
            set { activeField = value; activeSet = true; }
        }

        public override string Serialize()
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
            get { return activeField; }
            set { activeField = value; activeSet = true; }
        }

        public override string Serialize()
        {
            string xml = "\r\n<updatePlan>";
            xml += "\r\n<planCode>" + SecurityElement.Escape(planCode) + "</planCode>";
            if (activeSet) xml += "\r\n<active>" + activeField.ToString().ToLower() + "</active>";
            xml += "\r\n</updatePlan>";
            return xml;
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class cancelSubscriptionResponse
    {
        private string subscriptionIdField;

        /// <remarks/>
        public string subscriptionId
        {
            get
            {
                return this.subscriptionIdField;
            }
            set
            {
                this.subscriptionIdField = value;
            }
        }
        private string litleTxnIdField;

        /// <remarks/>
        public string litleTxnId
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

        private string responseField;

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

        private string messageField;

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

        private System.DateTime responseTimeField;

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
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class updateSubscriptionResponse
    {
        private string subscriptionIdField;

        /// <remarks/>
        public string subscriptionId
        {
            get
            {
                return this.subscriptionIdField;
            }
            set
            {
                this.subscriptionIdField = value;
            }
        }
        private string litleTxnIdField;

        /// <remarks/>
        public string litleTxnId
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

        private string responseField;

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

        private string messageField;

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

        private System.DateTime responseTimeField;

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

        public tokenResponseType tokenResponse;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class createPlanResponse : recurringTransactionResponseType
    {
        public string planCode;
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class updatePlanResponse : recurringTransactionResponseType
    {
        public string planCode;
    }
    
    
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum intervalType
    {
        ANNUAL,
        SEMIANNUAL,
        QUARTERLY,
        MONTHLY,
        WEEKLY
    }
    

    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum trialIntervalType
    {
        MONTH,
        DAY
    }
    
    
    public partial class litleInternalRecurringRequest
    {
        public string subscriptionId;
        public string recurringTxnId;

        private bool finalPaymentField;
        private bool finalPaymentSet;
        public bool finalPayment
        {
            get { return finalPaymentField; }
            set { finalPaymentField = value; finalPaymentSet = true; }
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
    
    
    public partial class subscription
    {
        public string planCode;
        private bool numberOfPaymentsSet;
        private int numberOfPaymentsField;
        public int numberOfPayments
        {
            get { return numberOfPaymentsField; }
            set { numberOfPaymentsField = value; numberOfPaymentsSet = true; }
        }
        private bool startDateSet;
        private DateTime startDateField;
        public DateTime startDate
        {
            get { return startDateField; }
            set { startDateField = value; startDateSet = true; }
        }
        private bool amountSet;
        private long amountField;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class recurringResponse
    {
        private long subscriptionIdField;
        private string responseCodeField;
        private string responseMessageField;
        private long recurringTxnIdField;

        /// <remarks/>
        public long subscriptionId
        {
            get
            {
                return this.subscriptionIdField;
            }
            set
            {
                this.subscriptionIdField = value;
            }
        }

        /// <remarks/>
        public string responseCode
        {
            get
            {
                return this.responseCodeField;
            }
            set
            {
                this.responseCodeField = value;
            }
        }

        /// <remarks/>
        public string responseMessage
        {
            get
            {
                return this.responseMessageField;
            }
            set
            {
                this.responseMessageField = value;
            }
        }

        /// <remarks/>
        public long recurringTxnId
        {
            get
            {
                return this.recurringTxnIdField;
            }
            set
            {
                this.recurringTxnIdField = value;
            }
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
            get { return nameField; }
            set { nameField = value; nameSet = true; }
        }

        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }

        private DateTime startDateField;
        private bool startDateSet;
        public DateTime startDate
        {
            get { return startDateField; }
            set { startDateField = value; startDateSet = true; }
        }

        private DateTime endDateField;
        private bool endDateSet;
        public DateTime endDate
        {
            get { return endDateField; }
            set { endDateField = value; endDateSet = true; }
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
            get { return nameField; }
            set { nameField = value; nameSet = true; }
        }

        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }

        private DateTime startDateField;
        private bool startDateSet;
        public DateTime startDate
        {
            get { return startDateField; }
            set { startDateField = value; startDateSet = true; }
        }

        private DateTime endDateField;
        private bool endDateSet;
        public DateTime endDate
        {
            get { return endDateField; }
            set { endDateField = value; endDateSet = true; }
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
    
    #endregion
    
    
    #region Litle Transaction
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(transactionTypeOptionReportGroup))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(transactionTypeWithReportGroupAndPartial))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(transactionTypeWithReportGroup))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(registerTokenRequestType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class transactionType : transactionRequest
    {

        private string idField;

        private string customerIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string customerId
        {
            get
            {
                return this.customerIdField;
            }
            set
            {
                this.customerIdField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(registerTokenRequestType))]
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class transactionTypeWithReportGroup : transactionType
    {

        private string reportGroupField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string reportGroup
        {
            get
            {
                return this.reportGroupField;
            }
            set
            {
                this.reportGroupField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class transactionTypeOptionReportGroup : transactionType
    {

        private string reportGroupField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string reportGroup
        {
            get
            {
                return this.reportGroupField;
            }
            set
            {
                this.reportGroupField = value;
            }
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
    
    
    public partial class transactionTypeWithReportGroupAndPartial : transactionType
    {
        public string reportGroup;
        private bool partialField;
        protected bool partialSet;
        public bool partial
        {
            get { return partialField; }
            set { partialField = value; partialSet = true; }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class driversLicenseInfo
    {

        private string licenseNumberField;

        private string stateField;

        private string dateOfBirthField;

        /// <remarks/>
        public string licenseNumber
        {
            get
            {
                return this.licenseNumberField;
            }
            set
            {
                this.licenseNumberField = value;
            }
        }

        /// <remarks/>
        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public string dateOfBirth
        {
            get
            {
                return this.dateOfBirthField;
            }
            set
            {
                this.dateOfBirthField = value;
            }
        }
    }
    
    
    public enum customerInfoCustomerType
    {

        /// <remarks/>
        New,
        Existing,
    }
    

    public enum customerInfoResidenceStatus
    {

        /// <remarks/>
        Own,
        Rent,
        Other,
    }
    
    
    public class customerInfo
    {

        public string ssn;

        public DateTime dob;

        public DateTime customerRegistrationDate;

        private customerInfoCustomerType customerTypeField;
        private bool customerTypeSet;
        public customerInfoCustomerType customerType
        {
            get { return customerTypeField; }
            set { customerTypeField = value; customerTypeSet = true; }
        }

        private long incomeAmountField;
        private bool incomeAmountSet;
        public long incomeAmount
        {
            get { return incomeAmountField; }
            set { incomeAmountField = value; incomeAmountSet = true; }
        }

        private currencyCodeEnum incomeCurrencyField;
        private bool incomeCurrencySet;
        public currencyCodeEnum incomeCurrency
        {
            get { return incomeCurrencyField; }
            set { incomeCurrencyField = value; incomeCurrencySet = true; }
        }

        private bool customerCheckingAccountField;
        private bool customerCheckingAccountSet;
        public bool customerCheckingAccount
        {
            get { return customerCheckingAccountField; }
            set { customerCheckingAccountField = value; customerCheckingAccountSet = true; }
        }

        private bool customerSavingAccountField;
        private bool customerSavingAccountSet;
        public bool customerSavingAccount
        {
            get { return customerSavingAccountField; }
            set { customerSavingAccountField = value; customerSavingAccountSet = true; }
        }

        public string employerName;

        public string customerWorkTelephone;

        private customerInfoResidenceStatus residenceStatusField;
        private bool residenceStatusSet;
        public customerInfoResidenceStatus residenceStatus
        {
            get { return residenceStatusField; }
            set { residenceStatusField = value; residenceStatusSet = true; }
        }

        private int yearsAtResidenceField;
        private bool yearsAtResidenceSet;
        public int yearsAtResidence
        {
            get { return yearsAtResidenceField; }
            set { yearsAtResidenceField = value; yearsAtResidenceSet = true; }
        }

        private int yearsAtEmployerField;
        private bool yearsAtEmployerSet;
        public int yearsAtEmployer
        {
            get
            {
                return yearsAtEmployerField;
            }
            set
            {
                yearsAtEmployerField = value;
                yearsAtEmployerSet = true;
            }
        }


        public customerInfo()
        {
            incomeCurrency = currencyCodeEnum.USD;
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
    
    
    public partial class billMeLaterRequest
    {
        private long bmlMerchantIdField;
        private bool bmlMerchantIdSet;
        public long bmlMerchantId
        {
            get { return bmlMerchantIdField; }
            set { bmlMerchantIdField = value; bmlMerchantIdSet = true; }
        }
        private long bmlProductTypeField;
        private bool bmlProductTypeSet;
        public long bmlProductType
        {
            get { return bmlProductTypeField; }
            set { bmlProductTypeField = value; bmlProductTypeSet = true; }
        }
        private int termsAndConditionsField;
        private bool termsAndConditionsSet;
        public int termsAndConditions
        {
            get { return termsAndConditionsField; }
            set { termsAndConditionsField = value; termsAndConditionsSet = true; }
        }
        public string preapprovalNumber;
        private int merchantPromotionalCodeField;
        private bool merchantPromotionalCodeSet;
        public int merchantPromotionalCode
        {
            get { return merchantPromotionalCodeField; }
            set { merchantPromotionalCodeField = value; merchantPromotionalCodeSet = true; }
        }
        public string virtualAuthenticationKeyPresenceIndicator;
        public string virtualAuthenticationKeyData;
        private int itemCategoryCodeField;
        private bool itemCategoryCodeSet;
        public int itemCategoryCode
        {
            get { return itemCategoryCodeField; }
            set { itemCategoryCodeField = value; itemCategoryCodeSet = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class billMeLaterResponseData
    {

        private long bmlMerchantIdField;

        private string promotionalOfferCodeField;

        private int approvedTermsCodeField;

        private bool approvedTermsCodeFieldSpecified;

        private string creditLineField;

        private string addressIndicatorField;

        private string loanToValueEstimatorField;

        private string riskEstimatorField;

        private string riskQueueAssignmentField;

        /// <remarks/>
        public long bmlMerchantId
        {
            get
            {
                return this.bmlMerchantIdField;
            }
            set
            {
                this.bmlMerchantIdField = value;
            }
        }

        /// <remarks/>
        public string promotionalOfferCode
        {
            get
            {
                return this.promotionalOfferCodeField;
            }
            set
            {
                this.promotionalOfferCodeField = value;
            }
        }

        /// <remarks/>
        public int approvedTermsCode
        {
            get
            {
                return this.approvedTermsCodeField;
            }
            set
            {
                this.approvedTermsCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool approvedTermsCodeSpecified
        {
            get
            {
                return this.approvedTermsCodeFieldSpecified;
            }
            set
            {
                this.approvedTermsCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string creditLine
        {
            get
            {
                return this.creditLineField;
            }
            set
            {
                this.creditLineField = value;
            }
        }

        /// <remarks/>
        public string addressIndicator
        {
            get
            {
                return this.addressIndicatorField;
            }
            set
            {
                this.addressIndicatorField = value;
            }
        }

        /// <remarks/>
        public string loanToValueEstimator
        {
            get
            {
                return this.loanToValueEstimatorField;
            }
            set
            {
                this.loanToValueEstimatorField = value;
            }
        }

        /// <remarks/>
        public string riskEstimator
        {
            get
            {
                return this.riskEstimatorField;
            }
            set
            {
                this.riskEstimatorField = value;
            }
        }

        /// <remarks/>
        public string riskQueueAssignment
        {
            get
            {
                return this.riskQueueAssignmentField;
            }
            set
            {
                this.riskQueueAssignmentField = value;
            }
        }
    }
    
    
    public partial class authReversal : transactionTypeWithReportGroup
    {
        public long litleTxnId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
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
    
    
    public partial class fraudCheck : transactionTypeWithReportGroup
    {

        public advancedFraudChecksType advancedFraudChecks;

        private contact billToAddressField;
        private bool billToAddressSet;
        public contact billToAddress
        {
            get
            {
                return billToAddressField;
            }
            set
            {
                billToAddressField = value; billToAddressSet = true;
            }
        }

        private contact shipToAddressField;
        private bool shipToAddressSet;
        public contact shipToAddress
        {
            get
            {
                return shipToAddressField;
            }
            set
            {
                shipToAddressField = value; shipToAddressSet = true;
            }
        }

        private int amountField;
        private bool amountSet;
        public int amount
        {
            get
            {
                return amountField;
            }
            set
            {
                amountField = value; amountSet = true;
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
    
    
    public partial class fraudCheckType
    {
        public string authenticationValue;
        public string authenticationTransactionId;
        public string customerIpAddress;
        private bool authenticatedByMerchantField;
        private bool authenticatedByMerchantSet;
        public bool authenticatedByMerchant
        {
            get { return authenticatedByMerchantField; }
            set { authenticatedByMerchantField = value; authenticatedByMerchantSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (authenticationValue != null) xml += "\r\n<authenticationValue>" + SecurityElement.Escape(authenticationValue) + "</authenticationValue>";
            if (authenticationTransactionId != null) xml += "\r\n<authenticationTransactionId>" + SecurityElement.Escape(authenticationTransactionId) + "</authenticationTransactionId>";
            if (customerIpAddress != null) xml += "\r\n<customerIpAddress>" + SecurityElement.Escape(customerIpAddress) + "</customerIpAddress>";
            if (authenticatedByMerchantSet) xml += "\r\n<authenticatedByMerchant>" + authenticatedByMerchantField.ToString().ToLower() + "</authenticatedByMerchant>";
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
                return litleTxnIdField;
            }
            set
            {
                litleTxnIdField = value;
                litleTxnIdSet = true;
            }
        }
        public string orderId;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
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
            get { return taxTypeField; }
            set { taxTypeField = value; taxTypeSet = true; }
        }
        public enhancedData enhancedData;
        public amexAggregatorData amexAggregatorData;
        private bool allowPartialAuthField;
        private bool allowPartialAuthSet;
        public bool allowPartialAuth
        {
            get
            {
                return allowPartialAuthField;
            }
            set
            {
                allowPartialAuthField = value;
                allowPartialAuthSet = true;
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
                return fraudFilterOverrideField;
            }
            set
            {
                fraudFilterOverrideField = value;
                fraudFilterOverrideSet = true;
            }
        }
        public recurringRequest recurringRequest;
        private bool debtRepaymentField;
        private bool debtRepaymentSet;
        public bool debtRepayment
        {
            get
            {
                return debtRepaymentField;
            }
            set
            {
                debtRepaymentField = value;
                debtRepaymentSet = true;
            }
        }
        public advancedFraudChecksType advancedFraudChecks;
        public wallet wallet;
        private processingType processingTypeField;
        private bool processingTypeSet;

        public processingType processingType
        {
            get { return processingTypeField; }
            set { processingTypeField = value; processingTypeSet = true; }
        }

        private string originalNetworkTransactionIdField;
        private bool originalNetworkTransactionIdSet;
        public string originalNetworkTransactionId
        {
            get { return originalNetworkTransactionIdField; }
            set { originalNetworkTransactionIdField = value; originalNetworkTransactionIdSet = true; }
        }
    
        private long originalTransactionAmountField;
        private bool originalTxnAmountSet;

        public long originalTransactionAmount
        {
            get { return originalTransactionAmountField; }
            set { originalTransactionAmountField = value; originalTxnAmountSet = true; }
        }

        public override string Serialize()
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
                if (originalTxnAmountSet)
                {
                    xml += "\r\n<originalTransactionAmount>" + originalTransactionAmount + "</originalTransactionAmount>";
                }
            }
            
            xml += "\r\n</authorization>";
            return xml;
        }
    }
    
    
    public partial class capture : transactionTypeWithReportGroupAndPartial
    {
        public long litleTxnId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
        }
        public enhancedData enhancedData;
        public processingInstructions processingInstructions;
        private bool payPalOrderCompleteField;
        private bool payPalOrderCompleteSet;
        public bool payPalOrderComplete
        {
            get { return payPalOrderCompleteField; }
            set { payPalOrderCompleteField = value; payPalOrderCompleteSet = true; }
        }
        public string payPalNotes;

        private string pinField;
        private bool pinSet;
        public string pin
        {
            get { return pinField; }
            set { pinField = value; pinSet = true; }
        }

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
            if (pinSet) xml += "\r\n<pin>" + pin + "</pin>";
            xml += "\r\n</capture>";

            return xml;
        }
    }
    
    
    public class forceCapture : transactionTypeWithReportGroup
    {
        public string orderId;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
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
            get { return taxTypeField; }
            set { taxTypeField = value; taxTypeSet = true; }
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
            get { return debtRepaymentField; }
            set { debtRepaymentField = value; debtRepaymentSet = true; }
        }

        private processingType processingTypeField;
        private bool processingTypeSet;
        public processingType processingType
        {
            get { return processingTypeField; }
            set { processingTypeField = value; processingTypeSet = true; }
        }

        public override string Serialize()
        {
            var xml = "\r\n<forceCapture";
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
            if (debtRepaymentSet) xml += "\r\n<debtRepayment>" + debtRepayment.ToString().ToLower() + "</debtRepayment>";
            if (processingTypeSet)
            {
                xml += "\r\n<processingType>" + processingType + "</processingType>";
            }
            xml += "\r\n</forceCapture>";
            return xml;
        }
    }
    
    
    public class captureGivenAuth : transactionTypeWithReportGroup
    {
        public string orderId;
        public authInformation authInformation;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
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
            get { return taxTypeField; }
            set { taxTypeField = value; taxTypeSet = true; }
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
            get { return debtRepaymentField; }
            set { debtRepaymentField = value; debtRepaymentSet = true; }
        }

        private processingType processingTypeField;
        private bool processingTypeSet;
        public processingType processingType
        {
            get { return processingTypeField; }
            set { processingTypeField = value; processingTypeSet = true; }
        }

        private string originalNetworkTransactionIdField;
        private bool originalNetworkTransactionIdSet;
        public string originalNetworkTransactionId
        {
            get { return originalNetworkTransactionIdField; }
            set { originalNetworkTransactionIdField = value; originalNetworkTransactionIdSet = true; }
        }

        private long originalTransactionAmountField;
        private bool originalTransactionAmountSet;
        public long originalTransactionAmount
        {
            get { return originalTransactionAmountField; }
            set { originalTransactionAmountField = value; originalTransactionAmountSet = true; }
        }

        public override string Serialize()
        {
            var xml = "\r\n<captureGivenAuth";
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
            if (debtRepaymentSet) xml += "\r\n<debtRepayment>" + debtRepayment.ToString().ToLower() + "</debtRepayment>";
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
    
    
    public partial class sale : transactionTypeWithReportGroup
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
        public string orderId;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
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
		public idealType ideal;
        public giropayType giropay;
        public sofortType sofort;
		public billMeLaterRequest billMeLaterRequest;
        public fraudCheckType cardholderAuthentication;
        public customBilling customBilling;
        private govtTaxTypeEnum taxTypeField;
        private bool taxTypeSet;
        public govtTaxTypeEnum taxType
        {
            get { return taxTypeField; }
            set { taxTypeField = value; taxTypeSet = true; }
        }
        public enhancedData enhancedData;
        public processingInstructions processingInstructions;
        public pos pos;
        private bool payPalOrderCompleteField;
        private bool payPalOrderCompleteSet;
        public bool payPalOrderComplete
        {
            get { return payPalOrderCompleteField; }
            set { payPalOrderCompleteField = value; payPalOrderCompleteSet = true; }
        }
        public string payPalNotes;
        public amexAggregatorData amexAggregatorData;
        private bool allowPartialAuthField;
        private bool allowPartialAuthSet;
        public bool allowPartialAuth
        {
            get
            {
                return allowPartialAuthField;
            }
            set
            {
                allowPartialAuthField = value;
                allowPartialAuthSet = true;
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
                return fraudFilterOverrideField;
            }
            set
            {
                fraudFilterOverrideField = value;
                fraudFilterOverrideSet = true;
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
                return debtRepaymentField;
            }
            set
            {
                debtRepaymentField = value;
                debtRepaymentSet = true;
            }
        }
        public advancedFraudChecksType advancedFraudChecks;
        public wallet wallet;

        private processingType processingTypeField;
        private bool processingTypeSet;
        public processingType processingType
        {
            get { return processingTypeField; }
            set { processingTypeField = value; processingTypeSet = true; }
        }

        private string originalNetworkTransactionIdField;
        private bool originalNetworkTxnSet;
        public string originalNetworkTransactionId
        {
            get { return originalNetworkTransactionIdField;}
            set { originalNetworkTransactionIdField = value; originalNetworkTxnSet = true; }
        }

        private long originalTransactionAmountField;
        private bool originalTxnAmountSet;
        public long originalTransactionAmount
        {
            get { return originalTransactionAmountField; }
            set { originalTransactionAmountField = value; originalTxnAmountSet = true; }
        }

        public override string Serialize()
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
            else if (ideal != null)
            {
                xml += "\r\n<ideal>" + ideal.Serialize() + "\r\n</ideal>";
            }
            else if (giropay != null)
            {
                xml += "\r\n<giropay>" + giropay.Serialize() + "\r\n</giropay>";
            }
            else if (sofort != null)
            {
                xml += "\r\n<sofort>" + sofort.Serialize() + "\r\n</sofort>";
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
            if (processingTypeSet && processingType != processingType.undefined)
            {
                xml += "\r\n<processingType>" + processingType + "</processingType>";
            }
            if (originalNetworkTxnSet)
            {
                xml += "\r\n<originalNetworkTransactionId>" + originalNetworkTransactionId + "</originalNetworkTransactionId>";
            }
            if (originalTxnAmountSet)
            {
                xml += "\r\n<originalTransactionAmount>" + originalTransactionAmount + "</originalTransactionAmount>";
            }
            xml += "\r\n</sale>";
            return xml;
        }
    }
    
    
    public class credit : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get { return litleTxnIdField; }
            set { litleTxnIdField = value; litleTxnIdSet = true; }
        }
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
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
            get { return taxTypeField; }
            set { taxTypeField = value; taxTypeSet = true; }
        }
        public billMeLaterRequest billMeLaterRequest;
        public pos pos;
        public amexAggregatorData amexAggregatorData;
        public merchantDataType merchantData;
        public string payPalNotes;
        public string actionReason;

        private string pinField;
        private bool pinSet;
        public string pin
        {
            get { return pinField; }
            set { pinField = value; pinSet = true; }
        }

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
                if (pinSet) { xml += "\r\n<pin>" + pin + "</pin>"; }
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
    
    
    public partial class authInformation
    {
        public DateTime authDate;
        public string authCode;
        public fraudResult fraudResult;
        private long authAmountField;
        private bool authAmountSet;
        public long authAmount
        {
            get { return authAmountField; }
            set { authAmountField = value; authAmountSet = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum IIASFlagType
    {

        /// <remarks/>
        Y,
    }
    
    
    public partial class healthcareIIAS
    {
        public healthcareAmounts healthcareAmounts;
        private IIASFlagType IIASFlagField;
        private bool IIASFlagSet;
        public IIASFlagType IIASFlag
        {
            get { return IIASFlagField; }
            set { IIASFlagField = value; IIASFlagSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (healthcareAmounts != null) xml += "\r\n<healthcareAmounts>" + healthcareAmounts.Serialize() + "</healthcareAmounts>";
            if (IIASFlagSet) xml += "\r\n<IIASFlag>" + IIASFlagField + "</IIASFlag>";
            return xml;
        }
    }
    
    
    public partial class healthcareAmounts
    {
        private int totalHealthcareAmountField;
        private bool totalHealthcareAmountSet;
        public int totalHealthcareAmount
        {
            get { return totalHealthcareAmountField; }
            set { totalHealthcareAmountField = value; totalHealthcareAmountSet = true; }
        }

        private int RxAmountField;
        private bool RxAmountSet;
        public int RxAmount
        {
            get { return RxAmountField; }
            set { RxAmountField = value; RxAmountSet = true; }
        }

        private int visionAmountField;
        private bool visionAmountSet;
        public int visionAmount
        {
            get { return visionAmountField; }
            set { visionAmountField = value; visionAmountSet = true; }
        }

        private int clinicOtherAmountField;
        private bool clinicOtherAmountSet;
        public int clinicOtherAmount
        {
            get { return clinicOtherAmountField; }
            set { clinicOtherAmountField = value; clinicOtherAmountSet = true; }
        }

        private int dentalAmountField;
        private bool dentalAmountSet;
        public int dentalAmount
        {
            get { return dentalAmountField; }
            set { dentalAmountField = value; dentalAmountSet = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    public enum enhancedDataDeliveryType
    {

        /// <remarks/>
        CNC,

        /// <remarks/>
        DIG,

        /// <remarks/>
        PHY,

        /// <remarks/>
        SVC,

        /// <remarks/>
        TBD,
    }
    
    
    public partial class enhancedData
    {
        public string customerReference;
        private long salesTaxField;
        private bool salesTaxSet;
        public long salesTax
        {
            get { return salesTaxField; }
            set { salesTaxField = value; salesTaxSet = true; }
        }
        private enhancedDataDeliveryType deliveryTypeField;
        private bool deliveryTypeSet;
        public enhancedDataDeliveryType deliveryType
        {
            get { return deliveryTypeField; }
            set { deliveryTypeField = value; deliveryTypeSet = true; }
        }
        public bool taxExemptField;
        public bool taxExemptSet;
        public bool taxExempt
        {
            get { return taxExemptField; }
            set { taxExemptField = value; taxExemptSet = true; }
        }
        private long discountAmountField;
        private bool discountAmountSet;
        public long discountAmount
        {
            get { return discountAmountField; }
            set { discountAmountField = value; discountAmountSet = true; }
        }
        private long shippingAmountField;
        private bool shippingAmountSet;
        public long shippingAmount
        {
            get { return shippingAmountField; }
            set { shippingAmountField = value; shippingAmountSet = true; }
        }
        private long dutyAmountField;
        private bool dutyAmountSet;
        public long dutyAmount
        {
            get { return dutyAmountField; }
            set { dutyAmountField = value; dutyAmountSet = true; }
        }
        public string shipFromPostalCode;
        public string destinationPostalCode;
        private countryTypeEnum destinationCountryCodeField;
        private bool destinationCountryCodeSet;
        public countryTypeEnum destinationCountry
        {
            get { return destinationCountryCodeField; }
            set { destinationCountryCodeField = value; destinationCountryCodeSet = true; }
        }
        public string invoiceReferenceNumber;
        private DateTime orderDateField;
        private bool orderDateSet;
        public DateTime orderDate
        {
            get { return orderDateField; }
            set { orderDateField = value; orderDateSet = true; }
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
    
    
    public partial class detailTax
    {
        private bool taxIncludedInTotalField;
        private bool taxIncludedInTotalSet;
        public bool taxIncludedInTotal
        {
            get { return taxIncludedInTotalField; }
            set { taxIncludedInTotalField = value; taxIncludedInTotalSet = true; }
        }
        private long taxAmountField;
        private bool taxAmountSet;
        public long taxAmount
        {
            get { return taxAmountField; }
            set { taxAmountField = value; taxAmountSet = true; }
        }
        public string taxRate;
        private taxTypeIdentifierEnum taxTypeIdentifierField;
        private bool taxTypeIdentifierSet;
        public taxTypeIdentifierEnum taxTypeIdentifier
        {
            get { return taxTypeIdentifierField; }
            set { taxTypeIdentifierField = value; taxTypeIdentifierSet = true; }
        }
        public string cardAcceptorTaxId;

        public string Serialize()
        {
            string xml = "";
            if (taxIncludedInTotalSet) xml += "\r\n<taxIncludedInTotal>" + taxIncludedInTotalField.ToString().ToLower() + "</taxIncludedInTotal>";
            if (taxAmountSet) xml += "\r\n<taxAmount>" + taxAmountField + "</taxAmount>";
            if (taxRate != null) xml += "\r\n<taxRate>" + SecurityElement.Escape(taxRate) + "</taxRate>";
            if (taxTypeIdentifierSet)
            {
                Type type = taxTypeIdentifierField.GetType();
                System.Reflection.FieldInfo info = type.GetField(Enum.GetName(typeof(taxTypeIdentifierEnum), taxTypeIdentifierField));
                XmlEnumAttribute att = (XmlEnumAttribute)info.GetCustomAttributes(typeof(XmlEnumAttribute), false)[0];
                //If there is an xmlattribute defined, return the name

                xml += "\r\n<taxTypeIdentifier>" + att.Name + "</taxTypeIdentifier>";
            }
            if (cardAcceptorTaxId != null) xml += "\r\n<cardAcceptorTaxId>" + SecurityElement.Escape(cardAcceptorTaxId) + "</cardAcceptorTaxId>";
            return xml;
        }
    }
    
    
    public partial class lineItemData
    {
        private int itemSeqenceNumberField;
        private bool itemSequenceNumberSet;
        public int itemSequenceNumber
        {
            get { return itemSeqenceNumberField; }
            set { itemSeqenceNumberField = value; itemSequenceNumberSet = true; }
        }
        public string itemDescription;
        public string productCode;
        public string quantity;
        public string unitOfMeasure;
        private long taxAmountField;
        private bool taxAmountSet;
        public long taxAmount
        {
            get { return taxAmountField; }
            set { taxAmountField = value; taxAmountSet = true; }
        }
        private long lineItemTotalField;
        private bool lineItemTotalSet;
        public long lineItemTotal
        {
            get { return lineItemTotalField; }
            set { lineItemTotalField = value; lineItemTotalSet = true; }
        }
        private long lineItemTotalWithTaxField;
        private bool lineItemTotalWithTaxSet;
        public long lineItemTotalWithTax
        {
            get { return lineItemTotalWithTaxField; }
            set { lineItemTotalWithTaxField = value; lineItemTotalWithTaxSet = true; }
        }
        private long itemDiscountAmountField;
        private bool itemDiscountAmountSet;
        public long itemDiscountAmount
        {
            get { return itemDiscountAmountField; }
            set { itemDiscountAmountField = value; itemDiscountAmountSet = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class accountInfoType
    {

        private methodOfPaymentTypeEnum typeField;

        private string numberField;

        /// <remarks/>
        public methodOfPaymentTypeEnum type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public string number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
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
            get { return accTypeField; }
            set { accTypeField = value; accTypeSet = true; }
        }
        public string checkNum;

        public string Serialize()
        {
            string xml = "";
            if (litleToken != null) xml += "\r\n<litleToken>" + SecurityElement.Escape(litleToken) + "</litleToken>";
            if (routingNum != null) xml += "\r\n<routingNum>" + SecurityElement.Escape(routingNum) + "</routingNum>";
            string accTypeName = accTypeField.ToString();
            XmlEnumAttribute[] attributes =
                (XmlEnumAttribute[])typeof(echeckAccountTypeEnum).GetMember(accTypeField.ToString())[0].GetCustomAttributes(typeof(XmlEnumAttribute), false);
            if (attributes.Length > 0) accTypeName = attributes[0].Name;
            if (accTypeSet) xml += "\r\n<accType>" + accTypeName + "</accType>";
            if (checkNum != null) xml += "\r\n<checkNum>" + SecurityElement.Escape(checkNum) + "</checkNum>";
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum taxTypeIdentifierEnum
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("00")]
        Item00,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("20")]
        Item20,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("21")]
        Item21,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("22")]
        Item22,
    }
    
    
    public partial class processingInstructions
    {
        private bool bypassVelocityCheckField;
        private bool bypassVelocityCheckSet;
        public bool bypassVelocityCheck
        {
            get { return bypassVelocityCheckField; }
            set { bypassVelocityCheckField = value; bypassVelocityCheckSet = true; }
        }

        public string Serialize()
        {
            string xml = "";
            if (bypassVelocityCheckSet) xml += "\r\n<bypassVelocityCheck>" + bypassVelocityCheckField.ToString().ToLower() + "</bypassVelocityCheck>";
            return xml;
        }
    }
    
    
    public partial class pos
    {
        private posCapabilityTypeEnum capabilityField;
        private bool capabilitySet;
        public posCapabilityTypeEnum capability
        {
            get { return capabilityField; }
            set { capabilityField = value; capabilitySet = true; }
        }

        private posEntryModeTypeEnum entryModeField;
        private bool entryModeSet;
        public posEntryModeTypeEnum entryMode
        {
            get { return entryModeField; }
            set { entryModeField = value; entryModeSet = true; }
        }

        private posCardholderIdTypeEnum cardholderIdField;
        private bool cardholderIdSet;
        public posCardholderIdTypeEnum cardholderId
        {
            get { return cardholderIdField; }
            set { cardholderIdField = value; cardholderIdSet = true; }
        }
        public string terminalId;

        private posCatLevelEnum catLevelField;
        private bool catLevelSet;
        public posCatLevelEnum catLevel
        {
            get { return catLevelField; }
            set { catLevelField = value; catLevelSet = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class registerTokenResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string orderIdField;

        private string litleTokenField;

        private string binField;

        private methodOfPaymentTypeEnum? typeField;

        private bool typeFieldSpecified;

        private string eCheckAccountSuffixField;

        private string responseField;

        private string messageField;

        private System.DateTime responseTimeField;

        private applepayResponse applepayResponseField;

        private androidpayResponse androidpayResponseField;

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
        public string litleToken
        {
            get
            {
                return this.litleTokenField;
            }
            set
            {
                this.litleTokenField = value;
            }
        }

        /// <remarks/>
        public string bin
        {
            get
            {
                return this.binField;
            }
            set
            {
                this.binField = value;
            }
        }

        /// <remarks/>
        public methodOfPaymentTypeEnum? type
        {
            get
            {
                return this.typeFieldSpecified ? this.typeField : null;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeSpecified
        {
            get
            {
                return this.typeFieldSpecified;
            }
            set
            {
                this.typeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string eCheckAccountSuffix
        {
            get
            {
                return this.eCheckAccountSuffixField;
            }
            set
            {
                this.eCheckAccountSuffixField = value;
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
        public applepayResponse applepayResponse
        {
            get
            {
                return this.applepayResponseField;
            }
            set
            {
                this.applepayResponseField = value;
            }
        }

        public androidpayResponse androidpayResponse
        {
            get { return androidpayResponseField; }
            set { androidpayResponseField = value; }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class authorizationResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string cardProductIdField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private string messageField;

        private string authCodeField;

        private string authorizationResponseSubCodeField;

        private string approvedAmountField;

        private accountInfoType accountInformationField;

        private accountUpdater accountUpdaterField;

        private fraudResult fraudResultField;

        private billMeLaterResponseData billMeLaterResponseDataField;

        private tokenResponseType tokenResponseField;

        private enhancedAuthResponse enhancedAuthResponseField;

        private recyclingType recyclingField;

        private recurringResponse recurringResponseField;

        private giftCardResponse giftCardResponseField;

        private applepayResponse applepayResponseField;

        private string cardSuffixField;

        private androidpayResponse androidpayResponseField;

        private string networkTransactionIdField;

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
        public string cardProductId
        {
            get
            {
                return this.cardProductIdField;
            }
            set
            {
                this.cardProductIdField = value;
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
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
        public string authCode
        {
            get
            {
                return this.authCodeField;
            }
            set
            {
                this.authCodeField = value;
            }
        }

        /// <remarks/>
        public string authorizationResponseSubCode
        {
            get
            {
                return this.authorizationResponseSubCodeField;
            }
            set
            {
                this.authorizationResponseSubCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string approvedAmount
        {
            get
            {
                return this.approvedAmountField;
            }
            set
            {
                this.approvedAmountField = value;
            }
        }

        /// <remarks/>
        public accountInfoType accountInformation
        {
            get
            {
                return this.accountInformationField;
            }
            set
            {
                this.accountInformationField = value;
            }
        }

        /// <remarks/>
        public accountUpdater accountUpdater
        {
            get
            {
                return this.accountUpdaterField;
            }
            set
            {
                this.accountUpdaterField = value;
            }
        }

        /// <remarks/>
        public fraudResult fraudResult
        {
            get
            {
                return this.fraudResultField;
            }
            set
            {
                this.fraudResultField = value;
            }
        }

        /// <remarks/>
        public billMeLaterResponseData billMeLaterResponseData
        {
            get
            {
                return this.billMeLaterResponseDataField;
            }
            set
            {
                this.billMeLaterResponseDataField = value;
            }
        }

        /// <remarks/>
        public tokenResponseType tokenResponse
        {
            get
            {
                return this.tokenResponseField;
            }
            set
            {
                this.tokenResponseField = value;
            }
        }

        /// <remarks/>
        public enhancedAuthResponse enhancedAuthResponse
        {
            get
            {
                return this.enhancedAuthResponseField;
            }
            set
            {
                this.enhancedAuthResponseField = value;
            }
        }

        /// <remarks/>
        public recyclingType recycling
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

        /// <remarks/>
        public recurringResponse recurringResponse
        {
            get
            {
                return this.recurringResponseField;
            }
            set
            {
                this.recurringResponseField = value;
            }
        }

        /// <remarks/>
        public giftCardResponse giftCardResponse
        {
            get
            {
                return this.giftCardResponseField;
            }
            set
            {
                this.giftCardResponseField = value;
            }
        }

        /// <remarks/>
        public applepayResponse applepayResponse
        {
            get
            {
                return this.applepayResponseField;
            }
            set
            {
                this.applepayResponseField = value;
            }
        }

        public string cardSuffix
        {
            get { return cardSuffixField; }
            set { cardSuffixField = value; }
        }

        public androidpayResponse androidpayResponse
        {
            get { return androidpayResponseField; }
            set { androidpayResponseField = value; }
        }

        public string networkTransactionId
        {
            get { return networkTransactionIdField; }
            set { networkTransactionIdField = value; }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum fundingSourceTypeEnum
    {

        /// <remarks/>
        UNKNOWN,

        /// <remarks/>
        PREPAID,

        /// <remarks/>
        FSA,

        /// <remarks/>
        CREDIT,

        /// <remarks/>
        DEBIT,
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum affluenceTypeEnum
    {

        /// <remarks/>
        AFFLUENT,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("MASS AFFLUENT")]
        MASSAFFLUENT,
    }
    
    
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class cardSuffixType
    {
        private string cardSuffixField;

        public string cardSuffix
        {
            get { return cardSuffixField; }
            set { cardSuffixField = value; }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum cardProductTypeEnum
    {

        /// <remarks/>
        UNKNOWN,

        /// <remarks/>
        COMMERCIAL,

        /// <remarks/>
        CONSUMER,
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class recycleAdviceType
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("nextRecycleTime", typeof(System.DateTime))]
        [System.Xml.Serialization.XmlElementAttribute("recycleAdviceEnd", typeof(string))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum recycleByTypeEnum
    {

        /// <remarks/>
        Merchant,

        /// <remarks/>
        Litle,

        /// <remarks/>
        None,
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class recyclingType
    {

        private recycleAdviceType recycleAdviceField;

        private bool recycleEngineActiveField;   

        /// <remarks/>
        public recycleAdviceType recycleAdvice
        {
            get
            {
                return this.recycleAdviceField;
            }
            set
            {
                this.recycleAdviceField = value;
            }
        }

        /// <remarks/>
        public bool recycleEngineActive
        {
            get
            {
                return this.recycleEngineActiveField;
            }
            set
            {
                this.recycleEngineActiveField = value;
            }
        }
    
    }
    
    
    public partial class recyclingRequestType
    {
        private recycleByTypeEnum recycleByField;
        private bool recycleBySet;
        public recycleByTypeEnum recycleBy
        {
            get { return recycleByField; }
            set { recycleByField = value; recycleBySet = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    public partial class enhancedAuthResponseFundingSource
    {

        private fundingSourceTypeEnum typeField;

        private string availableBalanceField;

        private string reloadableField;

        private string prepaidCardTypeField;

        /// <remarks/>
        public fundingSourceTypeEnum type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public string availableBalance
        {
            get
            {
                return this.availableBalanceField;
            }
            set
            {
                this.availableBalanceField = value;
            }
        }

        /// <remarks/>
        public string reloadable
        {
            get
            {
                return this.reloadableField;
            }
            set
            {
                this.reloadableField = value;
            }
        }

        /// <remarks/>
        public string prepaidCardType
        {
            get
            {
                return this.prepaidCardTypeField;
            }
            set
            {
                this.prepaidCardTypeField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class enhancedAuthResponse
    {
        //public string litleTxnId;
        //public string response;
        //public string message;
        //public System.DateTime responseTime;
        //public tokenResponseType tokenResponse;
        //public string virtualAccountNumber;
        //public enhancedAuthResponseFundingSource fundingSource;
        //public affluenceTypeEnum affluence;
        //public string issuerCountry;

        private enhancedAuthResponseFundingSource fundingSourceField;

        private affluenceTypeEnum? affluenceField;

        private bool affluenceFieldSpecified;

        private string issuerCountryField;

        private cardProductTypeEnum? cardProductTypeField;

        private bool cardProductTypeFieldSpecified;

        private bool virtualAccountNumberField;

        private bool virtualAccountNumberFieldSpecified;

        /// <remarks/>
        public enhancedAuthResponseFundingSource fundingSource
        {
            get
            {
                return this.fundingSourceField;
            }
            set
            {
                this.fundingSourceField = value;
            }
        }

        /// <remarks/>
        public affluenceTypeEnum? affluence
        {
            get
            {
                return this.affluenceFieldSpecified ? this.affluenceField : null;
                //(!null)?return ((affluenceTypeEnum?)this).affluenceField:return null;
            }
            set
            {
                this.affluenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool affluenceSpecified
        {
            get
            {
                return this.affluenceFieldSpecified;
            }
            set
            {
                this.affluenceFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string issuerCountry
        {
            get
            {
                return this.issuerCountryField;
            }
            set
            {
                this.issuerCountryField = value;
            }
        }

        /// <remarks/>
        public cardProductTypeEnum? cardProductType
        {
            get
            {
                return this.cardProductTypeFieldSpecified ? this.cardProductTypeField : null;
            }
            set
            {
                this.cardProductTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cardProductTypeSpecified
        {
            get
            {
                return this.cardProductTypeFieldSpecified;
            }
            set
            {
                this.cardProductTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool virtualAccountNumber
        {
            get
            {
                return this.virtualAccountNumberField;
            }
            set
            {
                this.virtualAccountNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool virtualAccountNumberSpecified
        {
            get
            {
                return this.virtualAccountNumberFieldSpecified;
            }
            set
            {
                this.virtualAccountNumberFieldSpecified = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class authReversalResponse : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private string messageField;

        public giftCardResponse giftCardResponse;

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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
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
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class depositReversalResponse : transactionTypeWithReportGroup
    {
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class refundReversalResponse : transactionTypeWithReportGroup
    {
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class activateReversalResponse : transactionTypeWithReportGroup
    {
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class deactivateReversalResponse : transactionTypeWithReportGroup
    {
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class loadReversalResponse : transactionTypeWithReportGroup
    {
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class unloadReversalResponse : transactionTypeWithReportGroup
    {
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class captureResponse : transactionTypeWithReportGroup
    {
        public giftCardResponse giftCardResponse;
        public fraudResult fraudResult;

        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private string messageField;

        private accountUpdater accountUpdaterField;

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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
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
        public accountUpdater accountUpdater
        {
            get
            {
                return this.accountUpdaterField;
            }
            set
            {
                this.accountUpdaterField = value;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class forceCaptureResponse : transactionTypeWithReportGroup
    {
        public giftCardResponse giftCardResponse;
        public fraudResult fraudResult;
        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private string messageField;

        private tokenResponseType tokenResponseField;

        private accountUpdater accountUpdaterField;

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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
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
        public tokenResponseType tokenResponse
        {
            get
            {
                return this.tokenResponseField;
            }
            set
            {
                this.tokenResponseField = value;
            }
        }

        /// <remarks/>
        public accountUpdater accountUpdater
        {
            get
            {
                return this.accountUpdaterField;
            }
            set
            {
                this.accountUpdaterField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class captureGivenAuthResponse : transactionTypeWithReportGroup
    {
        public giftCardResponse giftCardResponse;
        public fraudResult fraudResult;
        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private string messageField;

        private tokenResponseType tokenResponseField;

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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
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
        public tokenResponseType tokenResponse
        {
            get
            {
                return this.tokenResponseField;
            }
            set
            {
                this.tokenResponseField = value;
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
    public partial class saleResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string cardProductIdField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private string messageField;

        private string authCodeField;

        private string authorizationResponseSubCodeField;

        private string approvedAmountField;

        private accountInfoType accountInformationField;

        private fraudResult fraudResultField;

        private billMeLaterResponseData billMeLaterResponseDataField;

        private tokenResponseType tokenResponseField;

        private enhancedAuthResponse enhancedAuthResponseField;

        private accountUpdater accountUpdaterField;

        private recyclingType recyclingField;

        private recurringResponse recurringResponseField;

        private giftCardResponse giftCardResponseField;

        private applepayResponse applepayResponseField;
        
        private giropayResponse giropayResponseField;

        private sofortResponse sofortResponseField;

        private bool duplicateField;

        private bool duplicateFieldSpecified;

        private string cardSuffixField;

        private androidpayResponse androidpayResponseField;

        private string networkTransactionIdField;

        private sepaDirectDebitResponse sepaDirectDebitResponseField;

        private idealResponse idealResponseField;

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
        public string cardProductId
        {
            get
            {
                return this.cardProductIdField;
            }
            set
            {
                this.cardProductIdField = value;
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
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
        public string authCode
        {
            get
            {
                return this.authCodeField;
            }
            set
            {
                this.authCodeField = value;
            }
        }

        /// <remarks/>
        public string authorizationResponseSubCode
        {
            get
            {
                return this.authorizationResponseSubCodeField;
            }
            set
            {
                this.authorizationResponseSubCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string approvedAmount
        {
            get
            {
                return this.approvedAmountField;
            }
            set
            {
                this.approvedAmountField = value;
            }
        }

        /// <remarks/>
        public accountInfoType accountInformation
        {
            get
            {
                return this.accountInformationField;
            }
            set
            {
                this.accountInformationField = value;
            }
        }

        /// <remarks/>
        public fraudResult fraudResult
        {
            get
            {
                return this.fraudResultField;
            }
            set
            {
                this.fraudResultField = value;
            }
        }

        /// <remarks/>
        public billMeLaterResponseData billMeLaterResponseData
        {
            get
            {
                return this.billMeLaterResponseDataField;
            }
            set
            {
                this.billMeLaterResponseDataField = value;
            }
        }

        /// <remarks/>
        public tokenResponseType tokenResponse
        {
            get
            {
                return this.tokenResponseField;
            }
            set
            {
                this.tokenResponseField = value;
            }
        }

        /// <remarks/>
        public enhancedAuthResponse enhancedAuthResponse
        {
            get
            {
                return this.enhancedAuthResponseField;
            }
            set
            {
                this.enhancedAuthResponseField = value;
            }
        }

        /// <remarks/>
        public accountUpdater accountUpdater
        {
            get
            {
                return this.accountUpdaterField;
            }
            set
            {
                this.accountUpdaterField = value;
            }
        }

        /// <remarks/>
        public recyclingType recycling
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

        /// <remarks/>
        public recurringResponse recurringResponse
        {
            get
            {
                return this.recurringResponseField;
            }
            set
            {
                this.recurringResponseField = value;
            }
        }

        /// <remarks/>
        public giftCardResponse giftCardResponse
        {
            get
            {
                return this.giftCardResponseField;
            }
            set
            {
                this.giftCardResponseField = value;
            }
        }

        /// <remarks/>
        public applepayResponse applepayResponse
        {
            get
            {
                return this.applepayResponseField;
            }
            set
            {
                this.applepayResponseField = value;
            }
        }
        
        /// <remarks/>
        public giropayResponse giropayResponse
        {
            get
            {
                return this.giropayResponseField;
            }
            set
            {
                this.giropayResponseField = value;
            }
        }

        /// <remarks/>
        public sofortResponse sofortResponse
        {
            get
            {
                return this.sofortResponseField;
            }
            set
            {
                this.sofortResponseField = value;
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

        public string cardSuffix
        {
            get { return cardSuffixField; }
            set { cardSuffixField = value; }
        }

        public androidpayResponse androidpayResponse
        {
            get { return androidpayResponseField; }
            set { androidpayResponseField = value; }
        }

        public string networkTransactionId
        {
            get { return networkTransactionIdField; }
            set { networkTransactionIdField = value; }
        }

        /// <remarks/>
        public sepaDirectDebitResponse sepaDirectDebitResponse
        {
            get
            {
                return this.sepaDirectDebitResponseField;
            }
            set
            {
                this.sepaDirectDebitResponseField = value;
            }
        }

        /// <remarks/>
        public idealResponse idealResponse
        {
            get
            {
                return this.idealResponseField;
            }
            set
            {
                this.idealResponseField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class creditResponse : transactionTypeWithReportGroup
    {
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private string messageField;

        private tokenResponseType tokenResponseField;

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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
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
        public tokenResponseType tokenResponse
        {
            get
            {
                return this.tokenResponseField;
            }
            set
            {
                this.tokenResponseField = value;
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class fraudResult
    {

        private string avsResultField;

        private string cardValidationResultField;

        private string authenticationResultField;

        private string advancedAVSResultField;

        public advancedFraudResultsType advancedFraudResults;

        /// <remarks/>
        public string avsResult
        {
            get
            {
                return this.avsResultField;
            }
            set
            {
                this.avsResultField = value;
            }
        }

        /// <remarks/>
        public string cardValidationResult
        {
            get
            {
                return this.cardValidationResultField;
            }
            set
            {
                this.cardValidationResultField = value;
            }
        }

        /// <remarks/>
        public string authenticationResult
        {
            get
            {
                return this.authenticationResultField;
            }
            set
            {
                this.authenticationResultField = value;
            }
        }

        /// <remarks/>
        public string advancedAVSResult
        {
            get
            {
                return this.advancedAVSResultField;
            }
            set
            {
                this.advancedAVSResultField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class activateResponse : transactionTypeWithReportGroup
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool duplicate;
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
        public virtualGiftCardResponseType virtualGiftCardResponse;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class loadResponse : transactionTypeWithReportGroup
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool duplicate;
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class unloadResponse : transactionTypeWithReportGroup
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool duplicate;
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    public partial class giftCardResponse
    {
        public string availableBalance;
        public string beginningBalance;
        public string endingBalance;
        public string cashBackAmount;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    public partial class virtualGiftCardResponseType
    {
        public string accountNumber;
        public string cardValidationNum;
        public string pin;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class balanceInquiryResponse : transactionTypeWithReportGroup
    {
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
    }
    
    
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class deactivateResponse : transactionTypeWithReportGroup
    {
        public string litleTxnId;
        public string orderId;
        public string response;
        public DateTime responseTime;
        public DateTime postDate;
        public string message;
        public fraudResult fraudResult;
        public giftCardResponse giftCardResponse;
    }
    
    
    public partial class echeckType
    {
        private echeckAccountTypeEnum accTypeField;
        private bool accTypeSet;
        public echeckAccountTypeEnum accType
        {
            get { return accTypeField; }
            set { accTypeField = value; accTypeSet = true; }
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
    
    
    public partial class echeckSale : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get { return litleTxnIdField; }
            set { litleTxnIdField = value; litleTxnIdSet = true; }
        }
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        public customBilling customBilling;
        public string orderId;
        private bool verifyField;
        private bool verifySet;
        public bool verify
        {
            get { return verifyField; }
            set { verifyField = value; verifySet = true; }
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
    
    
    public partial class echeckCredit : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get { return litleTxnIdField; }
            set { litleTxnIdField = value; litleTxnIdSet = true; }
        }
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
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
    
    
    public partial class echeckVerification : transactionTypeWithReportGroup
    {
        private long litleTxnIdField;
        private bool litleTxnIdSet;
        public long litleTxnId
        {
            get { return litleTxnIdField; }
            set { litleTxnIdField = value; litleTxnIdSet = true; }
        }
        public string orderId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class echeckSalesResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        private string verificationCodeField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private accountUpdater accountUpdaterField;

        private tokenResponseType tokenResponseField;

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
        public string verificationCode
        {
            get
            {
                return this.verificationCodeField;
            }
            set
            {
                this.verificationCodeField = value;
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public accountUpdater accountUpdater
        {
            get
            {
                return this.accountUpdaterField;
            }
            set
            {
                this.accountUpdaterField = value;
            }
        }

        /// <remarks/>
        public tokenResponseType tokenResponse
        {
            get
            {
                return this.tokenResponseField;
            }
            set
            {
                this.tokenResponseField = value;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class echeckCreditResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private accountUpdater accountUpdaterField;

        private tokenResponseType tokenResponseField;

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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public accountUpdater accountUpdater
        {
            get
            {
                return this.accountUpdaterField;
            }
            set
            {
                this.accountUpdaterField = value;
            }
        }

        /// <remarks/>
        public tokenResponseType tokenResponse
        {
            get
            {
                return this.tokenResponseField;
            }
            set
            {
                this.tokenResponseField = value;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class echeckVerificationResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private tokenResponseType tokenResponseField;

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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public tokenResponseType tokenResponse
        {
            get
            {
                return this.tokenResponseField;
            }
            set
            {
                this.tokenResponseField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class echeckAccountInfoType
    {

        private echeckAccountTypeEnum accTypeField;

        private string accNumField;

        private string routingNumField;

        /// <remarks/>
        public echeckAccountTypeEnum accType
        {
            get
            {
                return this.accTypeField;
            }
            set
            {
                this.accTypeField = value;
            }
        }

        /// <remarks/>
        public string accNum
        {
            get
            {
                return this.accNumField;
            }
            set
            {
                this.accNumField = value;
            }
        }

        /// <remarks/>
        public string routingNum
        {
            get
            {
                return this.routingNumField;
            }
            set
            {
                this.routingNumField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class echeckTokenInfoType
    {

        private echeckAccountTypeEnum accTypeField;

        private string litleTokenField;

        private string routingNumField;

        /// <remarks/>
        public echeckAccountTypeEnum accType
        {
            get
            {
                return this.accTypeField;
            }
            set
            {
                this.accTypeField = value;
            }
        }

        /// <remarks/>
        public string litleToken
        {
            get
            {
                return this.litleTokenField;
            }
            set
            {
                this.litleTokenField = value;
            }
        }

        /// <remarks/>
        public string routingNum
        {
            get
            {
                return this.routingNumField;
            }
            set
            {
                this.routingNumField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class cardAccountInfoType
    {

        private methodOfPaymentTypeEnum typeField;

        private string numberField;

        private string expDateField;

        /// <remarks/>
        public methodOfPaymentTypeEnum type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public string number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        public string expDate
        {
            get
            {
                return this.expDateField;
            }
            set
            {
                this.expDateField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class cardTokenInfoType
    {

        private string litleTokenField;

        private methodOfPaymentTypeEnum typeField;

        private string expDateField;

        private string binField;

        /// <remarks/>
        public string litleToken
        {
            get
            {
                return this.litleTokenField;
            }
            set
            {
                this.litleTokenField = value;
            }
        }

        /// <remarks/>
        public methodOfPaymentTypeEnum type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public string expDate
        {
            get
            {
                return this.expDateField;
            }
            set
            {
                this.expDateField = value;
            }
        }

        /// <remarks/>
        public string bin
        {
            get
            {
                return this.binField;
            }
            set
            {
                this.binField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public partial class extendedCardResponseType
    {

        private string messageField;

        private string codeField;

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
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class accountUpdater
    {
        public extendedCardResponseType extendedCardResponse;
        public echeckAccountInfoType newAccountInfo;
        public cardAccountInfoType newCardInfo;
        public cardTokenInfoType newCardTokenInfo;
        public echeckTokenInfoType newTokenInfo;
        public echeckAccountInfoType originalAccountInfo;
        public cardAccountInfoType originalCardInfo;
        public cardTokenInfoType originalCardTokenInfo;
        public echeckTokenInfoType originalTokenInfo;
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute("echeckRedeposit", Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class baseRequestTransactionEcheckRedeposit : transactionTypeWithReportGroup
    {

        protected long litleTxnIdField;
        protected bool litleTxnIdSet;

        private object itemField;

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
                this.litleTxnIdSet = true;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("echeck", typeof(echeckType))]
        [System.Xml.Serialization.XmlElementAttribute("echeckToken", typeof(echeckTokenType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class echeckRedepositResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string responseField;

        private System.DateTime responseTimeField;

        private string messageField;

        private System.DateTime postDateField;

        private bool postDateFieldSpecified;

        private accountUpdater accountUpdaterField;

        private tokenResponseType tokenResponseField;

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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postDateSpecified
        {
            get
            {
                return this.postDateFieldSpecified;
            }
            set
            {
                this.postDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public accountUpdater accountUpdater
        {
            get
            {
                return this.accountUpdaterField;
            }
            set
            {
                this.accountUpdaterField = value;
            }
        }

        /// <remarks/>
        public tokenResponseType tokenResponse
        {
            get
            {
                return this.tokenResponseField;
            }
            set
            {
                this.tokenResponseField = value;
            }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum echeckAccountTypeEnum
    {

        /// <remarks/>
        Checking,

        /// <remarks/>
        Savings,

        /// <remarks/>
        Corporate,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Corp Savings")]
        CorpSavings,
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

        private orderSourceType(string value) { this.value = value; }
        public string Serialize() { return value; }
        private string value;
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum posCapabilityTypeEnum
    {

        /// <remarks/>
        notused,

        /// <remarks/>
        magstripe,

        /// <remarks/>
        keyedonly,
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum posEntryModeTypeEnum
    {

        /// <remarks/>
        notused,

        /// <remarks/>
        keyed,

        /// <remarks/>
        track1,

        /// <remarks/>
        track2,

        /// <remarks/>
        completeread,
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema")]
    public enum posCardholderIdTypeEnum
    {

        /// <remarks/>
        signature,

        /// <remarks/>
        pin,

        /// <remarks/>
        nopin,

        /// <remarks/>
        directmarket,
    }
    
    
    public sealed class posCatLevelEnum
    {
        public static readonly posCatLevelEnum selfservice = new posCatLevelEnum("self service");

        private posCatLevelEnum(String value) { this.value = value; }
        public string Serialize() { return value; }
        private string value;
    }
    
    
    public partial class filteringType
    {
        private bool prepaidField;
        private bool prepaidSet;
        public bool prepaid
        {
            get { return prepaidField; }
            set { prepaidField = value; prepaidSet = true; }
        }

        private bool internationalField;
        private bool internationalSet;
        public bool international
        {
            get { return internationalField; }
            set { internationalField = value; internationalSet = true; }
        }

        private bool chargebackField;
        private bool chargebackSet;
        public bool chargeback
        {
            get { return chargebackField; }
            set { chargebackField = value; chargebackSet = true; }
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class updateCardValidationNumOnTokenResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string orderIdField;

        private string responseField;

        private string messageField;

        private System.DateTime responseTimeField;

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
    }
    
    
    public partial class virtualGiftCardType
    {
        public int accountNumberLength
        {
            get { return accountNumberLengthField; }
            set { accountNumberLengthField = value; accountNumberLengthSet = true; }
        }
        private int accountNumberLengthField;
        private bool accountNumberLengthSet;

        public string giftCardBin;

        public string Serialize()
        {
            string xml = "";
            if (accountNumberLengthSet) xml += "\r\n<accountNumberLength>" + accountNumberLengthField + "</accountNumberLength>";
            if (giftCardBin != null) xml += "\r\n<giftCardBin>" + SecurityElement.Escape(giftCardBin) + "</giftCardBin>";
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
    public partial class fraudCheckResponse : transactionTypeWithReportGroup
    {

        private long litleTxnIdField;

        private string responseField;

        private string messageField;

        private System.DateTime responseTimeField;

        private advancedFraudResultsType advancedFraudResultsField;

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
        public advancedFraudResultsType advancedFraudResults
        {
            get
            {
                return this.advancedFraudResultsField;
            }
            set
            {
                this.advancedFraudResultsField = value;
            }
        }
    }
    
    
    public enum walletWalletSourceType
    {
        MasterPass,
        VisaCheckout
    }
    
    
    public class wallet
    {
        public walletWalletSourceType walletSourceType;
        public string walletSourceTypeId;

        public string Serialize()
        {
            var xml = "\r\n<walletSourceType>" + walletSourceType + "</walletSourceType>";
            if (walletSourceTypeId != null) xml += "\r\n<walletSourceTypeId>" + SecurityElement.Escape(walletSourceTypeId) + "</walletSourceTypeId>";
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
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class applepayResponse
    {

        private string applicationPrimaryAccountNumberField;

        private string applicationExpirationDateField;

        private string currencyCodeField;

        private string transactionAmountField;

        private string cardholderNameField;

        private string deviceManufacturerIdentifierField;

        private string paymentDataTypeField;

        private byte[] onlinePaymentCryptogramField;

        private string eciIndicatorField;

        /// <remarks/>
        public string applicationPrimaryAccountNumber
        {
            get
            {
                return this.applicationPrimaryAccountNumberField;
            }
            set
            {
                this.applicationPrimaryAccountNumberField = value;
            }
        }

        /// <remarks/>
        public string applicationExpirationDate
        {
            get
            {
                return this.applicationExpirationDateField;
            }
            set
            {
                this.applicationExpirationDateField = value;
            }
        }

        /// <remarks/>
        public string currencyCode
        {
            get
            {
                return this.currencyCodeField;
            }
            set
            {
                this.currencyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string transactionAmount
        {
            get
            {
                return this.transactionAmountField;
            }
            set
            {
                this.transactionAmountField = value;
            }
        }

        /// <remarks/>
        public string cardholderName
        {
            get
            {
                return this.cardholderNameField;
            }
            set
            {
                this.cardholderNameField = value;
            }
        }

        /// <remarks/>
        public string deviceManufacturerIdentifier
        {
            get
            {
                return this.deviceManufacturerIdentifierField;
            }
            set
            {
                this.deviceManufacturerIdentifierField = value;
            }
        }

        /// <remarks/>
        public string paymentDataType
        {
            get
            {
                return this.paymentDataTypeField;
            }
            set
            {
                this.paymentDataTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] onlinePaymentCryptogram
        {
            get
            {
                return this.onlinePaymentCryptogramField;
            }
            set
            {
                this.onlinePaymentCryptogramField = value;
            }
        }

        /// <remarks/>
        public string eciIndicator
        {
            get
            {
                return this.eciIndicatorField;
            }
            set
            {
                this.eciIndicatorField = value;
            }
        }
    }
    
    
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.litle.com/schema", IsNullable = false)]
    public partial class androidpayResponse
    {

        private string expMonthField;

        private string expYearField;

        private byte[] cryptogramField;

        private string eciIndicatorField;
        /// <remarks/>
        public string expYear
        {
            get
            {
                return expYearField;
            }
            set
            {
                expYearField = value;
            }
        }

        /// <remarks/>
        public string expMonth
        {
            get
            {
                return expMonthField;
            }
            set
            {
                expMonthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] cryptogram
        {
            get
            {
                return cryptogramField;
            }
            set
            {
                cryptogramField = value;
            }
        }

        /// <remarks/>
        public string eciIndicator
        {
            get
            {
                return this.eciIndicatorField;
            }
            set
            {
                this.eciIndicatorField = value;
            }
        }
    }
    
    
    public enum processingType
    {
        undefined,
        accountFunding,
        initialRecurring,
        initialInstallment,
        initialCOF,
        merchantInitiatedCOF,
        cardholderInitiatedCOF
    }
    
    
    public enum sequenceType
    {
        OneTime,
        FirstRecurring,
        SubsequentRecurring,
        FinalRecurring
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
    
    
    public partial class sepaDirectDebitType
    {
        public string mandateProvider;
        private sequenceType sequenceTypeCodeField;
        private bool sequenceTypeCodeSet;
        public sequenceType sequenceType
        {
            get { return sequenceTypeCodeField; }
            set { sequenceTypeCodeField = value; sequenceTypeCodeSet = true; }
        }
        public string mandateReference;
        public string mandateUrl;
        private DateTime mandateSignatureDateField;
        private bool mandateSignatureDateSet;
        public DateTime mandateSignatureDate
        {
            get
            {
                return mandateSignatureDateField;
            }
            set
            {
                mandateSignatureDateField = value;
                mandateSignatureDateSet = true;
            }
        }


        public string iban;
        private countryTypeEnum preferredLanguageCodeField;
        private bool preferredLanguageCodeSet;
        public countryTypeEnum preferredLanguage
        {
            get { return preferredLanguageCodeField; }
            set { preferredLanguageCodeField = value; preferredLanguageCodeSet = true; }
        }
        public string Serialize()
        {
            string xml = "";
            if (mandateProvider != null) xml += "\r\n<mandateProvider>" + SecurityElement.Escape(mandateProvider) + "</mandateProvider>";
            if (sequenceTypeCodeSet) xml += "\r\n<sequenceType>" + sequenceTypeCodeField + "</sequenceType>";
            if (mandateReference != null) xml += "\r\n<mandateReference>" + SecurityElement.Escape(mandateReference) + "</mandateReference>";
            if (mandateUrl != null) xml += "\r\n<mandateUrl>" + SecurityElement.Escape(mandateUrl) + "</mandateUrl>";
            if (mandateSignatureDateSet) xml += "\r\n<mandateSignatureDate>" + XmlUtil.toXsdDate(mandateSignatureDateField) + "</mandateSignatureDate>";
            if (iban != null) xml += "\r\n<iban>" + SecurityElement.Escape(iban) + "</iban>";
            if (preferredLanguageCodeSet) xml += "\r\n<preferredLanguage>" + preferredLanguageCodeField + "</preferredLanguage>";
            return xml;
        }
    }
    
    
    public partial class sepaDirectDebitResponse
    {
        private string redirectUrlField;
        private string redirectTokenField;
        private string mandateReferenceField;

        public string redirectUrl
        {
            get
            {
                return this.redirectUrlField;
            }
            set
            {
                this.redirectUrlField = value;
            }
        }
        public string redirectToken
        {
            get
            {
                return this.redirectTokenField;
            }
            set
            {
                this.redirectTokenField = value;
            }
        }
        public string mandateReference
        {
            get
            {
                return this.mandateReferenceField;
            }
            set
            {
                this.mandateReferenceField = value;
            }
        }
    }
    
    
    public partial class idealType
    {
        private countryTypeEnum preferredLanguageCodeField;
        private bool preferredLanguageCodeSet;
        public countryTypeEnum preferredLanguage
        {
            get { return preferredLanguageCodeField; }
            set { preferredLanguageCodeField = value; preferredLanguageCodeSet = true; }
        }
        public string Serialize()
        {
            string xml = "";
            if (preferredLanguageCodeSet) xml += "\r\n<preferredLanguage>" + preferredLanguageCodeField + "</preferredLanguage>";
            return xml;
        }
    }
    
    
    public partial class idealResponse
    {
        private string redirectUrlField;
        private string redirectTokenField;
        private string paymentPurposeField;

        public string redirectUrl
        {
            get
            {
                return this.redirectUrlField;
            }
            set
            {
                this.redirectUrlField = value;
            }
        }
        public string redirectToken
        {
            get
            {
                return this.redirectTokenField;
            }
            set
            {
                this.redirectTokenField = value;
            }
        }
        public string paymentPurpose
        {
            get
            {
                return this.paymentPurposeField;
            }
            set
            {
                this.paymentPurposeField = value;
            }
        }
    }
    
    
    public class giropayType
    {
        public countryTypeEnum preferredLanguageField;
        public bool preferredLanguageSet;
        public countryTypeEnum preferredLanguage
        {
            get
            {
                return preferredLanguageField;
            }
            set
            {
                preferredLanguageField = value;
                preferredLanguageSet = true;
            }
        }

        public string Serialize()
        {
            var xml = "";
            if (preferredLanguageSet)
            {
                xml += "\r\n<preferredLanguage>" + preferredLanguage + "</preferredLanguage>";
            }
            return xml;
        }
    }
    
    
    public partial class giropayResponse
    {
        private string redirectUrlField;
        private string redirectTokenField;
        private string paymentPurposeField;

        public string redirectUrl
        {
            get
            {
                return this.redirectUrlField;
            }
            set
            {
                this.redirectUrlField = value;
            }
        }

        public string redirectToken
        {
            get
            {
                return this.redirectTokenField;
            }
            set
            {
                this.redirectTokenField = value;
            }
        }

        public string paymentPurpose
        {
            get
            {
                return this.paymentPurposeField;
            }
            set
            {
                this.paymentPurposeField = value;
            }
        }
    }
    
    
    // The sofort element is a child of the sale transaction that, through its child elements,
    // defines information needed to process an SOFORT (Real-time Bank Transfer) transaction.
    // At this time, you can use the iDeal method of payment in Online transactions only.
    public class sofortType
    {
        public countryTypeEnum preferredLanguageField;
        public bool preferredLanguageSet;
        public countryTypeEnum preferredLanguage
        {
            get
            {
                return preferredLanguageField;
            }
            set
            {
                preferredLanguageField = value;
                preferredLanguageSet = true;
            }
        }

        public string Serialize()
        {
            var xml = "";
            if (preferredLanguageSet)
            {
                xml += "\r\n<preferredLanguage>" + preferredLanguage + "</preferredLanguage>";
            }
            return xml;
        }
    }
    
    
    public partial class sofortResponse
    {
        private string redirectUrlField;
        private string redirectTokenField;
        private string paymentPurposeField;

        public string redirectUrl
        {
            get
            {
                return this.redirectUrlField;
            }
            set
            {
                this.redirectUrlField = value;
            }
        }

        public string redirectToken
        {
            get
            {
                return this.redirectTokenField;
            }
            set
            {
                this.redirectTokenField = value;
            }
        }

        public string paymentPurpose
        {
            get
            {
                return this.paymentPurposeField;
            }
            set
            {
                this.paymentPurposeField = value;
            }
        }
    }
    
    #endregion

    #region Not In Use

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.litle.com/schema")]
    public partial class creditPaypal
    {

        private string itemField;

        private ItemChoiceType2 itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("payerEmail", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("payerId", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType2 ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.litle.com/schema", IncludeInSchema = false)]
    public enum ItemChoiceType2
    {

        /// <remarks/>
        payerEmail,

        /// <remarks/>
        payerId,
    }

    #endregion
}