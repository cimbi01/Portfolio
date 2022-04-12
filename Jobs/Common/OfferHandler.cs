using Jobs.Common.Factories;
using Jobs.Data;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common
{
    public class OfferHandler
    {
        /*
         * TODO: Identity:
         *      Users
         *      Login
         *      Roles:
         *          Employee
         *          Employer
         * 
         * TODO: Notifications:
         *      NeededData -> Notification for Employees/Employers for needed data
         *      Notifications (List Of Notifications):
         *          Target (WorkingPerson)
         *          Title (string)
         *          Details (string?)
         *          ActionToSolve (Action?)
         * 
         * TODO: EF Core
         *      TODO: Project:Data -> Data Context, Repositories, Entity FW
         */

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
            // If employee accepts offering
            if (receiver is Employee && !jobOffer.JobData.Employees.Contains((Employee)receiver))
            {
                jobOffer.JobData.Employees.RemoveAll(employee => employee == (Employee)receiver);
            }
            // If employer accepts application
            else if (receiver is Employer && !jobOffer.JobData.Employees.Contains((Employee)jobOffer.Offerer))
            {
                jobOffer.JobData.Employees.RemoveAll(employee => employee == (Employee)jobOffer.Offerer);
            }
            jobOffer.Accepted = false;
        }

        public JobOffer OfferJob(JobData jobData, Employer from, Employee to)
        {
            JobOffer jobOffer =  this.Factory.CreateJobOffer(OfferType.Offering, jobData, from, to);
            return jobOffer;
        }

        public JobOffer ApplyForJob(JobData jobData, Employee from, Employer to)
        {
            JobOffer jobOffer = this.Factory.CreateJobOffer(OfferType.Application, jobData, from, to);
            return jobOffer;
        }

        public JobOffer AdvertiseJob(JobData jobData, Employer from)
        {
            JobOffer jobOffer = this.Factory.CreateJobOffer(OfferType.Advertisement, jobData, from);
            return jobOffer;
        }
    }
}
