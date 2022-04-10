using Jobs.Common;
using Jobs.Common.Factories;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Test
{
    [TestFixture]
    class FactoryTest
    {
        [Test]
        public void CreateEmployeeTest()
        {
            OfferHandler offerHandler = new OfferHandler();
            Factory factory = new Factory(offerHandler);
            string name = "name";
            Employee employee = factory.CreateEmployee(name);
            Assert.AreEqual(name, employee.Contact.Name);
            Assert.IsTrue(offerHandler.Employees.Contains(employee));
        }
        [Test]
        public void CreateEmployerTest()
        {
            OfferHandler offerHandler = new OfferHandler();
            Factory factory = new Factory(offerHandler);
            string name = "name";
            Employer employer = factory.CreateEmployer(name);
            Assert.AreEqual(name, employer.Contact.Name);
            Assert.IsTrue(offerHandler.Employers.Contains(employer));
        }
    }
}
