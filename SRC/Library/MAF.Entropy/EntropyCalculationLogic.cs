using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MAF.Entropy
{
    /// <summary>A feldolgozás teljes logikai leírója. Csak arra használjuk, hogy az xml leírót közvetlenül 
    /// betöltsük objektumba.</summary>
    [Serializable]
    public class EntropyCalculationLogic
    {
        /// <summary>Feldolgozási logikát tartalmazó lista.</summary>
        [XmlElement("EntropyLogicList")]
        public List<EntropyLogic> EntropyLogicList = new List<EntropyLogic>();
    }
}
