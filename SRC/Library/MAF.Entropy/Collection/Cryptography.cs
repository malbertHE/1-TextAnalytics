using System;
using System.IO;
using System.Security.Cryptography;

namespace MAF.Entropy.Collection
{
    /// <summary>Kriptográfiai szolgáltatások.</summary>
    public static class Cryptography
    {
        /// <summary>Fájl md5 kiszámításához függvény.</summary>
        /// <param name="pFile">Fájl, aminek az md5-jét ki akarjuk számolni.</param>
        /// <returns>A fájl md5-je.</returns>
        public static string FileMD5Calculator(string pFile)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(pFile))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
