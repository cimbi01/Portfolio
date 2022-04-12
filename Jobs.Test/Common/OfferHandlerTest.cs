using Jobs.Common;
using Jobs.Data;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using Jobs.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jobs.Test.Common
{
    [TestFixture]
    class OfferHandlerTest : TestBase
    {
        private Employee ee;
        private Employer er; 
        private JobData jobData;

        private JobOffer CreateApplication()
        {
            this.ee = this.WorkingPersonFactory.CreateEmployee("");
            this.er = this.WorkingPersonFactory.CreateEmployer("");
            this.jobData = this.WorkingPersonFactory.CreateJobData("", er);
            return this.WorkingPersonFactory.CreateJobOffer(OfferType.Application, jobData, ee, er);
        }
        private JobOffer CreateOffering()
        {
            this.ee = this.WorkingPersonFactory.CreateEmployee("");
            this.er = this.WorkingPersonFactory.CreateEmployer("");
            this.jobData = this.WorkingPersonFactory.CreateJobData("", er);
            return this.WorkingPersonFactory.CreateJobOffer(OfferType.Offering, jobData, er, ee);
        }

        private void AssertJobOffer(JobOffer jobOffer)
        {
            Assert.IsTrue(jobOffer.Accepted);
            Assert.IsTrue(jobOffer.JobData.Employees.Contains(ee));
            Assert.AreEqual(1, jobOffer.JobData.Employees.Count(employee => employee == ee));
        }

        [Test]
        public void AcceptJobOfferApplicationTest()
        {
            JobOffer application = this.CreateApplication();
            this.offerHandler.AcceptJobOffer(application);
            this.AssertJobOffer(application);
        }

        [Test]
        public void AcceptJobOfferOfferingTest()
        {
            JobOffer application = this.CreateOffering();
            this.offerHandler.AcceptJobOffer(application);
            this.AssertJobOffer(application);
        }

        [Test]
        public void AcceptJobOfferTwiceTest()
        {
            JobOffer application = this.CreateOffering();
            this.offerHandler.AcceptJobOffer(application);
            this.AssertJobOffer(application);
            Assert.Throws<AlreadySetException>( () => this.offerHandler.AcceptJobOffer(application));
        }
    }
}
