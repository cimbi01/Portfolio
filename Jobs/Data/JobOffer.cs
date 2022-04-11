using System;

namespace Jobs.Data
{
    public class JobOffer
    {
        public JobOffer(OfferType offerType, WorkingPerson.WorkingPerson offerer, JobData jobData, WorkingPerson.WorkingPerson? receiver = null)
        {
            this.OfferType = offerType;
            this.Offerer = offerer;
            this.Receiver = receiver;
            this.JobData = jobData;
        }

        public OfferType OfferType { get; }

        /// <summary>
        /// The <see cref="WorkingPerson"/> whos offering the job
        /// </summary>
        public WorkingPerson.WorkingPerson Offerer { get; }

        /// <summary>
        /// The <see cref="WorkingPerson"/> whos receiving the job offer. Can be null if its an Advertisement.
        /// </summary>
        public WorkingPerson.WorkingPerson? Receiver { get; }

        public JobData JobData { get; }

        //TODO: Set only once
        /// <summary>
        /// True if accepted, false if declined, null if theres no feedback
        /// </summary>
        public bool? Accepted { get; set; }
    }
}