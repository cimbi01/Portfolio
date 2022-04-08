namespace Jobs.Data
{
    /// <summary>
    /// Interface for handling different OfferTypes (Offer, Application)
    /// </summary>
    public interface IAcceptanceHandler
    {
        //TODO: implementation

        /// <summary>
        /// Handles the acceptance (or declining) of JobOffer
        /// </summary>
        /// <param name="jobOffer">The JobOffer that needs to be handled</param>
        void HandleAcceptance(JobOffer jobOffer);
    }
}