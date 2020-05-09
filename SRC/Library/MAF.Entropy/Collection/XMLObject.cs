using System;
using System.IO;
using System.Xml.Serialization;

namespace MAF.Entropy.Collection
{
    /// <summary> XML objektum kezelés.</summary>
    public static class XMLObject
    {
        /// <summary> XML fájlból osztály betöltése.</summary>
        /// <param name="pXMLFile">XML fájl.</param>
        /// <param name="pClassType">Osztály típus, amibe betöltjük az adatokat.</param>
        /// <returns>Az új objektum.</returns>
        public static object XMLToObject(string pXMLFile, Type pClassType)
        {
            XmlSerializer serializer = new XmlSerializer(pClassType);
            using (StreamReader reader = File.OpenText(pXMLFile))
                return serializer.Deserialize(reader);
        }

        /// <summary> Objektum elmentése XML fájlba.</summary>
        /// <param name="pXMLFile">XML fájl.</param>
        /// <param name="pObject">Objektum amit ki kell menteni XML fájlba.</param>
        /// <returns>Az új objektum.</returns>
        public static void ObjectToXML(string pXMLFile, object pObject)
        {
            CreateDirectoryIfNotExist(pXMLFile);
            SaveObjectToXML(pXMLFile, pObject);
        }

        #region Privát terület!

        private static void CreateDirectoryIfNotExist(string pXMLFile)
        {
            string dir = Path.GetDirectoryName(pXMLFile);
            if (dir != string.Empty && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        private static void SaveObjectToXML(string pXMLFile, object pObject)
        {
            XmlSerializer writer = new XmlSerializer(pObject.GetType());
            using (StreamWriter file = new StreamWriter(pXMLFile))
                writer.Serialize(file, pObject);
        }

        #endregion
    }
}