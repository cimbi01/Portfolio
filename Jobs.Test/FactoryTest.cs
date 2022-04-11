using Jobs.Common;
using Jobs.Common.Factories;
using Jobs.Data;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Test
{
    [TestFixture]
    class FactoryTest : TestBase
    {

        [SetUp]
        public override void Init()
        {
            this.offerHandler = new OfferHandler();
            this.factory = new WorkingPersonFactory(this.offerHandler);
        }

        private WorkingPerson CreateAndAssertNamedWorkingPerson(bool employee)
        {
            string name = "test";
            WorkingPerson workingPerson;
            if (employee)
            {
                workingPerson = this.WorkingPersonFactory.CreateEmployee(name);
            }
            else
            {
                workingPerson = this.WorkingPersonFactory.CreateEmployer(name);
            }
            Assert.AreEqual(name, workingPerson.Contact.Name);
            return workingPerson;
        }

        private Reference CreateAndAssertReference(string? details)
        {
            string name = "ref";
            string url = "https://www.youtube.com/";
            Reference reference = this.factory.CreateReference(name, url, details);
            Assert.AreEqual(name, reference.Name);
            Assert.AreEqual(url, reference.Url.OriginalString);
            Assert.AreEqual(details, reference.Details);
            return reference;
        }

        private Skill CreateAndAssertSkill(string? details)
        {
            string name = "ref";
            int rangeOfKnowledge = 3;
            Skill skill = this.factory.CreateSkill(name, rangeOfKnowledge, details);
            Assert.AreEqual(name, skill.Name);
            Assert.AreEqual(rangeOfKnowledge, skill.RangeOfKnowledge);
            Assert.AreEqual(details, skill.Details);
            return skill;
        }

        private Contact CreateAndAssertContact(string? emailAddress, string? phoneNumber)
        {
            string name = "contact";
            Contact contact = this.factory.CreateContact(name, emailAddress, phoneNumber);
            Assert.AreEqual(name, contact.Name);
            Assert.AreEqual(emailAddress, contact.EmailAddress);
            Assert.AreEqual(phoneNumber, contact.PhoneNumber);
            return contact;
        }

        [Test]
        public void CreateEmployeeTest()
        {
            Employee employee = (Employee)this.CreateAndAssertNamedWorkingPerson(true);
            Assert.IsTrue(this.offerHandler.Employees.Contains(employee));
        }

        [Test]
        public void CreateEmployerTest()
        {
            Employer employer = (Employer)this.CreateAndAssertNamedWorkingPerson(false);
            Assert.IsTrue(this.offerHandler.Employers.Contains(employer));
        }

        [Test]
        public void CreateReferenceDetailsNullTest()
        {
            string? details = null;
            Reference reference = this.CreateAndAssertReference(details);
            Assert.Null(reference.Details);
        }

        [Test]
        public void CreateReferenceDetailsNotNullTest()
        {
            string details = "details";
            Reference reference = this.CreateAndAssertReference(details);
            Assert.NotNull(reference.Details);
        }

        [Test]
        public void CreateSkillDetailsNullTest()
        {
            string? details = null;
            Skill skill = this.CreateAndAssertSkill(details);
            Assert.IsNull(skill.Details);
        }

        [Test]
        public void CreateSkillDetailsNotNullTest()
        {
            string details = "asd";
            Skill skill = this.CreateAndAssertSkill(details);
            Assert.IsNotNull(skill.Details);
        }

        [Test]
        public void CreateContactNoNullTest()
        {
            string? email = "email";
            string? phone = "phone";
            Contact contact = this.CreateAndAssertContact(email, phone);
            Assert.NotNull(contact.EmailAddress);
            Assert.NotNull(contact.PhoneNumber);
        }

        [Test]
        public void CreateContactEmailNullTest()
        {
            string? email = null;
            string? phone = "phone";
            Contact contact = this.CreateAndAssertContact(email, phone);
            Assert.IsNull(contact.EmailAddress);
            Assert.NotNull(contact.PhoneNumber);
        }

        [Test]
        public void CreateContactPhoneNullTest()
        {
            string? email = "email";
            string? phone = null;
            Contact contact = this.CreateAndAssertContact(email, phone);
            Assert.NotNull(contact.EmailAddress);
            Assert.IsNull(contact.PhoneNumber);
        }

        [Test]
        public void CreateContactPhoneNullEmailNullTest()
        {
            string? email = null;
            string? phone = null;
            Contact contact = this.CreateAndAssertContact(email, phone);
            Assert.IsNull(contact.EmailAddress);
            Assert.IsNull(contact.PhoneNumber);
        }

    }
}
