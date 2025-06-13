using S201;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S201.Tests
{
    [TestClass()]
    public class ClientTests
    {


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]

        public void TestCreateCommandeDejaExistant()
        {
            Client client1 = new Client(1, "Duponte", "Jorge", "0123456789","120 place gabriel", "75002", "Annecy");
            client1.Create();
            Client client2 = new Client(1, "Dupont", "Jean", "0123456789", "123 rue de la Paix", "75001", "Paris");
            client1.Create();


        }


    }
}