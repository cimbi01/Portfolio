using Jobs.Common;
using Jobs.Common.Factories;
using Jobs.Data;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using Jobs.Exceptions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Test.Common.Factories
{
    [TestFixture]
    class FactoryTest : TestBase
    {

        [SetUp]
        public override void Init()
        {
            this.offerHandler = new OfferHandler();
            this.factory = new OfferHandleredFactory(this.offerHandler);
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
            Assert.AreEqual(url, reference.Url);
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

        [Test]
        public void CreateJobDataTest()
        {
            string employerName = "empn";
            string jobDataName = "jdn";
            Employer employer = this.WorkingPersonFactory.CreateEmployer(employerName);
            JobData jobData = this.factory.CreateJobData(jobDataName, employer);
            Assert.AreEqual(employer, jobData.Employer);
            Assert.AreEqual(jobDataName, jobData.Name);
        }

        [TestCaseSource(typeof(NoMatchCases))]
        public void CreateJobOfferNoEmployerMatchTest(OfferType offerType, JobData jobData, WorkingPerson offerer, WorkingPerson? receiver = null)
        {
            Assert.Throws<EmployerNotMatchException>(() => this.WorkingPersonFactory.CreateJobOffer(offerType, jobData, offerer, receiver));
        }

        [TestCaseSource(typeof(NotAuthorizedCases))]
        public void CreateJobOfferNotAuthorizedTest(OfferType offerType, JobData jobData, WorkingPerson offerer, WorkingPerson? receiver = null)
        {
            Assert.Throws<NotAuthorizedException>(() => this.WorkingPersonFactory.CreateJobOffer(offerType, jobData, offerer, receiver));
        }

        [TestCaseSource(typeof(SuccesFulCases))]
        public void CreateJobOfferSuccessfullyTest(OfferType offerType, JobData jobData, WorkingPerson offerer, WorkingPerson? receiver = null)
        {
            JobOffer jobOffer = this.WorkingPersonFactory.CreateJobOffer(offerType, jobData, offerer, receiver);
            Assert.True(this.offerHandler.JobOffers.Contains(jobOffer));
            Assert.True(offerer?.OfferedJobOffers.Contains(jobOffer));
            if(receiver != null)
            { 
                Assert.True(receiver.ReceivedJobOffers.Contains(jobOffer));
            }
        }
    }

    class NoMatchCases : IEnumerable
    {
        private OfferHandleredFactory WorkingPersonFactory { get; } = new OfferHandleredFactory(new OfferHandler());
        public IEnumerator GetEnumerator()
        {
            Employee ee = this.WorkingPersonFactory.CreateEmployee("");
            Employer er = this.WorkingPersonFactory.CreateEmployer("");
            JobData jobData = this.WorkingPersonFactory.CreateJobData("", this.WorkingPersonFactory.CreateEmployer(""));
            yield return new object[] { OfferType.Advertisement, jobData, ee, er };
            yield return new object[] { OfferType.Advertisement, jobData, er, ee };
        }
    }

    class NotAuthorizedCases : IEnumerable
    {
        private OfferHandleredFactory WorkingPersonFactory { get; } = new OfferHandleredFactory(new OfferHandler());
        public IEnumerator GetEnumerator()
        {
            Employee ee = this.WorkingPersonFactory.CreateEmployee("");
            Employee ee2 = this.WorkingPersonFactory.CreateEmployee("");
            Employer er = this.WorkingPersonFactory.CreateEmployer("");
            Employer er2 = this.WorkingPersonFactory.CreateEmployer("");
            JobData jobData = this.WorkingPersonFactory.CreateJobData("", er);
            yield return new object[] { OfferType.Application, jobData, ee, ee2 };
            yield return new object[] { OfferType.Application, jobData, ee, ee };

            yield return new object[] { OfferType.Advertisement, jobData, ee, er };
            yield return new object[] { OfferType.Offering, jobData, ee, er };

            yield return new object[] { OfferType.Application, jobData, er, ee };
            yield return new object[] { OfferType.Application, jobData, er2, er };

            yield return new object[] { OfferType.Application, jobData, er, null };
            yield return new object[] { OfferType.Offering, jobData, ee, null };
            yield return new object[] { OfferType.Advertisement, jobData, ee, null };
        }
    }

    class SuccesFulCases : IEnumerable
    {

        private OfferHandleredFactory WorkingPersonFactory { get; } = new OfferHandleredFactory(new OfferHandler());
        public IEnumerator GetEnumerator()
        {
            Employee ee = this.WorkingPersonFactory.CreateEmployee("");
            Employer er = this.WorkingPersonFactory.CreateEmployer("");
            JobData jobData = this.WorkingPersonFactory.CreateJobData("", er);

            yield return new object[] { OfferType.Application, jobData, ee, er };
            yield return new object[] { OfferType.Offering, jobData, er, ee };
            yield return new object[] { OfferType.Advertisement, jobData, er, null };
        }
    }
}
