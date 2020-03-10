namespace MAF.Entropy
{
    /// <summary>Entrópia számítás eredményét leíró osztály.</summary>
    public class EntropyResult
    {
        /// <summary>Shannon féle entrópia. H = sum(p log2 1/p)</summary>
        public double ShannonEntropy = 0;

        /// <summary>Információtartalom. Az egy jelre eső átlagos információt megszorozzuk a szöveg hosszával: I = N × H</summary>
        public double I;
    }
}
