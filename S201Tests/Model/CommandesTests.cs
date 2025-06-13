using Microsoft.VisualStudio.TestTools.UnitTesting;
using S201;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S201.Tests
{
    [TestClass()]
    public class CommandesTests
    {
        

        [TestMethod()]
        public void EqualsTest()
        {
            Commandes commande1 = new Commandes(1, 1, 17, new DateTime(2024, 1, 15), new DateTime(2024, 1, 18), true, false, 150.50);

            Commandes commande2 = new Commandes(2, 2, 18, new DateTime(2024, 1, 16), new DateTime(2024, 1, 19), false, false, 89.99);

            Commandes commande3 = new Commandes(3, 1, 22, new DateTime(2024, 1, 17), new DateTime(2024, 1, 20), true, true, 245.75);

            commande1.Create();
            commande2.Create();
            commande3.Create();

            commande1.Read();
            commande2.Read();
            commande3.Read();



            Assert.AreEqual(commande1.NumEmploye, 17,"Doit avoir meme numemploye");
            Assert.AreEqual(commande2.DateCommande, new DateTime(2024, 1, 16), "Doit avoir la bonne date");
            Assert.AreEqual(commande3.PrixTotal, 245.75, "Doit avoir meme prix total");



        }

        
    }
}