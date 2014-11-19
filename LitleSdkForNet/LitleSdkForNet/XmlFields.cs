using System;
using System.Collections.Generic;
using System.Text;
namespace Litle.Sdk
{
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
}