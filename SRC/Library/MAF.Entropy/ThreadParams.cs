
using System.Collections.Generic;

namespace MAF.Entropy
{
    /// <summary>Entrópia számítás szálainak paraméterei. Technikai osztály.</summary>
    class ThreadParams
    {
        public List<string> TextList = new List<string>();
        public List<EntropyResult> EntropyResultList = new List<EntropyResult>();
    }
}
