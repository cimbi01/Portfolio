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
        private static OfferHandler offerHandler;
        private static Factory factory;
        [SetUp]
        public static void Init()
        {
            FactoryTest.offerHandler = new OfferHandler();
            FactoryTest.factory = new Factory(offerHandler);
        }

        [Test]
        public void CreateEmployeeTest()
        {
            string name = "name";
            Employee employee = FactoryTest.factory.CreateEmployee(name);
            Assert.AreEqual(name, employee.Contact.Name);
            Assert.IsTrue(FactoryTest.offerHandler.Employees.Contains(employee));
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

        [Test]
        public void CreateReferenceDetailsNullTest()
        {
            string name = "ref";
            string? details = null;
            string url = "https://www.youtube.com/";
            Reference reference = FactoryTest.factory.CreateReference(name, url, details);
            Assert.AreEqual(name, reference.Name);
            Assert.AreEqual(url, reference.Url.OriginalString);
            Assert.AreEqual(details, reference.Details);
        }

        //TODO: Refactor function
        [Test]
        public void CreateReferenceDetailsNotNullTest()
        {
            string name = "ref";
            string details = "details";
            string url = "https://www.youtube.com/";
            Reference reference = FactoryTest.factory.CreateReference(name, url, details);
            Assert.AreEqual(name, reference.Name);
            Assert.AreEqual(url, reference.Url.OriginalString);
            Assert.AreEqual(details, reference.Details);
        }
    }
}
