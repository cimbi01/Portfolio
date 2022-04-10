using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Data.WorkingPerson
{
    public class WorkingPerson
    {
        public WorkingPerson()
        {

        }
        public WorkingPerson(string name)
        {
            this.Contact.Name = name;
        }
        public Contact Contact { get; } = new Contact();

        public List<JobOffer> JobOffers { get; } = new List<JobOffer>();
    }
}
