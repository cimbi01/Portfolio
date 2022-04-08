using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Data.WorkingPerson
{
    public class WorkingPerson
    {
        public Contact Contact { get; }

        public List<JobOffer> JobOffers{ get; }
    }
}
