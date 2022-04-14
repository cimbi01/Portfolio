using Jobs.Common.Factories;
using Jobs.Data;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jobs.Common
{
    public class OfferHandler
    {
        /*
         * TODO: Equality check for Datas -> private int Id (autoincremented) -> Dictionary<Type, int>
         */

        //TODO Equality check
        public List<JobOffer> GetJobOffers(WorkingPerson workingPerson)
        {
            return this.JobOffers.Where(offer =>
                workingPerson.Contact.Name == offer.Receiver?.Contact.Name
                || workingPerson.Contact.Name == offer.Offerer.Contact.Name).ToList();
        }

        public List<JobOffer> Advertisements
        {
            get => this.JobOffers.Where(offer => offer.OfferType == OfferType.Advertisement).ToList();
        }

        //TODO: readonlys
        public List<WorkingPerson> WorkingPeople
        {
            get
            {
                List<WorkingPerson> workingPeople = new List<WorkingPerson>();
                this.Employees.ForEach(emp => workingPeople.Add(emp));
                this.Employers.ForEach(emp => workingPeople.Add(emp));
                return workingPeople;
            }
        }
        public List<Employee> Employees { get; } = new List<Employee>();
        public List<Employer> Employers { get; } = new List<Employer>();
        public List<JobOffer> JobOffers { get; } = new List<JobOffer>();
        protected OfferHandleredFactory Factory { get; }

        public OfferHandler()
        {
            this.Factory = new OfferHandleredFactory(this);
        }

        //TODO: Refactor Code Clone 
        /// <summary>
        /// Adds Employee whos accepting - if not already exist - to <see cref="JobOffer.JobData.Employees"/> 
        /// Set <see cref="JobOffer.Accepted"/> to true
        /// </summary>
        /// <param name="jobOffer"></param>
        public void AcceptJobOffer(JobOffer jobOffer)
        {
            WorkingPerson receiver = jobOffer.Receiver;
            // If employee accepts offering and not exits in employees
            if (receiver is Employee && !jobOffer.JobData.Employees.Contains((Employee)receiver))
            {
                // add only once
                jobOffer.JobData.Employees.Add((Employee)receiver);
            }
            // If employer accepts application and not exits in employees
            else if (receiver is Employer && !jobOffer.JobData.Employees.Contains((Employee)jobOffer.Offerer))
            {   
                jobOffer.JobData.Employees.Add((Employee)jobOffer.Offerer);
            }
            jobOffer.Accepted = true;
        }

        //TODO: Refactor Code Clone
        /// <summary>
        /// Removes Employee whos declining - if  exist - from <see cref="JobOffer.JobData.Employees"/> 
        /// Set <see cref="JobOffer.Accepted"/> to false
        /// </summary>
        /// <param name="jobOffer"></param>
        public void DeclineJobOffer(JobOffer jobOffer)
        {
            WorkingPerson receiver = jobOffer.Receiver;
            // If employee declines offering
            if (receiver is Employee && !jobOffer.JobData.Employees.Contains((Employee)receiver))
            {
                jobOffer.JobData.Employees.RemoveAll(employee => employee == (Employee)receiver);
            }
            // If employer declines application
            else if (receiver is Employer && !jobOffer.JobData.Employees.Contains((Employee)jobOffer.Offerer))
            {
                jobOffer.JobData.Employees.RemoveAll(employee => employee == (Employee)jobOffer.Offerer);
            }
            jobOffer.Accepted = false;
        }

        //TODO: throws exception for offering a job thats employee contains joboffer for this jobdata
        public JobOffer OfferJob(JobData jobData, Employer from, Employee to)
        {
            JobOffer jobOffer =  this.Factory.CreateJobOffer(OfferType.Offering, jobData, from, to);
            return jobOffer;
        }

        //TODO: throws exception for applying a job thats jobdata.employees contains employee or 
        public JobOffer ApplyForJob(JobData jobData, Employee from)
        {
            JobOffer jobOffer = this.Factory.CreateJobOffer(OfferType.Application, jobData, from, jobData.Employer);
            return jobOffer;
        }

        //TODO: throws exception for advertise a job thats employer already contains joboffer for this jobdata
        public JobOffer AdvertiseJob(JobData jobData, Employer from)
        {
            JobOffer jobOffer = this.Factory.CreateJobOffer(OfferType.Advertisement, jobData, from);
            return jobOffer;
        }
    }
}
