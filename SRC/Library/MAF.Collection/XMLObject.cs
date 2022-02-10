using System;
using System.IO;
using System.Xml.Serialization;

namespace MAF.Collection
{
    /// <summary> XML objektum kezelés.
    /// Az Object statikus osztályban nincs hiba kezelés, mert ezen a szinten nem tudjuk lekezelni, azt csak
    /// a hívó fél tudja, hogy hiba esetén mit kíván tenni.
    /// </summary>
    public static class XMLObject
    {
        /// <summary> XML stringből osztály betöltése.
        /// </summary>
        /// <param name="pXMLData">XML adat.</param>
        /// <param name="pClassType">Osztály típus, amibe betöltjük az adatokat.</param>
        /// <returns>Az új objektum.</returns>
        public static object XMLToObject(Stream pXMLData, Type pClassType)
        {
            XmlSerializer serializer = new XmlSerializer(pClassType);
            return serializer.Deserialize(pXMLData);
        }

        /// <summary> XML fájlból osztály betöltése.
        /// </summary>
        /// <param name="pXMLFile">XML fájl.</param>
        /// <param name="pClassType">Osztály típus, amibe betöltjük az adatokat.</param>
        /// <returns>Az új objektum.</returns>
        public static object XMLToObject(string pXMLFile, Type pClassType)
        {
            XmlSerializer serializer = new XmlSerializer(pClassType);

            using (StreamReader reader = File.OpenText(pXMLFile))
            {
                return serializer.Deserialize(reader);
            }
        }

        /// <summary> Objektum elmentése XML fájlba. Megnézi, hogy létezik-e a könyvtár,
        /// ha nem létezik, létrehozza.</summary>
        /// <param name="pXMLFile">XML fájl.</param>
        /// <param name="pObject">Objektum amit ki kell menteni XML fájlba.</param>
        /// <returns>Az új objektum.</returns>
        public static void ObjectToXML(string pXMLFile, object pObject)
        {
            string dir = Path.GetDirectoryName(pXMLFile);
            if (dir != string.Empty && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            XmlSerializer writer = new XmlSerializer(pObject.GetType());

            StreamWriter file = new StreamWriter(pXMLFile);

            writer.Serialize(file, pObject);
            file.Close();
        }
    }
}
