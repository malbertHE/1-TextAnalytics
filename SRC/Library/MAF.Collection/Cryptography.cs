using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MAF.Collection
{
    static public class Cryptography
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

        /// <summary>Egy szöveghez SHA512 számítás.
        /// Váratlan eseményeknél <see cref="CryptographyException"/> hibát dob.</summary>
        /// <param name="pInput">Szöveg.</param>
        /// <returns>A szöveghez tartozó SHA512 kód.</returns>
        public static string CalculateSHA512Hash(string pInput)
        {
            SHA512 sha512 = new SHA512Managed();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pInput);
            byte[] hash = sha512.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; ++i)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
