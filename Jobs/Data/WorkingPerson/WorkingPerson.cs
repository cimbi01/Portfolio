using Jobs.Common.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Data.WorkingPerson
{
    public abstract class WorkingPerson
    {
        public WorkingPerson(string name)
        {
            this.Contact = new Factory().CreateContact(name);
        }

        public Contact Contact { get; }

        public List<JobOffer> ReceivedJobOffers { get; } = new List<JobOffer>();
        public List<JobOffer> OfferedJobOffers { get; } = new List<JobOffer>();
    }
}
