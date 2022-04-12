using Jobs.Common.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Data.WorkingPerson
{
    public class WorkingPerson
    {
        public WorkingPerson()
        {
            this.Contact = new Factory().CreateContact();
        }
        public WorkingPerson(WorkingPerson workingPerson)
        {
            this.Contact = workingPerson.Contact;
            this.ReceivedJobOffers = workingPerson.ReceivedJobOffers;
            this.OfferedJobOffers = workingPerson.OfferedJobOffers;
        }
        public WorkingPerson(string name)
        {
            this.Contact = new Factory().CreateContact(name);
        }
        
        
        public Contact Contact { get; set; }

        public List<JobOffer> ReceivedJobOffers { get; } = new List<JobOffer>();
        public List<JobOffer> OfferedJobOffers { get; } = new List<JobOffer>();
    }
}
