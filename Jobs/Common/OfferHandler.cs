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
         * TODO: Project:Test -> Test Models
         * 
         * TODO: MVC:
         * 
         * TODO: EntityFrameWork
         * TODO: Project:Data -> Data Context, Repositories, Entity FW
         * 
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
         *          Details (string)
         *          ActionToSolve? (Action
         *          
         * TODO: Rate Employees, Employers
         */

        public List<Employee> Employees { get; } = new List<Employee>();
        public List<Employer> Employers { get; } = new List<Employer>();
        public List<JobOffer> JobOffers { get; set; }

        //TODO: implementation
        public void AcceptJobOffer(JobOffer jobOffer)
        {
            throw new NotImplementedException();
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
    }
}
