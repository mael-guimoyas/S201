using Microsoft.VisualStudio.TestTools.UnitTesting;
using S201.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S201.Model.Tests
{
    [TestClass()]
    public class EmployeTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]

        public void TestCreateEmployeDejaExistant()
        {
            Employe employe1 = new Employe(14, "Durano", "Luka", "jurureu", "duran", new Role(2, "Responsable"));
            employe1.Create();
            Employe employe2 = new Employe(15, "Duri", "Louis", "jururoi", "frer", new Role(1, "Vendeur"));
            employe2.Create();
        }
    }
}