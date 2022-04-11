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
         * 2 út:
         *      Employer -> JobOffer -> Offer -> accept / decline
         *                              Advertisement -> Application
         * OfferJob(JobOffer, From, To) ->  if From is Employee -> Application
         *                                  else -> Offer
         *                                  To.JobOffers.Add(JobOffer), From.JobOffers.Add(JobOffer)
         * AcceptJobOffer(JobOffer) -> To.JobOffers.Add(JobOffer) -> From.JobOffers.Add(JobOffer) -> JobOffer.Accepted = true
         * Decline(JobOffer) -> JobOffer.Accepted = false
         * 
         * TODO: MVC:
         * 
         * TODO: Notifications:
         *      NeededData -> Notification for Employees/Employers for needed data
         *      Notifications (List Of Notifications):
         *          Target (WorkingPerson)
         *          Title (string)
         *          Details (string)
         *          ActionToSolve? (Action)
         * 
         * TODO: EntityFrameWork
         *      TODO: Project:Data -> Data Context, Repositories, Entity FW
         * 
         * TODO: Identity:
         *      Users
         *      Login
         *      Roles:
         *          Employee
         *          Employer
         */

        public List<Employee> Employees { get; } = new List<Employee>();
        public List<Employer> Employers { get; } = new List<Employer>();
        public List<JobOffer> JobOffers { get; } = new List<JobOffer>();

        /// <summary>
        /// Adds Employee whos accepting to <see cref="JobOffer.JobData.Employees"/>
        /// Set <see cref="JobOffer.Accepted"/> to true
        /// </summary>
        /// <param name="jobOffer"></param>
        public void AcceptJobOffer(JobOffer jobOffer)
        {
            WorkingPerson receiver = jobOffer.Receiver;
            // If employee accepts offering
            if (receiver is Employee)
            {
                jobOffer.JobData.Employees.Add((Employee)receiver);
            }
            // If employer accepts application
            else
            {
                jobOffer.JobData.Employees.Add((Employee)jobOffer.Offerer);
            }
            jobOffer.Accepted = true;
        }

        //TODO: implementation
        public void DeclineJobOffer(JobOffer jobOffer)
        {
            throw new NotImplementedException();
        }

        //TODO: implementation
        public void OfferJob(JobData jobData, Employer from, Employee to)
        {
            throw new NotImplementedException();
        }

        //TODO: implementation
        public void ApplyForJob(JobData jobData, Employee from, Employer to)
        {
            throw new NotImplementedException();
        }

        //TODO: implementation
        public void AdvertiseJob(JobData jobData, Employee from)
        {
            throw new NotImplementedException();
        }
    }
}
