using System;

namespace Jobs.Data
{
    //TODO: Refactor by OfferTypes
    public class JobOffer
    {
        public OfferType OfferType { get; }

        /// <summary>
        /// <see cref="IAcceptanceHandler"/> that handles the acceptance or declining
        /// </summary>
        public IAcceptanceHandler AcceptanceHandler { get; }

        /// <summary>
        /// The <see cref="WorkingPerson"/> whos offering the job
        /// </summary>
        public WorkingPerson.WorkingPerson Offerer { get; }

        /// <summary>
        /// The <see cref="WorkingPerson"/> whos receiving the job offer. Can be null if its Advertisement
        /// </summary>
        public WorkingPerson.WorkingPerson? Receiver { get; }

        public JobData JobData { get; }

        //TODO: event handler -> change -> this.AcceptanceHandler.HandleAcceptance(this)
        /// <summary>
        /// True if accepted, false if declined, null if theres no feedback
        /// </summary>
        public bool? Accepted { get; set; }

        // TODO: Businness Logic -> separate class
        /// <summary>
        /// Only the receiver can Accept
        /// </summary>
        public void Accept()
        {
            //TODO: implementation
            throw new NotImplementedException();
        }

        // TODO: Businness Logic -> separate class
        /// <summary>
        /// Only the receiver can Decline
        /// </summary>
        public void Decline()
        {
            //TODO: implementation
            throw new NotImplementedException();
        }

    }
}