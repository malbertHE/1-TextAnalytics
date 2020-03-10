using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAF.Entropy.Test.Collection
{
    [TestClass]
    public class XMLObjectTest
    {
        #region Tesz osztályok.

        class A
        {
            public string sA = string.Empty;
            public int iA = 0;
            public double dA = 0;
            public bool bA = false;
            public List<B> b = new List<B>();
        }

        class B
        {
            public string sB;
        }

        #endregion

        [TestMethod]
        public void TestXMLObject()
        {
            A a1 = new A() { sA = "sA szöveg", iA = 1, dA = 2.3, bA = true };
            a1.b.Add(new B() { sB = "sB 1 szöveg" });
            a1.b.Add(new B() { sB = "sB 2 szöveg" });

            const string c_TestXMLFile = @"RunTimeTest\a.xml";

            XMLObject.ObjectToXML(c_TestXMLFile, a1);
            A a2 = (A)XMLObject.XMLToObject(c_TestXMLFile, typeof(A));

            Assert.IsTrue(a1.sA == a2.sA);
            Assert.IsTrue(a1.iA == a2.iA);
            Assert.IsTrue(a1.dA == a2.dA);
            Assert.IsTrue(a1.bA == a2.bA);
            Assert.IsTrue(a1.b.Count == 2);
            Assert.IsTrue(a1.b.Count == a2.b.Count);
            Assert.IsTrue(a1.b[0].sB == a2.b[0].sB);
            Assert.IsTrue(a1.b[1].sB == a2.b[1].sB);

            const string cSA = "sA másik szöveg";
            a2.sA = cSA;

            XMLObject.ObjectToXML(c_TestXMLFile, a1);
            A a3 = (A)XMLObject.XMLToObject(c_TestXMLFile, typeof(A));
            Assert.IsTrue(a3.sA == cSA);
        }
    }
}
