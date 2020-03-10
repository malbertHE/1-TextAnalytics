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

        /// <summary>Ennyi szálon fogunk próbálkozni a feldolgozással, ha a szöveg mérete ezt igényli.</summary>
        public int ThreadCount { get; private set; } = 1;

        /// <summary>Ennyi szál indult el végül.</summary>
        public int RunningThreadCount { get; private set; } = 0;

        /// <summary>Igaz, ha fut a feldolgozás és hamis, ha nem.</summary>
        public bool IsRunCalculation { get; private set; } = false;

        /// <summary>Eredményfájl. Ide kerülnek be az entrópia számítás eredményei.</summary>
        public string ResultFile { get; private set; } = string.Empty;

        /// <summary>Entrópia feldolgozó osztály konstruktora, ami betölti a feldolgozás logikáját.</summary>
        public EntropyCalculator()
        {
            LoadLogic();
        }


        /// <summary>Entrópia számítást elindító függvény. 
        /// Figyelem! A függvény saját szálat vagy szálakat indít és a futást visszaadja a hívónak!
        /// A feldolgozás végét és a szálak leállását a <see cref="IsRunCalculation"/> jelzi.
        /// Amíg ez igaz, addig folyamatban van a feldolgozás.
        /// A szálak bevárásához a <see cref="WaitForAll"/> metódus szolgál.</summary>
        public void RunCalculation(string pSourceDataFile)
        {
            SourceDataFile = pSourceDataFile;
        }


        private void LoadLogic()
        {
            
        }
    }
}
