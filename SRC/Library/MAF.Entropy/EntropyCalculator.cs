using System;

namespace MAF.Entropy
{
    /// <summary>Entrópia számítást végző osztály.
    /// </summary>
    public class EntropyCalculator
    {
        /// <summary>Alapértelmezett logika.</summary>
        public const string C_DefaultLogicFile = "Logic\\ecl.xml";

        /// <summary>A legutoljára feldolgozott forrás adat.</summary>
        public string SourceDataFile { get; private set; } = string.Empty;

        /// <summary>Feldolgozási logikát leíró fájl.</summary>
        public string CalculationLogic { get; private set; } = C_DefaultLogicFile;

        /// <summary>Igaz, ha fut a feldolgozás és hamis, ha nem.</summary>
        public bool IsRunCalculation { get; private set; } = false;

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
