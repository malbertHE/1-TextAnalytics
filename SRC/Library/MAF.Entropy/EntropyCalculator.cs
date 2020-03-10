using System;

namespace MAF.Entropy
{
    /// <summary>Entrópia számítást végző osztály.
    /// </summary>
    public class EntropyCalculator
    {
        /// <summary>A legutoljára feldolgozott forrás adat.</summary>
        public string SourceDataFile { get; private set; } = string.Empty;

        /// <summary>Eredményfájl. Ide kerülnek be az entrópia számítás eredményei.</summary>
        public string ResultFile { get; private set; } = string.Empty;

        /// <summary>Entrópia feldolgozó osztály konstruktora, ami betölti a feldolgozás logikáját.</summary>
        public EntropyCalculator()
        {
            LoadLogic();
        }



        private void LoadLogic()
        {
            
        }
    }
}
