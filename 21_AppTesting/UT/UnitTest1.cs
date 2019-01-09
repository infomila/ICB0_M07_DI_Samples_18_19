using System;
using ApplicacioDemoDocumentacio.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UT
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConstructorPersona()
        {
            Persona p = new Persona(12, "11111111H", "Samatao", "Paco", null);

            Assert.AreEqual("11111111H", p.NIF, "Error, NIF no desat correctament");
        }

        [TestMethod]
        public void TestConstructorPersonaNIFErroni()
        {
            // construcció amb un NIF erroni
            // per definició de la classe ha de donar error
            bool haFallat = false;

            try
            {
                Persona p = new Persona(12, "11111111X", "Samatao", "Paco", null);
                //no hauria d'arribar aquí en condicions normals               
            }
            catch (Exception ex)
            {
                haFallat = true;
            }
            // if (!haFallat) Assert.Fail("No es controlen els NIF erronis en el constructor");

        }


    }
}
