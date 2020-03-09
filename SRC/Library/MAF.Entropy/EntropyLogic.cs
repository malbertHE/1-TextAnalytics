using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MAF.Entropy
{
    /// <summary>A feldolgozás egy logikai leírója.</summary>
    [Serializable]
    public class EntropyLogic
    {
        /// <summary>A feldolgozás neve.</summary>
        [XmlAttribute(AttributeName = "Name")]
        public string Name = string.Empty;

        /// <summary>A feldolgozást leíró reguláris kifejezések listája.</summary>
        [XmlElement("Regex")]
        public List<RegEx> RegexList = new List<RegEx>();
    }
}
