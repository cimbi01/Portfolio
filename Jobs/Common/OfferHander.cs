using Jobs.Data;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common
{
    public class OfferHander
    {
        /*
         * Employer -> JobOffer -> Offer -> accept / decline
         *                              Advertisement -> Application
         * OfferJob(JobOffer, From, To) ->  if From is Employee -> Application
         *                                  else -> Offer
         *                                  To.JobOffers.Add(JobOffer), From.JobOffers.Add(JobOffer)
         * AcceptJobOffer(JobOffer) -> To.JobOffers.Add(JobOffer) -> From.JobOffers.Add(JobOffer) -> JobOffer.Accepted = true
         * Decline(JobOffer) -> JobOffer.Accepted = false
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
        public void OfferJob(JobData jobData, WorkingPerson from, WorkingPerson to)
        {
            throw new NotImplementedException();
        }
    }
}
