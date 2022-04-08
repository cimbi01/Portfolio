using System;

namespace Jobs.Data
{
    //TODO: Refactor by OfferTypes
    public class JobOffer
    {
        public OfferType OfferType { get; }

        /// <summary>
        /// The <see cref="WorkingPerson"/> whos offering the job
        /// </summary>
        public WorkingPerson.WorkingPerson Offerer { get; }

        /// <summary>
        /// The <see cref="WorkingPerson"/> whos receiving the job offer. Can be null if its Advertisement
        /// </summary>
        public WorkingPerson.WorkingPerson? Receiver { get; }

        public JobData JobData { get; }

        /// <summary>
        /// True if accepted, false if declined, null if theres no feedback
        /// </summary>
        public bool? Accepted { get; set; }
    }
}