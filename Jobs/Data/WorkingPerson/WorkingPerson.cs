using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Data.WorkingPerson
{
    public abstract class WorkingPerson
    {
        public WorkingPerson()
        {

        }
        public WorkingPerson(string name)
        {
            this.Contact.Name = name;
        }

        //TODO: use Factory.CreateContact()
        public Contact Contact { get; } = new Contact();

        public List<JobOffer> JobOffers { get; } = new List<JobOffer>();
    }
}
