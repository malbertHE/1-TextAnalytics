namespace MAF.Entropy
{
    /// <summary>Entrópia számítást végző osztály.
    /// </summary>
    public class EntropyCalculator
    {
        /// <summary>A forrás adat, ami feldolgozásra került.</summary>
        public string SourceDataFile { get; private set; }

        /// <summary>Entrópia feldolgozó osztály konstruktora.</summary>
        /// <param name="pSourceDataFile">Forrás adat.</param>
        public EntropyCalculator(string pSourceDataFile)
        {
            SourceDataFile = pSourceDataFile;
        }


    }
}
