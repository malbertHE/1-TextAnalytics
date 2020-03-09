using System;
using System.Xml.Serialization;

namespace MAF.Entropy
{
    /// <summary>A feldolgozó logika reguláris kifejezéseinek leírója.</summary>
    [Serializable]
    public class RegEx
    {
        /// <summary>A regex pattern része.</summary>
        [XmlAttribute(AttributeName = "Pattern")]
        public string Pattern = string.Empty;

        /// <summary>A regex replace része. Ha meg van adva, akkor replace fut, ha nincs akkor csak matches.</summary>
        [XmlAttribute(AttributeName = "Replace")]
        public string Replace = string.Empty;
    }
}
