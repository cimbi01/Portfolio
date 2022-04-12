using Jobs.Exceptions;
using System;

namespace Jobs.Data
{
    public class JobOffer
    {
        private bool? accepted;

        public JobOffer(OfferType offerType, JobData jobData, WorkingPerson.WorkingPerson offerer, WorkingPerson.WorkingPerson? receiver = null)
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

        /// <summary>
        /// True if accepted, false if declined, null if theres no feedback
        /// </summary>
        public bool? Accepted
        {
            get => accepted;
            set
            {
                if(accepted == null)
                {
                    accepted = value;
                }
                else
                {
                    throw new AlreadySetException();
                }
            }
        }
    }
}